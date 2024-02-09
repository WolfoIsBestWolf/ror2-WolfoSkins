using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
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
    [BepInPlugin("Wolfo.WolfoSkins", "WolfoSkins", "1.2.0")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    public class WolfoSkins : BaseUnityPlugin
    {
        public static event System.Action<Run> unlockSkins;
        //public static string unlockCondition = ", complete wave 50 in Simulacrum or defeat a Twisted Scavenger or escape the Planetarium.";
        public static string unlockCondition = ", complete wave 50 in Simulacrum, A Moment, Whole or the Planetarium.";

        public void Awake()
        {
            WConfig.InitConfig();

            SkinsCommando.CommandoSkin(); //Marine + Provi Trial
            SkinsHuntress.HuntressSkin(); // BlackPink Bunny + Bee
            SkinsBandit.BanditSkin(); //RoRR Red + Purple
            SkinsMULT.ToolbotSkin(); //Damage Chest + Healing
            SkinsEngineer.EngiSkin(); //RoRR Red
            SkinsArtificer.ArtificerSkin();
            SkinsMerc.MercSkin();
            SkinsREX.TreebotSkin(); //Lepton Daisy
            SkinsLoader.LoaderSkin();
            SkinsAcrid.AcridSkin(); //RoRR White/Blue
            SkinsCaptain.CaptainSkin(); //Pink Captain Artwork
            SkinsRailGunner.RailGunnerSkins();
            SkinsVoidFiend.VoidSkins();
            //Modded
            SkinsCHEF.CallDuringAwake();
            SkinsHand.CallDuringAwake();
            SkinsEnforcer.CallDuringAwake();
            SkinsExecutioner.CallDuringAwake();

            BodyCatalog.availability.CallWhenAvailable(ModSupport);

            On.RoR2.SkinDef.Apply += (orig, self, model) =>
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
            };

            On.RoR2.InfiniteTowerWaveController.PlayAllEnemiesDefeatedSound += (orig, self) =>
            {
                orig(self);
                if (self.waveIndex >= 50)
                {
                    System.Action<Run> action = WolfoSkins.unlockSkins;
                    if (action == null)
                    {
                        return;
                    }
                    action(Run.instance);
                }
            };
            On.EntityStates.Missions.LunarScavengerEncounter.FadeOut.OnEnter += (orig, self) =>
            {
                orig(self);
                System.Action<Run> action = WolfoSkins.unlockSkins;
                if (action == null)
                {
                    return;
                }
                action(Run.instance);
            };
            On.EntityStates.GameOver.VoidEndingStart.OnEnter += (orig, self) =>
            {
                orig(self);
                Debug.Log("EntityStates.GameOver.VoidEndingStart.OnEnter");
                System.Action<Run> action = WolfoSkins.unlockSkins;
                if (action == null)
                {
                    return;
                }
                action(Run.instance);
            };
            VoidlingNerfs();
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

        internal static void ModSupport()
        {
            GameObject ModdedBody = BodyCatalog.FindBodyPrefab("CHEF");
            if (ModdedBody != null)
            {
                SkinsCHEF.ModdedSkin(ModdedBody);
            }
            else
            {
                LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_DESCRIPTION", "You do not have the CHEF mod installed.");
            }
            //HAND
            ModdedBody = BodyCatalog.FindBodyPrefab("HANDOverclockedBody");
            if (ModdedBody != null)
            {
                SkinsHand.ModdedSkin(ModdedBody);
            }
            else
            {
                LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HAND_DESCRIPTION", "You do not have the Han-D mod installed.");
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
            else
            {
                LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ENFORCER_DESCRIPTION", "You do not have the Enforcer mod installed.");
            }
            //Executioner2Body
            ModdedBody = BodyCatalog.FindBodyPrefab("Executioner2Body");
            if (ModdedBody != null)
            {
                SkinsExecutioner.ModdedSkin(ModdedBody);
            }
            else
            {
                LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_EXECUTIONER_DESCRIPTION", "You do not have Starstorm 2 installed.");
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

            skinDefWolfoTracker.model = model;
            skinDefWolfoTracker.changedLights = new SkinDefWolfoTracker.ChangedLightColors[lightColorsChanges.Length];
            for (int i = 0; lightColorsChanges.Length > i; i++)
            {
                Transform transform = modelObject.transform.Find(lightColorsChanges[i].lightPath);
                if (transform)
                {
                    Light light = transform.GetComponent<Light>();
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

        [System.Serializable]
        public struct LightColorChanges
        {
            public string lightPath;

            public Color color;
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

            public Color originalColor;
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

    public class SimuOrVoidEnding : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            WolfoSkins.unlockSkins += this.Unlock;
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            WolfoSkins.unlockSkins -= this.Unlock;
            Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
            base.OnBodyRequirementBroken();
        }

        private void OnClientGameOverGlobal(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding)
            {
                return;
            }
            if (runReport.gameEnding.cachedName.StartsWith("InfiniteT"))
            {
                base.Grant();
            }
            else if (runReport.gameEnding.isWin)
            {
                if (runReport.gameEnding == RoR2Content.GameEndings.MainEnding)
                {
                    return;
                }
                else if (runReport.gameEnding == RoR2Content.GameEndings.ObliterationEnding)
                {
                    return;
                }
                else
                {
                    //Leaves Void and Limbo ending and probably Bulwarks Haunt.
                    base.Grant();
                }  
            }
        }

        private void Unlock(Run run)
        {
            base.Grant();
        }



    }

}