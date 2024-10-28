using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsMerc
    {
        public static SkinDef red_SKIN;
        public static SkinDef green_SKIN;

        internal static void Start()
        {
            Merc_Alt();
            Merc_MasteryAlt();
            Merc_AltColossus();
        }

        internal static void Merc_MasteryAlt()
        {
            SkinDef skinMercAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfosRED = new CharacterModel.RendererInfo[skinMercAlt.rendererInfos.Length];
            System.Array.Copy(skinMercAlt.rendererInfos, NewRenderInfosRED, skinMercAlt.rendererInfos.Length);

            Material matMercRED = Object.Instantiate(skinMercAlt.rendererInfos[0].defaultMaterial);
            Material matMercSwordRED = Object.Instantiate(skinMercAlt.rendererInfos[1].defaultMaterial);


            Texture2D texMercDiffuseRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texMercDiffuseRed.png");
            texMercDiffuseRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercEmissionRED = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texMercEmissionRED.png");
            texMercEmissionRED.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercSwordDiffuseRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texMercSwordDiffuseRed.png");
            texMercSwordDiffuseRed.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampFallbootsRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texRampFallbootsRed.png");
            texRampFallbootsRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/texRampHuntressRed.png");
            texRampHuntressRed.filterMode = FilterMode.Point;
            texRampHuntressRed.wrapMode = TextureWrapMode.Clamp;

            matMercRED.mainTexture = texMercDiffuseRed;
            matMercRED.SetTexture("_EmTex", texMercEmissionRED);
            matMercRED.SetColor("_EmColor", new Color(1, 0, 0));

            matMercSwordRED.mainTexture = texMercSwordDiffuseRed;
            matMercSwordRED.SetColor("_EmColor", new Color(1f, -1f, -1f));
            matMercSwordRED.SetTexture("_FlowHeightRamp", texRampFallbootsRed);
            matMercSwordRED.SetTexture("_FresnelRamp", texRampHuntressRed);

            NewRenderInfosRED[0].defaultMaterial = matMercRED;
            NewRenderInfosRED[1].defaultMaterial = matMercSwordRED;


            SkinDefWolfo newSkinDefRED = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefRED.name = "skinMercWolfo_Simu_Red";
            newSkinDefRED.nameToken = "SIMU_SKIN_MERC2";
            newSkinDefRED.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Red/skinIconMercRed.png"));
            newSkinDefRED.baseSkins = skinMercAlt.baseSkins;
            newSkinDefRED.meshReplacements = skinMercAlt.meshReplacements;
            newSkinDefRED.rendererInfos = NewRenderInfosRED;
            newSkinDefRED.rootObject = skinMercAlt.rootObject;
            newSkinDefRED.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
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

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MercBody"), newSkinDefRED);
            red_SKIN = newSkinDefRED;
        }


        internal static void Merc_Alt()
        {
            SkinDef skinMercDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercDefault.asset").WaitForCompletion();
           
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMercDefault.rendererInfos.Length];
            System.Array.Copy(skinMercDefault.rendererInfos, NewRenderInfos, skinMercDefault.rendererInfos.Length);

            Material matMerc = Object.Instantiate(skinMercDefault.rendererInfos[0].defaultMaterial);
            Material matMercSword = Object.Instantiate(skinMercDefault.rendererInfos[1].defaultMaterial);
          

            Texture2D texMercDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Blue/texMercDiffuse.png");
            texMercDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Blue/texMercEmission.png");
            texMercEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercSwordDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Blue/texMercSwordDiffuse.png");
            texMercSwordDiffuse.wrapMode = TextureWrapMode.Repeat;

            matMerc.mainTexture = texMercDiffuse;
            matMerc.SetTexture("_EmTex", texMercEmission);
            matMerc.SetColor("_EmColor", new Color(0,0.8f,1));

            matMercSword.mainTexture = texMercSwordDiffuse;
            matMercSword.SetColor("_EmColor", new Color(0f, 0.3f, 1));

            NewRenderInfos[0].defaultMaterial = matMerc;
            NewRenderInfos[1].defaultMaterial = matMercSword;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinMercWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_MERC";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Blue/skinIconMerc.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinMercDefault };
            newSkinDef.meshReplacements = skinMercDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinMercDefault.rootObject;
            //newSkinDef.lightColorsChanges = null;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MercBody"), newSkinDef);
        }

        internal static void Merc_AltColossus()
        {
            SkinDef skinMercDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercDefault.asset").WaitForCompletion();
            SkinDef skinMercAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercAltColossus.asset").WaitForCompletion();
           
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMercAltColossus.rendererInfos.Length];
            System.Array.Copy(skinMercAltColossus.rendererInfos, NewRenderInfos, skinMercAltColossus.rendererInfos.Length);

            Material matMercAltColossus = Object.Instantiate(skinMercAltColossus.rendererInfos[0].defaultMaterial);
            Material matMercSword = Object.Instantiate(skinMercAltColossus.rendererInfos[1].defaultMaterial);

            Texture2D texMercAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texMercAltColossusDiffuse.png");
            texMercAltColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercAltColossusDiffuseSword = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texMercAltColossusDiffuse.png");
            texMercAltColossusDiffuseSword.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texMercAltColossusEmiMask.png");
            texMercEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercSwordDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texMercAltColossusDiffuse.png");
            texMercSwordDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampFallbootsRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texRampFallbootsGreen.png");
            texRampFallbootsRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressRed = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/texRampHuntressGreen.png");
            texRampHuntressRed.filterMode = FilterMode.Point;
            texRampHuntressRed.wrapMode = TextureWrapMode.Clamp;
             


            matMercAltColossus.mainTexture = texMercAltColossusDiffuse;
            //matMercAltColossus.color = new Color(0.9f,0.9f,0.9f, 1);
            matMercAltColossus.SetTexture("_EmTex", texMercEmission);
            matMercAltColossus.SetTexture("_FlowHeightmap", null);
            matMercAltColossus.SetTexture("_FlowHeightRamp", null);
            matMercAltColossus.DisableKeyword("FLOWMAP");
            /*matMercAltColossus.SetTexture("_FresnelMask", null);
            matMercAltColossus.SetTexture("_FresnelRamp", null);*/
            matMercAltColossus.SetFloat("_EmPower", 1);
            matMercAltColossus.SetColor("_EmColor", new Color(0.2f, 0.7f, 0.2f));

            matMercSword.mainTexture = texMercAltColossusDiffuseSword;
            matMercSword.color = new Color(0.8f, 1f, 0.8f, 1);
            matMercSword.SetTexture("_FlowHeightRamp", null);
            //matMercSword.SetTexture("_FlowHeightmap", null);
            matMercSword.SetTexture("_EmTex", matMercSword.GetTexture("_FlowHeightmap"));
            matMercSword.SetFloat("_EmPower", 1);
            matMercSword.SetColor("_EmColor", new Color(0.2f, 3f, 0.2f));


            NewRenderInfos[0].defaultMaterial = matMercAltColossus;
            NewRenderInfos[1].defaultMaterial = matMercSword;
            //
            SkinDefWolfo newSkinDefRED = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefRED.name = "skinMercWolfo_AltBoss_Green";
            newSkinDefRED.nameToken = "SIMU_SKIN_MERC_GREEN";
            newSkinDefRED.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Merc/Colossus/skinIconMerc_GREEN.png"));
            newSkinDefRED.baseSkins = skinMercAltColossus.baseSkins;
            //newSkinDefRED.meshReplacements = skinMercDefault.meshReplacements;
            newSkinDefRED.meshReplacements = skinMercAltColossus.meshReplacements;
            newSkinDefRED.rendererInfos = NewRenderInfos;
            newSkinDefRED.rootObject = skinMercDefault.rootObject;
            newSkinDefRED.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
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
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MercBody"), newSkinDefRED);
            green_SKIN = newSkinDefRED;
        }


        [RegisterAchievement("CLEAR_ANY_MERC", "Skins.Merc.Wolfo.First", "CompleteUnknownEnding", 5, null)]
        public class ClearSimulacrumMercBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MercBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_MERC", "Skins.Merc.Wolfo.Both", "CompleteUnknownEnding", 5, null)]
        public class ClearSimulacrumMercBody2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MercBody");
            }
        }
    }
}