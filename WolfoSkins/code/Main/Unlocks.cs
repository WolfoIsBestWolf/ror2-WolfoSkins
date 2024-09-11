using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class Unlocks
    {
        public static event System.Action<Run> unlockSkins;
        public static event System.Action<Run> unlockSkinsEliteMithrix;

        public static string unlockName = ": Alternated";
        public static string unlockNameGrand = ": Grand Alternation";
        public static string unlockNameSimu = " : Simulated";
        public static string unlockNamePrism = ": Colorized"; //Prismatized
        public static string unlockNameDissonance = ": Dissonant";
        public static string wip = " Placeholder";
        public static string unlockCondition = ", complete wave 50 in Simulacrum, A Moment, Whole or the Planetarium.";
        public static string unlockConditiontwo = ", complete stage 15, wave 50 in Simulacrum, A Moment, Whole or the Planetarium.";
        public static string unlockConditionGrand = ", defeat an elite or umbral Mithrix or with Artifact of Dissonance enabled.";
        //public static string unlockConditionPrism = ", beat the game on a Prismatic Trial or Eclipse run.\n<color=#8888>(Use LittleGameplayTweaks for Prismatic Trials)</color>";
        public static string unlockConditionPrism = ", beat the game with Artifact of Dissonance enabled or during a Prismatic Trial.";
        //public static string unlockConditionPrism = ", finish a Prismatic Trial or Eclipse run." + ltgNotice;
        //public static string unlockConditionPrism = ", escape the moon or planetarium during a Prismatic Trial or Eclipse run.\n<color=#8888>(Use LittleGameplayTweaks for Prismatic Trials)</color>";
        //public static string unlockConditionDissonance = ", beat the game or obliterate with Artifact of Dissonance enabled.";
        //public static string ltgNotice = "\n<color=#8888>(Use LittleGameplayTweaks for Prismatic Trials)</color>";

        internal static void Hooks()
        {
            On.RoR2.InfiniteTowerWaveController.PlayAllEnemiesDefeatedSound += (orig, self) =>
            {
                orig(self);
                if (self.waveIndex >= 50)
                {
                    System.Action<Run> action = unlockSkins;
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
                System.Action<Run> action = unlockSkins;
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
                System.Action<Run> action = unlockSkins;
                if (action == null)
                {
                    return;
                }
                action(Run.instance);
            };
            On.RoR2.UI.LogBook.LogBookController.CanSelectAchievementEntry += HideUnimplementedUnlocks;

            On.RoR2.RoR2Content.CreateEclipseUnlockablesForSurvivor += AutogenerateUnlockableDefs;
            
        }

        private static UnlockableDef[] AutogenerateUnlockableDefs(On.RoR2.RoR2Content.orig_CreateEclipseUnlockablesForSurvivor orig, SurvivorDef survivorDef, int minEclipseLevel, int maxEclipseLevel)
        {
            return orig(survivorDef, minEclipseLevel, maxEclipseLevel);
        }

        public static bool HideUnimplementedUnlocks(On.RoR2.UI.LogBook.LogBookController.orig_CanSelectAchievementEntry orig, AchievementDef achievementDef, System.Collections.Generic.Dictionary<RoR2.ExpansionManagement.ExpansionDef, bool> expansionAvailability)
        {
            if (achievementDef.identifier.StartsWith("SIMU") || achievementDef.identifier.StartsWith("PRIS"))
            {
                UnlockableDef def = UnlockableCatalog.GetUnlockableDef(achievementDef.unlockableRewardIdentifier);
                //Debug.LogWarning(achievementDef.identifier + " " + def);
                if (def && def.hidden)
                {
                    return false;
                }
            }
            return orig(achievementDef, expansionAvailability);
        }
    }

    public class AchievementSimuVoidTwisted : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            Unlocks.unlockSkins += this.Unlock;
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Unlocks.unlockSkins -= this.Unlock;
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
                    //Leaves Void, Limbo, IT ending and probably Bulwarks Haunt.
                    base.Grant();
                }
            }
        }

        private void Unlock(Run run)
        {
            base.Grant();
        }
    }
    /*
    public class AchievementPrismaticDisso : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
            base.OnBodyRequirementBroken();
        }

        private void OnClientGameOverGlobal(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding)
            {
                return;
            }
            if (runReport.gameEnding.isWin && runReport.gameEnding != RoR2Content.GameEndings.ObliterationEnding)
            {
                if (run.GetComponent<WeeklyRun>())
                {
                    base.Grant();
                }
                else if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.MixEnemy))
                {
                    base.Grant();
                }
                
            }
        }
    }


    */
}