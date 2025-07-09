using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsCaptain
    {
        internal static void Start()
        {
            //SkinDef CaptainSkinDefault = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef skinCaptainAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Captain/skinCaptainAlt.asset").WaitForCompletion();
            SkinDef skinCaptainAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Captain/skinCaptainAltColossus.asset").WaitForCompletion();

            Captain_Pink(skinCaptainAlt);
            Captain_Blue(skinCaptainAlt);
            //CaptainSkinRED();
            AltColossus(skinCaptainAltColossus);
            //Colossus_Orange();

            //0 matCaptainColossusAltClothes
            //1 matCaptainColossusAltArmor
            //2 matCaptainColossusAltArmor
            //3 matCaptainColossusAltClothes
            //4 matCaptainColossusAltArmor
            //5 matCaptainColossusAltArmor
            //6 matCaptainColossusAltClothes
        }

        internal static void AltColossus(SkinDef skinCaptainAltColossus)
        {
            CharacterModel.RendererInfo[] renderInfo = H.CreateNewSkinR(new SkinInfo
            {
                nameToken = "SIMU_SKIN_CAPTAIN_COLOSSUS",
                name = "skinCaptainAltColossus_DLC2",
                icon = H.GetIcon("captain_dlc2"),
                original = skinCaptainAltColossus,
                unsetMat = true
            });


            Material matCaptainColossusAltClothes = H.CloneMat(renderInfo, 0);
            Material matCaptainColossusAltArmor = H.CloneMat(renderInfo, 1);

            matCaptainColossusAltClothes.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/texCaptainColossusClothesDiffuse.png");
            matCaptainColossusAltArmor.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/texCaptainColossusDiffuse.png");
            matCaptainColossusAltArmor.SetTexture("_NormalTex", null); //texTrimSheetLemurianRuins
            matCaptainColossusAltArmor.SetTexture("_GreenChannelTex", null); //texTrimSheetLemurianRuins
            matCaptainColossusAltArmor.SetTexture("_GreenChannelNormalTex", null); //texTrimSheetLemurianRuins

            matCaptainColossusAltArmor.SetColor("_EmColor", new Color(1.5f, 0.45f, 0.3f, 1f));
            Material matCaptainColossusAltArmorHat = Object.Instantiate(matCaptainColossusAltArmor);


            //matCaptainColossusAltArmor.SetTexture("_FresnelMask", texCaptainColossusFresnelMask);
            matCaptainColossusAltArmorHat.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Colossus/texCaptainColossusDiffuseHat.png");


            renderInfo[0].defaultMaterial = matCaptainColossusAltClothes;
            renderInfo[1].defaultMaterial = matCaptainColossusAltArmorHat;
            renderInfo[2].defaultMaterial = matCaptainColossusAltArmor;
            renderInfo[3].defaultMaterial = matCaptainColossusAltClothes;
            renderInfo[4].defaultMaterial = matCaptainColossusAltArmor;
            renderInfo[5].defaultMaterial = matCaptainColossusAltArmor;
            renderInfo[6].defaultMaterial = matCaptainColossusAltClothes;

        }
        #region Orange Idea
        /*
                internal static void Colossus_Orange(SkinDef skinCaptainAltColossus)
                {

                    CharacterModel.RendererInfo[] renderInfo = new CharacterModel.RendererInfo[skinCaptainAltColossus.rendererInfos.Length];
                    System.Array.Copy(skinCaptainAltColossus.rendererInfos, renderInfo, skinCaptainAltColossus.rendererInfos.Length);

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

                    Material matCaptainColossusAltClothes = CloneMat(CaptainAltColossus.rendererInfos[0].defaultMaterial);
                    Material matCaptainColossusAltArmor = CloneMat(CaptainAltColossus.rendererInfos[1].defaultMaterial);

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
                        Name = "skinCaptainAltColossus_AltBoss2",
                        BaseSkins = skinCaptainAltColossus.baseSkins,
                        RootObject = skinCaptainAltColossus.rootObject,
                        RendererInfos = renderInfo,
                        MeshReplacements = skinCaptainAltColossus.meshReplacements,
                        Icon = Help.GetIcon("captain_orange2"),
                    };
                    Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
                }
                */
        #endregion

        internal static void Captain_Pink(SkinDef skinCaptainAlt)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                nameToken = "SIMU_SKIN_CAPTAIN",
                name = "skinCaptain_1",
                icon = H.GetIcon("captain_pink"),
                original = skinCaptainAlt,
                unsetMat = true
            });

            Material PinkmatCaptainAlt = CloneMat(newRenderInfos, 0);
            Material PinkmatCaptainAlt2 = CloneMat(newRenderInfos, 0);
            Material PinkmatCaptainAlt3 = CloneMat(newRenderInfos, 0);
            Material PinkmatCaptainArmorAlt = CloneMat(newRenderInfos, 2);
            Material PinkmatCaptainJacketAlt = CloneMat(newRenderInfos, 3);
            Material PinkmatCaptainRobotBitsAlt = CloneMat(newRenderInfos, 4);

            PinkmatCaptainAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainPaletteW.png");
            PinkmatCaptainAlt2.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainPaletteW2.png");
            PinkmatCaptainAlt3.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainPaletteW3.png");
            //PinkmatCaptainArmorAlt.color = new Color32(255, 223, 188, 255);
            PinkmatCaptainArmorAlt.color = new Color32(255, 190, 135, 255);//(255, 195, 150, 255);
            //_EmColor is juts fucking weird
            PinkmatCaptainJacketAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Pink/PinktexCaptainJacketDiffuseW.png");
            PinkmatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1.2f, 0.6f, 1.2f, 1f));
            PinkmatCaptainRobotBitsAlt.color = new Color32(255, 190, 135, 255);

            newRenderInfos[0].defaultMaterial = PinkmatCaptainAlt; //matCaptainAlt
            newRenderInfos[1].defaultMaterial = PinkmatCaptainAlt3; //matCaptainAlt //Hat
            newRenderInfos[2].defaultMaterial = PinkmatCaptainArmorAlt; //matCaptainArmorAlt
            newRenderInfos[3].defaultMaterial = PinkmatCaptainJacketAlt; //matCaptainJacketAlt
            newRenderInfos[4].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            newRenderInfos[5].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            newRenderInfos[6].defaultMaterial = PinkmatCaptainAlt2; //matCaptainAlt //Skirt

        }

        internal static void Captain_Blue(SkinDef skinCaptainAlt)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                nameToken = "SIMU_SKIN_CAPTAIN_BLUE",
                name = "skinCaptain_Blue_1",
                icon = H.GetIcon("captain_blue"),
                original = skinCaptainAlt,
                unsetMat = true
            });

            Material MatCaptainAlt = CloneMat(newRenderInfos, 0);
            Material MatCaptainAltDark = CloneMat(newRenderInfos, 0);
            Material MatCaptainArmorAlt = CloneMat(newRenderInfos, 2);
            Material MatCaptainJacketAlt = CloneMat(newRenderInfos, 3);
            Material MatCaptainRobotBitsAlt = CloneMat(newRenderInfos, 4);

            MatCaptainAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Blue/texCaptainPaletteAltBLUE.png");
            MatCaptainAltDark.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Blue/texCaptainPaletteAltBLUE2.png");
            MatCaptainAltDark.color = new Color(0.8f, 0.8f, 0.8f);
            MatCaptainArmorAlt.color = new Color32(255, 225, 135, 255);//(255, 195, 150, 255);
            MatCaptainJacketAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Captain/Blue/texCaptainJacketDiffuseAltBLUE.png");

            MatCaptainRobotBitsAlt.color = new Color32(255, 225, 135, 255);
            MatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1f, 1f, 0.3f, 1f));


            newRenderInfos[0].defaultMaterial = MatCaptainAlt; //matCaptainAlt
            newRenderInfos[1].defaultMaterial = MatCaptainAlt; //matCaptainAlt
            newRenderInfos[2].defaultMaterial = MatCaptainArmorAlt; //matCaptainArmorAlt
            newRenderInfos[3].defaultMaterial = MatCaptainJacketAlt; //matCaptainJacketAlt
            newRenderInfos[4].defaultMaterial = MatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            newRenderInfos[5].defaultMaterial = MatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            newRenderInfos[6].defaultMaterial = MatCaptainAltDark; //matCaptainAlt //Skirt

        }


        #region Old Red
        /*
        internal static void CaptainSkinRED()
        {
            SkinDef CaptainSkinDefault = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef skinCaptainAlt = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D texCaptainJacketDiffuseRED = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texCaptainJacketDiffuseRED.LoadImage(Properties.Resources.texCaptainJacketDiffuseRED, true);
            texCaptainJacketDiffuseRED.filterMode = FilterMode.Bilinear;
            texCaptainJacketDiffuseRED.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainPaletteRED2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texCaptainPaletteRED2.LoadImage(Properties.Resources.texCaptainPaletteRED2, true);
            texCaptainPaletteRED2.filterMode = FilterMode.Bilinear;
            texCaptainPaletteRED2.wrapMode = TextureWrapMode.Clamp;

            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[7];
            System.Array.Copy(skinCaptainAlt.rendererInfos, newRenderInfos, 7);

            CharacterModel.RendererInfo[] newRenderInfosRED = new CharacterModel.RendererInfo[7];
            System.Array.Copy(skinCaptainAlt.rendererInfos, newRenderInfosRED, 7);

            Material RedMatCaptainAlt = CloneMat(CaptainAlt.rendererInfos[0].defaultMaterial);
            Material RedMatCaptainAlt2 = CloneMat(CaptainAlt.rendererInfos[0].defaultMaterial);
            //Material RedMatCaptainAlt3 = CloneMat(CaptainAlt.rendererInfos[0].defaultMaterial);
            Material RedMatCaptainArmorAlt = CloneMat(CaptainAlt.rendererInfos[2].defaultMaterial);
            Material RedMatCaptainJacketAlt = CloneMat(CaptainAlt.rendererInfos[3].defaultMaterial);
            Material RedMatCaptainRobotBitsAlt = CloneMat(CaptainAlt.rendererInfos[4].defaultMaterial);

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
            newRenderInfosRED[0].defaultMaterial = RedMatCaptainAlt; //matCaptainAlt
            newRenderInfosRED[1].defaultMaterial = RedMatCaptainAlt2; //matCaptainAlt
            newRenderInfosRED[2].defaultMaterial = RedMatCaptainArmorAlt; //matCaptainArmorAlt
            newRenderInfosRED[3].defaultMaterial = RedMatCaptainJacketAlt; //matCaptainJacketAlt
            newRenderInfosRED[4].defaultMaterial = RedMatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            newRenderInfosRED[5].defaultMaterial = RedMatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            newRenderInfosRED[6].defaultMaterial = RedMatCaptainAlt2; //matCaptainAlt //Skirt
            //

            Texture2D skinIconCaptainRED = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinIconCaptainRED.LoadImage(Properties.Resources.skinIconCaptainRED, true);
            skinIconCaptainRED.filterMode = FilterMode.Bilinear;
            Sprite skinIconCaptainREDS = Sprite.Create(skinIconCaptainRED, WRect.rec128, WRect.half);

            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                BaseSkins = skinCaptainAlt.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN_RED",
                RootObject = skinCaptainAlt.rootObject,
                RendererInfos = newRenderInfosRED,
                Name = "skinCaptain_Red_1",
                Icon = skinIconCaptainREDS,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_CAPTAIN", "Skins.Captain.Wolfo.First", "CompleteMainEnding", 5, null)]
        public class Bandit2ClearGameMonsoonAchievement : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CaptainBody");
            }
        }
        [RegisterAchievement("CLEAR_BOTH_CAPTAIN", "Skins.Captain.Wolfo.Both", "CompleteMainEnding", 5, null)]
        public class Bandit2ClearGameMonsoonAchievement2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CaptainBody");
            }
        }

    }
}