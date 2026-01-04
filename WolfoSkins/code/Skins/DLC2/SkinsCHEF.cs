using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC2
{
    public class SkinsChef
    {
        internal static void Start()
        {
            SkinDef skinChefDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefDefault.asset").WaitForCompletion();
            SkinDef skinChefAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefAlt.asset").WaitForCompletion();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinChef_Red_1",
                nameToken = "SIMU_SKIN_CHEF_RED",
                icon = H.GetIcon("dlc2/chef_red"),
                original = skinChefDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Red));
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinChefAlt_Green_1",
                nameToken = "SIMU_SKIN_CHEF_GREEN",
                icon = H.GetIcon("dlc2/chef_green"),
                original = skinChefAlt,
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Alt));
            /*CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinChef_Green_1",
                nameToken = "SIMU_SKIN_CHEF_GREEN",
                icon = H.GetIcon("dlc2/chef_green"),
                original = skinChefDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Green));*/
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinChef_Blue_1",
                nameToken = "SIMU_SKIN_CHEF_BLUE",
                icon = H.GetIcon("dlc2/chef_blue"),
                original = skinChefDefault,
            }, new System.Action<SkinDefMakeOnApply>(Defailt_Blue));

            //Mastery_Alt();

            /*
            [0] matChef | meshChef
            [1] matChef | meshChefPizzaCutter
            [2] matChef | meshlChefCleaver
            [3] matChef | meshChefOven
            [4] matChefIceBox | meshChefIceBox
            */
        }

        #region Unused Mastery

        internal static void Mastery_Alt(SkinDef newSkin)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkin.skinDefParams.rendererInfos;

            Material matChefAlt = CloneMat(ref newRenderInfos, 0);
            Material matChefAltAccessories = CloneMat(ref newRenderInfos, 1);
            Material matChefAltIceBox = CloneMat(ref newRenderInfos, 4);

            matChefAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/MasteryGreen/texChefAltDiffuse.png");
            matChefAltAccessories.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/MasteryGreen/texChefAltAccessoriesDiffuse.png");
            matChefAltIceBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/MasteryGreen/texChefAltIceBoxDiffuse.png");

            newRenderInfos[0].defaultMaterial = matChefAlt;
            newRenderInfos[1].defaultMaterial = matChefAltAccessories;
            newRenderInfos[2].defaultMaterial = matChefAlt;
            newRenderInfos[3].defaultMaterial = matChefAlt;
            newRenderInfos[4].defaultMaterial = matChefAltIceBox;
        }

        #endregion

        internal static void Default_Red(SkinDef newSkin)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkin.skinDefParams.rendererInfos;

            Material matChef = CloneMat(ref newRenderInfos, 0);
            Material matChefIceBox = CloneMat(ref newRenderInfos, 4);

            matChef.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Red.png");
            matChefIceBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/icebox_Red.png");

            newRenderInfos[0].defaultMaterial = matChef;
            newRenderInfos[1].defaultMaterial = matChef;
            newRenderInfos[2].defaultMaterial = matChef;
            newRenderInfos[3].defaultMaterial = matChef;
            newRenderInfos[4].defaultMaterial = matChefIceBox;
        }

        internal static void Default_Green(SkinDef newSkin)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkin.skinDefParams.rendererInfos;

            Material matChef = CloneMat(ref newRenderInfos, 0);
            Material matChefIceBox = CloneMat(ref newRenderInfos, 4);

            //Texture2D texChefNormalWood = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefNormalWood.png");
            matChef.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Green.png");
            //matChef.SetTexture("_NormalTex", texChefNormalWood); //Fucks up a lot of stuff idk how
            matChefIceBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/icebox_green.png");

            newRenderInfos[0].defaultMaterial = matChef;
            newRenderInfos[1].defaultMaterial = matChef;
            newRenderInfos[2].defaultMaterial = matChef;
            newRenderInfos[3].defaultMaterial = matChef;
            newRenderInfos[4].defaultMaterial = matChefIceBox;
        }

        internal static void Defailt_Blue(SkinDef newSkin)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkin.skinDefParams.rendererInfos;

            Material matChef = CloneMat(ref newRenderInfos, 0);
            Material matChefIceBox = CloneMat(ref newRenderInfos, 4);

            matChef.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Blue.png");
            matChefIceBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/icebox_blue.png");

            newRenderInfos[0].defaultMaterial = matChef;
            newRenderInfos[1].defaultMaterial = matChef;
            newRenderInfos[2].defaultMaterial = matChef;
            newRenderInfos[3].defaultMaterial = matChef;
            newRenderInfos[4].defaultMaterial = matChefIceBox;
        }


        [RegisterAchievement("CLEAR_ANY_CHEF", "Skins.Chef.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumChefBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChefBody");
            }
        }

    }
}