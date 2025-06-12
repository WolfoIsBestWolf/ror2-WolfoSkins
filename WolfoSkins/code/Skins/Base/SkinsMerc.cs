using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod
{
    public class SkinsMerc
    {
        public static SkinDef red_SKIN;
        public static SkinDef green_SKIN;

        internal static void Start()
        {
            SkinDef skinMercDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercDefault.asset").WaitForCompletion();
            SkinDef skinMercAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercAlt.asset").WaitForCompletion();
            SkinDef skinMercAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercAltColossus.asset").WaitForCompletion();

            Merc_Alt(skinMercDefault);
            Merc_MasteryAlt(skinMercAlt);
            Merc_AltColossus(skinMercAltColossus);
        }

        internal static void Merc_MasteryAlt(SkinDef skinMercAlt)
        {
            SkinDefWolfo newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinMerc_Simu_Red",
                nameToken = "SIMU_SKIN_MERC2",
                icon = H.GetIcon("merc_red"),
                original = skinMercAlt,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matMerc = CloneMat(newRenderInfos, 0);
            Material matMercSword = CloneMat(newRenderInfos, 1);

            matMerc.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texMercDiffuseRed.png");
            matMerc.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texMercEmissionRED.png"));
            matMerc.SetColor("_EmColor", new Color(1, 0, 0));

            matMercSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texMercSwordDiffuseRed.png");
            matMercSword.SetColor("_EmColor", new Color(1f, -1f, -1f));
            matMercSword.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texRampFallbootsRed.png"));
            matMercSword.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texRampHuntressRed.png"));


            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1,0,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/Point Light (1)",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1,0,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/Point Light",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1,0,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/SwingCenter/SwordBase/Point Light",
                }
            };
            red_SKIN = newSkinDef;
        }


        internal static void Merc_Alt(SkinDef skinMercDefault)
        {
            SkinDefWolfo newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinMerc_1",
                nameToken = "SIMU_SKIN_MERC",
                icon = H.GetIcon("merc_blue"),
                original = skinMercDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matMerc = CloneMat(newRenderInfos, 0);
            Material matMercSword = CloneMat(newRenderInfos, 1);

            matMerc.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Blue/texMercDiffuse.png");
            matMerc.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Blue/texMercEmission.png"));
            matMerc.SetColor("_EmColor", new Color(0, 0.8f, 1));

            matMercSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Blue/texMercSwordDiffuse.png");
            matMercSword.SetColor("_EmColor", new Color(0f, 0.3f, 1));
        }

        internal static void Merc_AltColossus(SkinDef skinMercAltColossus)
        {
            SkinDefWolfo newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinMerc_DLC2_Green",
                nameToken = "SIMU_SKIN_MERC_GREEN",
                icon = H.GetIcon("merc_green"),
                original = skinMercAltColossus,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matMercAltColossus = CloneMat(newRenderInfos, 0);
            Material matMercSword = CloneMat(newRenderInfos, 1);

            matMercAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texMercAltColossusDiffuse.png");
            //matMercAltColossus.color = new Color(0.9f,0.9f,0.9f, 1);
            matMercAltColossus.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texMercAltColossusEmiMask.png"));
            matMercAltColossus.SetTexture("_FlowHeightmap", null);
            matMercAltColossus.SetTexture("_FlowHeightRamp", null);
            matMercAltColossus.DisableKeyword("FLOWMAP");
            /*matMercAltColossus.SetTexture("_FresnelMask", null);
            matMercAltColossus.SetTexture("_FresnelRamp", null);*/
            matMercAltColossus.SetFloat("_EmPower", 1);
            matMercAltColossus.SetColor("_EmColor", new Color(0.2f, 0.7f, 0.2f));

            matMercSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texMercAltColossusDiffuse.png");
            matMercSword.color = new Color(0.8f, 1f, 0.8f, 1);
            matMercSword.SetTexture("_FlowHeightRamp", null);
            //matMercSword.SetTexture("_FlowHeightmap", null);
            matMercSword.SetTexture("_EmTex", matMercSword.GetTexture("_FlowHeightmap"));
            matMercSword.SetFloat("_EmPower", 1);
            matMercSword.SetColor("_EmColor", new Color(0.2f, 3f, 0.2f));

            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0,1f,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/Point Light (1)",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0,0.5f,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/Point Light",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0,0.5f,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/SwingCenter/SwordBase/Point Light",
                }
            };
            green_SKIN = newSkinDef;
        }


        [RegisterAchievement("CLEAR_ANY_MERC", "Skins.Merc.Wolfo.First", "CompleteUnknownEnding", 5, null)]
        public class ClearSimulacrumMercBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MercBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_MERC", "Skins.Merc.Wolfo.Both", "CompleteUnknownEnding", 5, null)]
        public class ClearSimulacrumMercBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MercBody");
            }
        }
    }
}