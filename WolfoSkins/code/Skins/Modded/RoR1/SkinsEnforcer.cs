using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;


namespace WolfoSkinsMod.Mod
{
    public class SkinsEnforcer
    {
        internal static void ModdedSkin(GameObject EnforcerBody)
        {
            Debug.Log("Enforcer Skins");
            BodyIndex EnforcerIndex = EnforcerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = EnforcerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinEnforcer = modelSkinController.skins[0];
            SkinDef skinEnforcerBot = modelSkinController.skins[3];

            SkinDef yellow = SkinYELLOW(skinEnforcer);
            //SkinWAR(EnforcerBody);
            SkinDef redBot = SkinRED_ALT(skinEnforcerBot);
            SkinDef red = SkinRED(skinEnforcer);

            //Fit him in before all the meme skins & After Grand Mastery
            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length];

            skinsNew[0] = modelSkinController.skins[0];
            skinsNew[1] = modelSkinController.skins[1];
            skinsNew[2] = modelSkinController.skins[2];
            skinsNew[3] = modelSkinController.skins[3];
            skinsNew[4] = red;
            skinsNew[5] = redBot;
            skinsNew[6] = yellow;

            for (int i = 7; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i] = modelSkinController.skins[i-3];
            }
            modelSkinController.skins = skinsNew;
            //SkinCatalog.skinsByBody[(int)EnforcerIndex] = skinsNew;


            //0 matEnforcerShield
            //1 matEnforcerShieldGlass (Instance)
            //2 matEnforcerBoard (Instance)
            //3 matEnforcerGun (Instance)
            //4 matClassicGunSuper (Instance)
            //5 matClassicGunHMG (Instance)
            //6 matEnforcerHammer (Instance)
            //7 matEnforcer (Instance)
            //8 matEnforcer (Instance)
        }

        internal static SkinDef SkinRED_ALT(SkinDef skinN4CR)
        {
            //0 matN4CR
            //1 matEnforcerShieldGlass (Instance)
            //2 matEnforcerBoard (Instance)
            //3 matN4CR
            //4 matN4CR
            //5 matN4CR
            //6 matEnforcerHammer (Instance)
            //7 matN4CR
            //8 matN4CR
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinEnforcerBot_1",
                nameToken = "SIMU_SKIN_ENFORCER_RED_ALT",
                icon = H.GetIcon("mod/ror1/enforcer_redBot"),
                original = skinN4CR,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;


            Material matN4CR = CloneMat(newRenderInfos, 0);
            Material matN4CR_W = CloneMat(newRenderInfos, 0);
            Material matN4CR_S = CloneMat(newRenderInfos, 0);
            Material matEnforcerShieldGlass = CloneMat(newRenderInfos, 1);
            Material matEnforcerHammer = CloneMat(newRenderInfos, 6);

            Texture2D texN4CREmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CREmission.png");
            Texture2D texN4CREmission_Weapon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CREmission_Weapon.png");


            Color ExtraRed = new Color(1f, 0.9f, 0.9f, 1);

            matN4CR.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CR.png");
            matN4CR.SetTexture("_EmTex", texN4CREmission);
            matN4CR.SetTexture("_EmissionMap", texN4CREmission);
            matN4CR.color = ExtraRed;

            matN4CR_W.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CR_Weapon.png");
            matN4CR_W.SetTexture("_EmTex", texN4CREmission_Weapon);
            matN4CR_W.SetTexture("_EmissionMap", texN4CREmission_Weapon);
            matN4CR_W.color = ExtraRed;

            matN4CR_S.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CR_Shield.png");
            matN4CR_S.SetTexture("_EmTex", texN4CREmission_Weapon);
            matN4CR_S.SetTexture("_EmissionMap", texN4CREmission_Weapon);
            matN4CR_S.color = ExtraRed;


            matEnforcerShieldGlass.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texSexforcerShieldGlass.png");

            newRenderInfos[0].defaultMaterial = matN4CR_W;
            newRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            newRenderInfos[2].defaultMaterial = matN4CR;
            newRenderInfos[3].defaultMaterial = matN4CR_W;
            newRenderInfos[4].defaultMaterial = matN4CR_W;
            newRenderInfos[5].defaultMaterial = matN4CR_W;
            newRenderInfos[6].defaultMaterial = matEnforcerHammer;
            newRenderInfos[7].defaultMaterial = matN4CR;
            newRenderInfos[8].defaultMaterial = matN4CR;

            return newSkinDef;
        }

        internal static SkinDef SkinRED(SkinDef skinEnforcer)
        {

            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinEnforcer_1",
                nameToken = "SIMU_SKIN_ENFORCER_RED",
                icon = H.GetIcon("mod/ror1/enforcer_red"),
                original = skinEnforcer,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matEnforcerShield = CloneMat(newRenderInfos, 0);
            Material matEnforcerShieldGlass = CloneMat(newRenderInfos, 1);
            Material matEnforcerBoard = CloneMat(newRenderInfos, 2);
            Material matEnforcerGun = CloneMat(newRenderInfos, 3);
            Material matClassicGunSuper = CloneMat(newRenderInfos, 4);
            Material matClassicGunHMG = CloneMat(newRenderInfos, 5);
            Material matEnforcerHammer = CloneMat(newRenderInfos, 6);
            Material matEnforcer = CloneMat(newRenderInfos, 7);

            Texture2D texEnforcer_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcer_Emission.png");

            Color ExtraRed = new Color(1f, 0.7f, 0.7f, 1);
            matEnforcerShield.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcerShield.png"); ;
            matEnforcerShield.color = ExtraRed;
            matEnforcerShieldGlass.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texSexforcerShieldGlass.png");

            matEnforcerGun.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcerGun.png"); ;
            matEnforcerGun.color = new Color(1f, 1f, 0.85f);
            matEnforcerGun.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcerGun_Emission.png"));
            matEnforcerGun.SetTexture("_EmissionMap", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcerGun_Emission.png"));

            matClassicGunSuper.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texClassicGunSuper.png"); ;
            //matClassicGunSuper.SetTexture("_EmTex", texShotgunEmissive);
            //matClassicGunSuper.SetTexture("_EmissionMap", texShotgunEmissive);

            matClassicGunHMG.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texClassicGunHMG.png"); ;
            //matClassicGunHMG.SetTexture("_EmTex", texEnforcerGun_Emission);
            //matClassicGunHMG.SetTexture("_EmissionMap", texEnforcerGun_Emission);
            matEnforcerHammer.color = new Color(0.5f, 0.4f, 0.2f);

            matEnforcer.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcer.png");
            matEnforcer.SetTexture("_EmTex", texEnforcer_Emission);
            matEnforcer.SetTexture("_EmissionMap", texEnforcer_Emission);
            matEnforcer.color = ExtraRed;

            newRenderInfos[0].defaultMaterial = matEnforcerShield;
            newRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            newRenderInfos[2].defaultMaterial = matEnforcerBoard;
            newRenderInfos[3].defaultMaterial = matEnforcerGun;
            newRenderInfos[4].defaultMaterial = matClassicGunSuper;
            newRenderInfos[5].defaultMaterial = matClassicGunHMG;
            newRenderInfos[6].defaultMaterial = matEnforcerHammer;
            newRenderInfos[7].defaultMaterial = matEnforcer;
            newRenderInfos[8].defaultMaterial = matEnforcer;
            return newSkinDef;
        }

        internal static SkinDef SkinYELLOW(SkinDef skinEnforcer)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinEnforcer_Safety_1",
                nameToken = "SIMU_SKIN_ENFORCER_YELLOW",
                icon = H.GetIcon("mod/ror1/enforcer_yellow"),
                original = skinEnforcer,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matEnforcerShield = CloneMat(newRenderInfos, 0);
            Material matEnforcerShieldGlass = CloneMat(newRenderInfos, 1);
            Material matEnforcerBoard = CloneMat(newRenderInfos, 2);
            Material matEnforcerGun = CloneMat(newRenderInfos, 3);
            Material matClassicGunSuper = CloneMat(newRenderInfos, 4);
            Material matClassicGunHMG = CloneMat(newRenderInfos, 5);
            Material matEnforcerHammer = CloneMat(newRenderInfos, 6);
            Material matEnforcer = CloneMat(newRenderInfos, 7);


            Texture2D texEnforcerGun_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerGun_EmissionSAFETY.png");
            Texture2D texEnforcer_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcer_EmissionSAFETY.png");


            Color extraYellow = new Color(1f, 1f, 0.9f, 1);

            matEnforcerShield.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerShieldSAFETY.png");
            matEnforcerShield.color = extraYellow;
            matEnforcerShieldGlass.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texSexforcerShieldGlassSAFETY.png");

            matEnforcerGun.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerGunSAFETY.png");
            matEnforcerGun.color = new Color(1f, 1f, 1f);
            matEnforcerGun.SetTexture("_EmTex", texEnforcerGun_Emission);
            matEnforcerGun.SetTexture("_EmissionMap", texEnforcerGun_Emission);

            matClassicGunSuper.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texClassicGunSuperSAFETY.png");
            //matClassicGunSuper.SetTexture("_EmTex", texShotgunEmissive);
            //matClassicGunSuper.SetTexture("_EmissionMap", texShotgunEmissive);

            matClassicGunHMG.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texClassicGunHMGSAFETY.png");
            //matClassicGunHMG.SetTexture("_EmTex", texEnforcerGun_Emission);
            //matClassicGunHMG.SetTexture("_EmissionMap", texEnforcerGun_Emission);
            matEnforcerHammer.color = new Color(0.5f, 0.3f, 0.5f);

            matEnforcer.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerSAFETY.png");
            matEnforcer.SetTexture("_EmTex", texEnforcer_Emission);
            matEnforcer.SetTexture("_EmissionMap", texEnforcer_Emission);
            matEnforcer.color = extraYellow;

            newRenderInfos[0].defaultMaterial = matEnforcerShield;
            newRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            newRenderInfos[2].defaultMaterial = matEnforcerBoard;
            newRenderInfos[3].defaultMaterial = matEnforcerGun;
            newRenderInfos[4].defaultMaterial = matClassicGunSuper;
            newRenderInfos[5].defaultMaterial = matClassicGunHMG;
            newRenderInfos[6].defaultMaterial = matEnforcerHammer;
            newRenderInfos[7].defaultMaterial = matEnforcer;
            newRenderInfos[8].defaultMaterial = matEnforcer;

            return newSkinDef;
        }

        #region Unfininished Green
        /*
        internal static void SkinWAR(GameObject EnforcerBody)
        {
            BodyIndex EnforcerIndex = EnforcerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = EnforcerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinEnforcer = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[skinEnforcer.rendererInfos.Length];
            System.Array.Copy(skinEnforcer.rendererInfos, newRenderInfos, skinEnforcer.rendererInfos.Length);

            Material matEnforcerShield = CloneMat(Enforcer.rendererInfos[0].defaultMaterial);
            Material matEnforcerShieldGlass = CloneMat(Enforcer.rendererInfos[1].defaultMaterial);
            Material matEnforcerBoard = CloneMat(Enforcer.rendererInfos[2].defaultMaterial);
            Material matEnforcerGun = CloneMat(Enforcer.rendererInfos[3].defaultMaterial);
            Material matClassicGunSuper = CloneMat(Enforcer.rendererInfos[4].defaultMaterial);
            Material matClassicGunHMG = CloneMat(Enforcer.rendererInfos[5].defaultMaterial);
            Material matEnforcerHammer = CloneMat(Enforcer.rendererInfos[6].defaultMaterial);
            Material matEnforcer = CloneMat(Enforcer.rendererInfos[7].defaultMaterial);

            Texture2D texEnforcerShield = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texEnforcerShield.LoadImage(Properties.Resources.texEnforcerShieldGREEN, true);
            texEnforcerShield.filterMode = FilterMode.Bilinear;
            texEnforcerShield.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSexforcerShieldGlass = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texSexforcerShieldGlass.LoadImage(Properties.Resources.texSexforcerShieldGlassGREEN, true);
            texSexforcerShieldGlass.filterMode = FilterMode.Bilinear;
            texSexforcerShieldGlass.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texEnforcerGun.LoadImage(Properties.Resources.texEnforcerGunGREEN, true);
            texEnforcerGun.filterMode = FilterMode.Bilinear;
            texEnforcerGun.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun_Emission = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texEnforcerGun_Emission.LoadImage(Properties.Resources.texEnforcerGun_EmissionGREEN, true);
            texEnforcerGun_Emission.filterMode = FilterMode.Bilinear;
            texEnforcerGun_Emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunSuper = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texClassicGunSuper.LoadImage(Properties.Resources.texClassicGunSuperGREEN, true);
            texClassicGunSuper.filterMode = FilterMode.Bilinear;
            texClassicGunSuper.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunHMG = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texClassicGunHMG.LoadImage(Properties.Resources.texClassicGunHMGGREEN, true);
            texClassicGunHMG.filterMode = FilterMode.Bilinear;
            texClassicGunHMG.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texEnforcer.LoadImage(Properties.Resources.texEnforcerGREEN, true);
            texEnforcer.filterMode = FilterMode.Bilinear;
            texEnforcer.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer_Emission = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texEnforcer_Emission.LoadImage(Properties.Resources.texEnforcer_EmissionGREEN, true);
            texEnforcer_Emission.filterMode = FilterMode.Bilinear;
            texEnforcer_Emission.wrapMode = TextureWrapMode.Repeat;

            Color ExtraRed = new Color(1f, 1f, 1f, 1);

            matEnforcerShield.mainTexture = texEnforcerShield;
            matEnforcerShield.color = ExtraRed;
            matEnforcerShieldGlass.mainTexture = texSexforcerShieldGlass;

            matEnforcerGun.mainTexture = texEnforcerGun;
            matEnforcerGun.color = new Color(1f, 1f, 1f);
            matEnforcerGun.SetTexture("_EmTex", texEnforcerGun_Emission);
            matEnforcerGun.SetTexture("_EmissionMap", texEnforcerGun_Emission);

            matClassicGunSuper.mainTexture = texClassicGunSuper;
            //matClassicGunSuper.SetTexture("_EmTex", texShotgunEmissive);
            //matClassicGunSuper.SetTexture("_EmissionMap", texShotgunEmissive);

            matClassicGunHMG.mainTexture = texClassicGunHMG;
            //matClassicGunHMG.SetTexture("_EmTex", texEnforcerGun_Emission);
            //matClassicGunHMG.SetTexture("_EmissionMap", texEnforcerGun_Emission);
            matEnforcerHammer.color = new Color(0.6f, 0.4f, 0.2f);

            matEnforcer.mainTexture = texEnforcer;
            matEnforcer.SetTexture("_EmTex", texEnforcer_Emission);
            matEnforcer.SetTexture("_EmissionMap", texEnforcer_Emission);
            matEnforcer.color = ExtraRed;

            newRenderInfos[0].defaultMaterial = matEnforcerShield;
            newRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            newRenderInfos[2].defaultMaterial = matEnforcerBoard;
            newRenderInfos[3].defaultMaterial = matEnforcerGun;
            newRenderInfos[4].defaultMaterial = matClassicGunSuper;
            newRenderInfos[5].defaultMaterial = matClassicGunHMG;
            newRenderInfos[6].defaultMaterial = matEnforcerHammer;
            newRenderInfos[7].defaultMaterial = matEnforcer;
            newRenderInfos[8].defaultMaterial = matEnforcer;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinEnforcer_GREEN_1",
                NameToken = "SIMU_SKIN_ENFORCER_GREEN",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                RendererInfos = newRenderInfos,
                MeshReplacements = skinEnforcer.meshReplacements,
                GameObjectActivations = skinEnforcer.gameObjectActivations,
                ProjectileGhostReplacements = skinEnforcer.projectileGhostReplacements,
            };
            SkinDef EnforcerSkinDefNew = Skins.CreatenewSkinDef(SkinInfo);

           
        }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_ENFORCER", "Skins.Enforcer.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumENFORCER : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EnforcerBody");
            }
        }

        /*
        [RegisterAchievement("CLEAR_BOTH_ENFORCER", "Skins.Enforcer.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumENFORCER2 : Achievement_AltBoss
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EnforcerBody");
            }
        }
        */
    }
}