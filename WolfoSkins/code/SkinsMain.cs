﻿using BepInEx;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using WolfoSkinsMod.Base;
using WolfoSkinsMod.DLC1;
using WolfoSkinsMod.DLC2;
using WolfoSkinsMod.Mod;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[module: UnverifiableCode]

namespace WolfoSkinsMod
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.TheTimeSweeper.RedAlert", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("Wolfo.WolfoSkins", "WolfoSkins", "2.2.4")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]

    public class WolfoSkins : BaseUnityPlugin
    {

        public void Awake()
        {
            WConfig.InitConfig();
 
            if (WConfig.cfgTest.Value)
            {
                WConfig.cfgTest.Value = false;
                Destroy(this);
                return;
            }
            Assets.Init(Info);
            Unlocks.Hooks();


            //BASE
            SkinsCommando.Start();
            SkinsHuntress.Start();
            SkinsBandit2.Start();
            SkinsToolbot_MULT.Start();
            SkinsEngineer.Start();
            SkinsMage_Artificer.Start();
            SkinsMerc.Start();
            SkinsTreebot_REX.Start();
            SkinsLoader.Start();
            SkinsCroco_Acrid.Start();
            SkinsCaptain.Start();
            //DLC1
            SkinsRailGunner.Start();
            SkinsVoidFiend.Start();
            //DLC2
            SkinsSeeker.Start();
            SkinsChef.Start();
            SkinsFalseSon.Start();
            //DLC3
            //
            //

            RuleCatalog.availability.CallWhenAvailable(ModSupport);

            GameModeCatalog.availability.CallWhenAvailable(SortSkinsLate);

            On.RoR2.SkinDef.ApplyAsync += SkinDef_ApplyAsync;

            On.RoR2.TemporaryOverlay.AddToCharacerModel += ReplaceTemporaryOverlayMaterial;
 
            On.RoR2.SkinDef.BakeAsync += SkinDef_BakeAsync;
        }

        private System.Collections.IEnumerator SkinDef_ApplyAsync(On.RoR2.SkinDef.orig_ApplyAsync orig, SkinDef self, GameObject modelObject, List<UnityEngine.AddressableAssets.AssetReferenceT<Material>> loadedMaterials, List<UnityEngine.AddressableAssets.AssetReferenceT<Mesh>> loadedMeshes, RoR2.ContentManagement.AsyncReferenceHandleUnloadType unloadType)
        {
            var temp = orig(self, modelObject, loadedMaterials, loadedMeshes, unloadType);
            while (temp.MoveNext())
            {
                object obj2 = temp.Current;
                yield return obj2;
            }

            //Debug.Log("SkinApply " + self);
            if (modelObject.GetComponent<SkinDefWolfoTracker>())
            {
                modelObject.GetComponent<SkinDefWolfoTracker>().UndoWolfoSkin();
            }
            if (self is SkinDefWolfo)
            {
                (self as SkinDefWolfo).ApplyExtras(modelObject);
            }
            yield break;
        }

        private System.Collections.IEnumerator SkinDef_BakeAsync(On.RoR2.SkinDef.orig_BakeAsync orig, SkinDef self)
        {
            if (self.runtimeSkin == null)
            {
                if (self is SkinDefPrioritizeDirect)
                {
                    for (int i = 0; self.skinDefParams.rendererInfos.Length > i; i++)
                    {
                        if (self.skinDefParams.rendererInfos[i].defaultMaterial != null)
                        {
                            self.skinDefParams.rendererInfos[i].defaultMaterialAddress = null;
                        }
                    }
                }  
            }
            return orig(self);
        }

        private void SkinDef_Apply(On.RoR2.SkinDef.orig_Apply orig, SkinDef self, GameObject model)
        {
            orig(self, model);
            //Debug.Log("SkinApply " + self);
            if (model.GetComponent<SkinDefWolfoTracker>())
            {
                model.GetComponent<SkinDefWolfoTracker>().UndoWolfoSkin();
            }
            if (self is SkinDefWolfo)
            {
                (self as SkinDefWolfo).ApplyExtras(model);
            }
        }

        internal static void SortSkinsLate()
        {
            //What is this again?
            if (!WConfig.cfgSort.Value)
            {
                return;
            }


            //Manually sorting these 
            List<string> blacklistedSorting = new List<string>()
            {
                "Enforcer",
                "HANDOverclocked",
                "Miner",
                "GnomeChefBody",
                "RobPaladin"
            };


            for (int i = 0; i < SurvivorCatalog.survivorDefs.Length; i++)
            {
                if (blacklistedSorting.Contains(SurvivorCatalog.survivorDefs[i].cachedName))
                {
                    continue; //?
                }

                //Debug.LogWarning(SurvivorCatalog.survivorDefs[i]);
                GameObject Body = SurvivorCatalog.survivorDefs[i].bodyPrefab;
                ModelSkinController modelSkinController = Body.GetComponentInChildren<ModelSkinController>();
                if (!modelSkinController)
                {
                    Debug.LogWarning(SurvivorCatalog.survivorDefs[i] + " has no ModelSkinController");
                }
                else if (modelSkinController.skins.Length > 4)
                {
                    BodyIndex Index = Body.GetComponent<CharacterBody>().bodyIndex;

                    List<SkinDef> oldList = new List<SkinDef>();
                    List<SkinDef> wolfList = new List<SkinDef>();
                    for (int ii = 0; ii < modelSkinController.skins.Length; ii++)
                    {
                        //Debug.LogWarning(modelSkinController.skins[ii]);
                        if (modelSkinController.skins[ii].name.Contains("Wolfo"))
                        {
                            wolfList.Add(modelSkinController.skins[ii]);
                        }
                        else
                        {
                            oldList.Add(modelSkinController.skins[ii]);
                        }
                    }
                    if (wolfList.Count > 0)
                    {
                        oldList.AddRange(wolfList);
                        SkinDef[] skinsNew = oldList.ToArray();
                        modelSkinController.skins = skinsNew;
                        SkinCatalog.skinsByBody[(int)Index] = skinsNew;
                        SkinCatalog.skinsByBody[(int)Index] = skinsNew;
                    }
                }
            }
            System.GC.Collect(); //?
        }

        internal static void ModSupport()
        {
            WConfig.RiskConfig();
            SkinsFalseSon.falseSonBodyIndex = DLC2Content.Survivors.FalseSon.bodyPrefab.GetComponent<CharacterBody>().bodyIndex;
            /*GameObject ModdedBody = BodyCatalog.FindBodyPrefab("GnomeChefBody");
            if (ModdedBody != null)
            {
                //SkinsCHEFMod.ModdedSkin(ModdedBody);
            }*/
            //HAND
            GameObject ModdedBody = BodyCatalog.FindBodyPrefab("HANDOverclockedBody");
            if (ModdedBody != null)
            {
                SkinsHand.ModdedSkin(ModdedBody);
            }
            //EnforcerBody
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.EnforcerGang.Enforcer"))
            {
                ModdedBody = BodyCatalog.FindBodyPrefab("EnforcerBody");
                if (ModdedBody != null)
                {
                    SkinsEnforcer.ModdedSkin(ModdedBody);
                }
            }
            //Executioner2Body
            ModdedBody = BodyCatalog.FindBodyPrefab("Executioner2Body");
            if (ModdedBody != null)
            {
                SkinsExecutioner.ModdedSkin(ModdedBody);
            }
            //SniperClassicBody
            ModdedBody = BodyCatalog.FindBodyPrefab("SniperClassicBody");
            if (ModdedBody != null)
            {
                SkinsSniper.ModdedSkin(ModdedBody);
            }
            //RobRavagerBody
            ModdedBody = BodyCatalog.FindBodyPrefab("RobRavagerBody");
            if (ModdedBody != null)
            {
                SkinsRavager.ModdedSkin(ModdedBody);
            }
            //RocketSurvivorBody
            ModdedBody = BodyCatalog.FindBodyPrefab("RocketSurvivorBody");
            if (ModdedBody != null)
            {
                SkinsRocket.ModdedSkin(ModdedBody);
            }
            //MoffeinPilotBody
            ModdedBody = BodyCatalog.FindBodyPrefab("MoffeinPilotBody");
            if (ModdedBody != null)
            {
                SkinsPilot.ModdedSkin(ModdedBody);
            }
            //MinerBody
            ModdedBody = BodyCatalog.FindBodyPrefab("MinerBody");
            if (ModdedBody != null)
            {
                SkinsMiner.ModdedSkin(ModdedBody);
            }
            //ChirrBody
            ModdedBody = BodyCatalog.FindBodyPrefab("ChirrBody");
            if (ModdedBody != null)
            {
                SkinsChirr.ModdedSkin(ModdedBody);
            }
            //
            //ArsonistBody
            ModdedBody = BodyCatalog.FindBodyPrefab("ArsonistBody");
            if (ModdedBody != null)
            {
                SkinsArsonist.ModdedSkin(ModdedBody);
            }
            //Tesla Trooper and the likes
            ModdedBody = BodyCatalog.FindBodyPrefab("TeslaTrooperBody");
            if (ModdedBody != null)
            {
                TeslaDesolatorColors.AddToTeslaTrooper(ModdedBody);
            }
            ModdedBody = BodyCatalog.FindBodyPrefab("TeslaTowerBody");
            if (ModdedBody != null)
            {
                TeslaDesolatorColors.AddToTeslaTower(ModdedBody);
            }
            ModdedBody = BodyCatalog.FindBodyPrefab("DesolatorBody");
            if (ModdedBody != null)
            {
                TeslaDesolatorColors.AddToDesolator(ModdedBody);
            }
            //RobPaladinBody
            ModdedBody = BodyCatalog.FindBodyPrefab("RobPaladinBody");
            if (ModdedBody != null)
            {
                SkinsPaladin.ModdedSkin(ModdedBody);
            }

            //Nemesis-es
            //Nem-Enforcer
            ModdedBody = BodyCatalog.FindBodyPrefab("NemesisEnforcerBody");
            if (ModdedBody != null)
            {
                SkinsNemEnforcer.ModdedSkin(ModdedBody);
            }
        }

        private void ReplaceTemporaryOverlayMaterial(On.RoR2.TemporaryOverlay.orig_AddToCharacerModel orig, TemporaryOverlay self, CharacterModel characterModel)
        {
            if (characterModel)
            {
                OverlayMaterialReplacer overlayMaterialReplacer = characterModel.GetComponent<OverlayMaterialReplacer>();
                if (overlayMaterialReplacer)
                {
                    //Debug.Log(self.originalMaterial);
                    if (self.originalMaterial == overlayMaterialReplacer.targetMaterial)
                    {
                        self.originalMaterial = overlayMaterialReplacer.replacementMaterial;
                    }
                }
            }
            orig(self, characterModel);
        }


    }

    public class SkinDefPrioritizeDirect : SkinDef
    {

    }

    public class SkinDefWolfo : SkinDefPrioritizeDirect
    {

        //Some sort of Undo thing
        public void ApplyExtras(GameObject modelObject)
        {
            CharacterModel model = modelObject.GetComponent<CharacterModel>();

            SkinDefWolfoTracker skinDefWolfoTracker;
            if (!modelObject.TryGetComponent<SkinDefWolfoTracker>(out skinDefWolfoTracker))
            {
                skinDefWolfoTracker = modelObject.AddComponent<SkinDefWolfoTracker>();
            }

            if (changeMaterial.targetMaterial)
            {
                OverlayMaterialReplacer overlayMaterialReplacer = modelObject.AddComponent<OverlayMaterialReplacer>();
                overlayMaterialReplacer.targetMaterial = changeMaterial.targetMaterial;
                overlayMaterialReplacer.replacementMaterial = changeMaterial.replacementMaterial;
            }

            skinDefWolfoTracker.model = model;
            skinDefWolfoTracker.changedLights = new SkinDefWolfoTracker.ChangedLightColors[lightColorsChanges.Length];
            for (int i = 0; lightColorsChanges.Length > i; i++)
            {
                Transform transform = modelObject.transform.Find(lightColorsChanges[i].lightPath);
                if (transform)
                {
                    Light light = transform.GetComponent<Light>();
                    if (light)
                    {
                        skinDefWolfoTracker.changedLights[i] = new SkinDefWolfoTracker.ChangedLightColors
                        {
                            light = light,
                            originalColor = light.color
                        };
                        light.color = lightColorsChanges[i].color;
                        for (int j = 0; model.baseLightInfos.Length > j; j++)
                        {
                            if (model.baseLightInfos[j].light == light)
                            {
                                model.baseLightInfos[j].defaultColor = lightColorsChanges[i].color;
                            }
                        }
                    }
                    TrailRenderer trail = transform.GetComponent<TrailRenderer>();
                    if (trail)
                    {
                        skinDefWolfoTracker.changedLights[i] = new SkinDefWolfoTracker.ChangedLightColors
                        {
                            trail = trail,
                            originalColor = trail.startColor,
                            trailEndColor = trail.endColor
                        };
                        trail.startColor = lightColorsChanges[i].color;
                        trail.endColor = lightColorsChanges[i].color2;
                    }
                    if (!light && !trail)
                    {
                        Debug.LogWarning(lightColorsChanges[i].lightPath + " : Not Light or Trail attached");
                    }
                }
                else
                {
                    Debug.LogWarning(lightColorsChanges[i].lightPath + " : Not Found");
                }
            }

            skinDefWolfoTracker.addedObjects = new GameObject[addGameObjects.Length];
            for (int i = 0; addGameObjects.Length > i; i++)
            {
                Transform transform = model.childLocator.FindChild(addGameObjects[i].childName);
                if (transform)
                {
                    GameObject display = Object.Instantiate(addGameObjects[i].followerPrefab, transform);
                    display.transform.localPosition = addGameObjects[i].localPos;
                    display.transform.localEulerAngles = addGameObjects[i].localAngles;
                    display.transform.localScale = addGameObjects[i].localScale;

                    skinDefWolfoTracker.addedObjects[i] = display;

                    ItemDisplay itemDisplay = display.GetComponent<ItemDisplay>();
                    if (itemDisplay)
                    {
                        model.baseRendererInfos = HG.ArrayUtils.Join(model.baseRendererInfos, itemDisplay.rendererInfos);
                        itemDisplay.RefreshRenderers();
                    }
                }
            }
            if (!Run.instance && modelObject.transform.parent && modelObject.name.EndsWith("Engi"))
            {
                this.EngiDisplay(modelObject, skinDefWolfoTracker);
            }
            if (disableThis)
            {
                disableThis.gameObject.SetActive(false);
                skinDefWolfoTracker.disabledTransform = disableThis;
            }
        }

        public void EngiDisplay(GameObject modelObject, SkinDefWolfoTracker tracker)
        {
            Transform mineHolder = modelObject.transform.parent.Find("mdlEngi/EngiArmature/ROOT/base/stomach/chest/upper_arm.l/lower_arm.l/hand.l/IKBoneStart/IKBoneMid/MineHolder");
            Material newMaterial = this.skinDefParams.minionSkinReplacements[0].minionSkin.skinDefParams.rendererInfos[0].defaultMaterial;

            GameObject Mine1 = Instantiate(this.skinDefParams.projectileGhostReplacements[1].projectileGhostReplacementPrefab, mineHolder.GetChild(0));
            GameObject Mine2 = Instantiate(this.skinDefParams.projectileGhostReplacements[2].projectileGhostReplacementPrefab, mineHolder.GetChild(1));
            Mine2.transform.localEulerAngles = new Vector3(0, 0, 0);

            tracker.addedObjects = new GameObject[]
            {
                Mine1,
                Mine2
            };

            mineHolder.GetChild(0).GetChild(1).gameObject.SetActive(false);
            mineHolder.GetChild(0).GetChild(2).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(1).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(0).gameObject.SetActive(false);

            modelObject.transform.parent.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = newMaterial;
        }

        public LightColorChanges[] lightColorsChanges = System.Array.Empty<LightColorChanges>();
        public ItemDisplayRule[] addGameObjects = System.Array.Empty<ItemDisplayRule>();
        public MaterialChanger changeMaterial;
        public Transform disableThis;


        [System.Serializable]
        public struct MaterialChanger
        {
            public Material targetMaterial;
            public Material replacementMaterial;
        }

        [System.Serializable]
        public struct LightColorChanges
        {
            public string lightPath;
            public Color color;
            public Color color2;
        }
    }

    public class SkinDefWolfoTracker : MonoBehaviour
    {
        public GameObject[] addedObjects;
        public ChangedLightColors[] changedLights;
        public CharacterModel model;
        public Transform disabledTransform;

        [System.Serializable]
        public struct ChangedLightColors
        {
            public Light light;
            public TrailRenderer trail;
            public Color originalColor;
            public Color trailEndColor;
        }

        public void UndoWolfoSkin()
        {
            if (disabledTransform)
            {
                disabledTransform.gameObject.SetActive(true);
            }

            if (changedLights != null)
            {
                for (int i = 0; changedLights.Length > i; i++)
                {
                    if (changedLights[i].light)
                    {
                        changedLights[i].light.color = changedLights[i].originalColor;
                        for (int j = 0; model.baseLightInfos.Length > j; j++)
                        {
                            if (model.baseLightInfos[j].light == changedLights[i].light)
                            {
                                model.baseLightInfos[j].defaultColor = changedLights[i].originalColor;
                            }
                        }
                    }
                    if (changedLights[i].trail)
                    {
                        changedLights[i].trail.startColor = changedLights[i].originalColor;
                        changedLights[i].trail.endColor = changedLights[i].trailEndColor;
                    }
                }
            }
            if (addedObjects != null)
            {
                for (int i = 0; addedObjects.Length > i; i++)
                {
                    Destroy(addedObjects[i]);
                }
            }
            DestroyImmediate(this);
        }
    }
 
    public class OverlayMaterialReplacer : MonoBehaviour
    {
        public Material targetMaterial;
        public Material replacementMaterial;
    }
}