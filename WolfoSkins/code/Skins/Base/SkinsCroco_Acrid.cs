using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsCroco_Acrid
    {
        internal static void Start()
        {
            SkinDef skinCrocoDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoDefault.asset").WaitForCompletion();
            //SkinDef skinCrocoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAlt.asset").WaitForCompletion();
            SkinDef skinCrocoAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAltColossus.asset").WaitForCompletion();

            //0 matCrocoColossus
            //1 matCrocoDiseaseDrippings
            //2 matCrocoColossus

            //Black Returns
            AcridSkinBlack(skinCrocoDefault);  
            //Orange highlits      
            Acrid_AltColossus_Default(skinCrocoAltColossus);
            //Lunar Returns
            RoRR_Lunar(skinCrocoDefault);
            //Purple highlits      
            Acrid_AltColossus(skinCrocoAltColossus);
          
            //Joke Lemurian
            AcridSkinLemurian(skinCrocoDefault);
        }



        internal static void Acrid_AltColossus_Default(SkinDef skinCrocoAltColossus)
        {
            CharacterModel.RendererInfo[] renderInfo = H.CreateNewSkinR(new SkinInfo
            {
                nameToken = "CROCO_SKIN_ALT2_NAME2",
                name = "skinCrocoAltColossus_Green_DLC2",
                icon = H.GetIcon("acrid_dlc2"),
                original = skinCrocoAltColossus,
            });
            Material matCrocoColossus = CloneMat(renderInfo, 0);

            matCrocoColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusDiffuse2.png");
            matCrocoColossus.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusEmissive.png")); //texRampHealingVariant

            renderInfo[0].defaultMaterial = matCrocoColossus;
            renderInfo[2].defaultMaterial = matCrocoColossus;


        }


        internal static void Acrid_AltColossus(SkinDef skinCrocoAltColossus)
        {
            CharacterModel.RendererInfo[] renderInfo = H.CreateNewSkinR(new SkinInfo
            {
                nameToken = "SIMU_SKIN_CROCO_COLOSSUS",
                name = "skinCrocoAltColossus_DLC2",
                icon = H.GetIcon("acrid_dlc2_purple"),
                original = skinCrocoAltColossus,
            });
            Material matCrocoColossus = CloneMat(renderInfo, 0);

            matCrocoColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusDiffuse.png");
            matCrocoColossus.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusEmissive.png")); //texRampHealingVariant
            matCrocoColossus.SetTexture("_FlowHeightRamp", null); //texRampHealingVariant
            matCrocoColossus.SetColor("_EmColor", new Color(0.8f, 0.45f, 1f, 1f));//1 0.4226 0.2235 1

            renderInfo[0].defaultMaterial = matCrocoColossus;
            renderInfo[2].defaultMaterial = matCrocoColossus;


        }

        internal static void RoRR_Lunar(SkinDef skinCrocoDefault)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                nameToken = "SIMU_SKIN_CROCO",
                name = "skinCroco_1",
                icon = H.GetIcon("acrid_white"),
                original = skinCrocoDefault,
            });

            Material matCroco = CloneMat(newRenderInfos, 0);
            Material matCrocoDiseaseDrippings = CloneMat(newRenderInfos, 1);
            Material matCrocoSpine = CloneMat(newRenderInfos, 2);

            Texture2D texCrocoPoisonMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoPoisonMask.png");

            matCroco.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoDiffuse.png");
            matCroco.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoEmission.png"));
            matCroco.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texRampCrocoDiseaseDark.png"));
            matCroco.SetTexture("_FlowHeightmap", texCrocoPoisonMask);
            matCroco.SetColor("_EmColor", new Color(0.2f, 0.8f, 1f));

            matCrocoDiseaseDrippings.SetTexture("_FlowHeightmap", texCrocoPoisonMask);

            matCrocoSpine.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoSpinesDiffuse.png");
            matCrocoSpine.SetTexture("_EmTex", matCrocoSpine.mainTexture);
            matCrocoSpine.SetColor("_EmColor", new Color(0.3f, 0.3f, 0.3f));

            newRenderInfos[0].defaultMaterial = matCroco;
            newRenderInfos[1].defaultMaterial = matCrocoDiseaseDrippings;
            newRenderInfos[2].defaultMaterial = matCrocoSpine;


        }

        internal static void AcridSkinBlack(SkinDef skinCrocoDefault)
        {
            CharacterModel.RendererInfo[] newRenderInfos = H.CreateNewSkinR(new SkinInfo
            {
                name = "skinCrocoWolfoBlack_1",
                nameToken = "SIMU_SKIN_CROCO_BLACK",
                icon = H.GetIcon("acrid_black"),
                original = skinCrocoDefault,
            });


            Material matCroco = CloneMat(newRenderInfos, 0);
            Material matCrocoDiseaseDrippings = CloneMat(newRenderInfos, 1);
            Material matCrocoSpine = CloneMat(newRenderInfos, 2);

            Texture2D texCrocoPoisonMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoPoisonMaskBLACK.png");

            matCroco.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoDiffuseBLACK.png");
            matCroco.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoEmissionBLACK.png"));
            matCroco.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texRampCrocoDiseaseDarkBLACK.png"));
            matCroco.SetTexture("_FlowHeightmap", texCrocoPoisonMask);
            matCroco.SetColor("_EmColor", new Color(0.3f, 1.2f, 0.2f));

            matCrocoDiseaseDrippings.SetTexture("_FlowHeightmap", texCrocoPoisonMask);

            matCrocoSpine.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoSpinesDiffuseBLACK.png");
            matCrocoSpine.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoSpinesDiffuseBLACK_EM.png"));
            matCrocoSpine.SetColor("_EmColor", new Color(0.66f, 0.88f, 0.22f));

            newRenderInfos[0].defaultMaterial = matCroco;
            newRenderInfos[1].defaultMaterial = matCrocoDiseaseDrippings;
            newRenderInfos[2].defaultMaterial = matCrocoSpine;

        }

        internal static void AcridSkinLemurian(SkinDef skinCrocoDefault)
        {
            SkinDefAltColor newSkinDef = (SkinDefAltColor)H.CreateNewSkin(new SkinInfo
            {
                name = "skinCrocoWolfoLemurian",
                nameToken = "LEMURIAN_BODY_NAME",
                icon = H.GetIcon("acrid_lemurian"),
                original = skinCrocoDefault,
                w = true,
                unsetMat = true,
            });
            var newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matCroco = CloneMat(newRenderInfos, 0);

            matCroco.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lemurian/texCrocoDiffuseLEMURIAN.png");
            matCroco.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lemurian/texCrocoEmissionLEMURIAN.png"));
            matCroco.SetTexture("_FlowHeightRamp", null);
            matCroco.SetTexture("_FlowHeightmap", null);
            matCroco.SetColor("_EmColor", new Color(0.8f, 0.8f, 0.8f));
            //matCroco.DisableKeyword("FLOWMAP");

            newRenderInfos[0].defaultMaterial = matCroco;
            newRenderInfos[1].defaultMaterial = null;
            newRenderInfos[2].defaultMaterial = null;

            newSkinDef.disableThis = skinCrocoDefault.rootObject.transform.GetChild(7);
        }


        [RegisterAchievement("CLEAR_ANY_CROCO", "Skins.Croco.Wolfo.First", "BeatArena", 5, null)]
        public class CrocoBodyAltEndings : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CrocoBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_CROCO", "Skins.Croco.Wolfo.Both", "BeatArena", 5, null)]
        public class CrocoBodyAltEndings2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CrocoBody");
            }
        }
    }
}