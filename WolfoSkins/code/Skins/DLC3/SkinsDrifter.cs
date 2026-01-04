using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC1
{
    public class SkinsDrifter
    {
        internal static void Start()
        {
            SkinDef DrifterDefault = Addressables.LoadAssetAsync<SkinDef>(key: "fd3bbb69748a0674ab66c0ab3ad04a30").WaitForCompletion();
            SkinDef DrifterAlt = Addressables.LoadAssetAsync<SkinDef>(key: "851c64b14150a4743b55b5cc5d03eb78").WaitForCompletion();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinDrifterDefault_Red_1",
                nameToken = "SIMU_SKIN_DRIFTER_RED",
                icon = H.GetIcon("dlc3/drifter_red"),
                original = DrifterDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Red));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinDrifterDefault_Pink_1",
                nameToken = "SIMU_SKIN_DRIFTER_PINK",
                icon = H.GetIcon("dlc3/drifter_pink"),
                original = DrifterDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Pink));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinDrifterDefault_Forest_1",
                nameToken = "SIMU_SKIN_DRIFTER_FOREST",
                icon = H.GetIcon("dlc3/drifter_forest"),
                original = DrifterDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Forest));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinDrifterAlt_Green_1",
                nameToken = "SIMU_SKIN_DRIFTER_GREEN",
                icon = H.GetIcon("dlc3/drifter_green"),
                original = DrifterAlt,
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Green));

            //0 matDrifter | Drifter | matDrifterBodyAlt 
            //1 matDrifter | Bag | matDrifterBagAlt
            //2 JunkTrimSheet | Junk
        }

        internal static void Default_Red(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matDrifter = CloneMat(ref newRenderInfos, 0);

            matDrifter.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Red/texDrifterDiffuse.png");
            matDrifter.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Red/texDrifterfresnelMask.png"));
            matDrifter.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Red/texDrifterRamp.png"));

            newRenderInfos[0].defaultMaterial = matDrifter;
            newRenderInfos[1].defaultMaterial = matDrifter;
        }

        internal static void Default_Pink(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matDrifter = CloneMat(ref newRenderInfos, 0);

            matDrifter.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Pink/texDrifterDiffuse.png");
            //matDrifter.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Pink/texDrifterfresnelMask.png"));
            //matDrifter.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Pink/texDrifterRamp.png"));

            newRenderInfos[0].defaultMaterial = matDrifter;
            newRenderInfos[1].defaultMaterial = matDrifter;
        }

        internal static void Default_Forest(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matDrifter = CloneMat(ref newRenderInfos, 0);

            matDrifter.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Forest/texDrifterDiffuse.png");
            matDrifter.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Forest/texDrifterfresnelMask.png"));
            matDrifter.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Forest/texDrifterRamp.png"));

            newRenderInfos[0].defaultMaterial = matDrifter;
            newRenderInfos[1].defaultMaterial = matDrifter;
        }

        internal static void Mastery_Green(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matDrifterBodyAlt = CloneMat(ref newRenderInfos, 0);
            Material matDrifterBagAlt = CloneMat(ref newRenderInfos, 1);

            matDrifterBodyAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Green/texDrifterBodyDiff.png");
            matDrifterBodyAlt.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Green/texDrifterRamp.png"));
            matDrifterBodyAlt.SetFloat("_FresnelPower", 2f);
            //matDrifterBodyAlt.SetFloat("_FresnelBoost", 2f);
            matDrifterBagAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Green/texDrifterBagDiff.png");

            newRenderInfos[0].defaultMaterial = matDrifterBodyAlt;
            newRenderInfos[1].defaultMaterial = matDrifterBagAlt;

        }

        [RegisterAchievement("CLEAR_ANY_DRIFTER", "Skins.Drifter.Wolfo.First", "FreeDrifter", 3, null)]
        public class ClearSimulacrumDrifterBodyBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DrifterBody");
            }
        }

        /*[RegisterAchievement("CLEAR_BOTH_DRIFTER", "Skins.Drifter.Wolfo.Both", null, 3, null)]
        public class ClearSimulacrumDrifterBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DrifterBody");
            }
        }*/

    }
}