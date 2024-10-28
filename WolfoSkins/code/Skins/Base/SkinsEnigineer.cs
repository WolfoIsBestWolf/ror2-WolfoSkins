using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsEngineer
    {
        internal static void Start()
        {
            EngiSkin();
            EngiSkinBLUE();
            Engi_AltColossus();
        }

        internal static void Engi_AltColossus()
        {
            //RoRR Red/Yellow Engineer
            SkinDef skinEngiDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiDefault.asset").WaitForCompletion();
            SkinDef skinEngiAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiAltColossus.asset").WaitForCompletion();
            SkinDef skinEngiTurretDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretDefault.asset").WaitForCompletion();
            SkinDef skinEngiTurretAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretAltColossus.asset").WaitForCompletion();

            //Engi
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[3];
            System.Array.Copy(skinEngiAltColossus.rendererInfos, NewRenderInfos, 3);

            CharacterModel.RendererInfo[] TurretNewRenderInfos = new CharacterModel.RendererInfo[1];
            System.Array.Copy(skinEngiTurretAltColossus.rendererInfos, TurretNewRenderInfos, 1);

            Material matEngiAltColossus = Object.Instantiate(skinEngiAltColossus.rendererInfos[2].defaultMaterial);
            Material matEngiTrail = Object.Instantiate(skinEngiAltColossus.rendererInfos[0].defaultMaterial);
            Material matEngiTurret = Object.Instantiate(skinEngiTurretDefault.rendererInfos[0].defaultMaterial);

            Texture2D texEngiAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texEngiAltColossusDiffuse.png");
            texEngiAltColossusDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampEngiAltColossus = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texRampEngiAltColossus.png");
            texRampEngiAltColossus.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampCrocoDiseaseDark1 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texRampCrocoDiseaseDark.png");
            texRampCrocoDiseaseDark1.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampLightning21 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texRampLightning2.png");
            texRampLightning21.wrapMode = TextureWrapMode.Clamp;

            Texture2D texEngiTurretAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/texEngiTurretAltColossusDiffuse.png");
            texEngiTurretAltColossusDiffuse.wrapMode = TextureWrapMode.Repeat;

            matEngiTurret.mainTexture = texEngiTurretAltColossusDiffuse;
            matEngiTurret.SetTexture("_PrintRamp", texRampEngiAltColossus);
            matEngiTurret.SetColor("_EmColor", new Color(1.1f, 0.1f, 0.1f));

            matEngiAltColossus.mainTexture = texEngiAltColossusDiffuse; //texEngiAltColossusDiffuse
            matEngiAltColossus.SetColor("_EmColor", new Color(1.5f,0f,0f,1f));
            matEngiAltColossus.SetTexture("_FlowHeightRamp", texRampLightning21); //texRampLightning2.png
            matEngiAltColossus.SetTexture("_FresnelRamp", texRampCrocoDiseaseDark1); //texRampCrocoDiseaseDark.png
            matEngiAltColossus.SetFloat("_FlowEmissionStrength", 5f);
            matEngiAltColossus.SetFloat("_FresnelPower", 2f);

            matEngiTrail.SetTexture("_RemapTex", texRampEngiAltColossus);

            NewRenderInfos[0].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[1].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[2].defaultMaterial = matEngiAltColossus;          //matEngi

            TurretNewRenderInfos[0].defaultMaterial = matEngiTurret;
            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[1];
            skinEngiDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            //
            #region ProjectileReplacements
            GameObject EngiGrenadeGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiGrenadeGhostSkin2.prefab").WaitForCompletion(), "EngiGrenadeGhostSkinW_Red", false);
            GameObject EngiMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiMineGhost2.prefab").WaitForCompletion(), "EngiMineGhostW_Red", false);
            GameObject SpiderMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/SpiderMineGhost2.prefab").WaitForCompletion(), "SpiderMineGhostW_Red", false);
            GameObject EngiBubbleShieldGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiBubbleShieldGhost2.prefab").WaitForCompletion(), "EngiBubbleShieldGhostW_Red", false);
            GameObject EngiHarpoonProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoon.prefab").WaitForCompletion();
            GameObject EngiHarpoonGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoonGhost.prefab").WaitForCompletion(), "EngiHarpoonGhostSkinW_Red", false);

            RoR2.SkinDef.ProjectileGhostReplacement[] ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[5];
            skinEngiAltColossus.projectileGhostReplacements.CopyTo(ProjectileGhostReplacements, 0);

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
            newShards.SetTexture("_RemapTex", texRampEngiAltColossus);
            particleSystem.material = newShards;

            EngiHarpoonGhostSkinW.transform.GetChild(1).GetComponent<MeshRenderer>().material = matEngiAltColossus;
            TrailRenderer trailRender = EngiHarpoonGhostSkinW.transform.GetChild(1).GetChild(0).GetComponent<TrailRenderer>(); //matEngiHarpoonTrail 
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

            ProjectileGhostReplacements[0].projectileGhostReplacementPrefab = EngiGrenadeGhostSkinW;
            ProjectileGhostReplacements[1].projectileGhostReplacementPrefab = EngiMineGhostW;
            ProjectileGhostReplacements[2].projectileGhostReplacementPrefab = SpiderMineGhostW;
            ProjectileGhostReplacements[3].projectileGhostReplacementPrefab = EngiBubbleShieldGhostW;
            ProjectileGhostReplacements[4] = new SkinDef.ProjectileGhostReplacement
            {
                projectileGhostReplacementPrefab = EngiHarpoonGhostSkinW,
                projectilePrefab = EngiHarpoonProjectile
            };
            #endregion
            #region MinionReplacements
            //MinionReplacements
            RoR2.SkinDef.MinionSkinReplacement[] MinionSkinReplacements = new SkinDef.MinionSkinReplacement[2];
            skinEngiAltColossus.minionSkinReplacements.CopyTo(MinionSkinReplacements, 0);

            SkinDefInfo SkinInfoTurret = new SkinDefInfo
            {
                Name = "skinEngiTurretAltColossusWolfo",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAltColossus.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = skinEngiTurretDefault.rootObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew = Skins.CreateNewSkinDef(SkinInfoTurret);
            TurretSkinDefNew.Bake();
            MinionSkinReplacements[0].minionSkin = TurretSkinDefNew;

            TurretNewRenderInfos[0].renderer = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<SkinnedMeshRenderer>();
            SkinDefInfo SkinInfoTurret2 = new SkinDefInfo
            {
                Name = "skinEngiTurretAltColossusWolfoWalker",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAltColossus.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).gameObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew2 = Skins.CreateNewSkinDef(SkinInfoTurret2);
            TurretSkinDefNew2.Bake();
            MinionSkinReplacements[1].minionSkin = TurretSkinDefNew2;
            #endregion

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinEngiAltColossusWolfo_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_ENGINEER_COLOSSUS";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/Colossus/EnGI.png"));
            newSkinDef.baseSkins = skinEngiAltColossus.baseSkins;
            newSkinDef.meshReplacements = skinEngiAltColossus.meshReplacements;
            newSkinDef.projectileGhostReplacements = ProjectileGhostReplacements;
            newSkinDef.minionSkinReplacements = MinionSkinReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinEngiDefault.rootObject;
            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0.9f,0.1f,0), //0 1 0.5508 1
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.l/cannonJoint2.l/cannonHead.l/EngiJet/Point Light",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0.9f,0.1f,0),
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.r/cannonJoint2.r/cannonHead.r/EngiJet/Point Light (1)",
                }
            };

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiBody"), newSkinDef);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiTurretBody"), TurretSkinDefNew);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody"), TurretSkinDefNew2);
        }


        internal static void EngiSkin()
        {
            //RoRR Red/Yellow Engineer
            SkinDef skinEngiDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiDefault.asset").WaitForCompletion();
            SkinDef skinEngiAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiAlt.asset").WaitForCompletion();
            SkinDef skinEngiTurretDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretDefault.asset").WaitForCompletion();
            SkinDef skinEngiTurretAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretAlt.asset").WaitForCompletion();

            //Engi
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[3];
            System.Array.Copy(skinEngiDefault.rendererInfos, NewRenderInfos, 3);

            CharacterModel.RendererInfo[] TurretNewRenderInfos = new CharacterModel.RendererInfo[1];
            System.Array.Copy(skinEngiTurretDefault.rendererInfos, TurretNewRenderInfos, 1);

            Material matEngi = Object.Instantiate(skinEngiDefault.rendererInfos[2].defaultMaterial);
            Material matEngiTrail = Object.Instantiate(skinEngiDefault.rendererInfos[0].defaultMaterial);
            Material matEngiTurret = Object.Instantiate(skinEngiTurretDefault.rendererInfos[0].defaultMaterial);

            Texture2D texEngiDiffuseAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texEngiDiffuseAlt2.png");
            texEngiDiffuseAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEngiEmissionAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texEngiEmissionAlt2.png");
            texEngiEmissionAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampEngiAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texRampEngiAlt2.png");
            texRampEngiAlt2.wrapMode = TextureWrapMode.Clamp;

            Texture2D texEngiTurretDiffuseAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/texEngiTurretDiffuseAlt2.png");
            texEngiTurretDiffuseAlt2.wrapMode = TextureWrapMode.Repeat;

            matEngiTurret.mainTexture = texEngiTurretDiffuseAlt2;
            matEngiTurret.SetTexture("_PrintRamp", texRampEngiAlt2);
            matEngiTurret.SetColor("_EmColor", new Color(0.845f, 0.8f, 0.365f));

            matEngi.mainTexture = texEngiDiffuseAlt2;
            matEngi.SetTexture("_EmTex", texEngiEmissionAlt2);
            matEngiTrail.SetTexture("_RemapTex", texRampEngiAlt2);

            NewRenderInfos[0].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[1].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[2].defaultMaterial = matEngi;          //matEngi

            TurretNewRenderInfos[0].defaultMaterial = matEngiTurret;
            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[1];
            skinEngiDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            //
            //ProjectileReplacements
            GameObject EngiGrenadeGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiGrenadeGhostSkin2.prefab").WaitForCompletion(), "EngiGrenadeGhostSkinW", false);
            GameObject EngiMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiMineGhost2.prefab").WaitForCompletion(), "EngiMineGhostW", false);
            GameObject SpiderMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/SpiderMineGhost2.prefab").WaitForCompletion(), "SpiderMineGhostW", false);
            GameObject EngiBubbleShieldGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiBubbleShieldGhost2.prefab").WaitForCompletion(), "EngiBubbleShieldGhostW", false);
            GameObject EngiHarpoonProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoon.prefab").WaitForCompletion();
            GameObject EngiHarpoonGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoonGhost.prefab").WaitForCompletion(), "EngiHarpoonGhostSkinW", false);

            RoR2.SkinDef.ProjectileGhostReplacement[] ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[5];
            skinEngiAlt.projectileGhostReplacements.CopyTo(ProjectileGhostReplacements, 0);

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
            newShards.SetTexture("_RemapTex", texRampEngiAlt2);
            particleSystem.material = newShards;

            EngiHarpoonGhostSkinW.transform.GetChild(1).GetComponent<MeshRenderer>().material = matEngi;
            TrailRenderer trailRender = EngiHarpoonGhostSkinW.transform.GetChild(1).GetChild(0).GetComponent<TrailRenderer>(); //matEngiHarpoonTrail 
            Material newTrail = Object.Instantiate(trailRender.material);
            newTrail.SetTexture("_RemapTex", texRampEngiAlt2);
            trailRender.material = newTrail;

            //EngiHarpoonGhostSkinW.transform.GetChild(2).GetComponent<ParticleSystemRenderer>(); //GenericFlash
            particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>(); //matEngiShieldSHards 
            newShards = Object.Instantiate(particleSystem.material);
            newShards.SetTexture("_RemapTex", texRampEngiAlt2);
            newShards.SetColor("_TintColor", new Color(1,1,0,1));
            particleSystem.material = newShards;

            //Would ideally also need a muzzle flash replacement guh

            ProjectileGhostReplacements[0].projectileGhostReplacementPrefab = EngiGrenadeGhostSkinW;
            ProjectileGhostReplacements[1].projectileGhostReplacementPrefab = EngiMineGhostW;
            ProjectileGhostReplacements[2].projectileGhostReplacementPrefab = SpiderMineGhostW;
            ProjectileGhostReplacements[3].projectileGhostReplacementPrefab = EngiBubbleShieldGhostW;
            ProjectileGhostReplacements[4] = new SkinDef.ProjectileGhostReplacement
            {
                projectileGhostReplacementPrefab = EngiHarpoonGhostSkinW,
                projectilePrefab = EngiHarpoonProjectile
            };
            //
            //MinionReplacements
            RoR2.SkinDef.MinionSkinReplacement[] MinionSkinReplacements = new SkinDef.MinionSkinReplacement[2];
            skinEngiAlt.minionSkinReplacements.CopyTo(MinionSkinReplacements, 0);

            SkinDefInfo SkinInfoTurret = new SkinDefInfo
            {
                Name = "skinEngiTurretWolfo",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAlt.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = skinEngiTurretDefault.rootObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew = Skins.CreateNewSkinDef(SkinInfoTurret);
            TurretSkinDefNew.Bake();
            MinionSkinReplacements[0].minionSkin = TurretSkinDefNew;

            TurretNewRenderInfos[0].renderer = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<SkinnedMeshRenderer>();
            SkinDefInfo SkinInfoTurret2 = new SkinDefInfo
            {
                Name = "skinEngiTurretWalkerWolfo",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAlt.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).gameObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew2 = Skins.CreateNewSkinDef(SkinInfoTurret2);
            TurretSkinDefNew2.Bake();
            MinionSkinReplacements[1].minionSkin = TurretSkinDefNew2;
            //
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinEngiWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_ENGINEER";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/wine/skinIconEngi.png"));
            newSkinDef.baseSkins = skinEngiAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = ProjectileGhostReplacements;
            newSkinDef.minionSkinReplacements = MinionSkinReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinEngiDefault.rootObject;
            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0.9f,0.9f,0), //0 1 0.5508 1
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.l/cannonJoint2.l/cannonHead.l/EngiJet/Point Light",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0.9f,0.9f,0),
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.r/cannonJoint2.r/cannonHead.r/EngiJet/Point Light (1)",
                }
            };

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiBody"), newSkinDef);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiTurretBody"), TurretSkinDefNew);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody"), TurretSkinDefNew2);
        }

        internal static void EngiSkinBLUE()
        {
            //RoRR Red/Yellow Engineer
            SkinDef skinEngiDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiDefault.asset").WaitForCompletion();
            SkinDef skinEngiAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiAlt.asset").WaitForCompletion();
            SkinDef skinEngiTurretDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretDefault.asset").WaitForCompletion();
            SkinDef skinEngiTurretAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretAlt.asset").WaitForCompletion();

            //Engi
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[3];
            System.Array.Copy(skinEngiDefault.rendererInfos, NewRenderInfos, 3);

            CharacterModel.RendererInfo[] TurretNewRenderInfos = new CharacterModel.RendererInfo[1];
            System.Array.Copy(skinEngiTurretDefault.rendererInfos, TurretNewRenderInfos, 1);

            Material matEngi = Object.Instantiate(skinEngiDefault.rendererInfos[2].defaultMaterial);
            Material matEngiTrail = Object.Instantiate(skinEngiDefault.rendererInfos[0].defaultMaterial);
            Material matEngiTurret = Object.Instantiate(skinEngiTurretDefault.rendererInfos[0].defaultMaterial);

            Texture2D texEngiDiffuseAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texEngiDiffuseAlt2BLUE.png");
            texEngiDiffuseAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEngiEmissionAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texEngiEmissionAlt2BLUE.png");
            texEngiEmissionAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampEngiAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texRampEngiAlt2BLUE.png");
            texRampEngiAlt2.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampEngiAlt2BLUEForWeapons = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texRampEngiAlt2BLUEForWeapons.png");
            texRampEngiAlt2BLUEForWeapons.wrapMode = TextureWrapMode.Clamp;

            Texture2D texEngiTurretDiffuseAlt2 = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/texEngiTurretDiffuseAlt2BLUE.png");
            texEngiTurretDiffuseAlt2.wrapMode = TextureWrapMode.Repeat;

            matEngiTurret.mainTexture = texEngiTurretDiffuseAlt2;
            matEngiTurret.SetTexture("_PrintRamp", texRampEngiAlt2);
            matEngiTurret.SetColor("_EmColor", new Color(2f, 1f, 2f));

            matEngi.mainTexture = texEngiDiffuseAlt2;
            matEngi.SetTexture("_EmTex", texEngiEmissionAlt2);
            matEngi.SetColor("_EmColor", new Color(1.6f, 0.75f, 1.6f, 1));

            matEngiTrail.SetTexture("_RemapTex", texRampEngiAlt2);

            NewRenderInfos[0].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[1].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[2].defaultMaterial = matEngi;          //matEngi

            TurretNewRenderInfos[0].defaultMaterial = matEngiTurret;
            //
            //ProjectileReplacements
            GameObject EngiGrenadeGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiGrenadeGhostSkin2.prefab").WaitForCompletion(), "EngiGrenadeGhostSkinW", false);
            GameObject EngiMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiMineGhost2.prefab").WaitForCompletion(), "EngiMineGhostW", false);
            GameObject SpiderMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/SpiderMineGhost2.prefab").WaitForCompletion(), "SpiderMineGhostW", false);
            GameObject EngiBubbleShieldGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiBubbleShieldGhost2.prefab").WaitForCompletion(), "EngiBubbleShieldGhostW", false);
            GameObject EngiHarpoonProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoon.prefab").WaitForCompletion();
            GameObject EngiHarpoonGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoonGhost.prefab").WaitForCompletion(), "EngiHarpoonGhostSkinW", false);

            RoR2.SkinDef.ProjectileGhostReplacement[] ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[5];
            skinEngiAlt.projectileGhostReplacements.CopyTo(ProjectileGhostReplacements, 0);

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
            newShards.SetTexture("_RemapTex", texRampEngiAlt2BLUEForWeapons);
            particleSystem.material = newShards;

            EngiHarpoonGhostSkinW.transform.GetChild(1).GetComponent<MeshRenderer>().material = matEngi;
            TrailRenderer trailRender = EngiHarpoonGhostSkinW.transform.GetChild(1).GetChild(0).GetComponent<TrailRenderer>(); //matEngiHarpoonTrail 
            Material newTrail = Object.Instantiate(trailRender.material);
            newTrail.SetTexture("_RemapTex", texRampEngiAlt2BLUEForWeapons);
            trailRender.material = newTrail;

            //EngiHarpoonGhostSkinW.transform.GetChild(2).GetComponent<ParticleSystemRenderer>(); //GenericFlash
            particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>(); //matEngiShieldSHards 
            newShards = Object.Instantiate(particleSystem.material);
            newShards.SetTexture("_RemapTex", texRampEngiAlt2BLUEForWeapons);
            newShards.SetColor("_TintColor", new Color(1, 2.7f, 0.1f, 1));
            particleSystem.material = newShards;

            //Would ideally also need a muzzle flash replacement guh

            ProjectileGhostReplacements[0].projectileGhostReplacementPrefab = EngiGrenadeGhostSkinW;
            ProjectileGhostReplacements[1].projectileGhostReplacementPrefab = EngiMineGhostW;
            ProjectileGhostReplacements[2].projectileGhostReplacementPrefab = SpiderMineGhostW;
            ProjectileGhostReplacements[3].projectileGhostReplacementPrefab = EngiBubbleShieldGhostW;
            ProjectileGhostReplacements[4] = new SkinDef.ProjectileGhostReplacement
            {
                projectileGhostReplacementPrefab = EngiHarpoonGhostSkinW,
                projectilePrefab = EngiHarpoonProjectile
            };
            //
            //MinionReplacements
            RoR2.SkinDef.MinionSkinReplacement[] MinionSkinReplacements = new SkinDef.MinionSkinReplacement[2];
            skinEngiAlt.minionSkinReplacements.CopyTo(MinionSkinReplacements, 0);

            SkinDefInfo SkinInfoTurret = new SkinDefInfo
            {
                Name = "skinEngiTurretWolfoBlue",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAlt.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = skinEngiTurretDefault.rootObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew = Skins.CreateNewSkinDef(SkinInfoTurret);
            TurretSkinDefNew.Bake();
            MinionSkinReplacements[0].minionSkin = TurretSkinDefNew;

            TurretNewRenderInfos[0].renderer = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<SkinnedMeshRenderer>();
            SkinDefInfo SkinInfoTurret2 = new SkinDefInfo
            {
                Name = "skinEngiTurretWalkerWolfoBlue",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAlt.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).gameObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew2 = Skins.CreateNewSkinDef(SkinInfoTurret2);
            TurretSkinDefNew2.Bake();
            MinionSkinReplacements[1].minionSkin = TurretSkinDefNew2;
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinEngiAltWolfoBlue_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_ENGINEER_BLUE";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Engi/blue/skinIconEngiBLUE.png"));
            newSkinDef.baseSkins = skinEngiAlt.baseSkins;
            newSkinDef.meshReplacements = skinEngiAlt.meshReplacements;
            newSkinDef.projectileGhostReplacements = ProjectileGhostReplacements;
            newSkinDef.minionSkinReplacements = MinionSkinReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinEngiDefault.rootObject;
            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1.8f,0.9f,1.8f), //0 1 0.5508 1
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.l/cannonJoint2.l/cannonHead.l/EngiJet/Point Light",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1.8f,0.9f,1.8f),
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.r/cannonJoint2.r/cannonHead.r/EngiJet/Point Light (1)",
                }
            };

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiBody"), newSkinDef);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiTurretBody"), TurretSkinDefNew);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody"), TurretSkinDefNew2);
        }

        [RegisterAchievement("CLEAR_ANY_ENGI", "Skins.Engi.Wolfo.First", "Complete30StagesCareer", 5, null)]
        public class ClearSimulacrumTreebotBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EngiBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_ENGI", "Skins.Engi.Wolfo.Both", "Complete30StagesCareer", 5, null)]
        public class ClearSimulacrumTreebotBody2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EngiBody");
            }
        }
    }
}