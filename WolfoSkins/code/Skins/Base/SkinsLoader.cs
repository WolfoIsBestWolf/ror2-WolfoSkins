using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsLoader
    {
        internal static void Start()
        {
            SkinDef skinLoaderDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderDefault.asset").WaitForCompletion();
            SkinDef skinLoaderAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAlt.asset").WaitForCompletion();
            SkinDef skinLoaderAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAltColossus.asset").WaitForCompletion();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinLoader_1",
                nameToken = "SIMU_SKIN_LOADER",
                icon = H.GetIcon("base/loader_green"),
                original = skinLoaderDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Green));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinLoader_RED_1",
                nameToken = "SIMU_SKIN_LOADER_RED",
                icon = H.GetIcon("base/loader_red"),
                original = skinLoaderAlt,
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Red));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinLoaderAltColossus_DLC2",
                nameToken = "SIMU_SKIN_LOADER_COLOSSUS",
                icon = H.GetIcon("base/loader_dlc2"),
                original = skinLoaderAltColossus,
            }, new System.Action<SkinDefMakeOnApply>(Colossus_Bee));

        }
        internal static void Colossus_Bee(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matCommandoAltColossus = CloneMat(ref newRenderInfos, 2);

            matCommandoAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Colossus/texLoaderAltColossusDiffuse.png");
            matCommandoAltColossus.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Colossus/texRampRJMushroom.png")); //texRampRJMushroom
            matCommandoAltColossus.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Colossus/texRampLightning2.png")); //texRampLightning2

            newRenderInfos[0].defaultMaterial = matCommandoAltColossus;
            newRenderInfos[1].defaultMaterial = matCommandoAltColossus;
            newRenderInfos[2].defaultMaterial = matCommandoAltColossus;

        }

        internal static void Default_Green(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matTrimsheetConstructionLoaderAlt = CloneMat(ref newRenderInfos, 0);
            Material matLoaderPilotDiffuseAlt = CloneMat(ref newRenderInfos, 1);

            Texture2D texTrimSheetConstructionLoaderAlt = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Green/texTrimSheetConstructionLoaderAlt.png");
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;


            matTrimsheetConstructionLoaderAlt.mainTexture = texTrimSheetConstructionLoaderAlt;
            matLoaderPilotDiffuseAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Green/texLoaderPilotDiffuse.png");
            matLoaderPilotDiffuseAlt.SetColor("_EmColor", new Color(1f, 0.8f, 0f, 1));

            newRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            newRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            newRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;

        }

        internal static void Mastery_Red(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matTrimsheetConstructionLoaderAlt = CloneMat(ref newRenderInfos, 0);
            Material matLoaderPilotDiffuseAlt = CloneMat(ref newRenderInfos, 1);


            Texture2D texTrimSheetConstructionLoaderAlt = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Red/texTrimSheetConstructionLoaderAltNewRED.png");
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;

            matTrimsheetConstructionLoaderAlt.mainTexture = texTrimSheetConstructionLoaderAlt;
            matLoaderPilotDiffuseAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Red/texLoaderPilotDiffuseAltRED.png");
            matLoaderPilotDiffuseAlt.SetColor("_EmColor", new Color(1f, 0.6f, 0f, 1));

            newRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            newRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            newRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;

        }

        #region purple gray
        /*
        internal static void LoaderSkinAlt()
        {
            SkinDef skinLoaderDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderDefault.asset").WaitForCompletion();
            SkinDef skinLoaderAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[skinLoaderAlt.rendererInfos.Length];
            System.Array.Copy(skinLoaderAlt.rendererInfos, newRenderInfos, skinLoaderAlt.rendererInfos.Length);

            Material matTrimsheetConstructionLoaderAlt = CloneMat(ref LoaderAlt.rendererInfos[0].defaultMaterial);
            Material matLoaderPilotDiffuseAlt = CloneMat(ref LoaderAlt.rendererInfos[1].defaultMaterial);


            Texture2D texTrimSheetConstructionLoaderAlt = new Texture2D(256, 512, TextureFormat.DXT5, false);
            texTrimSheetConstructionLoaderAlt.LoadImage(Properties.Resources.texTrimSheetConstructionLoaderAltALT, true);
            texTrimSheetConstructionLoaderAlt.filterMode = FilterMode.Bilinear;
            texTrimSheetConstructionLoaderAlt.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texLoaderPilotDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texLoaderPilotDiffuse.LoadImage(Properties.Resources.texLoaderPilotDiffuseALT, true);
            texLoaderPilotDiffuse.filterMode = FilterMode.Bilinear;
            texLoaderPilotDiffuse.wrapMode = TextureWrapMode.Clamp;

            matTrimsheetConstructionLoaderAlt.mainTexture = texTrimSheetConstructionLoaderAlt;
            matLoaderPilotDiffuseAlt.mainTexture = texLoaderPilotDiffuse;
            matLoaderPilotDiffuseAlt.SetColor("_EmColor", new Color(1f, 0.6f, 0f, 1));

            newRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            newRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            newRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;
            //
            //
            //Unlockable
       
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinLoaderDefault.meshReplacements.Length];
            skinLoaderDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[1].mesh = skinLoaderAlt.meshReplacements[1].mesh;
            //MeshReplacements[2].mesh = skinLoaderAlt.meshReplacements[2].mesh;


            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinLoaderWolfoAlt_1",
                NameToken = "SIMU_SKIN_LOADER_ALT",
                Icon = SkinIconS,
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = newRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
        }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_LOADER", "Skins.Loader.Wolfo.First", "DefeatSuperRoboBallBoss", 3, null)]
        public class ClearSimulacrumTreebotBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("LoaderBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_LOADER", "Skins.Loader.Wolfo.Both", "DefeatSuperRoboBallBoss", 3, null)]
        public class ClearSimulacrumTreebotBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("LoaderBody");
            }
        }
    }
}