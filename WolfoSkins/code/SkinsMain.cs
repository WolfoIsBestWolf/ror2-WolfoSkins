using BepInEx;
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
    [BepInPlugin("Wolfo.WolfoSkins", "WolfoSkins", "2.3.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]

    public class WolfoSkins : BaseUnityPlugin
    {
        public void Awake()
        {
            WConfig.InitConfig();

            Assets.Init(Info);
            Unlocks.Hooks();

            RoR2Application.onLoadFinished += Make;

        }
        public void Make()
        {

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
            SkinsOperator.Start();
            SkinsDrifter.Start();




            On.RoR2.SkinDef.ApplyAsync += SkinDef_ApplyAsync;

            On.RoR2.TemporaryOverlay.AddToCharacerModel += ReplaceTemporaryOverlayMaterial;

            //On.RoR2.SkinDef.BakeAsync += SkinDef_BakeAsync; //Idk why this just doesnt fucking work anymore

            BodyCatalog.availability.CallWhenAvailable(ModSupport);

        }



        private System.Collections.IEnumerator SkinDef_ApplyAsync(On.RoR2.SkinDef.orig_ApplyAsync orig, SkinDef self, GameObject modelObject, List<UnityEngine.AddressableAssets.AssetReferenceT<Material>> loadedMaterials, List<UnityEngine.AddressableAssets.AssetReferenceT<Mesh>> loadedMeshes, RoR2.ContentManagement.AsyncReferenceHandleUnloadType unloadType)
        {
            //Debug.Log(self);
            if (self is SkinDefMakeOnApply)
            {
                (self as SkinDefMakeOnApply).Override();
            }

            var temp = orig(self, modelObject, loadedMaterials, loadedMeshes, unloadType);
            while (temp.MoveNext())
            {
                object obj2 = temp.Current;
                yield return obj2;
            }

            //Debug.Log("SkinApply " + self);
            if (modelObject.GetComponent<SkinDefAltColorTracker>())
            {
                modelObject.GetComponent<SkinDefAltColorTracker>().UndoWolfoSkin();
            }
            if (self is SkinDefEnhanced)
            {
                (self as SkinDefEnhanced).ApplyExtras(modelObject);
            }
            yield break;
        }

        public System.Collections.IEnumerator SkinDef_BakeAsync(On.RoR2.SkinDef.orig_BakeAsync orig, SkinDef self)
        {
            //Somehow does not catch a lot of skins anymore
            //When first being baked
            //Something about R2API update

            return orig(self);
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
            /* //Executioner2Body
             ModdedBody = BodyCatalog.FindBodyPrefab("Executioner2Body");
             if (ModdedBody != null)
             {
                 SkinsExecutioner.ModdedSkin(ModdedBody);
             }*/
            //ChirrBody
            ModdedBody = BodyCatalog.FindBodyPrefab("ChirrBody");
            if (ModdedBody != null)
            {
                SkinsChirr.ModdedSkin(ModdedBody);
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
            /*ModdedBody = BodyCatalog.FindBodyPrefab("MoffeinPilotBody");
            if (ModdedBody != null)
            {
                SkinsPilot.ModdedSkin(ModdedBody);
            }*/
            //MinerBody
            ModdedBody = BodyCatalog.FindBodyPrefab("MinerBody");
            if (ModdedBody != null)
            {
                SkinsMiner.ModdedSkin(ModdedBody);
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
            /*ModdedBody = BodyCatalog.FindBodyPrefab("NemesisEnforcerBody");
            if (ModdedBody != null)
            {
                SkinsNemEnforcer.ModdedSkin(ModdedBody);
            }*/

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
    /*
    public class SkinDefPrioritizeDirect : SkinDef
    {
        public bool done = false;
        public void Override()
        {         
            //Debug.Log(this);
            if (!this.done)
            {
                this._runtimeSkin = null;
                for (int i = 0; this.skinDefParams.rendererInfos.Length > i; i++)
                {
                    if (this.skinDefParams.rendererInfos[i].defaultMaterial != null)
                    {
                        this.skinDefParams.rendererInfos[i].defaultMaterialAddress = null;
                    }
                }
                this.done = true;
            }
        }
    }
    */
    public class SkinDefEnhanced : SkinDefMakeOnApply
    {

        //Some sort of Undo thing
        public void ApplyExtras(GameObject modelObject)
        {
            CharacterModel model = modelObject.GetComponent<CharacterModel>();

            SkinDefAltColorTracker skinDefWolfoTracker;
            if (!modelObject.TryGetComponent<SkinDefAltColorTracker>(out skinDefWolfoTracker))
            {
                skinDefWolfoTracker = modelObject.AddComponent<SkinDefAltColorTracker>();
            }

            if (changeMaterial.targetMaterial)
            {
                OverlayMaterialReplacer overlayMaterialReplacer = modelObject.AddComponent<OverlayMaterialReplacer>();
                overlayMaterialReplacer.targetMaterial = changeMaterial.targetMaterial;
                overlayMaterialReplacer.replacementMaterial = changeMaterial.replacementMaterial;
            }

            skinDefWolfoTracker.model = model;
            skinDefWolfoTracker.changedLights = new SkinDefAltColorTracker.ChangedLightColors[lightColorsChanges.Length];
            for (int i = 0; lightColorsChanges.Length > i; i++)
            {
                Transform transform = modelObject.transform.Find(lightColorsChanges[i].lightPath);
                if (transform)
                {
                    Light light = transform.GetComponent<Light>();
                    if (light)
                    {
                        skinDefWolfoTracker.changedLights[i] = new SkinDefAltColorTracker.ChangedLightColors
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
                        skinDefWolfoTracker.changedLights[i] = new SkinDefAltColorTracker.ChangedLightColors
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

        public void EngiDisplay(GameObject modelObject, SkinDefAltColorTracker tracker)
        {
            Transform mineHolder = modelObject.transform.parent.Find("mdlEngi/EngiArmature/ROOT/base/stomach/chest/upper_arm.l/lower_arm.l/hand.l/IKBoneStart/IKBoneMid/MineHolder");
            for (int i = 0; i < mineHolder.childCount; i++)
            {
                for (int m = 0; m < mineHolder.GetChild(i).childCount; m++)
                {
                    mineHolder.GetChild(i).GetChild(m).gameObject.SetActive(false);
                }
            }


            Material newMaterial = this.skinDefParams.minionSkinReplacements[0].minionSkin.skinDefParams.rendererInfos[0].defaultMaterial;

            GameObject Mine1 = Instantiate(this.skinDefParams.projectileGhostReplacements[1].projectileGhostReplacementPrefab, mineHolder.GetChild(0));
            GameObject Mine2 = Instantiate(this.skinDefParams.projectileGhostReplacements[2].projectileGhostReplacementPrefab, mineHolder.GetChild(1));
            Mine2.transform.localEulerAngles = new Vector3(0, 0, 0);

            tracker.addedObjects = new GameObject[]
            {
                Mine1,
                Mine2
            };

            /*mineHolder.GetChild(0).GetChild(1).gameObject.SetActive(false);
            mineHolder.GetChild(0).GetChild(2).gameObject.SetActive(false);
            mineHolder.GetChild(0).GetChild(3).gameObject.SetActive(false);
            mineHolder.GetChild(0).GetChild(4).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(0).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(1).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(2).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(3).gameObject.SetActive(false);*/

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

    public class SkinDefAltColorTracker : MonoBehaviour
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

    public class SkinDefMakeOnApply : SkinDef
    {
        public SkinDef baseSkin;
        public SkinDef[] minionSkins;

        public System.Action<SkinDefMakeOnApply> creationMethod;

        public int extraRenders;
        public bool cloneMesh;

        public bool created = false;
        public void Override()
        {
            if (!created)
            {
                if (creationMethod != null)
                {
                    Destroy(this.skinDefParams);
                    H.CloneSkinDefReal(this, baseSkin, cloneMesh, extraRenders);

                    creationMethod.Invoke(this);
                    creationMethod = null;
                }

                _runtimeSkin = null;
                for (int i = 0; skinDefParams.rendererInfos.Length > i; i++)
                {
                    if (skinDefParams.rendererInfos[i].defaultMaterial != null)
                    {
                        skinDefParams.rendererInfos[i].defaultMaterialAddress = null;
                    }
                }
                created = true;
            }
        }

    }
}