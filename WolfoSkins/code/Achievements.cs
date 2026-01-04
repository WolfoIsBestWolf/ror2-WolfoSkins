using RoR2;
using System;

namespace WolfoSkinsMod
{
    public class Achievements
    {

        public static event Action unlockSkins_One;
        public static event Action unlockSkins_Two;
        public static event Action unlockSkins_Three;

        internal static void Hooks()
        {
            //These run on client so I guess doesnt need to be called for everyone?
            On.RoR2.InfiniteTowerWaveController.PlayAllEnemiesDefeatedSound += (orig, self) =>
            {
                orig(self);
                //Only check like twice
                if (self.waveIndex == 50 || self.waveIndex == 51)
                {
                    GrantUnlockAndAchievement("CLEAR_SIMU_");
                }
            };
            On.EntityStates.Missions.LunarScavengerEncounter.FadeOut.OnEnter += (orig, self) =>
            {
                orig(self);
                GrantUnlockAndAchievement("CLEAR_LUNARSCAV_");
            };
            On.EntityStates.VoidRaidCrab.DeathState.OnEnter += Voidling_Unlock;
            On.EntityStates.BrotherMonster.TrueDeathState.OnEnter += Eclipse_Unlock;
            Run.onClientGameOverGlobal += BackupInCaseMainFailed;
        }

        public static void BackupInCaseMainFailed(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding)
            {
                return;
            }
            if (runReport.gameEnding.isWin)
            {
                if (runReport.gameEnding.cachedName.Equals("InfiniteTowerEnding"))
                {
                    GrantUnlockAndAchievement("CLEAR_SIMU_");
                }
                else if (runReport.gameEnding == RoR2Content.GameEndings.MainEnding && run.selectedDifficulty >= DifficultyIndex.Eclipse4)
                {
                    GrantUnlockAndAchievement("CLEAR_LUNARSCAV_");
                }
                else if (runReport.gameEnding == RoR2Content.GameEndings.LimboEnding)
                {
                    GrantUnlockAndAchievement("CLEAR_ECLIPSE_");
                }
                else if (runReport.gameEnding == DLC1Content.GameEndings.VoidEnding)
                {
                    GrantUnlockAndAchievement("CLEAR_VOIDLING_");
                }
                return;
            }
        }


        public static void GrantUnlockAndAchievement(string achievement)
        {
            if (LocalUserManager.readOnlyLocalUsersList.Count == 0)
            {
                return;
            }
            UserProfile userProfile = LocalUserManager.readOnlyLocalUsersList[0].userProfile;
            SurvivorDef survivorDef = userProfile.GetSurvivorPreference();
            if (survivorDef)
            {
                string upperName = survivorDef.cachedName.ToUpperInvariant();

                if (!userProfile.HasAchievement(achievement + upperName))
                {
                    userProfile.AddAchievement(achievement + upperName, false);
                }

                //Both Unlockable
                int unlocks = 0;
                int token = 0;
                if (userProfile.HasAchievement("CLEAR_LUNARSCAV_" + upperName))
                {
                    unlocks++;
                    token = 1;
                }
                if (userProfile.HasAchievement("CLEAR_VOIDLING_" + upperName))
                {
                    unlocks++;
                    token = 2;
                }
                if (userProfile.HasAchievement("CLEAR_SIMU_" + upperName))
                {
                    unlocks++;
                    token = 3;
                }
                if (userProfile.HasAchievement("CLEAR_ECLIPSE_" + upperName))
                {
                    unlocks++;
                    token = 4;
                }

                Action action1 = unlockSkins_One;
                if (action1 != null)
                {
                    action1();
                }

                if (unlocks >= 2)
                {
                    Action action2 = unlockSkins_Two;
                    if (action2 != null)
                    {
                        action2();
                    }
                    Unlocks.UpdateTier2Objective_Specific(userProfile, survivorDef, 0);
                }
                else
                {
                    Unlocks.UpdateTier2Objective_Specific(userProfile, survivorDef, token);
                }
            }
        }

        private static void Voidling_Unlock(On.EntityStates.VoidRaidCrab.DeathState.orig_OnEnter orig, EntityStates.VoidRaidCrab.DeathState self)
        {
            orig(self);
            if (SceneInfo.instance && SceneInfo.instance.sceneDef.cachedName.Equals("voidraid"))
            {
                GrantUnlockAndAchievement("CLEAR_VOIDLING_");
            }
        }

        private static void Eclipse_Unlock(On.EntityStates.BrotherMonster.TrueDeathState.orig_OnEnter orig, EntityStates.BrotherMonster.TrueDeathState self)
        {
            orig(self);
            if (SceneInfo.instance && SceneInfo.instance.sceneDef.cachedName == "moon2")
            {
                if (Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse4)
                {
                    GrantUnlockAndAchievement("CLEAR_ECLIPSE_");
                }
            }
        }


    }


    public class ACHIEVEMENT_BASE : RoR2.Achievements.BaseAchievement
    {
        public void Unlock()
        {
            achievementDef.nameToken = Language.GetString(achievementDef.nameToken);
            base.Grant();
        }
    }

    public class Achievement_TWO_THINGS : ACHIEVEMENT_BASE
    {
        public override void OnBodyRequirementMet()
        {
            if (!HasBaseAchievement())
            {
                return;
            }
            base.OnBodyRequirementMet();
            Achievements.unlockSkins_Two += Unlock;
        }
        public override void OnBodyRequirementBroken()
        {
            if (!HasBaseAchievement())
            {
                return;
            }
            base.OnBodyRequirementBroken();
            Achievements.unlockSkins_Two -= Unlock;
        }

        public bool HasBaseAchievement()
        {
            if (owner == null)
            {
                return false;
            }
            BodyIndex bodyI = LookUpRequiredBodyIndex();
            if (bodyI == BodyIndex.None)
            {
                return false;
            }
            SurvivorIndex survI = SurvivorCatalog.GetSurvivorIndexFromBodyIndex(bodyI);
            SurvivorDef surv = SurvivorCatalog.GetSurvivorDef(survI);
            string baseAchievement = "CLEAR_ANY_" + surv.cachedName.ToUpperInvariant();
            AchievementDef baseAchieve = AchievementManager.GetAchievementDef(baseAchievement);
            //Debug.LogWarning(baseAchievement);
            if (baseAchieve == null)
            {
                //Debug.LogWarning("Tier2 Achievement has null Tier1 | "+achievementDef.identifier);
                return false;
            }
            //Debug.LogWarning("Tier2 Achievement Tier1 | has? : " + owner.userProfile.HasAchievement(baseAchievement));
            return owner.userProfile.HasAchievement(baseAchievement);
        }

    }

    public class Achievement_ONE_THINGS : ACHIEVEMENT_BASE
    {

        public override void OnBodyRequirementMet()
        {
            Achievements.unlockSkins_One += Unlock;
            base.OnBodyRequirementMet();


        }

        public override void OnBodyRequirementBroken()
        {
            Achievements.unlockSkins_One -= Unlock;
            base.OnBodyRequirementBroken();
        }


    }


}