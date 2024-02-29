using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsArsonist
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_ARSONIST_ORANGE", "Ember");
            LanguageAPI.Add("SIMU_SKIN_ARSONIST_BLUE", "Azure");
            LanguageAPI.Add("SIMU_SKIN_ARSONIST_GM_BLUE", "Lightningbug");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ARSONIST_NAME", "Arsonist: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ARSONIST_DESCRIPTION", "As Arsonist" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_ARSONIST_NAME";
            unlockableDef.cachedName = "Skins.Arsonist.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinArsonistIcon);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
        }

        internal static void ModdedSkin(GameObject ArsonistBody)
        {
            Debug.Log("Arsonist Skins");
            unlockableDef.hidden = false;
            BodyIndex ArsonistIndex = ArsonistBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ArsonistBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinArsonist = modelSkinController.skins[0];

            //0 matArsonist
            //1 matArsonistMetal
            //2 matArsonistMetal
            //3 matArsonistMetal
            //4 matArsonistMetal
            //5 matArsonistMetal
            //6 matArsonistMetal
            //7 null
            //8 matArsonistCloth

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinArsonist.rendererInfos.Length];
            System.Array.Copy(skinArsonist.rendererInfos, NewRenderInfos, skinArsonist.rendererInfos.Length);

            Material matArsonist = Object.Instantiate(skinArsonist.rendererInfos[0].defaultMaterial);
            Material matArsonistMetal = Object.Instantiate(skinArsonist.rendererInfos[1].defaultMaterial);
            Material matArsonistCloth = Object.Instantiate(skinArsonist.rendererInfos[8].defaultMaterial);

            Texture2D Arsonist_diffuse = new Texture2D(512, 512, TextureFormat.RGB24, false);
            Arsonist_diffuse.LoadImage(Properties.Resources.Arsonist_diffuse, true);
            Arsonist_diffuse.filterMode = FilterMode.Bilinear;
            Arsonist_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_diffuse = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
            ArsonistMetal_diffuse.LoadImage(Properties.Resources.ArsonistMetal_diffuse, true);
            ArsonistMetal_diffuse.filterMode = FilterMode.Bilinear;
            ArsonistMetal_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_emission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            ArsonistMetal_emission.LoadImage(Properties.Resources.ArsonistMetal_emission, true);
            ArsonistMetal_emission.filterMode = FilterMode.Bilinear;
            ArsonistMetal_emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistCloth_diffuse = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
            ArsonistCloth_diffuse.LoadImage(Properties.Resources.ArsonistCloth_diffuse, true);
            ArsonistCloth_diffuse.filterMode = FilterMode.Bilinear;
            ArsonistCloth_diffuse.wrapMode = TextureWrapMode.Repeat;


            matArsonist.mainTexture = Arsonist_diffuse;

            matArsonistMetal.mainTexture = ArsonistMetal_diffuse;
            matArsonistMetal.SetTexture("_EmTex", ArsonistMetal_emission);
            matArsonistMetal.SetTexture("_EmissionMap", ArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matArsonistCloth.mainTexture = ArsonistCloth_diffuse;


            NewRenderInfos[0].defaultMaterial = matArsonist;
            NewRenderInfos[1].defaultMaterial = matArsonistMetal;
            NewRenderInfos[2].defaultMaterial = matArsonistMetal;
            NewRenderInfos[3].defaultMaterial = matArsonistMetal;
            NewRenderInfos[4].defaultMaterial = matArsonistMetal;
            NewRenderInfos[5].defaultMaterial = matArsonistMetal;
            NewRenderInfos[6].defaultMaterial = matArsonistMetal;
            //NewRenderInfos[7].defaultMaterial = null;
            NewRenderInfos[8].defaultMaterial = matArsonistCloth;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinArsonistIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec256, WRect.half);
            //
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinArsonistWolfo_ORANGE",
                NameToken = "SIMU_SKIN_ARSONIST_ORANGE",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinArsonist },
                RootObject = skinArsonist.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinArsonist.meshReplacements,
                GameObjectActivations = skinArsonist.gameObjectActivations,
                ProjectileGhostReplacements = skinArsonist.projectileGhostReplacements,
            };
            SkinDef ArsonistSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(ArsonistSkinDefNew);
            BodyCatalog.skins[(int)ArsonistIndex] = BodyCatalog.skins[(int)ArsonistIndex].Add(ArsonistSkinDefNew);
            ModdedSkinBlue(ArsonistBody);
            ModdedSkinBlueGM(ArsonistBody);
        }

        internal static void ModdedSkinBlue(GameObject ArsonistBody)
        {
            BodyIndex ArsonistIndex = ArsonistBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ArsonistBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinArsonist = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinArsonist.rendererInfos.Length];
            System.Array.Copy(skinArsonist.rendererInfos, NewRenderInfos, skinArsonist.rendererInfos.Length);

            Material matArsonist = Object.Instantiate(skinArsonist.rendererInfos[0].defaultMaterial);
            Material matArsonistMetal = Object.Instantiate(skinArsonist.rendererInfos[1].defaultMaterial);
            Material matArsonistCloth = Object.Instantiate(skinArsonist.rendererInfos[8].defaultMaterial);

            Texture2D Arsonist_diffuse = new Texture2D(512, 512, TextureFormat.RGB24, false);
            Arsonist_diffuse.LoadImage(Properties.Resources.Arsonist_diffuseBLUE, true);
            Arsonist_diffuse.filterMode = FilterMode.Bilinear;
            Arsonist_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_diffuse = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
            ArsonistMetal_diffuse.LoadImage(Properties.Resources.ArsonistMetal_diffuseBLUE, true);
            ArsonistMetal_diffuse.filterMode = FilterMode.Bilinear;
            ArsonistMetal_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_emission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            ArsonistMetal_emission.LoadImage(Properties.Resources.ArsonistMetal_emissionBLUE, true);
            ArsonistMetal_emission.filterMode = FilterMode.Bilinear;
            ArsonistMetal_emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistCloth_diffuse = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
            ArsonistCloth_diffuse.LoadImage(Properties.Resources.ArsonistCloth_diffuseBLUE, true);
            ArsonistCloth_diffuse.filterMode = FilterMode.Bilinear;
            ArsonistCloth_diffuse.wrapMode = TextureWrapMode.Repeat;


            matArsonist.mainTexture = Arsonist_diffuse;

            matArsonistMetal.mainTexture = ArsonistMetal_diffuse;
            matArsonistMetal.SetTexture("_EmTex", ArsonistMetal_emission);
            matArsonistMetal.SetTexture("_EmissionMap", ArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matArsonistCloth.mainTexture = ArsonistCloth_diffuse;


            NewRenderInfos[0].defaultMaterial = matArsonist;
            NewRenderInfos[1].defaultMaterial = matArsonistMetal;
            NewRenderInfos[2].defaultMaterial = matArsonistMetal;
            NewRenderInfos[3].defaultMaterial = matArsonistMetal;
            NewRenderInfos[4].defaultMaterial = matArsonistMetal;
            NewRenderInfos[5].defaultMaterial = matArsonistMetal;
            NewRenderInfos[6].defaultMaterial = matArsonistMetal;
            //NewRenderInfos[7].defaultMaterial = null;
            NewRenderInfos[8].defaultMaterial = matArsonistCloth;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinArsonistIconBlue, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec256, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinArsonistWolfo_BLUE",
                NameToken = "SIMU_SKIN_ARSONIST_BLUE",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinArsonist },
                RootObject = skinArsonist.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinArsonist.meshReplacements,
                GameObjectActivations = skinArsonist.gameObjectActivations,
                ProjectileGhostReplacements = skinArsonist.projectileGhostReplacements,
            };
            SkinDef ArsonistSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(ArsonistSkinDefNew);
            BodyCatalog.skins[(int)ArsonistIndex] = BodyCatalog.skins[(int)ArsonistIndex].Add(ArsonistSkinDefNew);

        }

        internal static void ModdedSkinBlueGM(GameObject ArsonistBody)
        {
            BodyIndex ArsonistIndex = ArsonistBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ArsonistBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinArsonist = modelSkinController.skins[2];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinArsonist.rendererInfos.Length];
            System.Array.Copy(skinArsonist.rendererInfos, NewRenderInfos, skinArsonist.rendererInfos.Length);

            //0 matNeoArsonistMetal
            //1 matNeoArsonistMetal
            //2 matNeoArsonistMetal
            //3 matNeoArsonistMetal
            //4 matNeoArsonistMetal
            //5 matNeoArsonistMetal
            //6 matNeoArsonistMetal
            //7 null
            //8 matArsonistCloth

            Material matNeoArsonistMetal = Object.Instantiate(skinArsonist.rendererInfos[0].defaultMaterial);
            Material matNeoArsonistCloth = Object.Instantiate(skinArsonist.rendererInfos[8].defaultMaterial);

 

            Texture2D NeoArsonistMetal_diffuse = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            NeoArsonistMetal_diffuse.LoadImage(Properties.Resources.NeoArsonistMetal_diffuse, true);
            NeoArsonistMetal_diffuse.filterMode = FilterMode.Bilinear;
            NeoArsonistMetal_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D NeoArsonistMetal_emission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            NeoArsonistMetal_emission.LoadImage(Properties.Resources.NeoArsonistMetal_emission, true);
            NeoArsonistMetal_emission.filterMode = FilterMode.Bilinear;
            NeoArsonistMetal_emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D NeoArsonistCloth_diffuse = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            NeoArsonistCloth_diffuse.LoadImage(Properties.Resources.NeoArsonistCloth_diffuse, true);
            NeoArsonistCloth_diffuse.filterMode = FilterMode.Bilinear;
            NeoArsonistCloth_diffuse.wrapMode = TextureWrapMode.Repeat;


            matNeoArsonistMetal.mainTexture = NeoArsonistMetal_diffuse;
            matNeoArsonistMetal.SetTexture("_EmTex", NeoArsonistMetal_emission);
            matNeoArsonistMetal.SetTexture("_EmissionMap", NeoArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matNeoArsonistCloth.mainTexture = NeoArsonistCloth_diffuse;

            NewRenderInfos[0].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[1].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[2].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[3].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[4].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[5].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[6].defaultMaterial = matNeoArsonistMetal;
            //NewRenderInfos[7].defaultMaterial = null;
            NewRenderInfos[8].defaultMaterial = matNeoArsonistCloth;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinArsonistIconGMBLue, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec256, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinArsonistWolfo_GM_BLUE",
                NameToken = "SIMU_SKIN_ARSONIST_GM_BLUE",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinArsonist },
                RootObject = skinArsonist.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinArsonist.meshReplacements,
                GameObjectActivations = skinArsonist.gameObjectActivations,
                ProjectileGhostReplacements = skinArsonist.projectileGhostReplacements,
            };
            SkinDef ArsonistSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(ArsonistSkinDefNew);
            BodyCatalog.skins[(int)ArsonistIndex] = BodyCatalog.skins[(int)ArsonistIndex].Add(ArsonistSkinDefNew);
        }


        [RegisterAchievement("SIMU_SKIN_ARSONIST", "Skins.Arsonist.Wolfo", null, null)]
        public class ClearSimulacrumArsonistClassic : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ArsonistClassicBody");
            }
        }

    }
}