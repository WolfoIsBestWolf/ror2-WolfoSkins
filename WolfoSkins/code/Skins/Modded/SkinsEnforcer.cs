using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsEnforcer
    {
        internal static void ModdedSkin(GameObject EnforcerBody)
        {
            Debug.Log("Enforcer Skins");

            SkinYELLOW(EnforcerBody);
            //SkinWAR(EnforcerBody);
            SkinRED_ALT(EnforcerBody);
            SkinRED(EnforcerBody);
        }

        internal static void SkinRED_ALT(GameObject EnforcerBody)
        {
            BodyIndex EnforcerIndex = EnforcerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = EnforcerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinEnforcer = modelSkinController.skins[3];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinEnforcer.rendererInfos.Length];
            System.Array.Copy(skinEnforcer.rendererInfos, NewRenderInfos, skinEnforcer.rendererInfos.Length);

            //0 matN4CR
            //1 matEnforcerShieldGlass (Instance)
            //2 matEnforcerBoard (Instance)
            //3 matN4CR
            //4 matN4CR
            //5 matN4CR
            //6 matEnforcerHammer (Instance)
            //7 matN4CR
            //8 matN4CR


            Material matN4CR = Object.Instantiate(skinEnforcer.rendererInfos[0].defaultMaterial);
            Material matN4CR_W = Object.Instantiate(skinEnforcer.rendererInfos[0].defaultMaterial);
            Material matN4CR_S = Object.Instantiate(skinEnforcer.rendererInfos[0].defaultMaterial);
            Material matEnforcerShieldGlass = Object.Instantiate(skinEnforcer.rendererInfos[1].defaultMaterial);
            Material matEnforcerHammer = Object.Instantiate(skinEnforcer.rendererInfos[6].defaultMaterial);

 
            Texture2D texSexforcerShieldGlass = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texSexforcerShieldGlass.png");
            texSexforcerShieldGlass.wrapMode = TextureWrapMode.Repeat;

            Texture2D texN4CR = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CR.png");
            texN4CR.wrapMode = TextureWrapMode.Repeat;

            Texture2D texN4CREmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CREmission.png");
            texN4CREmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texN4CR_Weapon2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CR_Shield.png");
            texN4CR_Weapon2.wrapMode = TextureWrapMode.Repeat;
            
            Texture2D texN4CR_Weapon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CR_Weapon.png");
            texN4CR_Weapon.wrapMode = TextureWrapMode.Repeat;

            Texture2D texN4CREmission_Weapon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CREmission_Weapon.png");
            texN4CREmission_Weapon.wrapMode = TextureWrapMode.Repeat;

            Color ExtraRed = new Color(1f, 0.9f, 0.9f, 1);

            matN4CR.mainTexture = texN4CR;
            matN4CR.SetTexture("_EmTex", texN4CREmission);
            matN4CR.SetTexture("_EmissionMap", texN4CREmission);
            matN4CR.color = ExtraRed;

            matN4CR_W.mainTexture = texN4CR_Weapon;
            matN4CR_W.SetTexture("_EmTex", texN4CREmission_Weapon);
            matN4CR_W.SetTexture("_EmissionMap", texN4CREmission_Weapon);
            matN4CR_W.color = ExtraRed;

            matN4CR_S.mainTexture = texN4CR_Weapon2;
            matN4CR_S.SetTexture("_EmTex", texN4CREmission_Weapon);
            matN4CR_S.SetTexture("_EmissionMap", texN4CREmission_Weapon);
            matN4CR_S.color = ExtraRed;


            matEnforcerShieldGlass.mainTexture = texSexforcerShieldGlass;
          

            NewRenderInfos[0].defaultMaterial = matN4CR_W;
            NewRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            NewRenderInfos[2].defaultMaterial = matN4CR;
            NewRenderInfos[3].defaultMaterial = matN4CR_W;
            NewRenderInfos[4].defaultMaterial = matN4CR_W;
            NewRenderInfos[5].defaultMaterial = matN4CR_W;
            NewRenderInfos[6].defaultMaterial = matEnforcerHammer;
            NewRenderInfos[7].defaultMaterial = matN4CR;
            NewRenderInfos[8].defaultMaterial = matN4CR;
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinEnforcerBotWolfo_Simu",
                NameToken = "SIMU_SKIN_ENFORCER_RED_ALT",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/RedBot/texN4CRAchievement.png")),
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinEnforcer.meshReplacements,
                GameObjectActivations = skinEnforcer.gameObjectActivations,
                ProjectileGhostReplacements = skinEnforcer.projectileGhostReplacements,
            };
            SkinDef EnforcerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            //Maybe you can fit him in before all the meme skins / After Grand Mastery
            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length + 1];

            skinsNew[0] = modelSkinController.skins[0];
            skinsNew[1] = modelSkinController.skins[1];
            skinsNew[2] = modelSkinController.skins[2];
            skinsNew[3] = modelSkinController.skins[3];
            skinsNew[4] = EnforcerSkinDefNew;

            for (int i = 4; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i + 1] = modelSkinController.skins[i];
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)EnforcerIndex] = skinsNew;
        }

        internal static void SkinRED(GameObject EnforcerBody)
        {
            BodyIndex EnforcerIndex = EnforcerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = EnforcerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinEnforcer = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinEnforcer.rendererInfos.Length];
            System.Array.Copy(skinEnforcer.rendererInfos, NewRenderInfos, skinEnforcer.rendererInfos.Length);

            //0 matEnforcerShield
            //1 matEnforcerShieldGlass (Instance)
            //2 matEnforcerBoard (Instance)
            //3 matEnforcerGun (Instance)
            //4 matClassicGunSuper (Instance)
            //5 matClassicGunHMG (Instance)
            //6 matEnforcerHammer (Instance)
            //7 matEnforcer (Instance)
            //8 matEnforcer (Instance)

            Material matEnforcerShield = Object.Instantiate(skinEnforcer.rendererInfos[0].defaultMaterial);
            Material matEnforcerShieldGlass = Object.Instantiate(skinEnforcer.rendererInfos[1].defaultMaterial);
            Material matEnforcerBoard = Object.Instantiate(skinEnforcer.rendererInfos[2].defaultMaterial);
            Material matEnforcerGun = Object.Instantiate(skinEnforcer.rendererInfos[3].defaultMaterial);
            Material matClassicGunSuper = Object.Instantiate(skinEnforcer.rendererInfos[4].defaultMaterial);
            Material matClassicGunHMG = Object.Instantiate(skinEnforcer.rendererInfos[5].defaultMaterial);
            Material matEnforcerHammer = Object.Instantiate(skinEnforcer.rendererInfos[6].defaultMaterial);
            Material matEnforcer = Object.Instantiate(skinEnforcer.rendererInfos[7].defaultMaterial);

            Texture2D texEnforcerShield = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcerShield.png");
            texEnforcerShield.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSexforcerShieldGlass = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texSexforcerShieldGlass.png");
            texSexforcerShieldGlass.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcerGun.png");
            texEnforcerGun.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcerGun_Emission.png");
            texEnforcerGun_Emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunSuper = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texClassicGunSuper.png");
            texClassicGunSuper.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunHMG = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texClassicGunHMG.png");
            texClassicGunHMG.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcer.png");
            texEnforcer.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/texEnforcer_Emission.png");
            texEnforcer_Emission.wrapMode = TextureWrapMode.Repeat;

            Color ExtraRed = new Color(1f, 0.7f, 0.7f, 1);

            matEnforcerShield.mainTexture = texEnforcerShield;
            matEnforcerShield.color = ExtraRed;
            matEnforcerShieldGlass.mainTexture = texSexforcerShieldGlass;

            matEnforcerGun.mainTexture = texEnforcerGun;
            matEnforcerGun.color = new Color(1f,1f,0.85f);
            matEnforcerGun.SetTexture("_EmTex", texEnforcerGun_Emission);
            matEnforcerGun.SetTexture("_EmissionMap", texEnforcerGun_Emission);

            matClassicGunSuper.mainTexture = texClassicGunSuper;
            //matClassicGunSuper.SetTexture("_EmTex", texShotgunEmissive);
            //matClassicGunSuper.SetTexture("_EmissionMap", texShotgunEmissive);

            matClassicGunHMG.mainTexture = texClassicGunHMG;
            //matClassicGunHMG.SetTexture("_EmTex", texEnforcerGun_Emission);
            //matClassicGunHMG.SetTexture("_EmissionMap", texEnforcerGun_Emission);
            matEnforcerHammer.color = new Color(0.5f, 0.4f, 0.2f);

            matEnforcer.mainTexture = texEnforcer;
            matEnforcer.SetTexture("_EmTex", texEnforcer_Emission);
            matEnforcer.SetTexture("_EmissionMap", texEnforcer_Emission);
            matEnforcer.color = ExtraRed;

            NewRenderInfos[0].defaultMaterial = matEnforcerShield;
            NewRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            NewRenderInfos[2].defaultMaterial = matEnforcerBoard;
            NewRenderInfos[3].defaultMaterial = matEnforcerGun;
            NewRenderInfos[4].defaultMaterial = matClassicGunSuper;
            NewRenderInfos[5].defaultMaterial = matClassicGunHMG;
            NewRenderInfos[6].defaultMaterial = matEnforcerHammer;
            NewRenderInfos[7].defaultMaterial = matEnforcer;
            NewRenderInfos[8].defaultMaterial = matEnforcer;
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinEnforcerWolfo_Simu",
                NameToken = "SIMU_SKIN_ENFORCER_RED",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Red/skinEnforcerIcon.png")),
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinEnforcer.meshReplacements,
                GameObjectActivations = skinEnforcer.gameObjectActivations,
                ProjectileGhostReplacements = skinEnforcer.projectileGhostReplacements,
            };
            SkinDef EnforcerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            //Maybe you can fit him in before all the meme skins / After Grand Mastery
            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length+1];

            skinsNew[0] = modelSkinController.skins[0];
            skinsNew[1] = modelSkinController.skins[1];
            skinsNew[2] = modelSkinController.skins[2];
            skinsNew[3] = modelSkinController.skins[3];
            skinsNew[4] = EnforcerSkinDefNew;

            for (int i = 4; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i + 1] = modelSkinController.skins[i];
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)EnforcerIndex] = skinsNew;
        }

        internal static void SkinYELLOW(GameObject EnforcerBody)
        {
            BodyIndex EnforcerIndex = EnforcerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = EnforcerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinEnforcer = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinEnforcer.rendererInfos.Length];
            System.Array.Copy(skinEnforcer.rendererInfos, NewRenderInfos, skinEnforcer.rendererInfos.Length);

            Material matEnforcerShield = Object.Instantiate(skinEnforcer.rendererInfos[0].defaultMaterial);
            Material matEnforcerShieldGlass = Object.Instantiate(skinEnforcer.rendererInfos[1].defaultMaterial);
            Material matEnforcerBoard = Object.Instantiate(skinEnforcer.rendererInfos[2].defaultMaterial);
            Material matEnforcerGun = Object.Instantiate(skinEnforcer.rendererInfos[3].defaultMaterial);
            Material matClassicGunSuper = Object.Instantiate(skinEnforcer.rendererInfos[4].defaultMaterial);
            Material matClassicGunHMG = Object.Instantiate(skinEnforcer.rendererInfos[5].defaultMaterial);
            Material matEnforcerHammer = Object.Instantiate(skinEnforcer.rendererInfos[6].defaultMaterial);
            Material matEnforcer = Object.Instantiate(skinEnforcer.rendererInfos[7].defaultMaterial);

            Texture2D texEnforcerShield = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerShieldSAFETY.png");
            texEnforcerShield.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSexforcerShieldGlass = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texSexforcerShieldGlassSAFETY.png");
            texSexforcerShieldGlass.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerGunSAFETY.png");
            texEnforcerGun.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerGun_EmissionSAFETY.png");
            texEnforcerGun_Emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunSuper = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texClassicGunSuperSAFETY.png");
            texClassicGunSuper.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunHMG = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texClassicGunHMGSAFETY.png");
            texClassicGunHMG.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcerSAFETY.png");
            texEnforcer.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/texEnforcer_EmissionSAFETY.png");
            texEnforcer_Emission.wrapMode = TextureWrapMode.Repeat;


            Color ExtraRed = new Color(1f, 1f, 0.9f, 1);

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
            matEnforcerHammer.color = new Color(0.5f, 0.3f, 0.5f);

            matEnforcer.mainTexture = texEnforcer;
            matEnforcer.SetTexture("_EmTex", texEnforcer_Emission);
            matEnforcer.SetTexture("_EmissionMap", texEnforcer_Emission);
            matEnforcer.color = ExtraRed;

            NewRenderInfos[0].defaultMaterial = matEnforcerShield;
            NewRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            NewRenderInfos[2].defaultMaterial = matEnforcerBoard;
            NewRenderInfos[3].defaultMaterial = matEnforcerGun;
            NewRenderInfos[4].defaultMaterial = matClassicGunSuper;
            NewRenderInfos[5].defaultMaterial = matClassicGunHMG;
            NewRenderInfos[6].defaultMaterial = matEnforcerHammer;
            NewRenderInfos[7].defaultMaterial = matEnforcer;
            NewRenderInfos[8].defaultMaterial = matEnforcer;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinEnforcerWolfo_Safety_Simu",
                NameToken = "SIMU_SKIN_ENFORCER_YELLOW",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Enforcer/Yellow/skinEnforcerIconSAFETY.png")),
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinEnforcer.meshReplacements,
                GameObjectActivations = skinEnforcer.gameObjectActivations,
                ProjectileGhostReplacements = skinEnforcer.projectileGhostReplacements,
            };
            SkinDef EnforcerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length + 1];

            skinsNew[0] = modelSkinController.skins[0];
            skinsNew[1] = modelSkinController.skins[1];
            skinsNew[2] = modelSkinController.skins[2];
            skinsNew[3] = modelSkinController.skins[3];
            skinsNew[4] = EnforcerSkinDefNew;

            for (int i = 4; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i + 1] = modelSkinController.skins[i];
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)EnforcerIndex] = skinsNew;
        }

        #region Unfininished Green
        /*
        internal static void SkinWAR(GameObject EnforcerBody)
        {
            BodyIndex EnforcerIndex = EnforcerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = EnforcerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinEnforcer = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinEnforcer.rendererInfos.Length];
            System.Array.Copy(skinEnforcer.rendererInfos, NewRenderInfos, skinEnforcer.rendererInfos.Length);

            Material matEnforcerShield = Object.Instantiate(skinEnforcer.rendererInfos[0].defaultMaterial);
            Material matEnforcerShieldGlass = Object.Instantiate(skinEnforcer.rendererInfos[1].defaultMaterial);
            Material matEnforcerBoard = Object.Instantiate(skinEnforcer.rendererInfos[2].defaultMaterial);
            Material matEnforcerGun = Object.Instantiate(skinEnforcer.rendererInfos[3].defaultMaterial);
            Material matClassicGunSuper = Object.Instantiate(skinEnforcer.rendererInfos[4].defaultMaterial);
            Material matClassicGunHMG = Object.Instantiate(skinEnforcer.rendererInfos[5].defaultMaterial);
            Material matEnforcerHammer = Object.Instantiate(skinEnforcer.rendererInfos[6].defaultMaterial);
            Material matEnforcer = Object.Instantiate(skinEnforcer.rendererInfos[7].defaultMaterial);

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

            NewRenderInfos[0].defaultMaterial = matEnforcerShield;
            NewRenderInfos[1].defaultMaterial = matEnforcerShieldGlass;
            NewRenderInfos[2].defaultMaterial = matEnforcerBoard;
            NewRenderInfos[3].defaultMaterial = matEnforcerGun;
            NewRenderInfos[4].defaultMaterial = matClassicGunSuper;
            NewRenderInfos[5].defaultMaterial = matClassicGunHMG;
            NewRenderInfos[6].defaultMaterial = matEnforcerHammer;
            NewRenderInfos[7].defaultMaterial = matEnforcer;
            NewRenderInfos[8].defaultMaterial = matEnforcer;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinEnforcerWolfo_GREEN_Simu",
                NameToken = "SIMU_SKIN_ENFORCER_GREEN",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinEnforcer.meshReplacements,
                GameObjectActivations = skinEnforcer.gameObjectActivations,
                ProjectileGhostReplacements = skinEnforcer.projectileGhostReplacements,
            };
            SkinDef EnforcerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length + 1];

            skinsNew[0] = modelSkinController.skins[0];
            skinsNew[1] = modelSkinController.skins[1];
            skinsNew[2] = modelSkinController.skins[2];
            skinsNew[3] = modelSkinController.skins[3];
            skinsNew[4] = EnforcerSkinDefNew;

            for (int i = 4; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i + 1] = modelSkinController.skins[i];
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)EnforcerIndex] = skinsNew;
        }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_ENFORCER", "Skins.Enforcer.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumENFORCER : Achievement_AltBoss_Simu
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