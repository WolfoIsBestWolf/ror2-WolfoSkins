using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsMage_Artificer
    {
        internal static void Start()
        {
            ArtificerSkinORANGE();
            //ArtificerSkinPURPLE();
            Artificer_AltColossus();
            Artificer_AltColossus_Blue();
            //ArtificerSkinRAINBOW();
        }
        internal static void Artificer_AltColossus_Blue()
        {
            SkinDef skinMageAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAltColossus.asset").WaitForCompletion();
            Texture2D texRampLightning2 = Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Common/ColorRamps/texRampLightning2.png").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMageAltColossus.rendererInfos.Length+4];
            System.Array.Copy(skinMageAltColossus.rendererInfos, NewRenderInfos, skinMageAltColossus.rendererInfos.Length);

            ////Materials
            //0 : Jets
            //1 : Jets
            //2 : matMageAltColossus
            //3 : matMageAltColossus


            Material matMageAltColossus = Object.Instantiate(skinMageAltColossus.rendererInfos[2].defaultMaterial);
          
            Texture2D texMageAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/White/texMageAltColossusDiffuse.png");
            texMageAltColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMageAltColossusFlowMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/White/texMageAltColossusFlowMask.png");
            texMageAltColossusFlowMask.wrapMode = TextureWrapMode.Clamp;


            /*Texture2D texRampThermite2 = new Texture2D(256, 16, TextureFormat.RGB24, false);
            texRampThermite2.LoadImage(Properties.Resources.texRampThermite2, true);
            texRampThermite2.wrapMode = TextureWrapMode.Clamp;*/


            matMageAltColossus.mainTexture = texMageAltColossusDiffuse;
            matMageAltColossus.SetTexture("_FlowHeightmap", texMageAltColossusFlowMask);
           // matMageAltColossus.SetTexture("_FlowHeightMask", texMageAltColossusFlowMask);//texRampDroneFire.png
            matMageAltColossus.SetTexture("_FlowHeightRamp", texRampLightning2);//texRampThermite2.png
            matMageAltColossus.SetFloat("_FlowEmissionStrength", 0.6f);//3

            NewRenderInfos[2].defaultMaterial = matMageAltColossus;
            NewRenderInfos[3].defaultMaterial = matMageAltColossus;

            #region BlueJets
            Transform JetsOn = NewRenderInfos[0].renderer.transform.parent.GetChild(5);
            Material mageMageFireStarburst = Object.Instantiate(skinMageAltColossus.rendererInfos[0].defaultMaterial);
            mageMageFireStarburst.SetTexture("_RemapTex", texRampLightning2);

            Material matMageFlamethrower = Object.Instantiate(JetsOn.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            matMageFlamethrower.SetTexture("_RemapTex", texRampLightning2);
            matMageFlamethrower.SetFloat("_Boost", 0.25f);


            NewRenderInfos[0].defaultMaterial = mageMageFireStarburst;
            NewRenderInfos[1].defaultMaterial = mageMageFireStarburst;
            NewRenderInfos[4] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(0).GetComponent<ParticleSystemRenderer>(), //matMageFlamethrower 
                defaultMaterial = matMageFlamethrower,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            //Light
            NewRenderInfos[5] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(2).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            NewRenderInfos[6] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(3).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            NewRenderInfos[7] = new CharacterModel.RendererInfo
            {
                renderer = JetsOn.GetChild(4).GetComponent<ParticleSystemRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };


            #endregion
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinMageAltColossusWolfo2_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_MAGE_COLOSSUS_BLUE";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/White/Artificer.png"));
            newSkinDef.baseSkins = skinMageAltColossus.baseSkins;
            newSkinDef.meshReplacements = skinMageAltColossus.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinMageAltColossus.rootObject;
            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0,1f,0.4f),
                    lightPath = "MageArmature/ROOT/base/stomach/chest/JetsOn/Point Light",
                },
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), newSkinDef);
        }

        internal static void Artificer_AltColossus()
        {
            SkinDef skinMageAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMageAltColossus.rendererInfos.Length];
            System.Array.Copy(skinMageAltColossus.rendererInfos, NewRenderInfos, skinMageAltColossus.rendererInfos.Length);

            ////Materials
            //0 : Jets
            //1 : Jets
            //2 : matMageAltColossus
            //3 : matMageAltColossus

            Material matMageAltColossus = Object.Instantiate(skinMageAltColossus.rendererInfos[2].defaultMaterial);


            Texture2D texMageAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/Purple/texMageAltColossusDiffuse.png");
            texMageAltColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            matMageAltColossus.mainTexture = texMageAltColossusDiffuse;
            matMageAltColossus.SetFloat("_FlowEmissionStrength", 3);//0.9
            matMageAltColossus.SetFloat("_NormalStrength", 2);//0.9
            
            NewRenderInfos[2].defaultMaterial = matMageAltColossus;
            NewRenderInfos[3].defaultMaterial = matMageAltColossus;


            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinMageAltColossusWolfo_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_MAGE_COLOSSUS";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/Colossus/Purple/icon.png"));
            newSkinDef.baseSkins = skinMageAltColossus.baseSkins;
            newSkinDef.meshReplacements = skinMageAltColossus.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinMageAltColossus.rootObject;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), newSkinDef);
        }

        #region Old Rainbow
        /*
        internal static void ArtificerSkinRAINBOW()
        {
            SkinDef skinMageDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageDefault.asset").WaitForCompletion();
            SkinDef skinMageAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAlt.asset").WaitForCompletion();
            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[9];
            System.Array.Copy(skinMageAlt.rendererInfos, NewRenderInfos, 4);

            Texture2D texMageDiffuseAlt = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texMageDiffuseAlt.LoadImage(Properties.Resources.texMageDiffuseAlt, true);
            texMageDiffuseAlt.filterMode = FilterMode.Bilinear;
            texMageDiffuseAlt.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampMageFire = new Texture2D(256, 16, TextureFormat.DXT1, false);
            texRampMageFire.LoadImage(Properties.Resources.texRampMageFire, true);
            texRampMageFire.filterMode = FilterMode.Point;
            texRampMageFire.wrapMode = TextureWrapMode.Clamp;


            Material mageMageFireStarburst = Object.Instantiate(skinMageAlt.rendererInfos[0].defaultMaterial);
            Material matMage = Object.Instantiate(skinMageAlt.rendererInfos[3].defaultMaterial);
            Material matMageFlamethrower = Object.Instantiate(NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(0).GetComponent<ParticleSystemRenderer>().material);

            matMage.mainTexture = texMageDiffuseAlt;

            //mageMageFireStarburst.SetTexture("_RemapTex", Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Common/ColorRamps/texRampLightning2.png").WaitForCompletion());
            mageMageFireStarburst.SetTexture("_RemapTex", texRampMageFire);
            matMageFlamethrower.SetTexture("_RemapTex", texRampMageFire);
            //mageMageFireStarburst.SetColor("_CutoffScroll", new Color(-30, 0, 50, 0));

            //matMage.color = new Color(0.66f, 0.66f, 0.66f, 1);
            //matMage.color = new Color(0.66f, 0.66f, 0.66f, 1);
            //matMage.SetColor("_EmColor", new Color(0.4f, 0.2f, 0));

            NewRenderInfos[0].defaultMaterial = mageMageFireStarburst;     //mageMageFireStarburst //Jet
            NewRenderInfos[1].defaultMaterial = mageMageFireStarburst;     //mageMageFireStarburst
            NewRenderInfos[2].defaultMaterial = matMage;          //matMage //Cape
            NewRenderInfos[3].defaultMaterial = matMage;          //matMage //Mage

            NewRenderInfos[4] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(0).GetComponent<ParticleSystemRenderer>(), //matMageFlamethrower 
                defaultMaterial = matMageFlamethrower,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            //Light
            NewRenderInfos[5] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(2).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            NewRenderInfos[6] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(3).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            NewRenderInfos[7] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(4).GetComponent<ParticleSystemRenderer>(), //mageMageFireStarburst  
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
                Name = "skinMageWolfoRainbow_Simu",
                NameToken = "SIMU_SKIN_MAGE",
                Icon = SkinIconS,
                BaseSkins = skinMageAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinMageAlt.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), SkinInfo);
        }
        */
        #endregion

        internal static void ArtificerSkinORANGE()
        {
            //RoRR Orange Artificer
            SkinDef skinMageDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageDefault.asset").WaitForCompletion();
            SkinDef skinMageAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfosORANGE = new CharacterModel.RendererInfo[4];
            System.Array.Copy(skinMageAlt.rendererInfos, NewRenderInfosORANGE, 4);

            Texture2D texMageDiffuseORANGE = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/texMageDiffuseORANGE.png");
            texMageDiffuseORANGE.wrapMode = TextureWrapMode.Clamp;

            Material matMageORANGE = Object.Instantiate(skinMageAlt.rendererInfos[3].defaultMaterial);
            Material matMageORANGECOAT = Object.Instantiate(skinMageAlt.rendererInfos[3].defaultMaterial);

            matMageORANGE.mainTexture = texMageDiffuseORANGE;
            matMageORANGECOAT.mainTexture = texMageDiffuseORANGE;

            NewRenderInfosORANGE[2].defaultMaterial = matMageORANGE;
            NewRenderInfosORANGE[3].defaultMaterial = matMageORANGE;

            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[2];
            skinMageAlt.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[1].mesh = skinMageDefault.meshReplacements[1].mesh;
            //
            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                Name = "skinMageWolfo_Simu",
                NameToken = "SIMU_SKIN_MAGE_ORANGE",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Mage/skinIconMageOrange.png")),
                BaseSkins = skinMageAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfosORANGE,
                RootObject = skinMageAlt.rootObject,
            };
            //unlockableDef.achievementIcon = SkinIconS2;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), SkinInfo2);
        }

        #region Old Purple
        /*
        internal static void ArtificerSkinPURPLE()
        {
            //RoRR Orange Artificer
            SkinDef skinMageDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageDefault.asset").WaitForCompletion();
            SkinDef skinMageAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[4];
            System.Array.Copy(skinMageAlt.rendererInfos, NewRenderInfos, 4);

            Texture2D texMageDiffuseAlt = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texMageDiffuseAlt.LoadImage(Properties.Resources.texMageDiffuseAltPURPLE, true);
            texMageDiffuseAlt.filterMode = FilterMode.Bilinear;
            texMageDiffuseAlt.wrapMode = TextureWrapMode.Clamp;

            Material matMage = Object.Instantiate(skinMageAlt.rendererInfos[3].defaultMaterial);

            matMage.mainTexture = texMageDiffuseAlt;

            NewRenderInfos[2].defaultMaterial = matMage;//Cape
            NewRenderInfos[3].defaultMaterial = matMage;//Mage


            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinMageWolfo_Purple_Simu",
                NameToken = "SIMU_SKIN_MAGE_PURPLE",
                Icon = SkinIconS,
                BaseSkins = skinMageAlt.baseSkins,
                MeshReplacements = skinMageAlt.meshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinMageAlt.rootObject,
            };

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), SkinInfo);
         }
        */
        #endregion
        
        [RegisterAchievement("CLEAR_ANY_MAGE", "Skins.Mage.Wolfo.First", "FreeMage", 5, null)]
        public class ClearSimulacrumMageBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MageBody");
            }
        }
        [RegisterAchievement("CLEAR_BOTH_MAGE", "Skins.Mage.Wolfo.Both", "FreeMage", 5, null)]
        public class ClearSimulacrumMageBody2: Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MageBody");
            }
        }

    }
}