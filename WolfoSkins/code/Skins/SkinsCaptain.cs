using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsCaptain
    {
        internal static void Start()
        {

        }

        internal static void CaptainSkin()
        {
            //Pink stuff test
            SkinDef CaptainSkinDefault = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef CaptainSkinWhite = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D PinktexCaptainJacketDiffuseW = new Texture2D(512, 512, TextureFormat.DXT1, false);
            PinktexCaptainJacketDiffuseW.LoadImage(Properties.Resources.PinktexCaptainJacketDiffuseW, true);
            PinktexCaptainJacketDiffuseW.filterMode = FilterMode.Bilinear;
            PinktexCaptainJacketDiffuseW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW.LoadImage(Properties.Resources.PinktexCaptainPaletteW, true);
            PinktexCaptainPaletteW.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW2.LoadImage(Properties.Resources.PinktexCaptainPaletteW2, true);
            PinktexCaptainPaletteW2.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW2.wrapMode = TextureWrapMode.Clamp;

            //Pallete for HAT
            Texture2D PinktexCaptainPaletteW3 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW3.LoadImage(Properties.Resources.PinktexCaptainPaletteW3, true);
            PinktexCaptainPaletteW3.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW3.wrapMode = TextureWrapMode.Clamp;


            CharacterModel.RendererInfo[] CaptainPinkRenderInfos = new CharacterModel.RendererInfo[7];
            System.Array.Copy(CaptainSkinWhite.rendererInfos, CaptainPinkRenderInfos, 7);

            Material PinkmatCaptainAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt2 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt3 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainArmorAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
            Material PinkmatCaptainJacketAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
            Material PinkmatCaptainRobotBitsAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);

            PinkmatCaptainAlt.mainTexture = PinktexCaptainPaletteW;
            PinkmatCaptainAlt2.mainTexture = PinktexCaptainPaletteW2;
            PinkmatCaptainAlt3.mainTexture = PinktexCaptainPaletteW3;
            //PinkmatCaptainArmorAlt.color = new Color32(255, 223, 188, 255);
            PinkmatCaptainArmorAlt.color = new Color32(255, 190, 135, 255);//(255, 195, 150, 255);
            //_EmColor is juts fucking weird
            PinkmatCaptainJacketAlt.mainTexture = PinktexCaptainJacketDiffuseW;
            PinkmatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1.2f, 0.6f, 1.2f, 1f));
            PinkmatCaptainRobotBitsAlt.color = new Color32(255, 190, 135, 255);

            CaptainPinkRenderInfos[0].defaultMaterial = PinkmatCaptainAlt; //matCaptainAlt
            CaptainPinkRenderInfos[1].defaultMaterial = PinkmatCaptainAlt3; //matCaptainAlt //Hat
            CaptainPinkRenderInfos[2].defaultMaterial = PinkmatCaptainArmorAlt; //matCaptainArmorAlt
            CaptainPinkRenderInfos[3].defaultMaterial = PinkmatCaptainJacketAlt; //matCaptainJacketAlt
            CaptainPinkRenderInfos[4].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[5].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[6].defaultMaterial = PinkmatCaptainAlt2; //matCaptainAlt //Skirt
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_CAPTAIN", "Honeymoon");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CAPTAIN_NAME", "Captain: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CAPTAIN_DESCRIPTION", "As Captain"+ Unlocks.unlockCondition);
            //
            Texture2D texCaptainPinkSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texCaptainPinkSkinIcon.LoadImage(Properties.Resources.texCaptainPinkSkinIcon, true);
            texCaptainPinkSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texCaptainPinkSkinIconS = Sprite.Create(texCaptainPinkSkinIcon, WRect.rec128, WRect.half);

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CAPTAIN_NAME";
            unlockableDef.cachedName = "Skins.Captain.Wolfo";
            unlockableDef.achievementIcon = texCaptainPinkSkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            //
            R2API.SkinDefInfo CaptainPinkSkinInfos = new R2API.SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN",
                UnlockableDef = unlockableDef,
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainPinkRenderInfos,
                Name = "skinCaptainWolfo",
                Icon = texCaptainPinkSkinIconS,
            };
            //

            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), CaptainPinkSkinInfos);
            CaptainSkinBLUE(unlockableDef);
            CaptainSkinRED(unlockableDef);
        }

        internal static void CaptainSkinBLUE(UnlockableDef unlockableDef)
        {
            SkinDef CaptainSkinDefault = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef CaptainSkinWhite = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D texCaptainJacketDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texCaptainJacketDiffuse.LoadImage(Properties.Resources.texCaptainJacketDiffuseAltBLUE, true);
            texCaptainJacketDiffuse.filterMode = FilterMode.Bilinear;
            texCaptainJacketDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainPalette = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texCaptainPalette.LoadImage(Properties.Resources.texCaptainPaletteAltBLUE, true);
            texCaptainPalette.filterMode = FilterMode.Bilinear;
            texCaptainPalette.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainPalette2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texCaptainPalette2.LoadImage(Properties.Resources.texCaptainPaletteAltBLUE2, true);
            texCaptainPalette2.filterMode = FilterMode.Bilinear;
            texCaptainPalette2.wrapMode = TextureWrapMode.Clamp;


            CharacterModel.RendererInfo[] CaptainRenderInfos = new CharacterModel.RendererInfo[7];
            System.Array.Copy(CaptainSkinWhite.rendererInfos, CaptainRenderInfos, 7);

            Material MatCaptainAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material MatCaptainAltDark = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            //Material MatCaptainAlt3 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material MatCaptainArmorAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
            Material MatCaptainJacketAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
            Material MatCaptainRobotBitsAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);

            MatCaptainAlt.mainTexture = texCaptainPalette;
            MatCaptainAltDark.mainTexture = texCaptainPalette2;
            MatCaptainAltDark.color = new Color(0.8f, 0.8f, 0.8f);
            //MatCaptainAlt3.mainTexture = texCaptainPalette3;
            //MatCaptainArmorAlt.color = new Color(0.5f, 0.5f, 0.5f);
            MatCaptainArmorAlt.color = new Color32(255, 225, 135, 255);//(255, 195, 150, 255);
            MatCaptainJacketAlt.mainTexture = texCaptainJacketDiffuse;

            MatCaptainRobotBitsAlt.color = new Color32(255, 225, 135, 255);
            MatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1f, 1f, 0.3f, 1f));

            //
            CaptainRenderInfos[0].defaultMaterial = MatCaptainAlt; //matCaptainAlt
            CaptainRenderInfos[1].defaultMaterial = MatCaptainAlt; //matCaptainAlt
            CaptainRenderInfos[2].defaultMaterial = MatCaptainArmorAlt; //matCaptainArmorAlt
            CaptainRenderInfos[3].defaultMaterial = MatCaptainJacketAlt; //matCaptainJacketAlt
            CaptainRenderInfos[4].defaultMaterial = MatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainRenderInfos[5].defaultMaterial = MatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainRenderInfos[6].defaultMaterial = MatCaptainAltDark; //matCaptainAlt //Skirt
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_CAPTAIN_BLUE", "Attendant");

            Texture2D skinIconCaptain = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinIconCaptain.LoadImage(Properties.Resources.texSkinSwatches_25BLUE, true);
            skinIconCaptain.filterMode = FilterMode.Bilinear;
            Sprite skinIconCaptainS = Sprite.Create(skinIconCaptain, WRect.rec128, WRect.half);

            R2API.SkinDefInfo SkinInfo2 = new R2API.SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN_BLUE",
                UnlockableDef = unlockableDef,
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainRenderInfos,
                Name = "skinCaptainWolfo_Blue",
                Icon = skinIconCaptainS,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }

        internal static void CaptainSkinRED(UnlockableDef unlockableDef)
        {
            //Pink stuff test
            SkinDef CaptainSkinDefault = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef CaptainSkinWhite = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D texCaptainJacketDiffuseRED = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texCaptainJacketDiffuseRED.LoadImage(Properties.Resources.texCaptainJacketDiffuseRED, true);
            texCaptainJacketDiffuseRED.filterMode = FilterMode.Bilinear;
            texCaptainJacketDiffuseRED.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainPaletteRED2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texCaptainPaletteRED2.LoadImage(Properties.Resources.texCaptainPaletteRED2, true);
            texCaptainPaletteRED2.filterMode = FilterMode.Bilinear;
            texCaptainPaletteRED2.wrapMode = TextureWrapMode.Clamp;

            CharacterModel.RendererInfo[] CaptainPinkRenderInfos = new CharacterModel.RendererInfo[7];
            System.Array.Copy(CaptainSkinWhite.rendererInfos, CaptainPinkRenderInfos, 7);

            CharacterModel.RendererInfo[] CaptainRenderInfosRED = new CharacterModel.RendererInfo[7];
            System.Array.Copy(CaptainSkinWhite.rendererInfos, CaptainRenderInfosRED, 7);

            Material RedMatCaptainAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material RedMatCaptainAlt2 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            //Material RedMatCaptainAlt3 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material RedMatCaptainArmorAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
            Material RedMatCaptainJacketAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
            Material RedMatCaptainRobotBitsAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);

            RedMatCaptainAlt.mainTexture = texCaptainPaletteRED2;
            RedMatCaptainAlt2.mainTexture = texCaptainPaletteRED2;
            //RedMatCaptainAlt3.mainTexture = texCaptainPaletteRED3;
            RedMatCaptainArmorAlt.color = new Color(0.5f, 0.5f, 0.5f);
            RedMatCaptainJacketAlt.mainTexture = texCaptainJacketDiffuseRED;

            RedMatCaptainAlt.color = new Color(0.8f, 0.8f, 0.8f);
            RedMatCaptainJacketAlt.color = new Color(0.8f, 0.7f, 0.7f);

            RedMatCaptainRobotBitsAlt.color = new Color(0.65f, 0.65f, 0.65f);
            RedMatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1.3f, 0.6f, 0.4f, 1f));

            //
            CaptainRenderInfosRED[0].defaultMaterial = RedMatCaptainAlt; //matCaptainAlt
            CaptainRenderInfosRED[1].defaultMaterial = RedMatCaptainAlt2; //matCaptainAlt
            CaptainRenderInfosRED[2].defaultMaterial = RedMatCaptainArmorAlt; //matCaptainArmorAlt
            CaptainRenderInfosRED[3].defaultMaterial = RedMatCaptainJacketAlt; //matCaptainJacketAlt
            CaptainRenderInfosRED[4].defaultMaterial = RedMatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainRenderInfosRED[5].defaultMaterial = RedMatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainRenderInfosRED[6].defaultMaterial = RedMatCaptainAlt2; //matCaptainAlt //Skirt
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_CAPTAIN_RED", "Occasion");

            Texture2D skinIconCaptainRED = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinIconCaptainRED.LoadImage(Properties.Resources.skinIconCaptainRED, true);
            skinIconCaptainRED.filterMode = FilterMode.Bilinear;
            Sprite skinIconCaptainREDS = Sprite.Create(skinIconCaptainRED, WRect.rec128, WRect.half);

            R2API.SkinDefInfo SkinInfo2 = new R2API.SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN_RED",
                UnlockableDef = unlockableDef,
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainRenderInfosRED,
                Name = "skinCaptainWolfo_Red",
                Icon = skinIconCaptainREDS,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }

        [RegisterAchievement("SIMU_SKIN_Captain", "Skins.Captain.Wolfo", "CompleteMainEnding", null)]
        public class Bandit2ClearGameMonsoonAchievement : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CaptainBody");
            }
        }

        internal static void PrismAchievement()
        {
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_CAPTAIN_NAME", "Captain" + Unlocks.unlockNamePrism);
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_CAPTAIN_DESCRIPTION", "As Captain" + Unlocks.unlockConditionPrism);
            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CAPTAIN_NAME";
            unlockableDef.cachedName = "Skins.Captain.Wolfo.Prism";       
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
        }

        [RegisterAchievement("PRISM_SKIN_CAPTAIN", "Skins.Captain.Wolfo.Prism", null, null)]
        public class AchievementPrismaticDissoCaptain2Body : AchievementPrismaticDisso
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CaptainBody");
            }
        }
    }
}