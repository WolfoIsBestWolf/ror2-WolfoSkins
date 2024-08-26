using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System.Text;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace WolfoSkinsMod
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.TheTimeSweeper.TeslaTrooper", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("Wolfo.WolfoSkins", "WolfoSkins", "1.5.0")]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    public class WolfoSkins : BaseUnityPlugin
    {

        public void Awake()
        {
            WConfig.InitConfig();

            Unlocks.Hooks();
            //
            SkinsCommando.CommandoSkin();
            SkinsHuntress.HuntressSkin();
            SkinsBandit.Start();
            SkinsMULT.ToolbotSkin();
            SkinsEngineer.EngiSkin();
            SkinsArtificer.Start();
            SkinsMerc.MercSkin();
            SkinsREX.TreebotSkin();
            SkinsLoader.LoaderSkin();
            SkinsAcrid.Start();
            SkinsCaptain.CaptainSkin();
            SkinsRailGunner.RailGunnerSkins();
            SkinsVoidFiend.Start();

            //Modded     
            SkinsHand.CallDuringAwake();
            SkinsEnforcer.CallDuringAwake();
            SkinsSniper.CallDuringAwake();
            SkinsMiner.CallDuringAwake();
            SkinsCHEF.CallDuringAwake();
            SkinsPilot.CallDuringAwake();
            SkinsRocket.CallDuringAwake();         
            SkinsChirr.CallDuringAwake();
            SkinsExecutioner.CallDuringAwake();
            SkinsRavager.CallDuringAwake();

            SkinsArsonist.CallDuringAwake();
            TeslaDesolatorUnlocks.CallDuringAwake();
            SkinsPaladin.CallDuringAwake();
            //
            SkinsNemCommando.CallDuringAwake();
            SkinsNemMercenary.CallDuringAwake();
            SkinsNemEnforcer.CallDuringAwake();
            //
            SkinsFutureModSupport.CallDuringAwake();
            //
            //Prism stuff
            SkinsCommando.PrismAchievement();
            SkinsHuntress.PrismAchievement();
            SkinsBandit.PrismAchievement();
            SkinsMULT.PrismAchievement();
            SkinsEngineer.PrismAchievement();
            //SkinsArtificer.PrismAchievement();
            SkinsMerc.PrismAchievement();
            SkinsREX.PrismAchievement();
            SkinsLoader.PrismAchievement();
            //SkinsAcrid.PrismAchievement();
            SkinsCaptain.PrismAchievement();
            SkinsRailGunner.PrismAchievement();
            //SkinsVoidFiend.PrismAchievement();

            SkinsEnforcer.PrismAchievement();
            SkinsHand.PrismAchievement();
            //
            BodyCatalog.availability.CallWhenAvailable(ModSupport);
             
            GameModeCatalog.availability.CallWhenAvailable(SortSkinsLate);

            On.RoR2.SkinDef.Apply += (orig, self, model) =>
            {
                orig(self, model);
                //Debug.Log("SkinApply " + self);
                if (model.GetComponent<SkinDefWolfoTracker>())
                {
                    model.GetComponent<SkinDefWolfoTracker>().UndoWolfoSkin();
                }
                if (model.GetComponent<SkinsCHEF.FixChefDisplay>())
                {
                    model.GetComponent<SkinsCHEF.FixChefDisplay>().Fix(self);
                }
                if (self is SkinDefWolfo)
                {
                    (self as SkinDefWolfo).ApplyExtras(model);
                }
            };


            On.RoR2.TemporaryOverlay.AddToCharacerModel += ReplaceTemporaryOverlayMaterial;
            VoidlingNerfs();

        }
        internal static void SortSkinsLate()
        {
            List<string> blacklistedSorting = new List<string>()
            {
                "Enforcer",
                "HANDOverclocked",
                "Miner",
                "CHEF",
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
                BodyIndex Index = Body.GetComponent<CharacterBody>().bodyIndex;
                ModelSkinController modelSkinController = Body.GetComponentInChildren<ModelSkinController>();

                if (modelSkinController.skins.Length > 4)
                {                   
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
                        BodyCatalog.skins[(int)Index] = skinsNew;
                        SkinCatalog.skinsByBody[(int)Index] = skinsNew;
                    }           
                }
            }
            System.GC.Collect(); //?
        }

        internal static void ModSupport()
        {
            //SortSkinsLate();
            GameObject ModdedBody = BodyCatalog.FindBodyPrefab("CHEF");
            if (ModdedBody != null)
            {
                SkinsCHEF.ModdedSkin(ModdedBody);
            }
            //HAND
            ModdedBody = BodyCatalog.FindBodyPrefab("HANDOverclockedBody");
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
            if(characterModel)
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
            orig(self,characterModel);
        }

        public static void VoidlingNerfs()
        {          
            On.RoR2.ScriptedCombatEncounter.BeginEncounter += VoidlingLevelLimit;

            CharacterBody Voidling = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidRaidCrab/MiniVoidRaidCrabBodyPhase1.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            Voidling.baseDamage *= WConfig.VoidlingDamageMultiplier.Value;
            Voidling.levelDamage *= WConfig.VoidlingDamageMultiplier.Value;
            Voidling.baseMaxHealth *= WConfig.VoidlingHPMultiplier.Value;
            Voidling.levelMaxHealth *= WConfig.VoidlingHPMultiplier.Value;
            Voidling = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidRaidCrab/MiniVoidRaidCrabBodyPhase2.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            Voidling.baseDamage *= WConfig.VoidlingDamageMultiplier.Value;
            Voidling.levelDamage *= WConfig.VoidlingDamageMultiplier.Value;
            Voidling.baseMaxHealth *=  WConfig.VoidlingHPMultiplier.Value;
            Voidling.levelMaxHealth *=  WConfig.VoidlingHPMultiplier.Value;
            Voidling = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidRaidCrab/MiniVoidRaidCrabBodyPhase3.prefab").WaitForCompletion().GetComponent<CharacterBody>();
            Voidling.baseDamage *= WConfig.VoidlingDamageMultiplier.Value;
            Voidling.levelDamage *= WConfig.VoidlingDamageMultiplier.Value;
            Voidling.baseMaxHealth *= WConfig.VoidlingHPMultiplier.Value;
            Voidling.levelMaxHealth *= WConfig.VoidlingHPMultiplier.Value;
        }

        private static void VoidlingLevelLimit(On.RoR2.ScriptedCombatEncounter.orig_BeginEncounter orig, ScriptedCombatEncounter self)
        {
            orig(self);

            if (self.name.StartsWith("VoidRaid"))
            {
                if (Run.instance.ambientLevelFloor > 99)
                {
                    if (WConfig.VoidlingLimitLevel99.Value)
                    {  
                        for (int i = 0; i < self.combatSquad.membersList.Count; i++)
                        {
                            self.combatSquad.membersList[i].inventory.RemoveItem(RoR2Content.Items.UseAmbientLevel);
                            self.combatSquad.membersList[i].inventory.GiveItem(RoR2Content.Items.LevelBonus, 98);
                        }
                    }
                }
                /*
                if (Run.instance.livingPlayerCount > 1)
                {
                    float hpBonus = 1f;
                    hpBonus += Run.instance.difficultyCoefficient / 2.5f;
                    int hpBonusMultiplier = Mathf.Max(1, Run.instance.livingPlayerCount);
                    hpBonus *= Mathf.Pow((float)hpBonusMultiplier, 0.5f);
                    self.combatSquad.membersList[i].inventory.GiveItem(RoR2Content.Items.BoostHp, Mathf.RoundToInt((hpBonus - 1f) * 10f));



                    float dmgBonus = 1f;
                    dmgBonus += Run.instance.difficultyCoefficient / 30f;
                    self.combatSquad.membersList[i].inventory.GiveItem(RoR2Content.Items.BoostDamage, Mathf.RoundToInt((dmgBonus - 1f) * 10f));
                }*/
            }
        }

    }

    public class SkinDefWolfo : SkinDef
    {
        public new void Awake()
        {
            //Debug.LogWarning("SkinDefWolfo");
        }

        //Some sort of Undo thing
        public void ApplyExtras(GameObject modelObject)
        {
            CharacterModel model = modelObject.GetComponent<CharacterModel>();
            SkinDefWolfoTracker skinDefWolfoTracker = modelObject.AddComponent<SkinDefWolfoTracker>();

            OverlayMaterialReplacer overlayMaterialReplacer = modelObject.AddComponent<OverlayMaterialReplacer>();
            overlayMaterialReplacer.targetMaterial = changeMaterial.targetMaterial;
            overlayMaterialReplacer.replacementMaterial = changeMaterial.replacementMaterial;

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
                        model.baseRendererInfos = model.baseRendererInfos.Add(itemDisplay.rendererInfos);
                    }
                }
            }
            if (!Run.instance && modelObject.transform.parent && modelObject.name.EndsWith("Engi"))
            {
                this.EngiDisplay(modelObject, skinDefWolfoTracker);
            }
        }

        public void EngiDisplay(GameObject modelObject, SkinDefWolfoTracker tracker)
        {
            Transform mineHolder = modelObject.transform.parent.Find("mdlEngi/EngiArmature/ROOT/base/stomach/chest/upper_arm.l/lower_arm.l/hand.l/IKBoneStart/IKBoneMid/MineHolder");
            Material newMaterial = this.minionSkinReplacements[0].minionSkin.rendererInfos[0].defaultMaterial;

            GameObject Mine1 = Instantiate(this.projectileGhostReplacements[1].projectileGhostReplacementPrefab, mineHolder.GetChild(0));
            GameObject Mine2 = Instantiate(this.projectileGhostReplacements[2].projectileGhostReplacementPrefab, mineHolder.GetChild(1));
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