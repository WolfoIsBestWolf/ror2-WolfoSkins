using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsSeeker
    {
        internal static void Start()
        {
            Main();
            //Mastery();
        }

        public static void Main()
        {
            SkinDef skinSeekerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Seeker/skinSeekerDefault.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinSeekerDefault.rendererInfos.Length];
            System.Array.Copy(skinSeekerDefault.rendererInfos, NewRenderInfos, skinSeekerDefault.rendererInfos.Length);

            //0 matSeeker
            //1 matSeeker
            //2 matSeekerGlass


            Material matSeeker = Object.Instantiate(skinSeekerDefault.rendererInfos[0].defaultMaterial);
            Material matSeekerGlass = Object.Instantiate(skinSeekerDefault.rendererInfos[2].defaultMaterial);

            Texture2D texSeekerDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Main/texSeekerDiffuse.png");
            texSeekerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRandoRamp = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Main/texRandoRamp.png");
            texRandoRamp.wrapMode = TextureWrapMode.Clamp;

            matSeeker.mainTexture = texSeekerDiffuse;
            matSeekerGlass.SetTexture("_RemapTex", texRandoRamp);

            NewRenderInfos[0].defaultMaterial = matSeeker;
            NewRenderInfos[1].defaultMaterial = matSeeker;
            NewRenderInfos[2].defaultMaterial = matSeekerGlass;
            
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinSeekerDefaultWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_SEEKER";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Main/icon.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinSeekerDefault };
            newSkinDef.meshReplacements = skinSeekerDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinSeekerDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Seeker/SeekerBody.prefab").WaitForCompletion(), newSkinDef);

        }

        public static void Mastery()
        {
            SkinDef skinSeekerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Seeker/skinSeekerAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinSeekerDefault.rendererInfos.Length];
            System.Array.Copy(skinSeekerDefault.rendererInfos, NewRenderInfos, skinSeekerDefault.rendererInfos.Length);

            //0 matSeekerAlt
            //1 matSeeker
            //2 matSeekerGlass

            Material matSeekerAlt = Object.Instantiate(skinSeekerDefault.rendererInfos[0].defaultMaterial);
            Material matSeekerAltCloth = Object.Instantiate(skinSeekerDefault.rendererInfos[1].defaultMaterial);
            Material matSeekerGlass = Object.Instantiate(skinSeekerDefault.rendererInfos[2].defaultMaterial);


            Texture2D texSeekerAltDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/texSeekerAltDiffuse.png");
            texSeekerAltDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texSeekerAltFlow = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/texSeekerAltFlow.png");
            texSeekerAltFlow.wrapMode = TextureWrapMode.Clamp;


            Texture2D texSeekerAltClothDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/texSeekerAltClothDiffuse.png");
            texSeekerAltClothDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texSeekerAltClothFlow = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/texSeekerAltClothFlow.png");
            texSeekerAltClothFlow.wrapMode = TextureWrapMode.Clamp;


            Texture2D texRampCaptainAirstrike = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/texRampCaptainAirstrike.png");
            texRampCaptainAirstrike.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRandoRamp = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/texRandoRamp.png");
            texRandoRamp.wrapMode = TextureWrapMode.Clamp;

            matSeekerAlt.mainTexture = texSeekerAltDiffuse;
            matSeekerAlt.SetTexture("_FlowHeightRamp", texRampCaptainAirstrike);
            matSeekerAlt.SetTexture("_FlowHeightmap", texSeekerAltFlow);
            matSeekerAlt.SetTexture("_FresnelRamp", texRandoRamp);

            matSeekerAltCloth.mainTexture = texSeekerAltClothDiffuse;
            matSeekerAltCloth.SetTexture("_FlowHeightRamp", texRampCaptainAirstrike);
            matSeekerAltCloth.SetTexture("_FlowHeightmap", texSeekerAltClothFlow);

            matSeekerGlass.SetTexture("_RemapTex", texRandoRamp);

            NewRenderInfos[0].defaultMaterial = matSeekerAlt;
            NewRenderInfos[1].defaultMaterial = matSeekerAltCloth;
            NewRenderInfos[2].defaultMaterial = matSeekerGlass;

            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinSeekerAltWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_SEEKER_MASTER";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/icon.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinSeekerDefault };
            newSkinDef.meshReplacements = skinSeekerDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinSeekerDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Seeker/SeekerBody.prefab").WaitForCompletion(), newSkinDef);

        }

        [RegisterAchievement("CLEAR_ANY_SEEKER", "Skins.Seeker.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumSeekerBody : Achievement_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("SeekerBody");
            }
        }
        
    }
}