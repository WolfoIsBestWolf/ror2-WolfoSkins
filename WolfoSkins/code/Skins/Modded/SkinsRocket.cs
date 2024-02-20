using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsRocket
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_ROCKET_RED", "Reliable Excavation Demolition");
            LanguageAPI.Add("SIMU_SKIN_ROCKET_BLUE", "Builders League United");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ROCKET_NAME", "Rocket: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ROCKET_DESCRIPTION", "As Rocket" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_ROCKET_NAME";
            unlockableDef.cachedName = "Skins.Rocket.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.texRocketSkinDefault);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
        }

        internal static void ModdedSkin(GameObject RocketBody)
        {
            Debug.Log("Rocket Skins");
            unlockableDef.hidden = false;

            SkinRED(RocketBody);
            SkinBLUE(RocketBody);
        }

        internal static void SkinRED(GameObject RocketBody)
        {       
            BodyIndex RocketIndex = RocketBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RocketBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinRocket = modelSkinController.skins[0];

            //0 MatRocketBackpack
            //1 GrenadeMaterial (no texture)
            //2 MatGrenade
            //3 MatBlackBox
            //4 MatRocket
            //5 MatShovel
            //6 MatRocket

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRocket.rendererInfos.Length];
            System.Array.Copy(skinRocket.rendererInfos, NewRenderInfos, skinRocket.rendererInfos.Length);

            Material MatRocketBackpack = Object.Instantiate(skinRocket.rendererInfos[0].defaultMaterial);
            Material MatBlackBox = Object.Instantiate(skinRocket.rendererInfos[3].defaultMaterial);
            Material MatRocket = Object.Instantiate(skinRocket.rendererInfos[4].defaultMaterial);
            Material MatShovel = Object.Instantiate(skinRocket.rendererInfos[5].defaultMaterial);

            Texture2D TexRocketBackpack = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            TexRocketBackpack.LoadImage(Properties.Resources.TexRocketBackpack, true);
            TexRocketBackpack.filterMode = FilterMode.Bilinear;
            TexRocketBackpack.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexBlackBox = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            TexBlackBox.LoadImage(Properties.Resources.TexBlackBox, true);
            TexBlackBox.filterMode = FilterMode.Bilinear;
            TexBlackBox.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocket = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            TexRocket.LoadImage(Properties.Resources.TexRocket, true);
            TexRocket.filterMode = FilterMode.Bilinear;
            TexRocket.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocketEmissive = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            TexRocketEmissive.LoadImage(Properties.Resources.TexRocketEmissive, true);
            TexRocketEmissive.filterMode = FilterMode.Bilinear;
            TexRocketEmissive.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexShovel = new Texture2D(512, 512, TextureFormat.DXT5, false);
            TexShovel.LoadImage(Properties.Resources.TexShovel, true);
            TexShovel.filterMode = FilterMode.Bilinear;
            TexShovel.wrapMode = TextureWrapMode.Repeat;


            MatRocketBackpack.mainTexture = TexRocketBackpack;
            MatBlackBox.mainTexture = TexBlackBox;
            MatBlackBox.color = new Color(1, 0.85f, 0f);
            MatRocket.mainTexture = TexRocket;
            MatRocket.color = new Color(1, 0.8f, 0.8f);
            MatRocket.SetTexture("_EmTex", TexRocketEmissive);
            MatRocket.SetTexture("_EmissionMap", TexRocketEmissive);
            MatShovel.mainTexture = TexShovel;

            //
            NewRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //NewRenderInfos[1].defaultMaterial = GrenadeMaterial;
            //NewRenderInfos[2].defaultMaterial = MatGrenade;
            NewRenderInfos[3].defaultMaterial = MatBlackBox;
            NewRenderInfos[4].defaultMaterial = MatRocket;
            NewRenderInfos[5].defaultMaterial = MatShovel;
            NewRenderInfos[6].defaultMaterial = MatRocket;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.texRocketSkinDefault, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinRocketWolfo_Red",
                NameToken = "SIMU_SKIN_ROCKET_RED",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinRocket },
                RootObject = skinRocket.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinRocket.meshReplacements,
                GameObjectActivations = skinRocket.gameObjectActivations,
                ProjectileGhostReplacements = skinRocket.projectileGhostReplacements,
            };
            SkinDef RocketSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(RocketSkinDefNew);
            BodyCatalog.skins[(int)RocketIndex] = BodyCatalog.skins[(int)RocketIndex].Add(RocketSkinDefNew);
        }

        internal static void SkinBLUE(GameObject RocketBody)
        {
            BodyIndex RocketIndex = RocketBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RocketBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinRocket = modelSkinController.skins[0];

            //0 MatRocketBackpack
            //1 GrenadeMaterial (no texture)
            //2 MatGrenade
            //3 MatBlackBox
            //4 MatRocket
            //5 MatShovel
            //6 MatRocket

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRocket.rendererInfos.Length];
            System.Array.Copy(skinRocket.rendererInfos, NewRenderInfos, skinRocket.rendererInfos.Length);

            Material MatRocketBackpack = Object.Instantiate(skinRocket.rendererInfos[0].defaultMaterial);
            Material MatBlackBox = Object.Instantiate(skinRocket.rendererInfos[3].defaultMaterial);
            Material MatRocket = Object.Instantiate(skinRocket.rendererInfos[4].defaultMaterial);
            Material MatShovel = Object.Instantiate(skinRocket.rendererInfos[5].defaultMaterial);
            Material MatGrenade = Object.Instantiate(skinRocket.rendererInfos[2].defaultMaterial);

            Texture2D TexRocketBackpack = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            TexRocketBackpack.LoadImage(Properties.Resources.TexRocketBackpackBLUE, true);
            TexRocketBackpack.filterMode = FilterMode.Bilinear;
            TexRocketBackpack.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexBlackBox = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            TexBlackBox.LoadImage(Properties.Resources.TexBlackBoxBLUE, true);
            TexBlackBox.filterMode = FilterMode.Bilinear;
            TexBlackBox.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocket = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            TexRocket.LoadImage(Properties.Resources.TexRocketBLUE, true);
            TexRocket.filterMode = FilterMode.Bilinear;
            TexRocket.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocketEmissive = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            TexRocketEmissive.LoadImage(Properties.Resources.TexRocketEmissiveBLUE, true);
            TexRocketEmissive.filterMode = FilterMode.Bilinear;
            TexRocketEmissive.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexShovel = new Texture2D(512, 512, TextureFormat.DXT5, false);
            TexShovel.LoadImage(Properties.Resources.TexShovelBLUE, true);
            TexShovel.filterMode = FilterMode.Bilinear;
            TexShovel.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexGrenade = new Texture2D(512, 512, TextureFormat.DXT5, false);
            TexGrenade.LoadImage(Properties.Resources.TexGrenadeBLUE, true);
            TexGrenade.filterMode = FilterMode.Bilinear;
            TexGrenade.wrapMode = TextureWrapMode.Repeat;


            MatRocketBackpack.mainTexture = TexRocketBackpack;
            MatBlackBox.mainTexture = TexBlackBox;
            MatBlackBox.color = new Color(1, 1f, 1f);
            MatRocket.mainTexture = TexRocket;
            MatRocket.color = new Color(0.8f, 1f, 0.9f);
            MatRocket.SetTexture("_EmTex", TexRocketEmissive);
            MatRocket.SetTexture("_EmissionMap", TexRocketEmissive);
            MatRocketBackpack.color = new Color(0.8f, 1f, 0.8f);
            MatShovel.mainTexture = TexShovel;

            MatGrenade.mainTexture = TexGrenade;

            //
            NewRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //NewRenderInfos[1].defaultMaterial = GrenadeMaterial;
            NewRenderInfos[2].defaultMaterial = MatGrenade;
            NewRenderInfos[3].defaultMaterial = MatBlackBox;
            NewRenderInfos[4].defaultMaterial = MatRocket;
            NewRenderInfos[5].defaultMaterial = MatShovel;
            NewRenderInfos[6].defaultMaterial = MatRocket;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.texRocketSkinDefaultBLUE, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinRocketWolfo_Blue",
                NameToken = "SIMU_SKIN_ROCKET_BLUE",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinRocket },
                RootObject = skinRocket.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinRocket.meshReplacements,
                GameObjectActivations = skinRocket.gameObjectActivations,
                ProjectileGhostReplacements = skinRocket.projectileGhostReplacements,
            };
            SkinDef RocketSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(RocketSkinDefNew);
            BodyCatalog.skins[(int)RocketIndex] = BodyCatalog.skins[(int)RocketIndex].Add(RocketSkinDefNew);
        }


        [RegisterAchievement("SIMU_SKIN_ROCKET", "Skins.Rocket.Wolfo", null, null)]
        public class ClearSimulacrumRocket : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RocketSurvivorBody");
            }
        }
        
    }
}