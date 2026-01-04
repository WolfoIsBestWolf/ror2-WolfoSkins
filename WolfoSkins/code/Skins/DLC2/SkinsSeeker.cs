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
            SkinDef skinSeekerAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/Seeker/skinSeekerAlt.asset").WaitForCompletion();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinSeekerDefault_1",
                nameToken = "SIMU_SKIN_SEEKER",
                icon = H.GetIcon("dlc2/seeker_orange"),
                original = skinSeekerDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Orange));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinSeekerAltRome_1",
                nameToken = "SIMU_SKIN_SEEKER_ROME",
                icon = H.GetIcon("dlc2/seeker_rome"),
                original = skinSeekerAlt,
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Rome));

            //0 matSeeker
            //1 matSeeker
            //2 matSeekerGlass
        }

        public static void Default_Orange(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matSeeker = CloneMat(ref newRenderInfos, 0);
            Material matSeekerGlass = CloneMat(ref newRenderInfos, 2);

            matSeeker.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Main/texSeekerDiffuse.png");
            matSeekerGlass.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Main/texRandoRamp.png"));

            newRenderInfos[0].defaultMaterial = matSeeker;
            newRenderInfos[1].defaultMaterial = matSeeker;
            newRenderInfos[2].defaultMaterial = matSeekerGlass;
        }

        public static void Mastery_Rome(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matSeekerAlt = CloneMat(ref newRenderInfos, 0);
            Material matSeekerAltCloth = CloneMat(ref newRenderInfos, 1);
            Material matSeekerGlass = CloneMat(ref newRenderInfos, 2);

            matSeekerAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Rome/texSeekerAltDiffuse.png");
            matSeekerAlt.SetTexture("_FlowHeightmap", null);
            matSeekerAlt.SetTexture("_FlowHeightRamp", null);
            matSeekerAlt.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Rome/texRandoRamp.png"));
            matSeekerAlt.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Rome/texSeekerAltFresnel.png"));
            matSeekerAltCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Rome/texSeekerAltDiffuse.png");
            matSeekerAltCloth.SetTexture("_FlowHeightmap", null);
            matSeekerAltCloth.SetTexture("_FlowHeightRamp", null);
            matSeekerGlass.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/Seeker/Rome/texRandoRamp.png"));

            newRenderInfos[0].defaultMaterial = matSeekerAlt;
            newRenderInfos[1].defaultMaterial = matSeekerAlt;
            newRenderInfos[2].defaultMaterial = matSeekerGlass;
        }


        #region Unused Mastery Idea
        /*
        public static void Mastery(SkinDef newSkin)
        {
           
            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[skinSeekerDefault.rendererInfos.Length];
            System.Array.Copy(skinSeekerDefault.rendererInfos, newRenderInfos, skinSeekerDefault.rendererInfos.Length);

            //0 matSeekerAlt
            //1 matSeeker
            //2 matSeekerGlass

            Material matSeekerAlt = CloneMat(ref SeekerDefault.rendererInfos[0].defaultMaterial);
            Material matSeekerAltCloth = CloneMat(ref SeekerDefault.rendererInfos[1].defaultMaterial);
            Material matSeekerGlass = CloneMat(ref SeekerDefault.rendererInfos[2].defaultMaterial);


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
        #endregion

        [RegisterAchievement("CLEAR_ANY_SEEKER", "Skins.Seeker.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumSeekerBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("SeekerBody");
            }
        }

    }
}