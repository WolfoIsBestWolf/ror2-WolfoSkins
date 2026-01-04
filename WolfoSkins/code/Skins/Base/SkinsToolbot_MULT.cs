using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsToolbot_MULT
    {
        internal static void Start()
        {
            SkinDef skinToolbotDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotDefault.asset").WaitForCompletion();
            SkinDef skinToolbotAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotAlt.asset").WaitForCompletion();
            SkinDef skinToolbotAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotAltColossus.asset").WaitForCompletion();


            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinToolbotDefault_1",
                nameToken = "SIMU_SKIN_TOOLBOT1",
                icon = H.GetIcon("base/mult_damage"),
                original = skinToolbotDefault,
                cloneMesh = false,
            }, new System.Action<SkinDefMakeOnApply>(Default_Damage));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinToolbotMastery_1",
                nameToken = "SIMU_SKIN_TOOLBOT2",
                icon = H.GetIcon("base/mult_healing"),
                original = skinToolbotAlt,
                cloneMesh = false,
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Healing));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinToolbotAltColossus_DLC2",
                nameToken = "SIMU_SKIN_TOOLBOT3",
                icon = H.GetIcon("base/mult_utility"),
                original = skinToolbotAltColossus,
                cloneMesh = false,
            }, new System.Action<SkinDefMakeOnApply>(Colossus_Pink));

        }

        internal static void Colossus_Pink(SkinDefMakeOnApply newSkinDef)
        {
            Color UtilitySymbol = new Color(0.9855168f, 0.48584908f, 1f, 1f);

            Material matToolbot = CloneMat(ref newSkinDef.skinDefParams.rendererInfos, 0);
            Texture2D texToolbotNew = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotAltColossus_U.png");

            matToolbot.mainTexture = texToolbotNew;
            matToolbot.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotAltColossusFresnel.png"));
            matToolbot.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texRampCrocoDiseaseDark.png"));
            matToolbot.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/Colossus/texToolbotCategoryEmissionColossus.png"));
            matToolbot.SetColor("_EmColor", UtilitySymbol);
            matToolbot.SetFloat("_EmPower", 0.55f);
        }

        internal static void Default_Damage(SkinDefMakeOnApply newSkinDef)
        {
            Material matToolbotD = CloneMat(ref newSkinDef.skinDefParams.rendererInfos, 1);
            matToolbotD.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotNew_D.png");

            Color DamageSymbol = new Color(1f, 0.3695f, 0.316f, 1f);
            matToolbotD.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotCategoryEmission.png"));
            matToolbotD.SetColor("_EmColor", DamageSymbol);
            matToolbotD.SetFloat("_EmPower", 0.55f);

        }

        internal static void Mastery_Healing(SkinDefMakeOnApply newSkinDef)
        {
            Material matToolbotH = CloneMat(ref newSkinDef.skinDefParams.rendererInfos, 1);
            matToolbotH.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotNew_H.png");

            Color HealingSymbol = new Color(0.72542995f, 1f, 0.2971698f, 1f);
            matToolbotH.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Toolbot/texToolbotCategoryEmission.png"));
            matToolbotH.SetColor("_EmColor", HealingSymbol);
            matToolbotH.SetFloat("_EmPower", 0.55f);
        }


        [RegisterAchievement("CLEAR_ANY_TOOLBOT", "Skins.Toolbot.Wolfo.First", "RepeatFirstTeleporter", 3, null)]
        public class ClearSimulacrumToolbotBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ToolbotBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_TOOLBOT", "Skins.Toolbot.Wolfo.Both", "RepeatFirstTeleporter", 3, null)]
        public class ClearSimulacrumToolbotBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ToolbotBody");
            }
        }

    }
}