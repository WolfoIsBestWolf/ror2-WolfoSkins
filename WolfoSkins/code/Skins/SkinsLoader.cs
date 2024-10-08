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


            Texture2D texTrimSheetConstructionLoaderAlt = new Texture2D(256, 512, TextureFormat.DXT5, false);
            texTrimSheetConstructionLoaderAlt.LoadImage(Properties.Resources.texTrimSheetConstructionLoaderAlt1, true);
            texTrimSheetConstructionLoaderAlt.filterMode = FilterMode.Bilinear;
            texTrimSheetConstructionLoaderAlt.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texLoaderPilotDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texLoaderPilotDiffuse.LoadImage(Properties.Resources.texLoaderPilotDiffuse, true);
            texLoaderPilotDiffuse.filterMode = FilterMode.Bilinear;
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
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconLoader, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_LOADER", "Eco Friendly");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_LOADER_NAME", "Loader: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_LOADER_DESCRIPTION", "As Loader"+ Unlocks.unlockCondition);

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_LOADER_NAME";
            unlockableDef.cachedName = "Skins.Loader.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            //

            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinLoaderWolfo",
                NameToken = "SIMU_SKIN_LOADER",
                Icon = SkinIconS,
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
                UnlockableDef = unlockableDef,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
            //LoaderSkinAlt(unlockableDef);
            LoaderSkinRED(unlockableDef);
        }

        internal static void LoaderSkinRED(UnlockableDef unlockableDef)
        {
            SkinDef skinLoaderDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderDefault.asset").WaitForCompletion();
            SkinDef skinLoaderAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinLoaderAlt.rendererInfos.Length];
            System.Array.Copy(skinLoaderAlt.rendererInfos, NewRenderInfos, skinLoaderAlt.rendererInfos.Length);

            Material matTrimsheetConstructionLoaderAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[0].defaultMaterial);
            Material matLoaderPilotDiffuseAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[1].defaultMaterial);


            Texture2D texTrimSheetConstructionLoaderAlt = new Texture2D(256, 512, TextureFormat.DXT5, false);
            texTrimSheetConstructionLoaderAlt.LoadImage(Properties.Resources.texTrimSheetConstructionLoaderAltNewRED, true);
            texTrimSheetConstructionLoaderAlt.filterMode = FilterMode.Bilinear;
            texTrimSheetConstructionLoaderAlt.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texLoaderPilotDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texLoaderPilotDiffuse.LoadImage(Properties.Resources.texLoaderPilotDiffuseAltRED, true);
            texLoaderPilotDiffuse.filterMode = FilterMode.Bilinear;
            texLoaderPilotDiffuse.wrapMode = TextureWrapMode.Clamp;

            matTrimsheetConstructionLoaderAlt.mainTexture = texTrimSheetConstructionLoaderAlt;
            matLoaderPilotDiffuseAlt.mainTexture = texLoaderPilotDiffuse;
            matLoaderPilotDiffuseAlt.SetColor("_EmColor", new Color(1f, 0.6f, 0f, 1));

            NewRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            NewRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            NewRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconLoaderRED, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            LanguageAPI.Add("SIMU_SKIN_LOADER_RED", "Saftey Red");

            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinLoaderWolfo_RED",
                NameToken = "SIMU_SKIN_LOADER_RED",
                Icon = SkinIconS,
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = skinLoaderAlt.meshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
                UnlockableDef = unlockableDef,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
        }


        internal static void LoaderSkinAlt(UnlockableDef unlockableDef)
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
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconLoaderALT, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_LOADER_ALT", "Casual");
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinLoaderDefault.meshReplacements.Length];
            skinLoaderDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[1].mesh = skinLoaderAlt.meshReplacements[1].mesh;
            //MeshReplacements[2].mesh = skinLoaderAlt.meshReplacements[2].mesh;


            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinLoaderWolfoAlt",
                NameToken = "SIMU_SKIN_LOADER_ALT",
                Icon = SkinIconS,
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
                UnlockableDef = unlockableDef,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
        }

        [RegisterAchievement("SIMU_SKIN_LOADER", "Skins.Loader.Wolfo", "DefeatSuperRoboBallBoss", 5, null)]
        public class ClearSimulacrumTreebotBody : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("LoaderBody");
            }
        }

        internal static void PrismAchievement()
        {
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_LOADER_NAME", "Loader" + Unlocks.unlockNamePrism);
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_LOADER_DESCRIPTION", "As Loader" + Unlocks.unlockConditionPrism);
            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_LOADER_NAME";
            unlockableDef.cachedName = "Skins.Loader.Wolfo.Prism";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
        }

        /*[RegisterAchievement("PRISM_SKIN_LOADER", "Skins.Loader.Wolfo.Prism", null, 5, null)]
        public class AchievementPrismaticDissoLoader2Body : AchievementPrismaticDisso
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("LoaderBody");
            }
        }*/
    }
}