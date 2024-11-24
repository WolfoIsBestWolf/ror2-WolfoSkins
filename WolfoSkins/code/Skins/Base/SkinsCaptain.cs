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
            Captain_Pink();
            Captain_Blue();
            //CaptainSkinRED();
            AltColossus();
            //Colossus_Orange();
        }

        internal static void AltColossus()
        {
            SkinDef skinCaptainAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Captain/skinCaptainAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] renderInfo = new CharacterModel.RendererInfo[7];
            System.Array.Copy(skinCaptainAltColossus.rendererInfos, renderInfo, 7);

            //0 matCaptainColossusAltClothes
            //1 matCaptainColossusAltArmor
            //2 matCaptainColossusAltArmor
            //3 matCaptainColossusAltClothes
            //4 matCaptainColossusAltArmor
            //5 matCaptainColossusAltArmor
            //6 matCaptainColossusAltClothes

            Texture2D texCaptainColossusClothesDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/texCaptainColossusClothesDiffuse.png");
            texCaptainColossusClothesDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/texCaptainColossusDiffuse.png");
            texCaptainColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainColossusDiffuseHat = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/texCaptainColossusDiffuseHat.png");
            texCaptainColossusDiffuseHat.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainColossusFresnelMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/texCaptainColossusFresnelMask.png");
            texCaptainColossusFresnelMask.wrapMode = TextureWrapMode.Clamp;


            //Just edit the base game one it looks like shit
            skinCaptainAltColossus.rendererInfos[1].defaultMaterial.SetTexture("_NormalTex", null);
            skinCaptainAltColossus.rendererInfos[1].defaultMaterial.SetTexture("_GreenChannelNormalTex", null); //texTrimSheetLemurianRuins

            Material matCaptainColossusAltClothes = Object.Instantiate(skinCaptainAltColossus.rendererInfos[0].defaultMaterial);
            Material matCaptainColossusAltArmor = Object.Instantiate(skinCaptainAltColossus.rendererInfos[1].defaultMaterial);
         
            matCaptainColossusAltClothes.mainTexture = texCaptainColossusClothesDiffuse;
            matCaptainColossusAltArmor.mainTexture = texCaptainColossusDiffuse;
            matCaptainColossusAltArmor.SetTexture("_NormalTex", null); //texTrimSheetLemurianRuins
            matCaptainColossusAltArmor.SetTexture("_GreenChannelTex", null); //texTrimSheetLemurianRuins
            matCaptainColossusAltArmor.SetTexture("_GreenChannelNormalTex", null); //texTrimSheetLemurianRuins

            matCaptainColossusAltArmor.SetColor("_EmColor", new Color(1.5f,0.45f,0.3f,1f));
            Material matCaptainColossusAltArmorHat = Object.Instantiate(matCaptainColossusAltArmor);


            //matCaptainColossusAltArmor.SetTexture("_FresnelMask", texCaptainColossusFresnelMask);
            matCaptainColossusAltArmorHat.mainTexture = texCaptainColossusDiffuseHat;

            //
            renderInfo[0].defaultMaterial = matCaptainColossusAltClothes; //
            renderInfo[1].defaultMaterial = matCaptainColossusAltArmorHat; //Hat
            renderInfo[2].defaultMaterial = matCaptainColossusAltArmor; //
            renderInfo[3].defaultMaterial = matCaptainColossusAltClothes; //
            renderInfo[4].defaultMaterial = matCaptainColossusAltArmor; //
            renderInfo[5].defaultMaterial = matCaptainColossusAltArmor; //
            renderInfo[6].defaultMaterial = matCaptainColossusAltClothes; //
            //

            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                NameToken = "SIMU_SKIN_CAPTAIN_COLOSSUS",
                Name = "skinCaptainAltColossusWolfo_AltBoss",
                BaseSkins = skinCaptainAltColossus.baseSkins,  
                RootObject = skinCaptainAltColossus.rootObject,
                RendererInfos = renderInfo,
                MeshReplacements = skinCaptainAltColossus.meshReplacements,
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/Captain.png")),
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }


        internal static void Colossus_Orange()
        {
            SkinDef skinCaptainAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Captain/skinCaptainAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] renderInfo = new CharacterModel.RendererInfo[7];
            System.Array.Copy(skinCaptainAltColossus.rendererInfos, renderInfo, 7);

            //0 matCaptainColossusAltClothes
            //1 matCaptainColossusAltArmor
            //2 matCaptainColossusAltArmor
            //3 matCaptainColossusAltClothes
            //4 matCaptainColossusAltArmor
            //5 matCaptainColossusAltArmor
            //6 matCaptainColossusAltClothes

            Texture2D texCaptainColossusClothesDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/ColossusOrange/texCaptainColossusClothesDiffuse.png");
            texCaptainColossusClothesDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/ColossusOrange/texCaptainColossusDiffuse.png");
            texCaptainColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainColossusDiffuseHat = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/ColossusOrange/texCaptainColossusDiffuseHat.png");
            texCaptainColossusDiffuseHat.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainColossusFresnelMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/ColossusOrange/texCaptainColossusFresnelMask.png");
            texCaptainColossusFresnelMask.wrapMode = TextureWrapMode.Clamp;


            //Just edit the base game one it looks like shit
            skinCaptainAltColossus.rendererInfos[1].defaultMaterial.SetTexture("_NormalTex", null);
            skinCaptainAltColossus.rendererInfos[1].defaultMaterial.SetTexture("_GreenChannelNormalTex", null); //texTrimSheetLemurianRuins

            Material matCaptainColossusAltClothes = Object.Instantiate(skinCaptainAltColossus.rendererInfos[0].defaultMaterial);
            Material matCaptainColossusAltArmor = Object.Instantiate(skinCaptainAltColossus.rendererInfos[1].defaultMaterial);

            matCaptainColossusAltClothes.mainTexture = texCaptainColossusClothesDiffuse;
            matCaptainColossusAltArmor.mainTexture = texCaptainColossusDiffuse;
            matCaptainColossusAltArmor.SetTexture("_NormalTex", null); //texTrimSheetLemurianRuins
            matCaptainColossusAltArmor.SetTexture("_GreenChannelTex", null); //texTrimSheetLemurianRuins
            matCaptainColossusAltArmor.SetTexture("_GreenChannelNormalTex", null); //texTrimSheetLemurianRuins

            matCaptainColossusAltArmor.SetColor("_EmColor", new Color(1f, 1f, 0.2f, 1f));
            Material matCaptainColossusAltArmorHat = Object.Instantiate(matCaptainColossusAltArmor);


            //matCaptainColossusAltArmor.SetTexture("_FresnelMask", texCaptainColossusFresnelMask);
            matCaptainColossusAltArmorHat.mainTexture = texCaptainColossusDiffuseHat;

            //
            renderInfo[0].defaultMaterial = matCaptainColossusAltClothes; //
            renderInfo[1].defaultMaterial = matCaptainColossusAltArmorHat; //Hat
            renderInfo[2].defaultMaterial = matCaptainColossusAltArmor; //
            renderInfo[3].defaultMaterial = matCaptainColossusAltClothes; //
            renderInfo[4].defaultMaterial = matCaptainColossusAltArmor; //
            renderInfo[5].defaultMaterial = matCaptainColossusAltArmor; //
            renderInfo[6].defaultMaterial = matCaptainColossusAltClothes; //
            //

            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                NameToken = "SIMU_SKIN_CAPTAIN_COLOSSUS",
                Name = "skinCaptainAltColossusWolfo_AltBoss2",
                BaseSkins = skinCaptainAltColossus.baseSkins,
                RootObject = skinCaptainAltColossus.rootObject,
                RendererInfos = renderInfo,
                MeshReplacements = skinCaptainAltColossus.meshReplacements,
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/ColossusOrange/Captain.png")),
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }

        internal static void Captain_Pink()
        {
            //Pink stuff test
            //SkinDef CaptainSkinDefault = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef CaptainSkinWhite = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D PinktexCaptainJacketDiffuseW = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainJacketDiffuseW.png");
            PinktexCaptainJacketDiffuseW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainPaletteW.png");
            PinktexCaptainPaletteW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainPaletteW2.png");
            PinktexCaptainPaletteW2.wrapMode = TextureWrapMode.Clamp;

            //Pallete for HAT
            Texture2D PinktexCaptainPaletteW3 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainPaletteW3.png");
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
            SkinDefInfo CaptainPinkSkinInfos = new SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN",
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainPinkRenderInfos,
                Name = "skinCaptainWolfo_Simu",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/texCaptainPinkSkinIcon.png")),
            };
            //

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), CaptainPinkSkinInfos);
        }

        internal static void Captain_Blue()
        {
            SkinDef CaptainSkinDefault = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef CaptainSkinWhite = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D texCaptainJacketDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Blue/texCaptainJacketDiffuseAltBLUE.png");
            texCaptainJacketDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainPalette = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Blue/texCaptainPaletteAltBLUE.png");
            texCaptainPalette.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainPalette2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Blue/texCaptainPaletteAltBLUE2.png");
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
            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN_BLUE",
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainRenderInfos,
                Name = "skinCaptainWolfo_Blue_Simu",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Blue/texSkinSwatches_25BLUE.png")),
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }


        #region Old Red
        /*
        internal static void CaptainSkinRED()
        {
            SkinDef CaptainSkinDefault = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef CaptainSkinWhite = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

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

            Texture2D skinIconCaptainRED = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinIconCaptainRED.LoadImage(Properties.Resources.skinIconCaptainRED, true);
            skinIconCaptainRED.filterMode = FilterMode.Bilinear;
            Sprite skinIconCaptainREDS = Sprite.Create(skinIconCaptainRED, WRect.rec128, WRect.half);

            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN_RED",
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainRenderInfosRED,
                Name = "skinCaptainWolfo_Red_Simu",
                Icon = skinIconCaptainREDS,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_CAPTAIN", "Skins.Captain.Wolfo.First", "CompleteMainEnding", 5, null)]
        public class Bandit2ClearGameMonsoonAchievement : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CaptainBody");
            }
        }
        [RegisterAchievement("CLEAR_BOTH_CAPTAIN", "Skins.Captain.Wolfo.Both", "CompleteMainEnding", 5, null)]
        public class Bandit2ClearGameMonsoonAchievement2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CaptainBody");
            }
        }

    }
}