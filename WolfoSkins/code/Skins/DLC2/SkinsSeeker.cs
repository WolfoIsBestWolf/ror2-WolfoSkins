using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC2
{
    public class SkinsSeeker
    {
        internal static void Start()
        {
            SkinDef skinSeekerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Seeker/skinSeekerDefault.asset").WaitForCompletion();

            Main(skinSeekerDefault);
            //Mastery();


            //0 matSeeker
            //1 matSeeker
            //2 matSeekerGlass
        }

        public static void Main(SkinDef skinSeekerDefault)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                name = "skinSeekerDefault_1",
                nameToken = "SIMU_SKIN_SEEKER",
                icon = H.GetIcon("dlc2/seeker_orange"),
                original = skinSeekerDefault,
            });

            Material matSeeker = CloneMat(newRenderInfos, 0);
            Material matSeekerGlass = CloneMat(newRenderInfos, 2);

            matSeeker.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Main/texSeekerDiffuse.png");
            matSeekerGlass.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Main/texRandoRamp.png"));

            newRenderInfos[0].defaultMaterial = matSeeker;
            newRenderInfos[1].defaultMaterial = matSeeker;
            newRenderInfos[2].defaultMaterial = matSeekerGlass;
        }

        /*
        public static void Mastery()
        {
            SkinDef skinSeekerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Seeker/skinSeekerAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[skinSeekerDefault.rendererInfos.Length];
            System.Array.Copy(skinSeekerDefault.rendererInfos, newRenderInfos, skinSeekerDefault.rendererInfos.Length);

            //0 matSeekerAlt
            //1 matSeeker
            //2 matSeekerGlass

            Material matSeekerAlt = CloneMat(SeekerDefault.rendererInfos[0].defaultMaterial);
            Material matSeekerAltCloth = CloneMat(SeekerDefault.rendererInfos[1].defaultMaterial);
            Material matSeekerGlass = CloneMat(SeekerDefault.rendererInfos[2].defaultMaterial);


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

            newRenderInfos[0].defaultMaterial = matSeekerAlt;
            newRenderInfos[1].defaultMaterial = matSeekerAltCloth;
            newRenderInfos[2].defaultMaterial = matSeekerGlass;

            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinSeekerAlt_1";
            newSkinDef.nameToken = "SIMU_SKIN_SEEKER_MASTER";
            newSkinDef.icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Mastery/icon.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinSeekerDefault };
            newSkinDef.meshReplacements = skinSeekerDefault.meshReplacements;
            newSkinDef.rendererInfos = newRenderInfos;
            newSkinDef.rootObject = skinSeekerDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/Seeker/SeekerBody.prefab").WaitForCompletion(), newSkinDef);

        }
        */


        [RegisterAchievement("CLEAR_ANY_SEEKER", "Skins.Seeker.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumSeekerBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("SeekerBody");
            }
        }

    }
}