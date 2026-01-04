using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Mod
{
    public class SkinsHand
    {
        public static GameObject GoldAffixEffectNew;
        internal static void ModdedSkin(GameObject HandBody)
        {
            BodyIndex HandBodyIndex = HandBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = HandBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinHandDefault = modelSkinController.skins[0];
            SkinDef skinHandReturns = modelSkinController.skins[2];

            SkinDef Gold = Hand_Gold(skinHandDefault);
            SkinDef Rust = Hand_Rusty(skinHandDefault);
            SkinDef Oxid = Hand_Green(skinHandDefault);
            SkinDef GoldR = Hand_Gold_RETURNS(skinHandReturns);
            SkinDef RustR = Hand_Rusty_RETURNS(skinHandReturns);
            SkinDef OxidR = Hand_Green_RETURNS(skinHandReturns);


            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length];
            skinsNew[0] = modelSkinController.skins[0];
            skinsNew[1] = modelSkinController.skins[1];
            skinsNew[2] = modelSkinController.skins[2];
            skinsNew[3] = Gold;
            skinsNew[4] = Rust;
            skinsNew[5] = Oxid;
            skinsNew[6] = GoldR;
            skinsNew[7] = RustR;
            skinsNew[8] = OxidR;

            for (int i = 9; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i] = modelSkinController.skins[i - 6];
            }
            modelSkinController.skins = skinsNew;
            //SkinCatalog.skinsByBody[(int)HandBodyIndex] = skinsNew;

            //0 matHANDRorr
            //1 matHANDRorr
            //2 matHANDRorr
            //3 matDroneSawTemp

        }

        internal static SkinDef Hand_Gold(SkinDef skinHandDefault)
        {
            SkinDefEnhanced newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinHANDOverclockedGold_1",
                nameToken = "SIMU_SKIN_HAND",
                icon = H.GetIcon("mod/ror1/hand_gold"),
                original = skinHandDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matHANDDefault = CloneMat(ref newRenderInfos, 0);
            Material matHANDWeaponDefault = CloneMat(ref newRenderInfos, 1);
            Material matDroneBody = CloneMat(ref newRenderInfos, 2);

            Color ExtraGold = new Color(1.2f, 1.1f, 1f);

            matHANDDefault.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/HanD_Diffuse.png");
            matHANDDefault.color = ExtraGold;
            matHANDDefault.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/HanD_Emission.png"));
            matHANDDefault.SetColor("_EmColor", new Color(1, 1, 1.8f));

            matHANDWeaponDefault.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/HanDWeapon_Diffuse.png");
            matHANDWeaponDefault.color = ExtraGold;

            matDroneBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/texDroneBody.png");
            matDroneBody.color = ExtraGold;
            matDroneBody.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/texDroneBodyEmission.png"));
            matDroneBody.SetColor("_EmColor", new Color(1, 1, 1.5f));

            newRenderInfos[0].defaultMaterial = matHANDDefault;
            newRenderInfos[1].defaultMaterial = matHANDWeaponDefault;
            newRenderInfos[2].defaultMaterial = matDroneBody;

            //ADD SHINY SPARKLES


            GameObject GoldAffixEffect = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Junk/EliteGold/GoldAffixEffect.prefab").WaitForCompletion();
            GoldAffixEffectNew = R2API.PrefabAPI.InstantiateClone(GoldAffixEffect, "HANDGoldSparkles", false);

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
            return newSkinDef;
        }

        internal static SkinDef Hand_Rusty(SkinDef skinHandDefault)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinHANDOverclockedRusted_1",
                nameToken = "SIMU_SKIN_HAND_ORANGE",
                icon = H.GetIcon("mod/ror1/hand_orange"),
                original = skinHandDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matHANDDefault = CloneMat(ref newRenderInfos, 0);
            Material matHANDWeaponDefault = CloneMat(ref newRenderInfos, 1);
            Material matDroneBody = CloneMat(ref newRenderInfos, 2);

            Color ExtraColor = new Color(1f, 1f, 1f);

            matHANDDefault.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/HanD_DiffuseORANGE.png");
            matHANDDefault.color = ExtraColor;
            //matHANDDefault.SetTexture("_EmTex", HanD_Emission);
            matHANDDefault.SetColor("_EmColor", new Color(1.6f, 1, 1f));

            matHANDWeaponDefault.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/HanDWeapon_DiffuseORANGE.png");
            matHANDWeaponDefault.color = ExtraColor;

            matDroneBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/texDroneBodyORANGE.png");
            matDroneBody.color = ExtraColor;
            //matDroneBody.SetTexture("_EmTex", texDroneBodyEmission);
            matDroneBody.SetColor("_EmColor", new Color(1.4f, 1, 1f));

            newRenderInfos[0].defaultMaterial = matHANDDefault;
            newRenderInfos[1].defaultMaterial = matHANDWeaponDefault;
            newRenderInfos[2].defaultMaterial = matDroneBody;

            return newSkinDef;
        }

        internal static SkinDef Hand_Green(SkinDef skinHandDefault)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinHANDOverclockedOxidized_1",
                nameToken = "SIMU_SKIN_HAND_GREEN",
                icon = H.GetIcon("mod/ror1/hand_green"),
                original = skinHandDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matHANDDefault = CloneMat(ref newRenderInfos, 0);
            Material matHANDWeaponDefault = CloneMat(ref newRenderInfos, 1);
            Material matDroneBody = CloneMat(ref newRenderInfos, 2);


            Color ExtraColor = new Color(1f, 1f, 1f);
            matHANDDefault.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/HanD_DiffuseGREEN.png");
            matHANDDefault.color = ExtraColor;
            matHANDDefault.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/HanD_EmissionGREEN.png"));
            matHANDDefault.SetColor("_EmColor", new Color(1.44f, 1.6f, 0.6f));

            matHANDWeaponDefault.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/HanDWeapon_DiffuseGREEN.png");
            matHANDWeaponDefault.color = ExtraColor;

            matDroneBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/texDroneBodyGREEN.png");
            matDroneBody.color = ExtraColor;
            matDroneBody.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/texDroneBodyEmissionGREEN.png"));
            matDroneBody.SetColor("_EmColor", new Color(1.26f, 1.4f, 0.6f));

            newRenderInfos[0].defaultMaterial = matHANDDefault;
            newRenderInfos[1].defaultMaterial = matHANDWeaponDefault;
            newRenderInfos[2].defaultMaterial = matDroneBody;

            return newSkinDef;
        }

        internal static SkinDef Hand_Gold_RETURNS(SkinDef skinHandReturns)
        {
            SkinDefEnhanced newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinHANDOverclockedGold_RETURN_1",
                nameToken = "SIMU_SKIN_HAND2",
                icon = H.GetIcon("mod/ror1/hand_goldGM"),
                original = skinHandReturns,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matHANDRorr = CloneMat(ref newRenderInfos, 0);

            Color ExtraGold = new Color(1.2f, 1.1f, 1f);

            matHANDRorr.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/textrimsheetjanitortransparent.png");
            matHANDRorr.color = ExtraGold;
            matHANDRorr.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/Gold/textrimsheetconstructionemission.png"));
            matHANDRorr.SetColor("_EmColor", new Color(1, 1, 1.1f));

            newRenderInfos[0].defaultMaterial = matHANDRorr;
            newRenderInfos[1].defaultMaterial = matHANDRorr;
            newRenderInfos[2].defaultMaterial = matHANDRorr;


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
            return newSkinDef;
        }

        internal static SkinDef Hand_Rusty_RETURNS(SkinDef skinHandReturns)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinHANDOverclockedOxidized_RETURN_1",
                nameToken = "SIMU_SKIN_HAND2_ORANGE",
                icon = H.GetIcon("mod/ror1/hand_orangeGM"),
                original = skinHandReturns,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matHANDRorr = CloneMat(ref newRenderInfos, 0);

            Color ExtraColor = new Color(1f, 1f, 1f);

            matHANDRorr.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/textrimsheetjanitortransparent.png");
            matHANDRorr.color = ExtraColor;
            matHANDRorr.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/ORANGE/textrimsheetconstructionemission.png"));
            matHANDRorr.SetColor("_EmColor", new Color(1.1f, 1, 1f));

            newRenderInfos[0].defaultMaterial = matHANDRorr;
            newRenderInfos[1].defaultMaterial = matHANDRorr;
            newRenderInfos[2].defaultMaterial = matHANDRorr;

            return newSkinDef;
        }

        internal static SkinDef Hand_Green_RETURNS(SkinDef skinHandReturns)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinHANDOverclockedOxidized_RETURN_1",
                nameToken = "SIMU_SKIN_HAND2_GREEN",
                icon = H.GetIcon("mod/ror1/hand_greenGM"),
                original = skinHandReturns,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matHANDRorr = CloneMat(ref newRenderInfos, 0);

            Color ExtraColor = new Color(1f, 1f, 1f);
            matHANDRorr.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/textrimsheetjanitortransparent.png");
            matHANDRorr.color = ExtraColor;
            matHANDRorr.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/HanD/GREEN/textrimsheetconstructionemission.png"));
            matHANDRorr.SetColor("_EmColor", new Color(1f, 1.1f, 0.6f));

            newRenderInfos[0].defaultMaterial = matHANDRorr;
            newRenderInfos[1].defaultMaterial = matHANDRorr;
            newRenderInfos[2].defaultMaterial = matHANDRorr;

            return newSkinDef;
        }



        [RegisterAchievement("CLEAR_ANY_HANDOVERCLOCKED", "Skins.HANDOverclocked.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumHANDOverclockedBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HANDOverclockedBody");
            }
        }

        /*
        [RegisterAchievement("CLEAR_BOTH_HANDOVERCLOCKED", "Skins.HANDOverclocked.Wolfo.Both", null, 3, null)]
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