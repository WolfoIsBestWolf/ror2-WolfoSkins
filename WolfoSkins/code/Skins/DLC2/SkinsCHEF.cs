using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod
{
    public class SkinsChef
    {
        internal static void Start()
        {
            SkinDef skinChefDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefDefault.asset").WaitForCompletion();
            SkinDef skinChefAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefAlt.asset").WaitForCompletion();

            ChefSkin_RED(skinChefDefault);
            ChefSkin_GREEN(skinChefDefault);
            ChefSkin_Blue(skinChefDefault);
            //Mastery_Alt();

            /*
    
            skinChefDefault
            [0] matChef | meshChef
            [1] matChef | meshChefPizzaCutter
            [2] matChef | meshlChefCleaver
            [3] matChef | meshChefOven
            [4] matChefIceBox | meshChefIceBox
            */
        }

        #region Unused Mastery
        /*
        internal static void Mastery_Alt(SkinDef skinChefAlt)
        {
            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[skinChefAlt.rendererInfos.Length];
            System.Array.Copy(skinChefAlt.rendererInfos, newRenderInfos, skinChefAlt.rendererInfos.Length);

            //0 matChefAlt
            //1 matChefAltAccessories / PizzaCutter
            //2 matChefAlt / Cleaver

            Material matChefAlt = CloneMat(ChefAlt.rendererInfos[0].defaultMaterial);
            Material matChefAltAccessories = CloneMat(ChefAlt.rendererInfos[0].defaultMaterial);

            Texture2D texChefAltDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/Master/texChefAltDiffuse.png");
            texChefAltDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefAltAccessoriesDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/Master/texChefAltAccessoriesDiffuse.png");
            texChefAltAccessoriesDiffuse.wrapMode = TextureWrapMode.Clamp;


            matChefAlt.mainTexture = texChefAltDiffuse;
            matChefAltAccessories.mainTexture = texChefAltAccessoriesDiffuse;


            newRenderInfos[0].defaultMaterial = matChefAlt;
            newRenderInfos[1].defaultMaterial = matChefAltAccessories;
            newRenderInfos[2].defaultMaterial = matChefAlt;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinChef_BLUE_1";
            newSkinDef.nameToken = "SIMU_SKIN_CHEF_BLUE";
            newSkinDef.icon =  Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/icon/Chef/skinIconChef_Blue.png"));
            newSkinDef.baseSkins = skinChefAlt.baseSkins;
            newSkinDef.meshReplacements = skinChefAlt.meshReplacements;
            newSkinDef.rendererInfos = newRenderInfos;
            newSkinDef.rootObject = skinChefAlt.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Chef/ChefBody.prefab").WaitForCompletion(), newSkinDef);
        }
        */
        #endregion

        internal static void ChefSkin_RED(SkinDef skinChefDefault)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                name = "skinChef_Red_1",
                nameToken = "SIMU_SKIN_CHEF_RED",
                icon = H.GetIcon("dlc2/chef_red"),
                original = skinChefDefault,
            });

            Material matChef = CloneMat(newRenderInfos, 0);
            Material matChefIceBox = CloneMat(newRenderInfos, 4);

            matChef.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Red.png");
            matChefIceBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/icebox_Red.png");

            newRenderInfos[0].defaultMaterial = matChef;
            newRenderInfos[1].defaultMaterial = matChef;
            newRenderInfos[2].defaultMaterial = matChef;
            newRenderInfos[3].defaultMaterial = matChef;
            newRenderInfos[4].defaultMaterial = matChefIceBox;
        }

        internal static void ChefSkin_GREEN(SkinDef skinChefDefault)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                name = "skinChef_Green_1",
                nameToken = "SIMU_SKIN_CHEF_GREEN",
                icon = H.GetIcon("dlc2/chef_green"),
                original = skinChefDefault,
            });

            Material matChef = CloneMat(newRenderInfos, 0);
            Material matChefIceBox = CloneMat(newRenderInfos, 4);

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

        internal static void ChefSkin_Blue(SkinDef skinChefDefault)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                name = "skinChef_BLUE_1",
                nameToken = "SIMU_SKIN_CHEF_BLUE",
                icon = H.GetIcon("dlc2/chef_blue"),
                original = skinChefDefault,
            });

            Material matChef = CloneMat(newRenderInfos, 0);
            Material matChefIceBox = CloneMat(newRenderInfos, 4);

            matChef.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Blue.png");
            matChefIceBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/icebox_blue.png");

            newRenderInfos[0].defaultMaterial = matChef;
            newRenderInfos[1].defaultMaterial = matChef;
            newRenderInfos[2].defaultMaterial = matChef;
            newRenderInfos[3].defaultMaterial = matChef;
            newRenderInfos[4].defaultMaterial = matChefIceBox;
        }


        [RegisterAchievement("CLEAR_ANY_CHEF", "Skins.Chef.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumChefBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChefBody");
            }
        }

    }
}