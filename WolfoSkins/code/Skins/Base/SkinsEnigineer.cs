using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsEngineer
    {
        internal static void Start()
        {
            SkinDef skinEngiDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiDefault.asset").WaitForCompletion();
            SkinDef skinEngiAlt = Addressables.LoadAssetAsync<SkinDef>(key: "17628d80f4c9ee8468fec38d89586df1").WaitForCompletion();
            SkinDef skinEngiAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiAltColossus.asset").WaitForCompletion();

            SkinDef skinEngiTurretDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretDefault.asset").WaitForCompletion();
            SkinDef skinEngiTurretAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretAltColossus.asset").WaitForCompletion();

            SkinDef skinEngiWalkerTurretDefault = Addressables.LoadAssetAsync<SkinDef>(key: "80a2e7bddd0c9c4499107e5521978bd6").WaitForCompletion();
            SkinDef skinEngiWalkerTurretAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "1d85a8d3e48de684ba627a415aa2a6ec").WaitForCompletion();


            EngiSkin(skinEngiDefault, skinEngiTurretDefault, skinEngiWalkerTurretDefault);
            EngiSkinBLUE(skinEngiAlt, skinEngiTurretDefault, skinEngiWalkerTurretDefault);
            Engi_AltColossus(skinEngiAltColossus, skinEngiTurretAltColossus, skinEngiWalkerTurretAltColossus, skinEngiTurretDefault);
        }

        internal static void Engi_AltColossus(SkinDef skinEngiAltColossus, SkinDef skinEngiTurretAltColossus, SkinDef skinEngiWalkerTurretAltColossus, SkinDef skinEngiTurretDefault)
        {
            SkinDefAltColor newSkinDef = CreateNewSkinW(new SkinInfo
            {
                name = "skinEngiAltColossus_DLC2",
                nameToken = "SIMU_SKIN_ENGINEER_COLOSSUS",
                icon = H.GetIcon("engineer_dlc2"),
                original = skinEngiAltColossus,
                unsetMat = true,
            });
            SkinDefAltColor turretSkinDef = CreateNewSkinW(new SkinInfo
            {
                name = "skinEngiTurretAltColossus_DLC2",
                original = skinEngiTurretAltColossus,
            });
            SkinDefAltColor turretSkinDef2 = CreateNewSkinW(new SkinInfo
            {
                name = "skinEngiWalkerTurretAltColossus_DLC2",
                original = skinEngiWalkerTurretAltColossus,
            });

            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            CharacterModel.RendererInfo[] turretRenderInfos = turretSkinDef.skinDefParams.rendererInfos;
            SkinDefParams.ProjectileGhostReplacement[] projectile = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.projectileGhostReplacements);
            System.Array.Resize(ref projectile, projectile.Length + 1);
            SkinDefParams.MinionSkinReplacement[] minion = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.minionSkinReplacements);
            newSkinDef.skinDefParams.projectileGhostReplacements = projectile;
            newSkinDef.skinDefParams.minionSkinReplacements = minion;

            Material matEngiAltColossus = CloneMat(newRenderInfos, 2);
            Material matEngiTrail = CloneMat(newRenderInfos, 0);
            Material matEngiTurret = CloneMat(turretRenderInfos, 0);

            Texture2D texRampEngiAltColossus = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texRampEngiAltColossus.png");

            matEngiTurret.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texEngiTurretAltColossusDiffuse.png");
            matEngiTurret.SetTexture("_PrintRamp", texRampEngiAltColossus);
            matEngiTurret.SetColor("_EmColor", new Color(1.1f, 0.1f, 0.1f));

            matEngiAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texEngiAltColossusDiffuse.png"); //texEngiAltColossusDiffuse
            matEngiAltColossus.SetColor("_EmColor", new Color(1.5f, 0f, 0f, 1f));
            matEngiAltColossus.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texRampLightning2.png")); //texRampLightning2.png
            matEngiAltColossus.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texRampCrocoDiseaseDark.png")); //texRampCrocoDiseaseDark.png
            matEngiAltColossus.SetFloat("_FlowEmissionStrength", 5f);
            matEngiAltColossus.SetFloat("_FresnelPower", 2f);
            matEngiAltColossus.SetFloat("_FresnelBoost", 15f); //20

            matEngiTrail.SetTexture("_RemapTex", texRampEngiAltColossus);

            newRenderInfos[0].defaultMaterial = matEngiTrail;     //matEngiTrail
            newRenderInfos[1].defaultMaterial = matEngiTrail;     //matEngiTrail
            newRenderInfos[2].defaultMaterial = matEngiAltColossus;          //matEngi

            turretSkinDef2.skinDefParams.rendererInfos[0].defaultMaterial = matEngiTurret;

            #region ProjectileReplacements
            GameObject EngiGrenadeGhostSkinW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiGrenadeGhostSkin2.prefab").WaitForCompletion(), "EngiGrenadeGhostSkinW_Red", false);
            GameObject EngiMineGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiMineGhost2.prefab").WaitForCompletion(), "EngiMineGhostW_Red", false);
            GameObject SpiderMineGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/SpiderMineGhost2.prefab").WaitForCompletion(), "SpiderMineGhostW_Red", false);
            GameObject EngiBubbleShieldGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiBubbleShieldGhost2.prefab").WaitForCompletion(), "EngiBubbleShieldGhostW_Red", false);
            GameObject EngiHarpoonProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoon.prefab").WaitForCompletion();
            GameObject EngiHarpoonGhostSkinW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoonGhost.prefab").WaitForCompletion(), "EngiHarpoonGhostSkinW_Red", false);

            EngiGrenadeGhostSkinW.transform.GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            SpiderMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(2).GetComponent<MeshRenderer>().material = matEngiTurret;

            Transform SpiderMineLights = SpiderMineGhostW.transform.Find("mdlEngiSpiderMine/EngiSpiderMineArmature/Base/Screw");

            Color Yellow1 = new Color(0.75f, 0.25f, 0.2f);
            Color Yellow2 = new Color(1, 0.25f, 0.2f);

            SpiderMineLights.GetChild(1).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(1).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(2).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetChild(0).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1

            SpiderMineLights.GetChild(2).GetChild(1).GetComponent<UnityEngine.TrailRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(3).GetComponent<Light>().color = new Color(0.75f, 0.75f, 0.25f);
            SpiderMineLights.GetChild(3).GetComponent<LineRenderer>().startColor = new Color(1, 1, 0.25f);

            SpiderMineLights.GetChild(3).GetChild(0).GetComponent<Light>().color = Yellow1;
            //
            ParticleSystemRenderer particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>(); //matEngiHarpoonRing 
            Material newShards = Object.Instantiate(particleSystem.material);
            newShards.SetColor("_TintColor", new Color(1f, 0.2f, 0.2f, 1));
            particleSystem.material = newShards;

            EngiHarpoonGhostSkinW.transform.GetChild(1).GetComponent<MeshRenderer>().material = matEngiAltColossus;
            TrailRenderer trailRender = EngiHarpoonGhostSkinW.transform.GetChild(1).GetChild(0).GetComponent<TrailRenderer>(); //matEngiHarpoonTrail 
            trailRender.startColor = new Color(1f, 1f, 1f, 0f); //0.4392 1 0.3451 0
            trailRender.endColor = new Color(0.5f, 0.5f, 0.5f, 0f); //0.3451 0.9529 1 0
            Material newTrail = Object.Instantiate(trailRender.material);
            newTrail.SetTexture("_RemapTex", texRampEngiAltColossus);
            trailRender.material = newTrail;

            //EngiHarpoonGhostSkinW.transform.GetChild(2).GetComponent<ParticleSystemRenderer>(); //GenericFlash
            particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>(); //matEngiShieldSHards 
            newShards = Object.Instantiate(particleSystem.material);
            newShards.SetTexture("_RemapTex", texRampEngiAltColossus);
            newShards.SetColor("_TintColor", new Color(1, 1, 0, 1));
            particleSystem.material = newShards;

            //Would ideally also need a muzzle flash replacement guh


            #endregion


            minion[0].minionSkin = turretSkinDef;
            minion[1].minionSkin = turretSkinDef2;
            projectile[0].projectileGhostReplacementPrefab = EngiGrenadeGhostSkinW;
            projectile[1].projectileGhostReplacementPrefab = EngiMineGhostW;
            projectile[2].projectileGhostReplacementPrefab = SpiderMineGhostW;
            projectile[3].projectileGhostReplacementPrefab = EngiBubbleShieldGhostW;
            projectile[4] = new SkinDefParams.ProjectileGhostReplacement
            {
                projectileGhostReplacementPrefab = EngiHarpoonGhostSkinW,
                projectilePrefab = EngiHarpoonProjectile
            };
            newSkinDef.lightColorsChanges = new SkinDefAltColor.LightColorChanges[]
            {
                new SkinDefAltColor.LightColorChanges
                {
                    color = new Color(0.9f,0.1f,0), //0 1 0.5508 1
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.l/cannonJoint2.l/cannonHead.l/EngiJet/Point Light",
                },
                new SkinDefAltColor.LightColorChanges
                {
                    color = new Color(0.9f,0.1f,0),
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.r/cannonJoint2.r/cannonHead.r/EngiJet/Point Light (1)",
                }
            };

            H.AddSkinToObject(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody"), turretSkinDef);
        }


        internal static void EngiSkin(SkinDef skinEngiDefault, SkinDef skinEngiTurretDefault, SkinDef skinEngiWalkerTurretDefault)
        {
            SkinDefAltColor newSkinDef = CreateNewSkinW(new SkinInfo
            {
                name = "skinEngi_1",
                nameToken = "SIMU_SKIN_ENGINEER",
                icon = H.GetIcon("engineer_red"),
                original = skinEngiDefault,
                unsetMat = true,
            });
            SkinDef turretSkinDef = CreateNewSkin(new SkinInfo
            {
                name = "skinEngiTurret_1",
                original = skinEngiTurretDefault,
            });
            SkinDef turretSkinDefW = CreateNewSkin(new SkinInfo
            {
                name = "skinEngiWalkerTurretDefault_1",
                original = skinEngiWalkerTurretDefault,
            });

            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            CharacterModel.RendererInfo[] turretRenderInfos = turretSkinDef.skinDefParams.rendererInfos;
            SkinDefParams.ProjectileGhostReplacement[] projectile = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.projectileGhostReplacements);
            System.Array.Resize(ref projectile, projectile.Length + 1);
            SkinDefParams.MinionSkinReplacement[] minion = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.minionSkinReplacements);
            newSkinDef.skinDefParams.projectileGhostReplacements = projectile;
            newSkinDef.skinDefParams.minionSkinReplacements = minion;

            Material matEngi = CloneMat(newRenderInfos, 2);
            Material matEngiTrail = CloneMat(newRenderInfos, 0);
            Material matEngiTurret = CloneMat(turretRenderInfos, 0);

            Texture2D texRampEngiAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texRampEngiAlt2.png");

            Color red = new Color(1.1f, 1f, 1f);

            matEngiTurret.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texEngiTurretDiffuseAlt2.png");
            matEngiTurret.SetTexture("_PrintRamp", texRampEngiAlt2);
            matEngiTurret.SetColor("_EmColor", new Color(0.845f, 0.8f, 0.365f));
            matEngiTurret.color = red;

            matEngi.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texEngiDiffuseAlt2.png");
            matEngi.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texEngiEmissionAlt2.png"));
            matEngi.color = red;
            matEngiTrail.SetTexture("_RemapTex", texRampEngiAlt2);

            newRenderInfos[0].defaultMaterial = matEngiTrail;     //matEngiTrail
            newRenderInfos[1].defaultMaterial = matEngiTrail;     //matEngiTrail
            newRenderInfos[2].defaultMaterial = matEngi;          //matEngi

            turretSkinDefW.skinDefParams.rendererInfos[0].defaultMaterial = matEngiTurret;
       
            #region
            GameObject EngiGrenadeGhostSkinW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiGrenadeGhostSkin2.prefab").WaitForCompletion(), "EngiGrenadeGhostSkinW", false);
            GameObject EngiMineGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiMineGhost2.prefab").WaitForCompletion(), "EngiMineGhostW", false);
            GameObject SpiderMineGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/SpiderMineGhost2.prefab").WaitForCompletion(), "SpiderMineGhostW", false);
            GameObject EngiBubbleShieldGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiBubbleShieldGhost2.prefab").WaitForCompletion(), "EngiBubbleShieldGhostW", false);
            GameObject EngiHarpoonProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoon.prefab").WaitForCompletion();
            GameObject EngiHarpoonGhostSkinW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoonGhost.prefab").WaitForCompletion(), "EngiHarpoonGhostSkinW", false);

            EngiGrenadeGhostSkinW.transform.GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            SpiderMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(2).GetComponent<MeshRenderer>().material = matEngiTurret;

            Transform SpiderMineLights = SpiderMineGhostW.transform.Find("mdlEngiSpiderMine/EngiSpiderMineArmature/Base/Screw");

            Color Yellow1 = new Color(0.75f, 0.75f, 0.25f);
            Color Yellow2 = new Color(1, 1, 0.25f);

            SpiderMineLights.GetChild(1).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(1).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(2).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetChild(0).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1

            SpiderMineLights.GetChild(2).GetChild(1).GetComponent<UnityEngine.TrailRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(3).GetComponent<Light>().color = new Color(0.75f, 0.75f, 0.25f);
            SpiderMineLights.GetChild(3).GetComponent<LineRenderer>().startColor = new Color(1, 1, 0.25f);

            SpiderMineLights.GetChild(3).GetChild(0).GetComponent<Light>().color = Yellow1;
            //
            ParticleSystemRenderer particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>(); //matEngiHarpoonRing 
            Material newShards = Object.Instantiate(particleSystem.material);
            newShards.SetColor("_TintColor", new Color(1f, 1f, 0.2f, 1));
            particleSystem.material = newShards;

            EngiHarpoonGhostSkinW.transform.GetChild(1).GetComponent<MeshRenderer>().material = matEngi;
            TrailRenderer trailRender = EngiHarpoonGhostSkinW.transform.GetChild(1).GetChild(0).GetComponent<TrailRenderer>(); //matEngiHarpoonTrail 
            trailRender.startColor = new Color(1f, 1f, 0.75f, 0f); //0.4392 1 0.3451 0
            trailRender.endColor = new Color(0.6f, 1f, 0.25f, 0f); //0.3451 0.9529
            Material newTrail = Object.Instantiate(trailRender.material);
            newTrail.SetTexture("_RemapTex", texRampEngiAlt2);
            trailRender.material = newTrail;

            //EngiHarpoonGhostSkinW.transform.GetChild(2).GetComponent<ParticleSystemRenderer>(); //GenericFlash
            particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>(); //matEngiShieldSHards 
            newShards = Object.Instantiate(particleSystem.material);
            newShards.SetTexture("_RemapTex", texRampEngiAlt2);
            newShards.SetColor("_TintColor", new Color(1, 1f, 0, 1));
            particleSystem.material = newShards;
            #endregion

            minion[0].minionSkin = turretSkinDef;
            minion[1].minionSkin = turretSkinDefW;
            projectile[0].projectileGhostReplacementPrefab = EngiGrenadeGhostSkinW;
            projectile[1].projectileGhostReplacementPrefab = EngiMineGhostW;
            projectile[2].projectileGhostReplacementPrefab = SpiderMineGhostW;
            projectile[3].projectileGhostReplacementPrefab = EngiBubbleShieldGhostW;
            projectile[4] = new SkinDefParams.ProjectileGhostReplacement
            {
                projectileGhostReplacementPrefab = EngiHarpoonGhostSkinW,
                projectilePrefab = EngiHarpoonProjectile
            };
            newSkinDef.lightColorsChanges = new SkinDefAltColor.LightColorChanges[]
            {
                new SkinDefAltColor.LightColorChanges
                {
                    color = new Color(0.9f,0.9f,0), //0 1 0.5508 1
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.l/cannonJoint2.l/cannonHead.l/EngiJet/Point Light",
                },
                new SkinDefAltColor.LightColorChanges
                {
                    color = new Color(0.9f,0.9f,0),
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.r/cannonJoint2.r/cannonHead.r/EngiJet/Point Light (1)",
                }
            };


            H.AddSkinToObject(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody"), turretSkinDef);
        }

        internal static void EngiSkinBLUE(SkinDef skinEngiAlt, SkinDef skinEngiTurretDefault, SkinDef skinEngiWalkerTurretDefault)
        {
            SkinDefAltColor newSkinDef = CreateNewSkinW(new SkinInfo
            {
                name = "skinEngiAltWolfoBlue_1",
                nameToken = "SIMU_SKIN_ENGINEER_BLUE",
                icon = H.GetIcon("engineer_blue"),
                original = skinEngiAlt,
                unsetMat = true,
            });
            SkinDefAltColor turretSkinDef = CreateNewSkinW(new SkinInfo
            {
                name = "skinEngiTurretAlt_1",
                original = skinEngiTurretDefault,
            });
            SkinDefAltColor turretSkinDefW = CreateNewSkinW(new SkinInfo
            {
                name = "skinEngiWalkerTurretAlt_1",
                original = skinEngiWalkerTurretDefault,
            });

            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            CharacterModel.RendererInfo[] turretRenderInfos = turretSkinDef.skinDefParams.rendererInfos;
            SkinDefParams.ProjectileGhostReplacement[] projectile = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.projectileGhostReplacements);
            System.Array.Resize(ref projectile, projectile.Length + 1);
            SkinDefParams.MinionSkinReplacement[] minion = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.minionSkinReplacements);
            newSkinDef.skinDefParams.projectileGhostReplacements = projectile;
            newSkinDef.skinDefParams.minionSkinReplacements = minion;

            Material matEngi = CloneMat(newRenderInfos, 2);
            Material matEngiTrail = CloneMat(newRenderInfos, 0);
            Material matEngiTurret = CloneMat(turretRenderInfos, 0);

            Texture2D texEngiDiffuseAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texEngiDiffuseAlt2BLUE.png");
            texEngiDiffuseAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEngiEmissionAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texEngiEmissionAlt2BLUE.png");
            texEngiEmissionAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampEngiAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texRampEngiAlt2BLUE.png");

            Texture2D texEngiTurretDiffuseAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texEngiTurretDiffuseAlt2BLUE.png");

            matEngiTurret.mainTexture = texEngiTurretDiffuseAlt2;
            matEngiTurret.SetTexture("_PrintRamp", texRampEngiAlt2);
            matEngiTurret.SetColor("_EmColor", new Color(2f, 1f, 2f) * 0.8f);

            matEngi.mainTexture = texEngiDiffuseAlt2;
            matEngi.SetTexture("_EmTex", texEngiEmissionAlt2);
            matEngi.SetColor("_EmColor", new Color(1.6f, 0.75f, 1.6f, 1) * 0.8f);

            matEngiTrail.SetTexture("_RemapTex", texRampEngiAlt2);

            newRenderInfos[0].defaultMaterial = matEngiTrail;     //matEngiTrail
            newRenderInfos[1].defaultMaterial = matEngiTrail;     //matEngiTrail
            newRenderInfos[2].defaultMaterial = matEngi;          //matEngi

            turretSkinDefW.skinDefParams.rendererInfos[0].defaultMaterial = matEngiTurret;

#region ProjectileReplacements
            GameObject EngiGrenadeGhostSkinW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiGrenadeGhostSkin2.prefab").WaitForCompletion(), "EngiGrenadeGhostSkinW_B", false);
            GameObject EngiMineGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiMineGhost2.prefab").WaitForCompletion(), "EngiMineGhostW_B", false);
            GameObject SpiderMineGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/SpiderMineGhost2.prefab").WaitForCompletion(), "SpiderMineGhostW_B", false);
            GameObject EngiBubbleShieldGhostW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiBubbleShieldGhost2.prefab").WaitForCompletion(), "EngiBubbleShieldGhostW_B", false);
            GameObject EngiHarpoonProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoon.prefab").WaitForCompletion();
            GameObject EngiHarpoonGhostSkinW = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoonGhost.prefab").WaitForCompletion(), "EngiHarpoonGhostSkinW_B", false);

            EngiGrenadeGhostSkinW.transform.GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            SpiderMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(2).GetComponent<MeshRenderer>().material = matEngiTurret;

            Transform SpiderMineLights = SpiderMineGhostW.transform.Find("mdlEngiSpiderMine/EngiSpiderMineArmature/Base/Screw");

            Color Yellow1 = new Color(0.35f, 0.75f, 0.75f);
            Color Yellow2 = new Color(0.35f, 1.25f, 1.25f);

            SpiderMineLights.GetChild(1).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(1).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(2).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetChild(0).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1

            SpiderMineLights.GetChild(2).GetChild(1).GetComponent<UnityEngine.TrailRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(3).GetComponent<Light>().color = Yellow1;
            SpiderMineLights.GetChild(3).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(3).GetChild(0).GetComponent<Light>().color = Yellow1;
            //
            ParticleSystemRenderer particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>(); //matEngiHarpoonRing 
            Material newShards = Object.Instantiate(particleSystem.material);
            newShards.SetColor("_TintColor", new Color(1f, 0.375f, 1f, 1));
            particleSystem.material = newShards;

            EngiHarpoonGhostSkinW.transform.GetChild(1).GetComponent<MeshRenderer>().material = matEngi;
            TrailRenderer trailRender = EngiHarpoonGhostSkinW.transform.GetChild(1).GetChild(0).GetComponent<TrailRenderer>(); //matEngiHarpoonTrail 
            trailRender.startColor = new Color(1.2f, 1f, 1f, 0f); //0.4392 1 0.3451 0
            trailRender.endColor = new Color(1.2f, 1f, 1f, 0f); //0.3451 0.9529 1 0
            Material newTrail = Object.Instantiate(trailRender.material);
            newTrail.SetTexture("_RemapTex", texRampEngiAlt2);
            trailRender.material = newTrail;
#endregion

            minion[0].minionSkin = turretSkinDef;
            minion[1].minionSkin = turretSkinDefW;
            projectile[0].projectileGhostReplacementPrefab = EngiGrenadeGhostSkinW;
            projectile[1].projectileGhostReplacementPrefab = EngiMineGhostW;
            projectile[2].projectileGhostReplacementPrefab = SpiderMineGhostW;
            projectile[3].projectileGhostReplacementPrefab = EngiBubbleShieldGhostW;
            projectile[4] = new SkinDefParams.ProjectileGhostReplacement
            {
                projectileGhostReplacementPrefab = EngiHarpoonGhostSkinW,
                projectilePrefab = EngiHarpoonProjectile
            };
            newSkinDef.lightColorsChanges = new SkinDefAltColor.LightColorChanges[]
            {
                new SkinDefAltColor.LightColorChanges
                {
                    color = new Color(1.8f,0.9f,1.8f), //0 1 0.5508 1
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.l/cannonJoint2.l/cannonHead.l/EngiJet/Point Light",
                },
                new SkinDefAltColor.LightColorChanges
                {
                    color = new Color(1.8f,0.9f,1.8f),
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.r/cannonJoint2.r/cannonHead.r/EngiJet/Point Light (1)",
                }
            };

            H.AddSkinToObject(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody"), turretSkinDef);
        }

        [RegisterAchievement("CLEAR_ANY_ENGI", "Skins.Engi.Wolfo.First", "Complete30StagesCareer", 5, null)]
        public class ClearSimulacrumTreebotBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EngiBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_ENGI", "Skins.Engi.Wolfo.Both", "Complete30StagesCareer", 5, null)]
        public class ClearSimulacrumTreebotBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EngiBody");
            }
        }
    }
}