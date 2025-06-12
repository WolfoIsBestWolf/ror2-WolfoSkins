/*
using static WolfoSkinsMod.H;
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
                GameObject CleaverGhost = R2API.PrefabAPI.InstantiateClone(CleaverShot.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "CleaverGhost" + i, false);
                GameObject KnifeGhost = R2API.PrefabAPI.InstantiateClone(KnifeShot.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "KnifeGhost" + i, false);

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
            CharacterModel.RendererInfo[] newRenderInfosBLACK = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, newRenderInfosBLACK, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] newRenderInfosRED = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, newRenderInfosRED, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] newRenderInfosGREEN = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, newRenderInfosGREEN, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] newRenderInfosBLUE = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, newRenderInfosBLUE, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] newRenderInfosCYAN = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, newRenderInfosCYAN, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] newRenderInfosPROVI = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, newRenderInfosPROVI, skinChef.rendererInfos.Length);

            Material matChefBLACK = CloneMat(Chef.rendererInfos[0].defaultMaterial);
            Material matChefRED = CloneMat(Chef.rendererInfos[0].defaultMaterial);
            Material matChefGREEN = CloneMat(Chef.rendererInfos[0].defaultMaterial);
            Material matChefBLUE = CloneMat(Chef.rendererInfos[0].defaultMaterial);
            Material matChefCYAN = CloneMat(Chef.rendererInfos[0].defaultMaterial);
            Material matChefPROVI = CloneMat(Chef.rendererInfos[0].defaultMaterial);

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

            newRenderInfosBLACK[0].defaultMaterial = matChefBLACK;
            newRenderInfosRED[0].defaultMaterial = matChefRED;
            newRenderInfosGREEN[0].defaultMaterial = matChefGREEN;
            newRenderInfosBLUE[0].defaultMaterial = matChefBLUE;
            newRenderInfosCYAN[0].defaultMaterial = matChefCYAN;
            newRenderInfosPROVI[0].defaultMaterial = matChefPROVI;
            //
            Material matChefKnifeBLACK = CloneMat(Chef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeRED = CloneMat(Chef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeGREEN = CloneMat(Chef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeBLUE = CloneMat(Chef.rendererInfos[1].defaultMaterial);
            Material matChefKnifeCYAN = CloneMat(Chef.rendererInfos[1].defaultMaterial);
            Material matChefKnifePROVI = CloneMat(Chef.rendererInfos[1].defaultMaterial);

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

            newRenderInfosBLACK[1].defaultMaterial = matChefKnifeBLACK;
            newRenderInfosRED[1].defaultMaterial = matChefKnifeRED;
            newRenderInfosGREEN[1].defaultMaterial = matChefKnifeGREEN;
            newRenderInfosBLUE[1].defaultMaterial = matChefKnifeBLUE;
            newRenderInfosCYAN[1].defaultMaterial = matChefKnifeCYAN;
            newRenderInfosPROVI[1].defaultMaterial = matChefKnifePROVI;

            //
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinCHEF_MOD__Black_1",
                NameToken = "SIMU_SKIN_CHEFMOD_BLACK",
                Icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconBLACK.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = newRenderInfosBLACK,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoRED = new SkinDefInfo
            {
                Name = "skinCHEF_MOD__Red_1",
                NameToken = "SIMU_SKIN_CHEFMOD_RED",
                Icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconRed.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = newRenderInfosRED,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoGREEN = new SkinDefInfo
            {
                Name = "skinCHEF_MOD__Green_1",
                NameToken = "SIMU_SKIN_CHEFMOD_GREEN",
                Icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconGreen.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = newRenderInfosGREEN,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoBLUE = new SkinDefInfo
            {
                Name = "skinCHEF_MOD__BLUE",
                NameToken = "SIMU_SKIN_CHEFMOD_BLUE",
                Icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconBLUE.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = newRenderInfosBLUE,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoCYAN = new SkinDefInfo
            {
                Name = "skinCHEF_MOD__CYAN_1",
                NameToken = "SIMU_SKIN_CHEFMOD_CYAN",
                Icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconCYAN.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = newRenderInfosCYAN,
                RootObject = skinChef.rootObject,
            };
            SkinDefInfo SkinInfoPROVI = new SkinDefInfo
            {
                Name = "skinCHEF_MOD__PROVI_1",
                NameToken = "SIMU_SKIN_CHEFMOD_PROVI",
                Icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chef/skinChefIconProvi.png")),
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = newRenderInfosPROVI,
                RootObject = skinChef.rootObject,
            };
            SkinDef ChefSkinDefBLACK = Skins.CreatenewSkinDef(SkinInfo);
            SkinDef ChefSkinDefRED = Skins.CreatenewSkinDef(SkinInfoRED);
            SkinDef ChefSkinDefGREEN = Skins.CreatenewSkinDef(SkinInfoGREEN);
            SkinDef ChefSkinDefBLUE = Skins.CreatenewSkinDef(SkinInfoBLUE);
            SkinDef ChefSkinDefCYAN = Skins.CreatenewSkinDef(SkinInfoCYAN);
            SkinDef ChefSkinDefPROVI = Skins.CreatenewSkinDef(SkinInfoPROVI);

            modelSkinController.skins = modelSkinController.skins.Add(ChefSkinDefRED, ChefSkinDefGREEN, ChefSkinDefBLUE, ChefSkinDefBLACK, ChefSkinDefCYAN, ChefSkinDefPROVI);
            SkinCatalog.skinsByBody[(int)ChefIndex] = SkinCatalog.skinsByBody[(int)ChefIndex].Add(ChefSkinDefRED, ChefSkinDefGREEN, ChefSkinDefBLUE, ChefSkinDefBLACK, ChefSkinDefCYAN, ChefSkinDefPROVI);
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
        public class ClearSimulacrumCHEFMOD : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("GnomeChefBody");
            }
        }
    }
}
*/