using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsEnforcer
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_ENFORCER_RED", "Red Operator");
            LanguageAPI.Add("SIMU_SKIN_ENFORCER_YELLOW", "Saftey");
            LanguageAPI.Add("SIMU_SKIN_ENFORCER_GREEN", "Militant");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ENFORCER_NAME", "Enforcer: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ENFORCER_DESCRIPTION", "As Enforcer" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_ENFORCER_NAME";
            unlockableDef.cachedName = "Skins.Enforcer.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinEnforcerIcon);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            
        }

        internal static void ModdedSkin(GameObject EnforcerBody)
        {
            Debug.Log("Enforcer Skins");
            unlockableDef.hidden = false;
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }


            SkinYELLOW(EnforcerBody);
            //SkinWAR(EnforcerBody);
            SkinRED(EnforcerBody);
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

            Texture2D texEnforcerShield = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texEnforcerShield.LoadImage(Properties.Resources.texEnforcerShield, true);
            texEnforcerShield.filterMode = FilterMode.Bilinear;
            texEnforcerShield.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSexforcerShieldGlass = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texSexforcerShieldGlass.LoadImage(Properties.Resources.texSexforcerShieldGlass, true);
            texSexforcerShieldGlass.filterMode = FilterMode.Bilinear;
            texSexforcerShieldGlass.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texEnforcerGun.LoadImage(Properties.Resources.texEnforcerGun, true);
            texEnforcerGun.filterMode = FilterMode.Bilinear;
            texEnforcerGun.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun_Emission = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texEnforcerGun_Emission.LoadImage(Properties.Resources.texEnforcerGun_Emission, true);
            texEnforcerGun_Emission.filterMode = FilterMode.Bilinear;
            texEnforcerGun_Emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunSuper = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texClassicGunSuper.LoadImage(Properties.Resources.texClassicGunSuper, true);
            texClassicGunSuper.filterMode = FilterMode.Bilinear;
            texClassicGunSuper.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunHMG = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texClassicGunHMG.LoadImage(Properties.Resources.texClassicGunHMG, true);
            texClassicGunHMG.filterMode = FilterMode.Bilinear;
            texClassicGunHMG.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texEnforcer.LoadImage(Properties.Resources.texEnforcer, true);
            texEnforcer.filterMode = FilterMode.Bilinear;
            texEnforcer.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer_Emission = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texEnforcer_Emission.LoadImage(Properties.Resources.texEnforcer_Emission, true);
            texEnforcer_Emission.filterMode = FilterMode.Bilinear;
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
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinEnforcerIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinEnforcerWolfo",
                NameToken = "SIMU_SKIN_ENFORCER_RED",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                UnlockableDef = unlockableDef,
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

            Texture2D texEnforcerShield = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texEnforcerShield.LoadImage(Properties.Resources.texEnforcerShieldSAFETY, true);
            texEnforcerShield.filterMode = FilterMode.Bilinear;
            texEnforcerShield.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSexforcerShieldGlass = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texSexforcerShieldGlass.LoadImage(Properties.Resources.texSexforcerShieldGlassSAFETY, true);
            texSexforcerShieldGlass.filterMode = FilterMode.Bilinear;
            texSexforcerShieldGlass.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texEnforcerGun.LoadImage(Properties.Resources.texEnforcerGunSAFETY, true);
            texEnforcerGun.filterMode = FilterMode.Bilinear;
            texEnforcerGun.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcerGun_Emission = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texEnforcerGun_Emission.LoadImage(Properties.Resources.texEnforcerGun_EmissionSAFETY, true);
            texEnforcerGun_Emission.filterMode = FilterMode.Bilinear;
            texEnforcerGun_Emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunSuper = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texClassicGunSuper.LoadImage(Properties.Resources.texClassicGunSuperSAFETY, true);
            texClassicGunSuper.filterMode = FilterMode.Bilinear;
            texClassicGunSuper.wrapMode = TextureWrapMode.Repeat;

            Texture2D texClassicGunHMG = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texClassicGunHMG.LoadImage(Properties.Resources.texClassicGunHMGSAFETY, true);
            texClassicGunHMG.filterMode = FilterMode.Bilinear;
            texClassicGunHMG.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texEnforcer.LoadImage(Properties.Resources.texEnforcerSAFETY, true);
            texEnforcer.filterMode = FilterMode.Bilinear;
            texEnforcer.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEnforcer_Emission = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texEnforcer_Emission.LoadImage(Properties.Resources.texEnforcer_EmissionSAFETY, true);
            texEnforcer_Emission.filterMode = FilterMode.Bilinear;
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
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinEnforcerIconSAFETY, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinEnforcerWolfo_Safety",
                NameToken = "SIMU_SKIN_ENFORCER_YELLOW",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                UnlockableDef = unlockableDef,
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
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinEnforcerIconGREEN, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinEnforcerWolfo_GREEN",
                NameToken = "SIMU_SKIN_ENFORCER_GREEN",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinEnforcer },
                RootObject = skinEnforcer.rootObject,
                UnlockableDef = unlockableDef,
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


        [RegisterAchievement("SIMU_SKIN_ENFORCER", "Skins.Enforcer.Wolfo", null, 5, null)]
        public class ClearSimulacrumENFORCER : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EnforcerBody");
            }
        }


        internal static void PrismAchievement()
        {
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_ENFORCER_NAME", "Enforcer" + Unlocks.unlockNamePrism);
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_ENFORCER_DESCRIPTION", "As Enforcer" + Unlocks.unlockConditionPrism);
            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_PRISM_SKIN_ENFORCER_NAME";
            unlockableDef.cachedName = "Skins.Enforcer.Wolfo.Prism";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
        }

        /*[RegisterAchievement("PRISM_SKIN_ENFORCER", "Skins.Enforcer.Wolfo.Prism", null, 5, null)]
        public class AchievementPrismaticDissoEnforcer2Body : AchievementPrismaticDisso
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EnforcerBody");
            }
        }*/
    }
}