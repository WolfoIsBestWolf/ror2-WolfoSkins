using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod
{
    public class SkinsSniper
    {
        internal static void ModdedSkin(GameObject SniperBody)
        {
            Debug.Log("Sniper Skins");
            BodyIndex SniperIndex = SniperBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = SniperBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            SkinDef skinSniper = modelSkinController.skins[0];
            SkinDef skinSniperMASTERY = modelSkinController.skins[1];

            SkinDef orange = ModdedSkin_Orange(skinSniper, skinSniperMASTERY.ReturnParams());
            SkinDef grays = ModdedSkinGRAY(skinSniper, skinSniperMASTERY.ReturnParams());

            SkinCatalog.skinsByBody[(int)SniperIndex] = modelSkinController.skins;
            //0 matSniper
            //1 matSniper
            //2 matSniper
            //3 matSniper
            //4 matSniper
        }

        internal static SkinDef ModdedSkin_Orange(SkinDef skinSniper, SkinDefParams skinSniperMASTERY)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinSniperMix_1",
                nameToken = "SIMU_SKIN_SNIPER",
                icon = H.GetIcon("mod/ror1/sniper_orange"),
                original = skinSniper,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matSniper = CloneFromOriginal(skinSniperMASTERY, 0);

            matSniper.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/texSniperDefault.png");
            matSniper.SetColor("_EmColor", new Color(1f, 1f, 1f));
            //matSniper.SetTexture("_EmTex", texSniperDefault_Emission);

            newRenderInfos[0].defaultMaterial = matSniper;
            newRenderInfos[1].defaultMaterial = matSniper;
            newRenderInfos[2].defaultMaterial = matSniper;
            newRenderInfos[3].defaultMaterial = matSniper;
            newRenderInfos[4].defaultMaterial = matSniper;
            newSkinDef.skinDefParams.gameObjectActivations = skinSniperMASTERY.gameObjectActivations;
            return newSkinDef;
        }

        internal static SkinDef ModdedSkinGRAY(SkinDef skinSniper, SkinDefParams skinSniperMASTERY)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinSniperMixGray_1",
                nameToken = "SIMU_SKIN_SNIPER_GRAY",
                icon = H.GetIcon("mod/ror1/sniper_gray"),
                original = skinSniper,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matSniper = CloneMat(newRenderInfos, 0);

            matSniper.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/texSniperDefaultGRAY.png");
            matSniper.SetColor("_EmColor", new Color(1f, 1f, 1f));
            matSniper.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Sniper/texSniperDefault_EmissionGRAY.png"));

            newRenderInfos[0].defaultMaterial = matSniper;
            newRenderInfos[1].defaultMaterial = matSniper;
            newRenderInfos[2].defaultMaterial = matSniper;
            newRenderInfos[3].defaultMaterial = matSniper;
            newRenderInfos[4].defaultMaterial = matSniper;

            newSkinDef.skinDefParams.gameObjectActivations = skinSniperMASTERY.gameObjectActivations;
            return newSkinDef;
        }

        [RegisterAchievement("CLEAR_ANY_SNIPERCLASSIC", "Skins.SniperClassic.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumSniperClassic : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("SniperClassicBody");
            }
        }

    }
}