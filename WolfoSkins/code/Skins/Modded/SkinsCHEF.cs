using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsCHEF
    {
        public static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_CHEF_BLACK", "Rotisseur");
            LanguageAPI.Add("SIMU_SKIN_CHEF_RED", "Boucher");
            LanguageAPI.Add("SIMU_SKIN_CHEF_GREEN", "Entremetier");
            LanguageAPI.Add("SIMU_SKIN_CHEF_BLUE", "Poissonier");
            LanguageAPI.Add("SIMU_SKIN_CHEF_CYAN", "Saucier");
            LanguageAPI.Add("SIMU_SKIN_CHEF_PROVI", "Sous Chef");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_NAME", "CHEF: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_DESCRIPTION", "As CHEF" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CHEF_NAME";
            unlockableDef.cachedName = "Skins.Chef.Wolfo";
            unlockableDef.hidden = true;
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinChefIconRed);
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
        }

        internal static void ModdedSkin(GameObject ChefBody)
        {
            Debug.Log("CHEF Skins");
            unlockableDef.hidden = false;
            BodyIndex ChefIndex = ChefBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ChefBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinChef = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfosBLACK = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosBLACK, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRED = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosRED, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosGREEN = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosGREEN, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosBLUE = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosBLUE, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosCYAN = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosCYAN, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosPROVI = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosPROVI, skinChef.rendererInfos.Length);

            Material matChefBLACK = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefRED = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefGREEN = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefBLUE = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefCYAN = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefPROVI = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);

            Texture2D texChefDefault = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefDefault.LoadImage(Properties.Resources.texChefDefaultBLACK, true);
            texChefDefault.filterMode = FilterMode.Bilinear;
            texChefDefault.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefRed = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefRed.LoadImage(Properties.Resources.texChefRed, true);
            texChefRed.filterMode = FilterMode.Bilinear;
            texChefRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefGREEN = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefGREEN.LoadImage(Properties.Resources.texChefGreen, true);
            texChefGREEN.filterMode = FilterMode.Bilinear;
            texChefGREEN.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefBLUE = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefBLUE.LoadImage(Properties.Resources.texChefDefaultBLUE, true);
            texChefBLUE.filterMode = FilterMode.Bilinear;
            texChefBLUE.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefCYAN = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefCYAN.LoadImage(Properties.Resources.texChefDefaultCYAN, true);
            texChefCYAN.filterMode = FilterMode.Bilinear;
            texChefCYAN.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefPROVI = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefPROVI.LoadImage(Properties.Resources.texChefDefaultPROVI, true);
            texChefPROVI.filterMode = FilterMode.Bilinear;
            texChefPROVI.wrapMode = TextureWrapMode.Clamp;

            matChefBLACK.mainTexture = texChefDefault;
            matChefRED.mainTexture = texChefRed;
            matChefGREEN.mainTexture = texChefGREEN;
            matChefBLUE.mainTexture = texChefBLUE;
            matChefCYAN.mainTexture = texChefCYAN;
            matChefPROVI.mainTexture = texChefPROVI;
            matChefPROVI.SetColor("_EmColror", new Color(0.1f, 0.1f, 0.1f, 0.1f));
            matChefPROVI.SetTexture("_EmTex", texChefPROVI);

            NewRenderInfosBLACK[0].defaultMaterial = matChefBLACK;
            NewRenderInfosRED[0].defaultMaterial = matChefRED;
            NewRenderInfosGREEN[0].defaultMaterial = matChefGREEN;
            NewRenderInfosBLUE[0].defaultMaterial = matChefBLUE;
            NewRenderInfosCYAN[0].defaultMaterial = matChefCYAN;
            NewRenderInfosPROVI[0].defaultMaterial = matChefPROVI;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinChefIconBLACK, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            Texture2D skinChefIconRed = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconRed.LoadImage(Properties.Resources.skinChefIconRed, true);
            skinChefIconRed.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconRedS = Sprite.Create(skinChefIconRed, WRect.rec128, WRect.half);
            //
            Texture2D skinChefIconGREEN = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconGREEN.LoadImage(Properties.Resources.skinChefIconGreen, true);
            skinChefIconGREEN.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconGREENS = Sprite.Create(skinChefIconGREEN, WRect.rec128, WRect.half);

            Texture2D skinChefIconBLUE = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconBLUE.LoadImage(Properties.Resources.skinChefIconBLUE, true);
            skinChefIconBLUE.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconBLUES = Sprite.Create(skinChefIconBLUE, WRect.rec128, WRect.half);

            Texture2D skinChefIconCYAN = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconCYAN.LoadImage(Properties.Resources.skinChefIconCYAN, true);
            skinChefIconCYAN.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconCYANS = Sprite.Create(skinChefIconCYAN, WRect.rec128, WRect.half);
            
            Texture2D skinChefIconPROVI = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconPROVI.LoadImage(Properties.Resources.skinChefIconProvi, true);
            skinChefIconPROVI.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconPROVIS = Sprite.Create(skinChefIconPROVI, WRect.rec128, WRect.half);
            //
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_Black",
                NameToken = "SIMU_SKIN_CHEF_BLACK",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosBLACK,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoRED = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_Red",
                NameToken = "SIMU_SKIN_CHEF_RED",
                Icon = skinChefIconRedS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosRED,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoGREEN = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_Green",
                NameToken = "SIMU_SKIN_CHEF_GREEN",
                Icon = skinChefIconGREENS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosGREEN,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoBLUE = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_BLUE",
                NameToken = "SIMU_SKIN_CHEF_BLUE",
                Icon = skinChefIconBLUES,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosBLUE,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoCYAN = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_CYAN",
                NameToken = "SIMU_SKIN_CHEF_CYAN",
                Icon = skinChefIconCYANS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosCYAN,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoPROVI = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo_PROVI",
                NameToken = "SIMU_SKIN_CHEF_PROVI",
                Icon = skinChefIconPROVIS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosPROVI,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            SkinDef ChefSkinDefBLACK = Skins.CreateNewSkinDef(SkinInfo);
            SkinDef ChefSkinDefRED = Skins.CreateNewSkinDef(SkinInfoRED);
            SkinDef ChefSkinDefGREEN = Skins.CreateNewSkinDef(SkinInfoGREEN);
            SkinDef ChefSkinDefBLUE = Skins.CreateNewSkinDef(SkinInfoBLUE);
            SkinDef ChefSkinDefCYAN = Skins.CreateNewSkinDef(SkinInfoCYAN);
            SkinDef ChefSkinDefPROVI = Skins.CreateNewSkinDef(SkinInfoPROVI);

            modelSkinController.skins = modelSkinController.skins.Add(ChefSkinDefRED, ChefSkinDefGREEN, ChefSkinDefBLUE, ChefSkinDefBLACK, ChefSkinDefCYAN, ChefSkinDefPROVI);
            BodyCatalog.skins[(int)ChefIndex] = BodyCatalog.skins[(int)ChefIndex].Add(ChefSkinDefRED, ChefSkinDefGREEN, ChefSkinDefBLUE, ChefSkinDefBLACK, ChefSkinDefCYAN, ChefSkinDefPROVI);
        }



        [RegisterAchievement("SIMU_SKIN_CHEF", "Skins.Chef.Wolfo", null, null)]
        public class ClearSimulacrumCHEF : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CHEF");
            }
        }
    }
}