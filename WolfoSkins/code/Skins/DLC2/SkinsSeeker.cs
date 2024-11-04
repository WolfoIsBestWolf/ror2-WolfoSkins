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
            Inv();
        }

        public static void Inv()
        {
            SkinDef skinSeekerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Seeker/skinSeekerDefault.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinSeekerDefault.rendererInfos.Length];
            System.Array.Copy(skinSeekerDefault.rendererInfos, NewRenderInfos, skinSeekerDefault.rendererInfos.Length);

            //0 matSeeker
            //1 matSeeker
            //2 matSeekerGlass


            Material matSeeker = Object.Instantiate(skinSeekerDefault.rendererInfos[0].defaultMaterial);

            Texture2D texSeekerDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Inv/texSeekerDiffuse.png");
            texSeekerDiffuse.wrapMode = TextureWrapMode.Clamp;

            matSeeker.mainTexture = texSeekerDiffuse;

            NewRenderInfos[0].defaultMaterial = matSeeker;
            NewRenderInfos[1].defaultMaterial = matSeeker;

            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinSeekerDefaultWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_SEEKER";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Inv/icon.png"));
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