using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsHand
    {
        internal static void ModdedSkin(GameObject HandBody)
        {

            Hand_Gold(HandBody);
            Hand_Rusty(HandBody);
            Hand_Green(HandBody);

            //Move HO-POO Skin last
            BodyIndex HandBodyIndex = HandBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = HandBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();

            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length];
            skinsNew[skinsNew.Length - 1] = modelSkinController.skins[3];

            //IDK??
            int j = 0;
            for (int i = 0; i < modelSkinController.skins.Length; i++)
            {
                if (i == 3)
                {
                    i++;
                }
                skinsNew[j] = modelSkinController.skins[i];
                j++;
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)HandBodyIndex] = skinsNew;
        }

        internal static void Hand_Gold(GameObject HandBody)
        {
            Debug.Log("HandBody Skins");
            BodyIndex HandBodyIndex = HandBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = HandBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinHandDefault = modelSkinController.skins[0];
            SkinDef skinHandReturns = modelSkinController.skins[2];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinHandDefault.rendererInfos.Length];
            System.Array.Copy(skinHandDefault.rendererInfos, NewRenderInfos, skinHandDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRETURNS = new CharacterModel.RendererInfo[skinHandReturns.rendererInfos.Length];
            System.Array.Copy(skinHandReturns.rendererInfos, NewRenderInfosRETURNS, skinHandReturns.rendererInfos.Length);


            //0 matHANDRorr
            //1 matHANDRorr
            //2 matHANDRorr
            //3 matDroneSawTemp

            Material matHANDDefault = Object.Instantiate(skinHandDefault.rendererInfos[0].defaultMaterial);
            Material matHANDWeaponDefault = Object.Instantiate(skinHandDefault.rendererInfos[1].defaultMaterial);
            Material matDroneBody = Object.Instantiate(skinHandDefault.rendererInfos[2].defaultMaterial);

            Material matHANDRorr = Object.Instantiate(skinHandReturns.rendererInfos[0].defaultMaterial);

            Texture2D HanD_Diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/HanD_Diffuse.png");
            HanD_Diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D HanD_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/HanD_Emission.png");
            HanD_Emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D HanDWeapon_Diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/HanDWeapon_Diffuse.png");
            HanDWeapon_Diffuse.wrapMode = TextureWrapMode.Clamp;

            //RGB24
            Texture2D texDroneBody = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/texDroneBody.png");
            texDroneBody.filterMode = FilterMode.Point;
            texDroneBody.wrapMode = TextureWrapMode.Repeat;

            //RGB24
            Texture2D texDroneBodyEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/texDroneBodyEmission.png");
            texDroneBodyEmission.filterMode = FilterMode.Point;
            texDroneBodyEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D RoRRHANDTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/RoRRHANDTex.png");
            RoRRHANDTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D RoRRHANDTexEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/RoRRHANDTexEmission.png");
            RoRRHANDTexEmission.wrapMode = TextureWrapMode.Repeat;


            Color ExtraGold = new Color(1.2f, 1.1f, 1f);

            matHANDDefault.mainTexture = HanD_Diffuse;
            matHANDDefault.color = ExtraGold;
            matHANDDefault.SetTexture("_EmTex", HanD_Emission);           
            matHANDDefault.SetColor("_EmColor", new Color(1,1,1.8f));

            matHANDWeaponDefault.mainTexture = HanDWeapon_Diffuse;
            matHANDWeaponDefault.color = ExtraGold;

            matDroneBody.mainTexture = texDroneBody;
            matDroneBody.color = ExtraGold;
            matDroneBody.SetTexture("_EmTex", texDroneBodyEmission);
            matDroneBody.SetColor("_EmColor", new Color(1, 1, 1.5f));
           
            matHANDRorr.mainTexture = RoRRHANDTex;
            matHANDRorr.color = ExtraGold;
            matHANDRorr.SetTexture("_EmTex", RoRRHANDTexEmission);
            matHANDRorr.SetColor("_EmColor", new Color(1, 1, 1.1f));    

            NewRenderInfos[0].defaultMaterial = matHANDDefault;
            NewRenderInfos[1].defaultMaterial = matHANDWeaponDefault;
            NewRenderInfos[2].defaultMaterial = matDroneBody;
            NewRenderInfosRETURNS[0].defaultMaterial = matHANDRorr;
            NewRenderInfosRETURNS[1].defaultMaterial = matHANDRorr;
            NewRenderInfosRETURNS[2].defaultMaterial = matHANDRorr;
            //

            
            //ADD SHINY SPARKLES
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinHandBodyWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_HAND";
            newSkinDef.icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/texHANDSkinIconDefault.png"));
            newSkinDef.baseSkins = skinHandReturns.baseSkins;
            newSkinDef.meshReplacements = skinHandDefault.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinHandDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinHandDefault.rootObject;

            GameObject GoldAffixEffect = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Junk/EliteGold/GoldAffixEffect.prefab").WaitForCompletion();
            GameObject GoldAffixEffectNew = R2API.PrefabAPI.InstantiateClone(GoldAffixEffect, "HANDGoldSparkles", false);

            GoldAffixEffectNew.transform.GetChild(0).gameObject.SetActive(false);
            GoldAffixEffectNew.transform.GetChild(0).localPosition = new Vector3(0, 4, 0);
            GoldAffixEffectNew.transform.GetChild(0).GetComponent<Light>().intensity = 1;

            ParticleSystem particleSystem = GoldAffixEffectNew.GetComponent<ParticleSystem>();
            particleSystem.playbackSpeed = 0.5f;
            particleSystem.emissionRate = 6;
            particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;
            particleSystem.startColor = new Color(0f, 0.6f, 1);

            var Main = particleSystem.main;
            Main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

            var Shape = particleSystem.shape;
            Shape.shapeType = ParticleSystemShapeType.Sphere;
            Shape.scale = new Vector3(1.8f, 2.4f, 1.5f);

            newSkinDef.addGameObjects = new ItemDisplayRule[]
            {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = GoldAffixEffectNew,
                            childName = "Base",
                            localPos = new Vector3(0f, 4.6f, 0f),
                            localAngles = new Vector3(0f,0f,0f),
                            localScale = new Vector3(0.6f,0.6f,0.6f),
                            limbMask = LimbFlags.None
                        },
            };



            SkinDefWolfo newSkinDefRETURNS = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefRETURNS.name = "skinHandBodyWolfo_RETURN_Simu";
            newSkinDefRETURNS.nameToken = "SIMU_SKIN_HAND2";
            newSkinDefRETURNS.icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/texHANDSkinIconRorr.png"));
            newSkinDefRETURNS.baseSkins = skinHandReturns.baseSkins;
            newSkinDefRETURNS.meshReplacements = skinHandReturns.meshReplacements;
            newSkinDefRETURNS.projectileGhostReplacements = skinHandReturns.projectileGhostReplacements;
            newSkinDefRETURNS.rendererInfos = NewRenderInfosRETURNS;
            newSkinDefRETURNS.rootObject = skinHandDefault.rootObject;
            newSkinDefRETURNS.addGameObjects = newSkinDef.addGameObjects;

            modelSkinController.skins = modelSkinController.skins.Add(newSkinDef, newSkinDefRETURNS);
            BodyCatalog.skins[(int)HandBodyIndex] = BodyCatalog.skins[(int)HandBodyIndex].Add(newSkinDef, newSkinDefRETURNS);
        }

        internal static void Hand_Rusty(GameObject HandBody)
        {
            BodyIndex HandBodyIndex = HandBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = HandBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinHandDefault = modelSkinController.skins[0];
            SkinDef skinHandReturns = modelSkinController.skins[2];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinHandDefault.rendererInfos.Length];
            System.Array.Copy(skinHandDefault.rendererInfos, NewRenderInfos, skinHandDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRETURNS = new CharacterModel.RendererInfo[skinHandReturns.rendererInfos.Length];
            System.Array.Copy(skinHandReturns.rendererInfos, NewRenderInfosRETURNS, skinHandReturns.rendererInfos.Length);


            //0 matHANDRorr
            //1 matHANDRorr
            //2 matHANDRorr
            //3 matDroneSawTemp

            Material matHANDDefault = Object.Instantiate(skinHandDefault.rendererInfos[0].defaultMaterial);
            Material matHANDWeaponDefault = Object.Instantiate(skinHandDefault.rendererInfos[1].defaultMaterial);
            Material matDroneBody = Object.Instantiate(skinHandDefault.rendererInfos[2].defaultMaterial);

            Material matHANDRorr = Object.Instantiate(skinHandReturns.rendererInfos[0].defaultMaterial);


            Texture2D HanD_Diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/HanD_DiffuseORANGE.png");
            HanD_Diffuse.wrapMode = TextureWrapMode.Repeat;
 

            Texture2D HanDWeapon_Diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/HanDWeapon_DiffuseORANGE.png");
            HanDWeapon_Diffuse.wrapMode = TextureWrapMode.Clamp;

            //RGB24
            Texture2D texDroneBody = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/texDroneBodyORANGE.png");
            texDroneBody.filterMode = FilterMode.Point;
            texDroneBody.wrapMode = TextureWrapMode.Repeat;

            Texture2D RoRRHANDTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/RoRRHANDTexORANGE.png");
            RoRRHANDTex.wrapMode = TextureWrapMode.Repeat;
 


            Color ExtraColor = new Color(1f, 1f, 1f);

            matHANDDefault.mainTexture = HanD_Diffuse;
            matHANDDefault.color = ExtraColor;
            //matHANDDefault.SetTexture("_EmTex", HanD_Emission);
            matHANDDefault.SetColor("_EmColor", new Color(1.6f, 1, 1f));

            matHANDWeaponDefault.mainTexture = HanDWeapon_Diffuse;
            matHANDWeaponDefault.color = ExtraColor;

            matDroneBody.mainTexture = texDroneBody;
            matDroneBody.color = ExtraColor;
            //matDroneBody.SetTexture("_EmTex", texDroneBodyEmission);
            matDroneBody.SetColor("_EmColor", new Color(1.4f, 1, 1f));

            matHANDRorr.mainTexture = RoRRHANDTex;
            matHANDRorr.color = ExtraColor;
            //matHANDRorr.SetTexture("_EmTex", RoRRHANDTexEmission);
            matHANDRorr.SetColor("_EmColor", new Color(1.1f, 1, 1f));

            NewRenderInfos[0].defaultMaterial = matHANDDefault;
            NewRenderInfos[1].defaultMaterial = matHANDWeaponDefault;
            NewRenderInfos[2].defaultMaterial = matDroneBody;
            NewRenderInfosRETURNS[0].defaultMaterial = matHANDRorr;
            NewRenderInfosRETURNS[1].defaultMaterial = matHANDRorr;
            NewRenderInfosRETURNS[2].defaultMaterial = matHANDRorr;
            //


            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinHandBodyWolfoRusted_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_HAND_ORANGE";
            newSkinDef.icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/texHANDSkinIconDefaultORANGE.png"));
            newSkinDef.baseSkins = skinHandReturns.baseSkins;
            newSkinDef.meshReplacements = skinHandDefault.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinHandDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinHandDefault.rootObject;

            SkinDefWolfo newSkinDefRETURNS = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefRETURNS.name = "skinHandBodyWolfoRusted_RETURN_Simu";
            newSkinDefRETURNS.nameToken = "SIMU_SKIN_HAND2_ORANGE";
            newSkinDefRETURNS.icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/texHANDSkinIconRorrORANGE.png"));
            newSkinDefRETURNS.baseSkins = skinHandReturns.baseSkins;
            newSkinDefRETURNS.meshReplacements = skinHandReturns.meshReplacements;
            newSkinDefRETURNS.projectileGhostReplacements = skinHandReturns.projectileGhostReplacements;
            newSkinDefRETURNS.rendererInfos = NewRenderInfosRETURNS;
            newSkinDefRETURNS.rootObject = skinHandDefault.rootObject;

            modelSkinController.skins = modelSkinController.skins.Add(newSkinDef, newSkinDefRETURNS);
            BodyCatalog.skins[(int)HandBodyIndex] = BodyCatalog.skins[(int)HandBodyIndex].Add(newSkinDef, newSkinDefRETURNS);
        }

        internal static void Hand_Green(GameObject HandBody)
        {
            BodyIndex HandBodyIndex = HandBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = HandBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinHandDefault = modelSkinController.skins[0];
            SkinDef skinHandReturns = modelSkinController.skins[2];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinHandDefault.rendererInfos.Length];
            System.Array.Copy(skinHandDefault.rendererInfos, NewRenderInfos, skinHandDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRETURNS = new CharacterModel.RendererInfo[skinHandReturns.rendererInfos.Length];
            System.Array.Copy(skinHandReturns.rendererInfos, NewRenderInfosRETURNS, skinHandReturns.rendererInfos.Length);


            //0 matHANDRorr
            //1 matHANDRorr
            //2 matHANDRorr
            //3 matDroneSawTemp

            Material matHANDDefault = Object.Instantiate(skinHandDefault.rendererInfos[0].defaultMaterial);
            Material matHANDWeaponDefault = Object.Instantiate(skinHandDefault.rendererInfos[1].defaultMaterial);
            Material matDroneBody = Object.Instantiate(skinHandDefault.rendererInfos[2].defaultMaterial);

            Material matHANDRorr = Object.Instantiate(skinHandReturns.rendererInfos[0].defaultMaterial);


            Texture2D HanD_Diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/HanD_DiffuseGREEN.png");
            HanD_Diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D HanD_Emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/HanD_EmissionGREEN.png");
            HanD_Emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D HanDWeapon_Diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/HanDWeapon_DiffuseGREEN.png");
            HanDWeapon_Diffuse.wrapMode = TextureWrapMode.Clamp;

            //RGB24
            Texture2D texDroneBody = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/texDroneBodyGREEN.png");
            texDroneBody.filterMode = FilterMode.Point;
            texDroneBody.wrapMode = TextureWrapMode.Repeat;

            //RGB24
            Texture2D texDroneBodyEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/texDroneBodyEmissionGREEN.png");
            texDroneBodyEmission.filterMode = FilterMode.Point;
            texDroneBodyEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D RoRRHANDTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/RoRRHANDTexGREEN.png");
            RoRRHANDTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D RoRRHANDTexEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/RoRRHANDTexEmissionGREEN.png");
            RoRRHANDTexEmission.wrapMode = TextureWrapMode.Repeat;


            Color ExtraColor = new Color(1f, 1f, 1f);
            matHANDDefault.mainTexture = HanD_Diffuse;
            matHANDDefault.color = ExtraColor;
            matHANDDefault.SetTexture("_EmTex", HanD_Emission);
            matHANDDefault.SetColor("_EmColor", new Color(1.44f, 1.6f, 0.6f));

            matHANDWeaponDefault.mainTexture = HanDWeapon_Diffuse;
            matHANDWeaponDefault.color = ExtraColor;

            matDroneBody.mainTexture = texDroneBody;
            matDroneBody.color = ExtraColor;
            matDroneBody.SetTexture("_EmTex", texDroneBodyEmission);
            matDroneBody.SetColor("_EmColor", new Color(1.26f, 1.4f, 0.6f));

            matHANDRorr.mainTexture = RoRRHANDTex;
            matHANDRorr.color = ExtraColor;
            matHANDRorr.SetTexture("_EmTex", RoRRHANDTexEmission);
            matHANDRorr.SetColor("_EmColor", new Color(1f, 1.1f, 0.6f));

            NewRenderInfos[0].defaultMaterial = matHANDDefault;
            NewRenderInfos[1].defaultMaterial = matHANDWeaponDefault;
            NewRenderInfos[2].defaultMaterial = matDroneBody;
            NewRenderInfosRETURNS[0].defaultMaterial = matHANDRorr;
            NewRenderInfosRETURNS[1].defaultMaterial = matHANDRorr;
            NewRenderInfosRETURNS[2].defaultMaterial = matHANDRorr;
            //


            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinHandBodyWolfo_Green_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_HAND_GREEN";
            newSkinDef.icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/texHANDSkinIconDefaultGREEN.png"));
            newSkinDef.baseSkins = skinHandReturns.baseSkins;
            newSkinDef.meshReplacements = skinHandDefault.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinHandDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinHandDefault.rootObject;

            SkinDefWolfo newSkinDefRETURNS = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefRETURNS.name = "skinHandBodyWolfo_Green_RETURN_Simu";
            newSkinDefRETURNS.nameToken = "SIMU_SKIN_HAND2_GREEN";
            newSkinDefRETURNS.icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/texHANDSkinIconRorrGREEN.png"));
            newSkinDefRETURNS.baseSkins = skinHandReturns.baseSkins;
            newSkinDefRETURNS.meshReplacements = skinHandReturns.meshReplacements;
            newSkinDefRETURNS.projectileGhostReplacements = skinHandReturns.projectileGhostReplacements;
            newSkinDefRETURNS.rendererInfos = NewRenderInfosRETURNS;
            newSkinDefRETURNS.rootObject = skinHandDefault.rootObject;

            //modelSkinController.skins = modelSkinController.skins.Add(newSkinDef, newSkinDefRETURNS);
            //BodyCatalog.skins[(int)HandBodyIndex] = BodyCatalog.skins[(int)HandBodyIndex].Add(newSkinDef, newSkinDefRETURNS);
            modelSkinController.skins = modelSkinController.skins.Add(newSkinDef, newSkinDefRETURNS);
            BodyCatalog.skins[(int)HandBodyIndex] = BodyCatalog.skins[(int)HandBodyIndex].Add(newSkinDef, newSkinDefRETURNS);

        }


        [RegisterAchievement("CLEAR_ANY_HANDOVERCLOCKED", "Skins.HANDOverclocked.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumHANDOverclockedBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HANDOverclockedBody");
            }
        }

        /*
        [RegisterAchievement("CLEAR_BOTH_HANDOVERCLOCKED", "Skins.HANDOverclocked.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumHANDOverclockedBody2 : Achievement_AltBoss
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HANDOverclockedBody");
            }
        }
        */
    }
}