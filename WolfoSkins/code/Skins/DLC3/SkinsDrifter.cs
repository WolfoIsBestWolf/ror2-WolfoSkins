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

            BaseRed(DrifterDefault);
            Mastery(DrifterAlt);

            //0 matDrifter | Drifter | matDrifterBodyAlt 
            //1 matDrifter | Bag | matDrifterBagAlt
            //2 JunkTrimSheet | Junk

        }

        internal static void BaseRed(SkinDef DrifterDefault)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinDrifterDefault_Red",
                nameToken = "SIMU_SKIN_DRIFTER_RED",
                icon = H.GetIcon("dlc3/drifter_red"),
                original = DrifterDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matDrifter = CloneMat(newRenderInfos, 0);

            matDrifter.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Red/texDrifterDiffuse.png");
            matDrifter.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Red/texDrifterfresnelMask.png"));

            newRenderInfos[0].defaultMaterial = matDrifter;
            newRenderInfos[1].defaultMaterial = matDrifter;
        }

        internal static void Mastery(SkinDef DrifterAlt)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinDrifterAlt_Green",
                nameToken = "SIMU_SKIN_DRIFTER_GREEN",
                icon = H.GetIcon("dlc3/drifter_green"),
                original = DrifterAlt,
                cloneMesh = false,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matDrifterBodyAlt = CloneMat(newRenderInfos, 0);
            Material matDrifterBagAlt = CloneMat(newRenderInfos, 1);
 
            matDrifterBodyAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Green/texDrifterBodyDiff.png");
            matDrifterBagAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Drifter/Green/texDrifterBagDiff.png");
      
            newRenderInfos[0].defaultMaterial = matDrifterBodyAlt;
            newRenderInfos[1].defaultMaterial = matDrifterBagAlt;
 
        }

        [RegisterAchievement("CLEAR_ANY_DRIFTER", "Skins.Drifter.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumDrifterBodyBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DrifterBody");
            }
        }

        /*[RegisterAchievement("CLEAR_BOTH_DRIFTER", "Skins.Drifter.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumDrifterBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DrifterBody");
            }
        }*/

    }
}