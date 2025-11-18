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
 
            ArtificerSkinORANGE(skinMageDefault, skinMageAlt.ReturnParams());
            //ArtificerSkinPURPLE();
            Artificer_AltColossus(skinMageAltColossus);
            Artificer_AltColossus_Blue(skinMageAltColossus);
            //ArtificerSkinRAINBOW();
        }
        internal static void Artificer_AltColossus_Blue(SkinDef skinMageAltColossus)
        {
            SkinDefAltColor newSkinDef = CreateNewSkinW(new SkinInfo
            {
                name = "skinMageAltColossusWolfo2_DLC2",
                nameToken = "SIMU_SKIN_MAGE_COLOSSUS_BLUE",
                icon = H.GetIcon("artificer_dlc2_white"),
                original = skinMageAltColossus,
                extraRenders = 4,
                unsetMat = true,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matMageAltColossus = CloneMat(newRenderInfos, 2);

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
            Material mageMageFireStarburst = CloneMat(newRenderInfos, 0);
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

            newSkinDef.lightColorsChanges = new SkinDefAltColor.LightColorChanges[]
            {
                new SkinDefAltColor.LightColorChanges
                {
                    color = new Color(0.1f,0.5f,0.8f),
                    lightPath = "MageArmature/ROOT/base/stomach/chest/JetsOn/Point Light",
                },
            };
        }

        internal static void Artificer_AltColossus(SkinDef skinMageAltColossus)
        {
            CharacterModel.RendererInfo[] newRenderInfos = CreateNewSkinR(new SkinInfo
            {
                name = "skinMageAltColossus_DLC2",
                nameToken = "SIMU_SKIN_MAGE_COLOSSUS",
                icon = H.GetIcon("artificer_dlc2_purple"),
                original = skinMageAltColossus,
            });
            Material matMageAltColossus = CloneMat(newRenderInfos, 2);
            matMageAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/Purple/texMageAltColossusDiffuse.png");
            matMageAltColossus.SetTexture("_FlowHeightmap", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/White/texMageAltColossusFlowMask.png"));
            matMageAltColossus.SetFloat("_FlowEmissionStrength", 1);//0.9
            matMageAltColossus.SetFloat("_NormalStrength", 2);//0.9

            newRenderInfos[2].defaultMaterial = matMageAltColossus;
            newRenderInfos[3].defaultMaterial = matMageAltColossus;

        }

        #region Old Rainbow
        /*
        internal static void ArtificerSkinRAINBOW()
        {
            SkinDef skinMageDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageDefault.asset").WaitForCompletion();
            SkinDef skinMageAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAlt.asset").WaitForCompletion();
            //
            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[9];
            System.Array.Copy(skinMageAlt.rendererInfos, newRenderInfos, 4);

            Texture2D texMageDiffuseAlt = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texMageDiffuseAlt.LoadImage(Properties.Resources.texMageDiffuseAlt, true);
            texMageDiffuseAlt.filterMode = FilterMode.Bilinear;
            texMageDiffuseAlt.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampMageFire = new Texture2D(256, 16, TextureFormat.DXT1, false);
            texRampMageFire.LoadImage(Properties.Resources.texRampMageFire, true);
            texRampMageFire.filterMode = FilterMode.Point;
            texRampMageFire.wrapMode = TextureWrapMode.Clamp;


            Material mageMageFireStarburst = CloneMat(MageAlt.rendererInfos[0].defaultMaterial);
            Material matMage = CloneMat(MageAlt.rendererInfos[3].defaultMaterial);
            Material matMageFlamethrower = Object.Instantiate(newRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(0).GetComponent<ParticleSystemRenderer>().material);

            matMage.mainTexture = texMageDiffuseAlt;

            //mageMageFireStarburst.SetTexture("_RemapTex", Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Common/ColorRamps/texRampLightning2.png").WaitForCompletion());
            mageMageFireStarburst.SetTexture("_RemapTex", texRampMageFire);
            matMageFlamethrower.SetTexture("_RemapTex", texRampMageFire);
            //mageMageFireStarburst.SetColor("_CutoffScroll", new Color(-30, 0, 50, 0));

            //matMage.color = new Color(0.66f, 0.66f, 0.66f, 1);
            //matMage.color = new Color(0.66f, 0.66f, 0.66f, 1);
            //matMage.SetColor("_EmColor", new Color(0.4f, 0.2f, 0));

            newRenderInfos[0].defaultMaterial = mageMageFireStarburst;     //mageMageFireStarburst //Jet
            newRenderInfos[1].defaultMaterial = mageMageFireStarburst;     //mageMageFireStarburst
            newRenderInfos[2].defaultMaterial = matMage;          //matMage //Cape
            newRenderInfos[3].defaultMaterial = matMage;          //matMage //Mage

            newRenderInfos[4] = new CharacterModel.RendererInfo
            {
                renderer = newRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(0).GetComponent<ParticleSystemRenderer>(), //matMageFlamethrower 
                defaultMaterial = matMageFlamethrower,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            //Light
            newRenderInfos[5] = new CharacterModel.RendererInfo
            {
                renderer = newRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(2).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            newRenderInfos[6] = new CharacterModel.RendererInfo
            {
                renderer = newRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(3).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            newRenderInfos[7] = new CharacterModel.RendererInfo
            {
                renderer = newRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(4).GetComponent<ParticleSystemRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };

            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[2];
            skinMageAlt.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[1].mesh = skinMageDefault.meshReplacements[1].mesh;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinMageWolfoRainbow_1",
                NameToken = "SIMU_SKIN_MAGE",
                Icon = SkinIconS,
                BaseSkins = skinMageAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = newRenderInfos,
                RootObject = skinMageAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), SkinInfo);
        }
        */
        #endregion

        internal static void ArtificerSkinORANGE(SkinDef skinMageDefault, SkinDefParams skinMageAlt)
        {
            SkinDef newSkinDef = CreateNewSkin(new SkinInfo
            {
                name = "skinMage_1",
                nameToken = "SIMU_SKIN_MAGE_ORANGE",
                icon = H.GetIcon("artificer_orange"),
                original = skinMageDefault,
                cloneMesh = true
            });
            newSkinDef.skinDefParams.meshReplacements[0] = skinMageAlt.meshReplacements[0];
            CharacterModel.RendererInfo[] newRenderInfosORANGE = newSkinDef.skinDefParams.rendererInfos;

            Texture2D texMageDiffuseORANGE = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/texMageDiffuseORANGE.png");
            texMageDiffuseORANGE.wrapMode = TextureWrapMode.Clamp;

            Material matMageORANGE = CloneFromOriginal(skinMageAlt, 3);
            Material matMageORANGECOAT = CloneMat(newRenderInfosORANGE, 2);

            matMageORANGE.mainTexture = texMageDiffuseORANGE;
            matMageORANGECOAT.mainTexture = texMageDiffuseORANGE;

            newRenderInfosORANGE[2].defaultMaterial = matMageORANGE;
            newRenderInfosORANGE[3].defaultMaterial = matMageORANGE;



        }

        #region Old Purple
        /*
        internal static void ArtificerSkinPURPLE()
        {
            //RoRR Orange Artificer
            SkinDef skinMageDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageDefault.asset").WaitForCompletion();
            SkinDef skinMageAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[4];
            System.Array.Copy(skinMageAlt.rendererInfos, newRenderInfos, 4);

            Texture2D texMageDiffuseAlt = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texMageDiffuseAlt.LoadImage(Properties.Resources.texMageDiffuseAltPURPLE, true);
            texMageDiffuseAlt.filterMode = FilterMode.Bilinear;
            texMageDiffuseAlt.wrapMode = TextureWrapMode.Clamp;

            Material matMage = CloneMat(MageAlt.rendererInfos[3].defaultMaterial);

            matMage.mainTexture = texMageDiffuseAlt;

            newRenderInfos[2].defaultMaterial = matMage;//Cape
            newRenderInfos[3].defaultMaterial = matMage;//Mage


            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinMage_Purple_1",
                NameToken = "SIMU_SKIN_MAGE_PURPLE",
                Icon = SkinIconS,
                BaseSkins = skinMageAlt.baseSkins,
                MeshReplacements = skinMageAlt.meshReplacements,
                RendererInfos = newRenderInfos,
                RootObject = skinMageAlt.rootObject,
            };

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), SkinInfo);
         }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_MAGE", "Skins.Mage.Wolfo.First", "FreeMage", 5, null)]
        public class ClearSimulacrumMageBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MageBody");
            }
        }
        [RegisterAchievement("CLEAR_BOTH_MAGE", "Skins.Mage.Wolfo.Both", "FreeMage", 5, null)]
        public class ClearSimulacrumMageBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MageBody");
            }
        }

    }
}