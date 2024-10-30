using R2API;
using RoR2;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using RoR2.UI;
using RoR2.Stats;

namespace WolfoSkinsMod
{
    public class Achievements
    {
        public static event System.Action<Run> unlockSkins_Both;
        public static event System.Action<Run> unlockSkins_Simu;
        public static event System.Action<Run> unlockSkins_AltBoss;

        internal static void Hooks()
        {
            //These run on client so I guess doesnt need to be called for everyone?
            On.RoR2.InfiniteTowerWaveController.PlayAllEnemiesDefeatedSound += (orig, self) =>
            {
                orig(self);           
                if (self.waveIndex >= 50)
                {
                    Grant(".Wolfo.Simu", "CLEAR_SIMU_", false);
                    System.Action<Run> action = unlockSkins_Simu;
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
                Grant(".Wolfo.LunarScav", "CLEAR_LUNARSCAV_", true);
                System.Action<Run> action = unlockSkins_AltBoss;
                if (action == null)
                {
                    return;
                }
                action(Run.instance);
            };
            On.EntityStates.VoidRaidCrab.DeathState.OnEnter += Voidling_Unlock;
            On.EntityStates.BrotherMonster.TrueDeathState.OnEnter += Eclipse8_Unlock;
 
        }


        public static void Grant(string unlockable, string achievement, bool isAltBoss)
        {
            Debug.LogWarning("Attempting Unlockable :" + unlockable);
            ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
            for (int i = 0; i < instances.Count; i++)
            {
                PlayerCharacterMasterController playerCharacterMasterController = instances[i];
                NetworkUser networkUser = instances[i].networkUser;
                if (networkUser)
                {
                    if (networkUser.localUser != null)
                    {
                        UserProfile userProfile = networkUser.localUser.userProfile;
                        SurvivorDef survivorDef = networkUser.GetSurvivorPreference();
                        if (survivorDef)
                        {
                            string upperName = survivorDef.cachedName.ToUpperInvariant();

                            //Specific Unlockable
                            string fullUnlockable = "Skins." + survivorDef.cachedName + unlockable;
                            UnlockableDef safe = UnlockableCatalog.GetUnlockableDef(fullUnlockable);
                            if (safe)
                            {
                                Debug.Log("Granting Unlockable:" + fullUnlockable);
                                if (!userProfile.HasUnlockable(safe))
                                {
                                    networkUser.ServerHandleUnlock(safe);
                                }
                            }
                            if (!userProfile.HasAchievement(achievement + upperName))
                            {
                                userProfile.AddAchievement(achievement + upperName, false);
                            }
                                
                            //
                            //AltBoss
                            if (isAltBoss)
                            {
                                /*UnlockableDef AltBoss = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
                                if (!userProfile.HasUnlockable(AltBoss))
                                {
                                    networkUser.ServerHandleUnlock(AltBoss);
                                }*/
                                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_" + upperName))
                                {
                                    userProfile.AddAchievement("CLEAR_ALTBOSS_" + upperName, false);
                                }
                            }
                           

                            //
                            //First Unlockable
                            UnlockableDef unlockAlways = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.First");
                            if (unlockAlways)
                            {
                                Debug.Log("Granting Unlockable: Skins." + survivorDef.cachedName + ".Wolfo.First");
                                if (!userProfile.HasUnlockable(unlockAlways))
                                {
                                    networkUser.ServerHandleUnlock(unlockAlways);
                                }
                            }
                            if (!userProfile.HasAchievement("CLEAR_ANY_" + upperName))
                            {
                                userProfile.AddAchievement("CLEAR_ANY_" + upperName, false);
                            }

                            //
                            //Both Unlockable
                            bool hasAltBoss = isAltBoss || userProfile.HasAchievement("CLEAR_ALTBOSS_" + upperName);
                            bool hasSimu = unlockable.EndsWith("Simu") || userProfile.HasAchievement("CLEAR_SIMU_" + upperName);
                            if (hasAltBoss && hasSimu)
                            {
                                System.Action<Run> action = Achievements.unlockSkins_Both;
                                if (action != null)
                                {
                                    action(Run.instance);
                                }
                                UnlockableDef safe2 = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Both");
                                if (safe2 && !userProfile.HasUnlockable(safe2))
                                {
                                    Debug.Log("Granting Unlockable:" + safe2.cachedName);
                                    networkUser.ServerHandleUnlock(safe2);
                                }
                                if (!userProfile.HasAchievement("CLEAR_BOTH_" + upperName))
                                {
                                    userProfile.AddAchievement("CLEAR_BOTH_" + upperName, false);
                                }
                            }
    

                            Unlocks.UpdateBothObjective_Specific(userProfile, survivorDef);
                        }
                    }
                }
            }
        }

        private static void Voidling_Unlock(On.EntityStates.VoidRaidCrab.DeathState.orig_OnEnter orig, EntityStates.VoidRaidCrab.DeathState self)
        {
            orig(self);
            if (SceneInfo.instance && SceneInfo.instance.sceneDef.cachedName.Equals("voidraid"))
            {
                Grant(".Wolfo.Voidling", "CLEAR_VOIDLING", true);
                System.Action<Run> action = unlockSkins_AltBoss;
                if (action == null)
                {
                    return;
                }
                action(Run.instance);
            }
        }

        private static void Eclipse8_Unlock(On.EntityStates.BrotherMonster.TrueDeathState.orig_OnEnter orig, EntityStates.BrotherMonster.TrueDeathState self)
        {
            orig(self);

            if (SceneInfo.instance && SceneInfo.instance.sceneDef.cachedName == "moon2")
            {
                if (Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse8)
                {
                    Grant(".Wolfo.Eclipse", "CLEAR_ECLIPSE_", false);
                }
            }
        }

              
    }




    //Probably don't make a entirely new Eclipse8 achievement since Eclipse..Eclipse9 already exists
    public class Achievement_Simu : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            Achievements.unlockSkins_Simu += this.Unlock;
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Achievements.unlockSkins_Simu -= this.Unlock;
            Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
            base.OnBodyRequirementBroken();
        }

        private void OnClientGameOverGlobal(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding)
            {
                return;
            }
            if (runReport.gameEnding.isWin)
            {
                if (runReport.gameEnding.cachedName.Equals("InfiniteTowerEnding"))
                {
                    base.Grant();
                }
                return;
            }
        }

        private void Unlock(Run run)
        {
            base.Grant();
        }
    }

    public class Achievement_AltBoss_AND_Simu : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            Achievements.unlockSkins_Both += this.Unlock;
            //Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Achievements.unlockSkins_Both -= this.Unlock;
            //Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
            base.OnBodyRequirementBroken();
        }

        public void IsValid()
        {
            //bool hasSimu = this.userProfile.HasUnlockable(userProfile.survivorPreference.cachedName);
        }

        private void OnClientGameOverGlobal(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding)
            {
                return;
            }
            if (runReport.gameEnding.isWin)
            {
                if (runReport.gameEnding == RoR2Content.GameEndings.LimboEnding)
                {
                    base.Grant();
                }
                else if (runReport.gameEnding == DLC1Content.GameEndings.VoidEnding)
                {
                    base.Grant();
                }
                return;
            }
        }

        private void Unlock(Run run)
        {
            base.Grant();
        }
    }

    public class Achievement_AltBoss_Simu : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            Achievements.unlockSkins_Simu += this.Unlock;
            Achievements.unlockSkins_AltBoss += this.Unlock;
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Achievements.unlockSkins_Simu -= this.Unlock;
            Achievements.unlockSkins_AltBoss -= this.Unlock;
            Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
            base.OnBodyRequirementBroken();
        }

        private void OnClientGameOverGlobal(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding)
            {
                return;
            }
            if (runReport.gameEnding.isWin)
            {
                if (runReport.gameEnding.cachedName.Equals("InfiniteTowerEnding"))
                {
                    base.Grant();
                }
                else if (runReport.gameEnding == RoR2Content.GameEndings.LimboEnding)
                {
                    base.Grant();
                }
                else if (runReport.gameEnding == DLC1Content.GameEndings.VoidEnding)
                {
                    base.Grant();
                }
                return;
            }
        }

        private void Unlock(Run run)
        {
            base.Grant();
        }
    }



}