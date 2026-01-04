using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsMage_Artificer
    {
        internal static void Start()
        {
            SkinDef skinMageDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageDefault.asset").WaitForCompletion();
            SkinDef skinMageAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAlt.asset").WaitForCompletion();
            SkinDef skinMageAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAltColossus.asset").WaitForCompletion();

            ////Materials
            //0 : Jets
            //1 : Jets
            //2 : matMageAltColossus
            //3 : matMageAltColossus

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinMage_1",
                nameToken = "SIMU_SKIN_MAGE_ORANGE",
                icon = H.GetIcon("base/artificer_orange"),
                original = skinMageDefault,
                cloneMesh = true
            }, new System.Action<SkinDefMakeOnApply>(Mixed_Orange));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinMageAltColossus_DLC2",
                nameToken = "SIMU_SKIN_MAGE_COLOSSUS",
                icon = H.GetIcon("base/artificer_dlc2_purple"),
                original = skinMageAltColossus,
            }, new System.Action<SkinDefMakeOnApply>(Colossus_SkyMeadow));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinMageAltColossusWolfo2_DLC2",
                nameToken = "SIMU_SKIN_MAGE_COLOSSUS_BLUE",
                icon = H.GetIcon("base/artificer_dlc2_white"),
                original = skinMageAltColossus,
                extraRenders = 4,
                enhancedSkin = true,
            }, new System.Action<SkinDefMakeOnApply>(Colossus_BlueGlow));

        }
        internal static void Colossus_BlueGlow(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matMageAltColossus = CloneMat(ref newRenderInfos, 2);

            Texture2D texRampLightning2 = Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Common/ColorRamps/texRampLightning2.png").WaitForCompletion();
            Texture2D texMageAltColossusFlowMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/White/texMageAltColossusFlowMask.png");

            matMageAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/White/texMageAltColossusDiffuse.png");
            matMageAltColossus.SetTexture("_FlowHeightmap", texMageAltColossusFlowMask);
            matMageAltColossus.SetTexture("_FlowHeightMask", texMageAltColossusFlowMask);//texRampDroneFire.png
            matMageAltColossus.SetTexture("_FlowHeightRamp", texRampLightning2);//texRampThermite2.png
            matMageAltColossus.SetFloat("_FlowEmissionStrength", 0.75f);//3

            newRenderInfos[2].defaultMaterial = matMageAltColossus;
            newRenderInfos[3].defaultMaterial = matMageAltColossus;

            #region BlueJets
            Transform JetsOn = newRenderInfos[0].renderer.transform.parent.GetChild(5);
            Material mageMageFireStarburst = CloneMat(ref newRenderInfos, 0);
            mageMageFireStarburst.SetTexture("_RemapTex", texRampLightning2);

            Material matMageFlamethrower = Object.Instantiate(JetsOn.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            matMageFlamethrower.SetTexture("_RemapTex", texRampLightning2);
            matMageFlamethrower.SetFloat("_Boost", 0.25f);


            newRenderInfos[0].defaultMaterial = mageMageFireStarburst;
            newRenderInfos[1].defaultMaterial = mageMageFireStarburst;
            newRenderInfos[4] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(0).GetComponent<ParticleSystemRenderer>(), //matMageFlamethrower 
                defaultMaterial = matMageFlamethrower,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            //Light
            newRenderInfos[5] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(2).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            newRenderInfos[6] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(3).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            newRenderInfos[7] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(4).GetComponent<ParticleSystemRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };


            #endregion
            //

            (newSkinDef as SkinDefEnhanced).lightColorsChanges = new SkinDefEnhanced.LightColorChanges[]
            {
                new SkinDefEnhanced.LightColorChanges
                {
                    color = new Color(0.1f,0.5f,0.8f),
                    lightPath = "MageArmature/ROOT/base/stomach/chest/JetsOn/Point Light",
                },
            };
        }

        internal static void Colossus_SkyMeadow(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matMageAltColossus = CloneMat(ref newRenderInfos, 2);
            matMageAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/Purple/texMageAltColossusDiffuse.png");
            matMageAltColossus.SetTexture("_FlowHeightmap", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/White/texMageAltColossusFlowMask.png"));
            matMageAltColossus.SetFloat("_FlowEmissionStrength", 1);//0.9
            matMageAltColossus.SetFloat("_NormalStrength", 2);//0.9

            newRenderInfos[2].defaultMaterial = matMageAltColossus;
            newRenderInfos[3].defaultMaterial = matMageAltColossus;

        }

        internal static void Mixed_Orange(SkinDefMakeOnApply newSkinDef)
        {
            SkinDefParams skinMageAlt = Addressables.LoadAssetAsync<SkinDefParams>(key: "RoR2/Base/Mage/skinMageAlt_params.asset").WaitForCompletion();

            newSkinDef.skinDefParams.meshReplacements[0] = skinMageAlt.meshReplacements[0];
            CharacterModel.RendererInfo[] newRenderInfosORANGE = newSkinDef.skinDefParams.rendererInfos;

            Texture2D texMageDiffuseORANGE = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/texMageDiffuseORANGE.png");
            texMageDiffuseORANGE.wrapMode = TextureWrapMode.Clamp;

            Material matMageORANGE = CloneFromOriginal(skinMageAlt, 3);
            Material matMageORANGECOAT = CloneMat(ref newRenderInfosORANGE, 2);

            matMageORANGE.mainTexture = texMageDiffuseORANGE;
            matMageORANGECOAT.mainTexture = texMageDiffuseORANGE;

            newRenderInfosORANGE[2].defaultMaterial = matMageORANGE;
            newRenderInfosORANGE[3].defaultMaterial = matMageORANGE;

        }

        [RegisterAchievement("CLEAR_ANY_MAGE", "Skins.Mage.Wolfo.First", "FreeMage", 3, null)]
        public class ClearSimulacrumMageBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MageBody");
            }
        }
        [RegisterAchievement("CLEAR_BOTH_MAGE", "Skins.Mage.Wolfo.Both", "FreeMage", 3, null)]
        public class ClearSimulacrumMageBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MageBody");
            }
        }

    }
}