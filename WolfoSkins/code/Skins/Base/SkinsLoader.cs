using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsLoader
    {
        internal static void Start()
        {
            LoaderSkin();
            LoaderSkinRED();
            Loader_AltColossus();
        }
        internal static void Loader_AltColossus()
        {
            SkinDef skinLoaderAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinLoaderAltColossus.rendererInfos.Length];
            System.Array.Copy(skinLoaderAltColossus.rendererInfos, NewRenderInfos, skinLoaderAltColossus.rendererInfos.Length);

            Material matCommandoAltColossus = Object.Instantiate(skinLoaderAltColossus.rendererInfos[2].defaultMaterial);


            Texture2D texCommandoAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Colossus/texLoaderAltColossusDiffuse.png");
            texCommandoAltColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampLightning2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Colossus/texRampLightning2.png");
            texRampLightning2.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampRJMushroom = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Colossus/texRampRJMushroom.png");
            texRampRJMushroom.wrapMode = TextureWrapMode.Clamp;


            matCommandoAltColossus.mainTexture = texCommandoAltColossusDiffuse;
            matCommandoAltColossus.SetTexture("_FresnelRamp", texRampRJMushroom); //texRampRJMushroom
            matCommandoAltColossus.SetTexture("_FlowHeightRamp", texRampLightning2); //texRampLightning2

            NewRenderInfos[0].defaultMaterial = matCommandoAltColossus;
            NewRenderInfos[1].defaultMaterial = matCommandoAltColossus;
            NewRenderInfos[2].defaultMaterial = matCommandoAltColossus;
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinLoaderAltColossusWolfo_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_LOADER_COLOSSUS";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Colossus/Loader.png"));
            newSkinDef.baseSkins = skinLoaderAltColossus.baseSkins;
            newSkinDef.meshReplacements = skinLoaderAltColossus.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinLoaderAltColossus.rootObject;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), newSkinDef);

        }
        
        internal static void LoaderSkin()
        {

            SkinDef skinLoaderDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderDefault.asset").WaitForCompletion();
            SkinDef skinLoaderAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinLoaderAlt.rendererInfos.Length];
            System.Array.Copy(skinLoaderAlt.rendererInfos, NewRenderInfos, skinLoaderAlt.rendererInfos.Length);

            Material matTrimsheetConstructionLoaderAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[0].defaultMaterial);
            Material matLoaderPilotDiffuseAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[1].defaultMaterial);


            Texture2D texTrimSheetConstructionLoaderAlt = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Green/texTrimSheetConstructionLoaderAlt.png");
            texTrimSheetConstructionLoaderAlt.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texLoaderPilotDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Green/texLoaderPilotDiffuse.png");
            texLoaderPilotDiffuse.wrapMode = TextureWrapMode.Clamp;

            matTrimsheetConstructionLoaderAlt.mainTexture = texTrimSheetConstructionLoaderAlt;
            matLoaderPilotDiffuseAlt.mainTexture = texLoaderPilotDiffuse;
            matLoaderPilotDiffuseAlt.SetColor("_EmColor", new Color(1f, 0.8f, 0f, 1));

            NewRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            NewRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            NewRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;
            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinLoaderDefault.meshReplacements.Length];
            skinLoaderDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            //MeshReplacements[0].mesh = skinLoaderAlt.meshReplacements[0].mesh;
            //MeshReplacements[2].mesh = skinLoaderAlt.meshReplacements[2].mesh;
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinLoaderWolfo_Simu",
                NameToken = "SIMU_SKIN_LOADER",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Green/skinIconLoader.png")),
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
            //LoaderSkinAlt(unlockableDef);
        }

        internal static void LoaderSkinRED()
        {
            SkinDef skinLoaderDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderDefault.asset").WaitForCompletion();
            SkinDef skinLoaderAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinLoaderAlt.rendererInfos.Length];
            System.Array.Copy(skinLoaderAlt.rendererInfos, NewRenderInfos, skinLoaderAlt.rendererInfos.Length);

            Material matTrimsheetConstructionLoaderAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[0].defaultMaterial);
            Material matLoaderPilotDiffuseAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[1].defaultMaterial);


            Texture2D texTrimSheetConstructionLoaderAlt = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Red/texTrimSheetConstructionLoaderAltNewRED.png");
            texTrimSheetConstructionLoaderAlt.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texLoaderPilotDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Red/texLoaderPilotDiffuseAltRED.png");
            texLoaderPilotDiffuse.wrapMode = TextureWrapMode.Clamp;

            matTrimsheetConstructionLoaderAlt.mainTexture = texTrimSheetConstructionLoaderAlt;
            matLoaderPilotDiffuseAlt.mainTexture = texLoaderPilotDiffuse;
            matLoaderPilotDiffuseAlt.SetColor("_EmColor", new Color(1f, 0.6f, 0f, 1));

            NewRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            NewRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            NewRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinLoaderWolfo_RED_Simu",
                NameToken = "SIMU_SKIN_LOADER_RED",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Loader/Red/skinIconLoaderRED.png")),
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = skinLoaderAlt.meshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
        }
        /*
        internal static void LoaderSkinAlt()
        {
            SkinDef skinLoaderDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderDefault.asset").WaitForCompletion();
            SkinDef skinLoaderAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinLoaderAlt.rendererInfos.Length];
            System.Array.Copy(skinLoaderAlt.rendererInfos, NewRenderInfos, skinLoaderAlt.rendererInfos.Length);

            Material matTrimsheetConstructionLoaderAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[0].defaultMaterial);
            Material matLoaderPilotDiffuseAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[1].defaultMaterial);


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

            NewRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            NewRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            NewRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;
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
                Name = "skinLoaderWolfoAlt_Simu",
                NameToken = "SIMU_SKIN_LOADER_ALT",
                Icon = SkinIconS,
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
        }
        */
        [RegisterAchievement("CLEAR_ANY_LOADER", "Skins.Loader.Wolfo.First", "DefeatSuperRoboBallBoss", 5, null)]
        public class ClearSimulacrumTreebotBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("LoaderBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_LOADER", "Skins.Loader.Wolfo.Both", "DefeatSuperRoboBallBoss", 5, null)]
        public class ClearSimulacrumTreebotBody2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("LoaderBody");
            }
        }
    }
}