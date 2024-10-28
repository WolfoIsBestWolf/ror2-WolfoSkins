using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsToolbot_MULT
    {
        internal static void Start()
        {
            ToolbotSkin();
            Toolbot_AltColossus();
        }

        internal static void Toolbot_AltColossus()
        {
            SkinDef skinToolbotAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotAltColossus.asset").WaitForCompletion();
            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinToolbotAltColossus.rendererInfos.Length];
            System.Array.Copy(skinToolbotAltColossus.rendererInfos, NewRenderInfos, skinToolbotAltColossus.rendererInfos.Length);
            
            Color UtilitySymbol = new Color(0.9855168f, 0.48584908f, 1f, 1f);

            Material matToolbot = Object.Instantiate(skinToolbotAltColossus.rendererInfos[0].defaultMaterial);

            Texture2D texToolbotNew = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotAltColossus_U.png");
            texToolbotNew.wrapMode = TextureWrapMode.Clamp;
    
            Texture2D texToolbotAltColossusFresnel = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotAltColossusFresnel.png");
            texToolbotAltColossusFresnel.wrapMode = TextureWrapMode.Clamp;

            Texture2D texToolbotCategoryEmissionColossus = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotCategoryEmissionColossus.png");
            texToolbotCategoryEmissionColossus.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampCrocoDiseaseDark = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texRampCrocoDiseaseDark.png");
            texRampCrocoDiseaseDark.wrapMode = TextureWrapMode.Clamp;

            matToolbot.mainTexture = texToolbotNew;
            matToolbot.SetTexture("_BlueChannelTex", texToolbotNew);
            matToolbot.SetTexture("_FresnelMask", texToolbotAltColossusFresnel);
            matToolbot.SetTexture("_FresnelRamp", texRampCrocoDiseaseDark);
            matToolbot.SetTexture("_EmTex", texToolbotCategoryEmissionColossus);
            matToolbot.SetColor("_EmColor", UtilitySymbol);
            matToolbot.SetFloat("_EmPower", 0.55f);

            NewRenderInfos[0].defaultMaterial = matToolbot;     //MatToolbot

            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinToolbotAltColossusWolfo_AltBoss",
                NameToken = "SIMU_SKIN_TOOLBOT3",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/skinIconToolbot_U.png")),
                BaseSkins = skinToolbotAltColossus.baseSkins,
                MeshReplacements = skinToolbotAltColossus.meshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinToolbotAltColossus.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ToolbotBody"), SkinInfo);
        }

        internal static void ToolbotSkin()
        {
            SkinDef skinToolbotDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotDefault.asset").WaitForCompletion();
            SkinDef skinToolbotAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinToolbotDefault.rendererInfos.Length];
            System.Array.Copy(skinToolbotDefault.rendererInfos, NewRenderInfos, skinToolbotDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfos2 = new CharacterModel.RendererInfo[skinToolbotDefault.rendererInfos.Length];
            System.Array.Copy(skinToolbotDefault.rendererInfos, NewRenderInfos2, skinToolbotDefault.rendererInfos.Length);

            Material matToolbot = Object.Instantiate(skinToolbotDefault.rendererInfos[1].defaultMaterial);
            Material matToolbot2 = Object.Instantiate(skinToolbotDefault.rendererInfos[1].defaultMaterial);

            Texture2D texToolbotNew = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotNew_D.png");
            texToolbotNew.wrapMode = TextureWrapMode.Clamp;

            Texture2D texToolbotNew2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotNew_H.png");
            texToolbotNew2.wrapMode = TextureWrapMode.Clamp;

            Texture2D texToolbotAltColossusFresnel = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotCategoryEmission.png");
            texToolbotAltColossusFresnel.wrapMode = TextureWrapMode.Clamp;

            matToolbot.mainTexture = texToolbotNew;
            matToolbot2.mainTexture = texToolbotNew2;

           


            Color DamageSymbol = new Color(1f, 0.36953196f, 0.3160377f, 1f);
            Color HealingSymbol = new Color(0.72542995f, 1f, 0.2971698f, 1f);
            //Color UtilitySymbol = new Color(0.9855168f, 0.48584908f, 1f, 1f);

            matToolbot.SetTexture("_EmTex", texToolbotAltColossusFresnel);
            matToolbot2.SetTexture("_EmTex", texToolbotAltColossusFresnel);
            matToolbot.SetColor("_EmColor", DamageSymbol);
            matToolbot2.SetColor("_EmColor", HealingSymbol);
            matToolbot.SetFloat("_EmPower", 0.55f);
            matToolbot2.SetFloat("_EmPower", 0.55f);

            //NewRenderInfos[0].defaultMaterial = ;     //MatRebar 
            NewRenderInfos[1].defaultMaterial = matToolbot;     //MatToolbot
            NewRenderInfos2[1].defaultMaterial = matToolbot2;     //MatToolbot

            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinToolbotWolfo_Simu",
                NameToken = "SIMU_SKIN_TOOLBOT",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/skinIconToolbot_D.png")),
                BaseSkins = skinToolbotAlt.baseSkins,
                MeshReplacements = skinToolbotDefault.meshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinToolbotAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ToolbotBody"), SkinInfo);
            //
            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                Name = "skinToolbotWolfo2_Simu",
                NameToken = "SIMU_SKIN_TOOLBOT2",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/skinIconToolbot_H.png")),
                BaseSkins = skinToolbotAlt.baseSkins,
                MeshReplacements = skinToolbotAlt.meshReplacements,
                RendererInfos = NewRenderInfos2,
                RootObject = skinToolbotAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ToolbotBody"), SkinInfo2);
        }


        [RegisterAchievement("CLEAR_ANY_TOOLBOT", "Skins.Toolbot.Wolfo.First", "RepeatFirstTeleporter", 5, null)]
        public class ClearSimulacrumToolbotBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ToolbotBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_TOOLBOT", "Skins.Toolbot.Wolfo.Both", "RepeatFirstTeleporter", 5, null)]
        public class ClearSimulacrumToolbotBody2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ToolbotBody");
            }
        }

    }
}