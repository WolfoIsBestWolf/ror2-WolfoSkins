using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsSniper
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_SNIPER_NAME", "Sniper: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_SNIPER_DESCRIPTION", "As Sniper" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_SNIPER_NAME";
            unlockableDef.cachedName = "Skins.Sniper.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinSniperIcon);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
           
        }

        internal static void ModdedSkin(GameObject SniperBody)
        {
            Debug.Log("Sniper Skins");
            unlockableDef.hidden = false;
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            BodyIndex SniperIndex = SniperBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = SniperBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            SkinDef skinSniper = modelSkinController.skins[0];
            SkinDef skinSniperMASTERY = modelSkinController.skins[1];

            //0 matSniper
            //1 matSniper
            //2 matSniper
            //3 matSniper
            //4 matSniper
            
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinSniperMASTERY.rendererInfos.Length];
            System.Array.Copy(skinSniper.rendererInfos, NewRenderInfos, skinSniperMASTERY.rendererInfos.Length);

            Material matSniper = Object.Instantiate(skinSniperMASTERY.rendererInfos[0].defaultMaterial);
 
            Texture2D texSniperDefault = new Texture2D(128, 128, TextureFormat.RGB24, false);
            texSniperDefault.LoadImage(Properties.Resources.texSniperDefault, true);
            texSniperDefault.filterMode = FilterMode.Point;
            texSniperDefault.wrapMode = TextureWrapMode.Repeat;

            /*Texture2D texSniperDefault_Emission = new Texture2D(128, 128, TextureFormat.RGB24, false);
            texSniperDefault_Emission.LoadImage(Properties.Resources.texSniperDefault_Emission, true);
            texSniperDefault_Emission.filterMode = FilterMode.Point;
            texSniperDefault_Emission.wrapMode = TextureWrapMode.Repeat;*/

            matSniper.mainTexture = texSniperDefault;
            matSniper.SetColor("_EmColor", new Color(1f, 1f, 1f));
            //matSniper.SetTexture("_EmTex", texSniperDefault_Emission);

            NewRenderInfos[0].defaultMaterial = matSniper;
            NewRenderInfos[1].defaultMaterial = matSniper;
            NewRenderInfos[2].defaultMaterial = matSniper;
            NewRenderInfos[3].defaultMaterial = matSniper;
            NewRenderInfos[4].defaultMaterial = matSniper;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinSniperIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_SNIPER", "Dune");
            
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinSniperWolfo",
                NameToken = "SIMU_SKIN_SNIPER",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinSniper },
                RootObject = skinSniper.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinSniper.meshReplacements,
                //GameObjectActivations = skinSniper.gameObjectActivations,
                GameObjectActivations = skinSniperMASTERY.gameObjectActivations,
                ProjectileGhostReplacements = skinSniper.projectileGhostReplacements,
            };
            SkinDef SniperSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(SniperSkinDefNew);
            BodyCatalog.skins[(int)SniperIndex] = BodyCatalog.skins[(int)SniperIndex].Add(SniperSkinDefNew);
            ModdedSkinGRAY(SniperBody, unlockableDef);
        }

        internal static void ModdedSkinGRAY(GameObject SniperBody, UnlockableDef unlockableDef)
        {
            BodyIndex SniperIndex = SniperBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = SniperBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            SkinDef skinSniper = modelSkinController.skins[0];
            SkinDef skinSniperMASTERY = modelSkinController.skins[1];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinSniperMASTERY.rendererInfos.Length];
            System.Array.Copy(skinSniper.rendererInfos, NewRenderInfos, skinSniperMASTERY.rendererInfos.Length);

            Material matSniper = Object.Instantiate(skinSniperMASTERY.rendererInfos[0].defaultMaterial);

            Texture2D texSniperDefault = new Texture2D(128, 128, TextureFormat.RGB24, false);
            texSniperDefault.LoadImage(Properties.Resources.texSniperDefaultGRAY, true);
            texSniperDefault.filterMode = FilterMode.Point;
            texSniperDefault.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSniperDefault_Emission = new Texture2D(128, 128, TextureFormat.RGB24, false);
            texSniperDefault_Emission.LoadImage(Properties.Resources.texSniperDefault_EmissionGRAY, true);
            texSniperDefault_Emission.filterMode = FilterMode.Point;
            texSniperDefault_Emission.wrapMode = TextureWrapMode.Repeat;

            matSniper.mainTexture = texSniperDefault;
            matSniper.SetColor("_EmColor", new Color(1f, 1f, 1f));
            matSniper.SetTexture("_EmTex", texSniperDefault_Emission);

            NewRenderInfos[0].defaultMaterial = matSniper;
            NewRenderInfos[1].defaultMaterial = matSniper;
            NewRenderInfos[2].defaultMaterial = matSniper;
            NewRenderInfos[3].defaultMaterial = matSniper;
            NewRenderInfos[4].defaultMaterial = matSniper;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinSniperIconGRAY, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_SNIPER_GRAY", "Lone");

            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinSniperWolfo_GRAY",
                NameToken = "SIMU_SKIN_SNIPER_GRAY",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinSniper },
                RootObject = skinSniper.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinSniper.meshReplacements,
                //GameObjectActivations = skinSniper.gameObjectActivations,
                GameObjectActivations = skinSniperMASTERY.gameObjectActivations,
                ProjectileGhostReplacements = skinSniper.projectileGhostReplacements,
            };
            SkinDef SniperSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(SniperSkinDefNew);
            BodyCatalog.skins[(int)SniperIndex] = BodyCatalog.skins[(int)SniperIndex].Add(SniperSkinDefNew);
        }

        [RegisterAchievement("SIMU_SKIN_SNIPER", "Skins.Sniper.Wolfo", null, 5, null)]
        public class ClearSimulacrumSniperClassic : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("SniperClassicBody");
            }
        }

    }
}