using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsCHEFMod
    {
        public static void ChefDisplayFix()
        {
            SurvivorDef CHEF = SurvivorCatalog.FindSurvivorDef("GnomeChef");
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
            GameObject ChefBody = BodyCatalog.FindBodyPrefab("GnomeChefBody");
            ModelSkinController modelSkinController = ChefBody.GetComponentInChildren<ModelSkinController>();

            int catalogIndexCleaver = ProjectileCatalog.FindProjectileIndex("CHEFCleaver");
            int catalogIndexKnife = ProjectileCatalog.FindProjectileIndex("CHEFKnife");
            int catalogIndexKnife2 = ProjectileCatalog.FindProjectileIndex("ChefKnifeBoosted");
            Debug.Log(catalogIndexCleaver);
            Debug.Log(catalogIndexKnife);

            GameObject CleaverShot = ProjectileCatalog.GetProjectilePrefab(catalogIndexCleaver);
            GameObject KnifeShot = ProjectileCatalog.GetProjectilePrefab(catalogIndexKnife);
            GameObject KnifeShotBoost = ProjectileCatalog.GetProjectilePrefab(catalogIndexKnife2);

            if (!CleaverShot || !KnifeShot || !KnifeShotBoost)
            {
                Debug.LogWarning("CHEF MOD Knives couldn't be made");
                return;
            }


            Material ArmDefault = KnifeShot.GetComponent<LineRenderer>().material;
            Material ArmRed = Object.Instantiate(ArmDefault);
            Material ArmGreen = Object.Instantiate(ArmDefault);
            Material ArmBlue = Object.Instantiate(ArmDefault);
            Material ArmBlack = Object.Instantiate(ArmDefault);
            Material ArmCyan = Object.Instantiate(ArmDefault);
            Material ArmProvi = Object.Instantiate(ArmDefault);

            ArmRed.mainTexture = WRect.MakeTexture(TextureWrapMode.Repeat, Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texArmRED.png"));
            ArmGreen.mainTexture = WRect.MakeTexture(TextureWrapMode.Repeat, Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texArmGREEN.png"));
            ArmBlue.mainTexture = WRect.MakeTexture(TextureWrapMode.Repeat, Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texArmBLUE.png"));
            ArmBlack.mainTexture = WRect.MakeTexture(TextureWrapMode.Repeat, Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texArmBLACK.png"));
            ArmCyan.mainTexture = WRect.MakeTexture(TextureWrapMode.Repeat, Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texArmCYAN.png"));
            ArmProvi.mainTexture = WRect.MakeTexture(TextureWrapMode.Repeat, Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texArmPROVI.png"));

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

            Texture2D texChefDefault_Black = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefDefaultBLACK.png");
            texChefDefault_Black.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefRed.png");
            texChefRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefGREEN = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefGreen.png");
            texChefGREEN.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefBLUE = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefDefaultBLUE.png");
            texChefBLUE.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefCYAN = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefDefaultCYAN.png");
            texChefCYAN.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefPROVI = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefDefaultPROVI.png");
            texChefPROVI.wrapMode = TextureWrapMode.Clamp;

            matChefBLACK.mainTexture = texChefDefault_Black;
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

            Texture2D texChefKnifeBlack = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefKnifeBlack.png");
            texChefKnifeBlack.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefKnifeRED.png");
            texChefKnifeRed.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeGreen = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefKnifeGreen.png");
            texChefKnifeGreen.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeBlue = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefKnifeBlue.png");
            texChefKnifeBlue.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeCyan = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefKnifeCyan.png");
            texChefKnifeCyan.wrapMode = TextureWrapMode.Repeat;

            Texture2D texChefKnifeProvi = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/texChefKnifeProvi.png");
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
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinCHEF_MOD_Wolfo_Black_Simu",
                NameToken = "SIMU_SKIN_CHEFMOD_BLACK",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconBLACK.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosBLACK,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoRED = new SkinDefInfo
            {
                Name = "skinCHEF_MOD_Wolfo_Red_Simu",
                NameToken = "SIMU_SKIN_CHEFMOD_RED",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconRed.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosRED,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoGREEN = new SkinDefInfo
            {
                Name = "skinCHEF_MOD_Wolfo_Green_Simu",
                NameToken = "SIMU_SKIN_CHEFMOD_GREEN",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconGreen.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosGREEN,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoBLUE = new SkinDefInfo
            {
                Name = "skinCHEF_MOD_Wolfo_BLUE",
                NameToken = "SIMU_SKIN_CHEFMOD_BLUE",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconBLUE.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosBLUE,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoCYAN = new SkinDefInfo
            {
                Name = "skinCHEF_MOD_Wolfo_CYAN_Simu",
                NameToken = "SIMU_SKIN_CHEFMOD_CYAN",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconCYAN.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosCYAN,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoPROVI = new SkinDefInfo
            {
                Name = "skinCHEF_MOD_Wolfo_PROVI_Simu",
                NameToken = "SIMU_SKIN_CHEFMOD_PROVI",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconProvi.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosPROVI,
                RootObject = skinChef.rootObject,
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

        [RegisterAchievement("CLEAR_ANY_GNOMECHEF", "Skins.GnomeChef.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumCHEFMOD : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("GnomeChefBody");
            }
        }
    }
}