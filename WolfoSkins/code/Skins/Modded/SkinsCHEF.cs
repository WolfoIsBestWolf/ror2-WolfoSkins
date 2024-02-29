using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsCHEF
    {
        public static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_CHEF_BLACK", "Rotisseur");
            LanguageAPI.Add("SIMU_SKIN_CHEF_RED", "Boucher");
            LanguageAPI.Add("SIMU_SKIN_CHEF_GREEN", "Entremetier");
            LanguageAPI.Add("SIMU_SKIN_CHEF_BLUE", "Poissonier");
            LanguageAPI.Add("SIMU_SKIN_CHEF_CYAN", "Saucier");
            LanguageAPI.Add("SIMU_SKIN_CHEF_PROVI", "Sous Chef");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_NAME", "CHEF: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_DESCRIPTION", "As CHEF" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CHEF_NAME";
            unlockableDef.cachedName = "Skins.Chef.Wolfo";
            unlockableDef.hidden = true;
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinChefIconRed);
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            GameModeCatalog.availability.CallWhenAvailable(ChefDisplayFix);
        }

        public static void ChefDisplayFix()
        {
            SurvivorDef CHEF = SurvivorCatalog.FindSurvivorDef("CHEF");
            if (!CHEF)
            {
                Debug.LogWarning("No chef");
                return;
            }
            Debug.Log("CHEF fix");
            CHEF.displayPrefab.AddComponent<FixChefDisplay>();
        }

        public static void KnifeProjectiles()
        {
            GameObject ChefBody = BodyCatalog.FindBodyPrefab("CHEF");
            ModelSkinController modelSkinController = ChefBody.GetComponentInChildren<ModelSkinController>();

            int catalogIndexCleaver = ProjectileCatalog.FindProjectileIndex("CHEFCleaver");
            int catalogIndexKnife = ProjectileCatalog.FindProjectileIndex("CHEFKnife");
            int catalogIndexKnife2 = ProjectileCatalog.FindProjectileIndex("ChefKnifeBoosted");
            Debug.Log(catalogIndexCleaver);
            Debug.Log(catalogIndexKnife);

            GameObject CleaverShot = ProjectileCatalog.GetProjectilePrefab(catalogIndexCleaver);
            GameObject KnifeShot = ProjectileCatalog.GetProjectilePrefab(catalogIndexKnife);
            GameObject KnifeShotBoost = ProjectileCatalog.GetProjectilePrefab(catalogIndexKnife2);

            Material ArmDefault = KnifeShot.GetComponent<LineRenderer>().material;
            Material ArmRed = Object.Instantiate(ArmDefault);
            Material ArmGreen = Object.Instantiate(ArmDefault);
            Material ArmBlue = Object.Instantiate(ArmDefault);
            Material ArmBlack = Object.Instantiate(ArmDefault);
            Material ArmCyan = Object.Instantiate(ArmDefault);
            Material ArmProvi = Object.Instantiate(ArmDefault);

            ArmRed.mainTexture = WRect.MakeTexture(128, 64, TextureFormat.DXT1, FilterMode.Bilinear, TextureWrapMode.Repeat, Properties.Resources.texArmRED);
            ArmGreen.mainTexture = WRect.MakeTexture(128, 64, TextureFormat.DXT1, FilterMode.Bilinear, TextureWrapMode.Repeat, Properties.Resources.texArmGREEN);
            ArmBlue.mainTexture = WRect.MakeTexture(128, 64, TextureFormat.DXT1, FilterMode.Bilinear, TextureWrapMode.Repeat, Properties.Resources.texArmBLUE);
            ArmBlack.mainTexture = WRect.MakeTexture(128, 64, TextureFormat.DXT1, FilterMode.Bilinear, TextureWrapMode.Repeat, Properties.Resources.texArmBLACK);
            ArmCyan.mainTexture = WRect.MakeTexture(128, 64, TextureFormat.DXT1, FilterMode.Bilinear, TextureWrapMode.Repeat, Properties.Resources.texArmCYAN);
            ArmProvi.mainTexture = WRect.MakeTexture(128, 64, TextureFormat.DXT1, FilterMode.Bilinear, TextureWrapMode.Repeat, Properties.Resources.texArmPROVI);

            //Does not support other mods properly at all
            Material[] arms = new Material[]
            {
                ArmDefault,
                ArmRed,
                ArmGreen,
                ArmBlue,
                ArmBlack,
                ArmCyan,
                ArmProvi
            };

            KnifeShot.AddComponent<FixChefArmTrail>().arms = arms;
            KnifeShotBoost.AddComponent<FixChefArmTrail>().arms = arms;

            for (int i = 1; i < modelSkinController.skins.Length; i++)
            {
                GameObject CleaverGhost = PrefabAPI.InstantiateClone(CleaverShot.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "CleaverGhost" + i, false);
                GameObject KnifeGhost = PrefabAPI.InstantiateClone(KnifeShot.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "KnifeGhost" + i, false);

                CleaverGhost.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials = new Material[]
                {
                    modelSkinController.skins[i].rendererInfos[1].defaultMaterial,
                    modelSkinController.skins[i].rendererInfos[1].defaultMaterial,
                };

                KnifeGhost.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = modelSkinController.skins[i].rendererInfos[0].defaultMaterial;
                //KnifeGhost.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = modelSkinController.skins[i].rendererInfos[2].defaultMaterial;

                modelSkinController.skins[i].projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
                {
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectileGhostReplacementPrefab = CleaverGhost,
                        projectilePrefab = CleaverShot,
                    },
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectileGhostReplacementPrefab = KnifeGhost,
                        projectilePrefab = KnifeShot,
                    },
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectileGhostReplacementPrefab = KnifeGhost,
                        projectilePrefab = KnifeShotBoost,
                    }
                };
            }

        }

        internal static void ModdedSkin(GameObject ChefBody)
        {
            On.RoR2.ProjectileCatalog.Init += ProjectileCatalog_Init;

            Debug.Log("CHEF Skins");
            unlockableDef.hidden = false;
            BodyIndex ChefIndex = ChefBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ChefBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinChef = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfosBLACK = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosBLACK, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRED = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosRED, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosGREEN = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosGREEN, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosBLUE = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosBLUE, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosCYAN = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosCYAN, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosPROVI = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosPROVI, skinChef.rendererInfos.Length);

            Material matChefBLACK = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefRED = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefGREEN = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefBLUE = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefCYAN = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefPROVI = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);

            Texture2D texChefDefault = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefDefault.LoadImage(Properties.Resources.texChefDefaultBLACK, true);
            texChefDefault.filterMode = FilterMode.Bilinear;
            texChefDefault.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefRed = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefRed.LoadImage(Properties.Resources.texChefRed, true);
            texChefRed.filterMode = FilterMode.Bilinear;
            texChefRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefGREEN = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefGREEN.LoadImage(Properties.Resources.texChefGreen, true);
            texChefGREEN.filterMode = FilterMode.Bilinear;
            texChefGREEN.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefBLUE = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefBLUE.LoadImage(Properties.Resources.texChefDefaultBLUE, true);
            texChefBLUE.filterMode = FilterMode.Bilinear;
            texChefBLUE.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefCYAN = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefCYAN.LoadImage(Properties.Resources.texChefDefaultCYAN, true);
            texChefCYAN.filterMode = FilterMode.Bilinear;
            texChefCYAN.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefPROVI = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefPROVI.LoadImage(Properties.Resources.texChefDefaultPROVI, true);
            texChefPROVI.filterMode = FilterMode.Bilinear;
            texChefPROVI.wrapMode = TextureWrapMode.Clamp;

            matChefBLACK.mainTexture = texChefDefault;
            matChefRED.mainTexture = texChefRed;
            matChefGREEN.mainTexture = texChefGREEN;
            matChefBLUE.mainTexture = texChefBLUE;
            matChefCYAN.mainTexture = texChefCYAN;
            matChefPROVI.mainTexture = texChefPROVI;
            matChefPROVI.SetColor("_EmColor", new Color(0.1f, 0.1f, 0.1f, 0.1f));
            matChefPROVI.SetTexture("_EmTex", texChefPROVI);

            NewRenderInfosBLACK[0].defaultMaterial = matChefBLACK;
            NewRenderInfosRED[0].defaultMaterial = matChefRED;
            NewRenderInfosGREEN[0].defaultMaterial = matChefGREEN;
            NewRenderInfosBLUE[0].defaultMaterial = matChefBLUE;
            NewRenderInfosCYAN[0].defaultMaterial = matChefCYAN;
            NewRenderInfosPROVI[0].defaultMaterial = matChefPROVI;
            //
            Material matChefKnifeBLACK = Object.Instantiate(skinChef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeRED = Object.Instantiate(skinChef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeGREEN = Object.Instantiate(skinChef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeBLUE = Object.Instantiate(skinChef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeCYAN = Object.Instantiate(skinChef.rendererInfos[1].defaultMaterial);
            Material matChefKnifePROVI = Object.Instantiate(skinChef.rendererInfos[1].defaultMaterial);

            Texture2D texChefKnifeBlack = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texChefKnifeBlack.LoadImage(Properties.Resources.texChefKnifeBlack, true);
            texChefKnifeBlack.filterMode = FilterMode.Bilinear;
            texChefKnifeBlack.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeRed = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texChefKnifeRed.LoadImage(Properties.Resources.texChefKnifeRED, true);
            texChefKnifeRed.filterMode = FilterMode.Bilinear;
            texChefKnifeRed.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeGreen = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texChefKnifeGreen.LoadImage(Properties.Resources.texChefKnifeGreen, true);
            texChefKnifeGreen.filterMode = FilterMode.Bilinear;
            texChefKnifeGreen.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeBlue = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texChefKnifeBlue.LoadImage(Properties.Resources.texChefKnifeBlue, true);
            texChefKnifeBlue.filterMode = FilterMode.Bilinear;
            texChefKnifeBlue.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeCyan = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texChefKnifeCyan.LoadImage(Properties.Resources.texChefKnifeCyan, true);
            texChefKnifeCyan.filterMode = FilterMode.Bilinear;
            texChefKnifeCyan.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeProvi = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texChefKnifeProvi.LoadImage(Properties.Resources.texChefKnifeProvi, true);
            texChefKnifeProvi.filterMode = FilterMode.Bilinear;
            texChefKnifeProvi.wrapMode = TextureWrapMode.Repeat;

            matChefKnifeBLACK.mainTexture = texChefKnifeBlack;
            matChefKnifeRED.mainTexture = texChefKnifeRed;
            matChefKnifeGREEN.mainTexture = texChefKnifeGreen;
            matChefKnifeBLUE.mainTexture = texChefKnifeBlue;
            matChefKnifeCYAN.mainTexture = texChefKnifeCyan;
            matChefKnifePROVI.mainTexture = texChefKnifeProvi;

            NewRenderInfosBLACK[1].defaultMaterial = matChefKnifeBLACK;
            NewRenderInfosRED[1].defaultMaterial = matChefKnifeRED;
            NewRenderInfosGREEN[1].defaultMaterial = matChefKnifeGREEN;
            NewRenderInfosBLUE[1].defaultMaterial = matChefKnifeBLUE;
            NewRenderInfosCYAN[1].defaultMaterial = matChefKnifeCYAN;
            NewRenderInfosPROVI[1].defaultMaterial = matChefKnifePROVI;

            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinChefIconBLACK, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            Texture2D skinChefIconRed = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconRed.LoadImage(Properties.Resources.skinChefIconRed, true);
            skinChefIconRed.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconRedS = Sprite.Create(skinChefIconRed, WRect.rec128, WRect.half);
            //
            Texture2D skinChefIconGREEN = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconGREEN.LoadImage(Properties.Resources.skinChefIconGreen, true);
            skinChefIconGREEN.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconGREENS = Sprite.Create(skinChefIconGREEN, WRect.rec128, WRect.half);

            Texture2D skinChefIconBLUE = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconBLUE.LoadImage(Properties.Resources.skinChefIconBLUE, true);
            skinChefIconBLUE.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconBLUES = Sprite.Create(skinChefIconBLUE, WRect.rec128, WRect.half);

            Texture2D skinChefIconCYAN = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconCYAN.LoadImage(Properties.Resources.skinChefIconCYAN, true);
            skinChefIconCYAN.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconCYANS = Sprite.Create(skinChefIconCYAN, WRect.rec128, WRect.half);
            
            Texture2D skinChefIconPROVI = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconPROVI.LoadImage(Properties.Resources.skinChefIconProvi, true);
            skinChefIconPROVI.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconPROVIS = Sprite.Create(skinChefIconPROVI, WRect.rec128, WRect.half);
            //
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_Black",
                NameToken = "SIMU_SKIN_CHEF_BLACK",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosBLACK,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoRED = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_Red",
                NameToken = "SIMU_SKIN_CHEF_RED",
                Icon = skinChefIconRedS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosRED,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoGREEN = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_Green",
                NameToken = "SIMU_SKIN_CHEF_GREEN",
                Icon = skinChefIconGREENS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosGREEN,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoBLUE = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_BLUE",
                NameToken = "SIMU_SKIN_CHEF_BLUE",
                Icon = skinChefIconBLUES,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosBLUE,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoCYAN = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_CYAN",
                NameToken = "SIMU_SKIN_CHEF_CYAN",
                Icon = skinChefIconCYANS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosCYAN,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoPROVI = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_PROVI",
                NameToken = "SIMU_SKIN_CHEF_PROVI",
                Icon = skinChefIconPROVIS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosPROVI,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            SkinDef ChefSkinDefBLACK = Skins.CreateNewSkinDef(SkinInfo);
            SkinDef ChefSkinDefRED = Skins.CreateNewSkinDef(SkinInfoRED);
            SkinDef ChefSkinDefGREEN = Skins.CreateNewSkinDef(SkinInfoGREEN);
            SkinDef ChefSkinDefBLUE = Skins.CreateNewSkinDef(SkinInfoBLUE);
            SkinDef ChefSkinDefCYAN = Skins.CreateNewSkinDef(SkinInfoCYAN);
            SkinDef ChefSkinDefPROVI = Skins.CreateNewSkinDef(SkinInfoPROVI);

            modelSkinController.skins = modelSkinController.skins.Add(ChefSkinDefRED, ChefSkinDefGREEN, ChefSkinDefBLUE, ChefSkinDefBLACK, ChefSkinDefCYAN, ChefSkinDefPROVI);
            BodyCatalog.skins[(int)ChefIndex] = BodyCatalog.skins[(int)ChefIndex].Add(ChefSkinDefRED, ChefSkinDefGREEN, ChefSkinDefBLUE, ChefSkinDefBLACK, ChefSkinDefCYAN, ChefSkinDefPROVI);
        }

        private static void ProjectileCatalog_Init(On.RoR2.ProjectileCatalog.orig_Init orig)
        {
            orig();
            KnifeProjectiles();
        }

        public class FixChefDisplay : MonoBehaviour
        {
            public void Fix(SkinDef skinDef)
            {
                this.transform.GetChild(5).gameObject.SetActive(false);
                this.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material = skinDef.rendererInfos[1].defaultMaterial;
                this.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().materials = new Material[]
                {
                    skinDef.rendererInfos[1].defaultMaterial,
                    skinDef.rendererInfos[1].defaultMaterial
                };
            }
        }
        public class FixChefArmTrail : MonoBehaviour
        {
            public Material[] arms;

            public void Start()
            {
                GameObject Owner = this.GetComponent<RoR2.Projectile.ProjectileController>().owner;
                if (Owner)
                {
                    this.GetComponent<LineRenderer>().material = arms[Owner.GetComponent<CharacterBody>().skinIndex];
                }
            }
        }

        [RegisterAchievement("SIMU_SKIN_CHEF", "Skins.Chef.Wolfo", null, null)]
        public class ClearSimulacrumCHEF : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CHEF");
            }
        }
    }
}