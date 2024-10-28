using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsChef
    {
        internal static void Start()
        {
            ChefSkin_RED();
            ChefSkin_GREEN();
            ChefSkin_Blue();
            //Mastery_Alt();
        }
        
        internal static void Mastery_Alt()
        {
            SkinDef skinChefAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChefAlt.rendererInfos.Length];
            System.Array.Copy(skinChefAlt.rendererInfos, NewRenderInfos, skinChefAlt.rendererInfos.Length);

            //0 matChefAlt
            //1 matChefAltAccessories / PizzaCutter
            //2 matChefAlt / Cleaver

            Material matChefAlt = Object.Instantiate(skinChefAlt.rendererInfos[0].defaultMaterial);
            Material matChefAltAccessories = Object.Instantiate(skinChefAlt.rendererInfos[0].defaultMaterial);

            Texture2D texChefAltDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/Master/texChefAltDiffuse.png");
            texChefAltDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefAltAccessoriesDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/Master/texChefAltAccessoriesDiffuse.png");
            texChefAltAccessoriesDiffuse.wrapMode = TextureWrapMode.Clamp;


            matChefAlt.mainTexture = texChefAltDiffuse;
            matChefAltAccessories.mainTexture = texChefAltAccessoriesDiffuse;


            NewRenderInfos[0].defaultMaterial = matChefAlt;
            NewRenderInfos[1].defaultMaterial = matChefAltAccessories;
            NewRenderInfos[2].defaultMaterial = matChefAlt;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinChefWolfo_BLUE_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_CHEF_BLUE";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/skinIconChef_Blue.png"));
            newSkinDef.baseSkins = skinChefAlt.baseSkins;
            newSkinDef.meshReplacements = skinChefAlt.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinChefAlt.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Chef/ChefBody.prefab").WaitForCompletion(), newSkinDef);
        }
        
        internal static void ChefSkin_RED()
        {
            SkinDef skinChefDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefDefault.asset").WaitForCompletion();
           
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChefDefault.rendererInfos.Length];
            System.Array.Copy(skinChefDefault.rendererInfos, NewRenderInfos, skinChefDefault.rendererInfos.Length);

            //0 matChef
            //1 matChef / PizzaCutter
            //2 matChef / Cleaver

            Material matChef = Object.Instantiate(skinChefDefault.rendererInfos[0].defaultMaterial);

            Texture2D texChefDiffuse_Red = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Red.png");
            texChefDiffuse_Red.wrapMode = TextureWrapMode.Clamp;

            matChef.mainTexture = texChefDiffuse_Red;
            //matChef.SetTexture("_EmTex", texCrocoEmission);
            //matChef.SetColor("_EmColor", new Color(1f, 1f, 1f));

            NewRenderInfos[0].defaultMaterial = matChef;
            NewRenderInfos[1].defaultMaterial = matChef;
            NewRenderInfos[2].defaultMaterial = matChef;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinChefWolfo_Red_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_CHEF_RED";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/skinIconChef_Red.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinChefDefault };
            newSkinDef.meshReplacements = skinChefDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinChefDefault.rootObject;
           

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Chef/ChefBody.prefab").WaitForCompletion(), newSkinDef);
        }

        internal static void ChefSkin_GREEN()
        {
            SkinDef skinChefDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefDefault.asset").WaitForCompletion();
           
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChefDefault.rendererInfos.Length];
            System.Array.Copy(skinChefDefault.rendererInfos, NewRenderInfos, skinChefDefault.rendererInfos.Length);

            //0 matChef
            //1 matChef / PizzaCutter
            //2 matChef / Cleaver


            Material matChef = Object.Instantiate(skinChefDefault.rendererInfos[0].defaultMaterial);

            Texture2D texChefDiffuse_Red = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Green.png");
            texChefDiffuse_Red.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefNormalWood = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefNormalWood.png");
            texChefNormalWood.wrapMode = TextureWrapMode.Clamp;

            matChef.mainTexture = texChefDiffuse_Red;
            //matChef.SetTexture("_NormalTex", texChefNormalWood); //Fucks up a lot of stuff idk how
            //matChef.SetTexture("_EmTex", texCrocoEmission);
            //matChef.SetColor("_EmColor", new Color(1f, 1f, 1f));

            NewRenderInfos[0].defaultMaterial = matChef;
            NewRenderInfos[1].defaultMaterial = matChef;
            NewRenderInfos[2].defaultMaterial = matChef;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinChefWolfo_Green_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_CHEF_GREEN";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/skinIconChef_Green.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinChefDefault };
            newSkinDef.meshReplacements = skinChefDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinChefDefault.rootObject;
             
            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Chef/ChefBody.prefab").WaitForCompletion(), newSkinDef);

        }

        internal static void ChefSkin_Blue()
        {
            SkinDef skinChefDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Chef/skinChefDefault.asset").WaitForCompletion();
            
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChefDefault.rendererInfos.Length];
            System.Array.Copy(skinChefDefault.rendererInfos, NewRenderInfos, skinChefDefault.rendererInfos.Length);

            //0 matChef
            //1 matChef / PizzaCutter
            //2 matChef / Cleaver

            Material matChef = Object.Instantiate(skinChefDefault.rendererInfos[0].defaultMaterial);

            Texture2D texChefDiffuse_Red = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/texChefDiffuse_Blue.png");
            texChefDiffuse_Red.wrapMode = TextureWrapMode.Clamp;

            matChef.mainTexture = texChefDiffuse_Red;
            //matChef.SetTexture("_EmTex", texCrocoEmission);
            //matChef.SetColor("_EmColor", new Color(1f, 1f, 1f));

            NewRenderInfos[0].defaultMaterial = matChef;
            NewRenderInfos[1].defaultMaterial = matChef;
            NewRenderInfos[2].defaultMaterial = matChef;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinChefWolfo_BLUE_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_CHEF_BLUE";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Chef/skinIconChef_Blue.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinChefDefault };
            newSkinDef.meshReplacements = skinChefDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinChefDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Chef/ChefBody.prefab").WaitForCompletion(), newSkinDef);

        }


        [RegisterAchievement("CLEAR_ANY_CHEF", "Skins.Chef.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumChefBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChefBody");
            }
        }

    }
}