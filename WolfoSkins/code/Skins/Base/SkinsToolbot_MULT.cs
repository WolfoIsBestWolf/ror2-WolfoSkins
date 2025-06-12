using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod
{
    public class SkinsToolbot_MULT
    {
        internal static void Start()
        {
            SkinDef skinToolbotDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotDefault.asset").WaitForCompletion();
            SkinDef skinToolbotAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotAlt.asset").WaitForCompletion();
            SkinDef skinToolbotAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotAltColossus.asset").WaitForCompletion();

            ToolbotSkin(skinToolbotDefault);
            ToolbotSkinAlt(skinToolbotAlt);
            Toolbot_AltColossus(skinToolbotAltColossus);
        }

        internal static void Toolbot_AltColossus(SkinDef skinToolbotAltColossus)
        {
            CharacterModel.RendererInfo[] newRenderInfos = CreateNewSkinR(new SkinInfo
            {
                name = "skinToolbotAltColossus_DLC2",
                nameToken = "SIMU_SKIN_TOOLBOT3",
                icon = H.GetIcon("mult_utility"),
                original = skinToolbotAltColossus,
                cloneMesh = false,
            });
            Color UtilitySymbol = new Color(0.9855168f, 0.48584908f, 1f, 1f);

            Material matToolbot = CloneMat(newRenderInfos, 0);
            Texture2D texToolbotNew = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotAltColossus_U.png");

            matToolbot.mainTexture = texToolbotNew;
            matToolbot.SetTexture("_BlueChannelTex", texToolbotNew);
            matToolbot.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotAltColossusFresnel.png"));
            matToolbot.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texRampCrocoDiseaseDark.png"));
            matToolbot.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotCategoryEmissionColossus.png"));
            matToolbot.SetColor("_EmColor", UtilitySymbol);
            matToolbot.SetFloat("_EmPower", 0.55f);
        }

        internal static void ToolbotSkin(SkinDef skinToolbotDefault)
        {
            CharacterModel.RendererInfo[] render_D = H.CreateNewSkinR(new SkinInfo
            {
                name = "skinToolbot_1",
                nameToken = "SIMU_SKIN_TOOLBOT",
                icon = H.GetIcon("mult_damage"),
                original = skinToolbotDefault,
                cloneMesh = false,
            });

            Material matToolbotD = CloneMat(render_D, 1);
            matToolbotD.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotNew_D.png");

            Color DamageSymbol = new Color(1f, 0.36953196f, 0.3160377f, 1f);
            matToolbotD.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotCategoryEmission.png"));
            matToolbotD.SetColor("_EmColor", DamageSymbol);
            matToolbotD.SetFloat("_EmPower", 0.55f);

        }

        internal static void ToolbotSkinAlt(SkinDef skinToolbotAlt)
        {
            CharacterModel.RendererInfo[] render_H = H.CreateNewSkinR(new SkinInfo
            {
                name = "skinToolbotWolfo2_1",
                nameToken = "SIMU_SKIN_TOOLBOT2",
                icon = H.GetIcon("mult_healing"),
                original = skinToolbotAlt,
                cloneMesh = false,
            });

            Material matToolbotH = CloneMat(render_H, 1);
            matToolbotH.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotNew_H.png");

            Color HealingSymbol = new Color(0.72542995f, 1f, 0.2971698f, 1f);
            matToolbotH.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotCategoryEmission.png"));
            matToolbotH.SetColor("_EmColor", HealingSymbol);
            matToolbotH.SetFloat("_EmPower", 0.55f);
        }


        [RegisterAchievement("CLEAR_ANY_TOOLBOT", "Skins.Toolbot.Wolfo.First", "RepeatFirstTeleporter", 5, null)]
        public class ClearSimulacrumToolbotBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ToolbotBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_TOOLBOT", "Skins.Toolbot.Wolfo.Both", "RepeatFirstTeleporter", 5, null)]
        public class ClearSimulacrumToolbotBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ToolbotBody");
            }
        }

    }
}