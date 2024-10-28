using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsSniper
    {
        internal static void ModdedSkin(GameObject SniperBody)
        {
            ModdedSkin_Orange(SniperBody);
            ModdedSkinGRAY(SniperBody);
            
        }

        internal static void ModdedSkin_Orange(GameObject SniperBody)
        {
            Debug.Log("Sniper Skins");
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
 
            Texture2D texSniperDefault = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/texSniperDefault.png");
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
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinSniperWolfo_Simu",
                NameToken = "SIMU_SKIN_SNIPER",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/skinSniperIcon.png")),
                BaseSkins = new SkinDef[] { skinSniper },
                RootObject = skinSniper.rootObject,
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

        internal static void ModdedSkinGRAY(GameObject SniperBody)
        {
            BodyIndex SniperIndex = SniperBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = SniperBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            SkinDef skinSniper = modelSkinController.skins[0];
            SkinDef skinSniperMASTERY = modelSkinController.skins[1];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinSniperMASTERY.rendererInfos.Length];
            System.Array.Copy(skinSniper.rendererInfos, NewRenderInfos, skinSniperMASTERY.rendererInfos.Length);

            Material matSniper = Object.Instantiate(skinSniperMASTERY.rendererInfos[0].defaultMaterial);

            //Texture2D texSniperDefault = new Texture2D(128, 128, TextureFormat.RGB24, false);
            Texture2D texSniperDefault = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/texSniperDefaultGRAY.png");
            texSniperDefault.filterMode = FilterMode.Point;
            texSniperDefault.wrapMode = TextureWrapMode.Repeat;

            //Texture2D texSniperDefault_Emission = new Texture2D(128, 128, TextureFormat.RGB24, false);
            Texture2D texSniperDefault_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/texSniperDefault_EmissionGRAY.png");
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
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinSniperWolfo_GRAY_Simu",
                NameToken = "SIMU_SKIN_SNIPER_GRAY",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/skinSniperIconGRAY.png")),
                BaseSkins = new SkinDef[] { skinSniper },
                RootObject = skinSniper.rootObject,
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

        [RegisterAchievement("CLEAR_ANY_SNIPERCLASSIC", "Skins.SniperClassic.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumSniperClassic : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("SniperClassicBody");
            }
        }

    }
}