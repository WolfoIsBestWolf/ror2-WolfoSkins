using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using RoR2.Stats;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WolfoSkinsMod.Base;
using WolfoSkinsMod.Mod;

namespace WolfoSkinsMod
{
    public class Unlocks
    {
        internal static void Hooks()
        {
            Achievements.Hooks();
            On.RoR2.UI.LogBook.LogBookController.CanSelectAchievementEntry += HideUnimplementedUnlocks;

            On.RoR2.RoR2Content.CreateEclipseUnlockablesForSurvivor += AutogenerateUnlockableDefs;

            On.RoR2.LocalUserManager.AddUser += LocalUserManager_AddUser;

            On.RoR2.UnlockableCatalog.GenerateUnlockableMetaData += UnlockableCatalog_GenerateUnlockableMetaData;
            GameModeCatalog.availability.CallWhenAvailable(AutogenerateTokens);

            On.RoR2.UserProfile.RevokeUnlockable += RevokeUnlock_ActualSafteyCheck;
        }

        private static void LocalUserManager_AddUser(On.RoR2.LocalUserManager.orig_AddUser orig, Rewired.Player inputPlayer, UserProfile userProfile)
        {
            //Surely LocalUser manager isnt used by online
            orig(inputPlayer, userProfile);
            if (WConfig.cfgSilentRelockReunlock.Value)
            {
                //Silent revoke and regrant everything upon bugged updates                
                LockEverything(userProfile, false);
                CheckForPreviouslyEarned(userProfile, true);
            }
            else if (WConfig.RemoveSkinUnlocks.Value)
            {
                LockEverything(userProfile, false);
            }
            else if (WConfig.cfgRunAutoUnlocker.Value)
            {
                CheckForPreviouslyEarned(userProfile, true);
            }
            UpdateTier2_ForAll(userProfile);
        }

        private static void RevokeUnlock_ActualSafteyCheck(On.RoR2.UserProfile.orig_RevokeUnlockable orig, UserProfile self, UnlockableDef unlockableDef)
        {
            if (!unlockableDef)
            {
                return;
            }
            orig(self, unlockableDef);
        }

        private static void UnlockableCatalog_GenerateUnlockableMetaData(On.RoR2.UnlockableCatalog.orig_GenerateUnlockableMetaData orig, UnlockableDef[] unlockableDefs)
        {
            try
            {
                AssignUnlockables();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
            orig(unlockableDefs);
        }


        public static void CheckForPreviouslyEarned(UserProfile userProfile, bool clearView)
        {
            if (userProfile == null)
            {
                LocalUser localUser = LocalUserManager.GetFirstLocalUser();
                userProfile = localUser.userProfile;
            }
            if (userProfile == null)
            {
                Debug.LogWarning("NO USER PROFILE");
                return;
            }
            WConfig.cfgRunAutoUnlocker.Value = false;
            Debug.LogWarning("Checking for previously earned achievements " + userProfile.name);
            StatSheet statSheet = userProfile.statSheet;


            CheckOld(userProfile);
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {
                string upperName = survivorDef.cachedName.ToUpperInvariant();
                int unlocks = 0;
                if (userProfile.HasAchievement("CLEAR_SIMU_" + upperName))
                {
                    unlocks++;
                }
                else
                {
                    string bodyName = survivorDef.bodyPrefab.name;
                    ulong simu_H = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedHard, bodyName);
                    ulong simu_N = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedNormal, bodyName);
                    ulong simu_E = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedEasy, bodyName);
                    if (simu_H >= 50 || simu_N >= 50 || simu_E >= 50)
                    {
                        unlocks++;
                        userProfile.AddAchievement("CLEAR_SIMU_" + upperName, false);
                    }
                }

                if (userProfile.HasAchievement("CLEAR_ECLIPSE_" + upperName))
                {
                    unlocks++;
                    //userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Eclipse");
                }
                else if (userProfile.HasUnlockable("Eclipse." + survivorDef.cachedName + ".5"))
                {
                    unlocks++;
                    userProfile.AddAchievement("CLEAR_ECLIPSE_" + upperName, false);
                    //userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Eclipse");
                }

                if (userProfile.HasAchievement("CLEAR_LUNARSCAV_" + upperName))
                {
                    unlocks++;
                    //userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.LunarScav");
                }
                else if (userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.LunarScav"))
                {
                    unlocks++;
                    userProfile.AddAchievement("CLEAR_LUNARSCAV_" + upperName, false);
                }

                if (userProfile.HasAchievement("CLEAR_VOIDLING_" + upperName))
                {
                    unlocks++;
                    //userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Voidling");
                }
                else if (userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Voidling"))
                {
                    unlocks++;
                    userProfile.AddAchievement("CLEAR_VOIDLING_" + upperName, false);
                }

                if (unlocks > 0)
                {
                    UnlockableDef main_unlock = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.First");
                    if (main_unlock)
                    {
                        userProfile.GrantUnlockable(main_unlock);
                    }
                    if (!userProfile.HasAchievement("CLEAR_ANY_" + upperName))
                    {
                        userProfile.AddAchievement("CLEAR_ANY_" + upperName, true);
                    }
                }
                if (unlocks > 1)
                {
                    UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Both");
                    if (safe)
                    {
                        userProfile.GrantUnlockable(safe);
                    }
                    if (!userProfile.HasAchievement("CLEAR_BOTH_" + upperName))
                    {
                        userProfile.AddAchievement("CLEAR_BOTH_" + upperName, true);
                    }
                }
                if (!clearView)
                {
                    Debug.Log(survivorDef.cachedName
     + "\n hasSimulacrum | " + userProfile.HasAchievement("CLEAR_SIMU_" + upperName)
     + "\n has Eclipse 4 | " + userProfile.HasAchievement("CLEAR_ECLIPSE_" + upperName)
     + "\n has LunarScav | " + userProfile.HasAchievement("CLEAR_LUNARSCAV_" + upperName)
     + "\n has  Voidling | " + userProfile.HasAchievement("CLEAR_VOIDLING_" + upperName));
                }

            }
            if (clearView)
            {
                userProfile.ClearAllAchievementNotifications();
            }

            WConfig.RemoveSkinUnlocks.Value = false;
            WConfig.RemoveAllTrackers.Value = false;
            WConfig.cfgSilentRelockReunlock.Value = false;
            userProfile.RequestEventualSave();
        }

        public static void LockEverything(UserProfile userProfile, bool trueAll)
        {
            if (userProfile == null)
            {
                LocalUser localUser = LocalUserManager.GetFirstLocalUser();
                userProfile = localUser.userProfile;
            }
            Debug.Log(userProfile);
            if (userProfile == null)
            {
                Debug.LogWarning("NO USER PROFILE");
                return;
            }

            Debug.LogWarning("Removing all skin achievements and main unlockables " + userProfile.name);

            //Does not remove for survivors that don't exist I guess.
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {
                string upperName = survivorDef.cachedName.ToUpperInvariant();

                var First = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.First");
                var Both = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Both");
                userProfile.RevokeUnlockable(First);
                userProfile.RevokeUnlockable(Both);
                userProfile.RevokeAchievement("CLEAR_ANY_" + upperName);
                userProfile.RevokeAchievement("CLEAR_BOTH_" + upperName);
                if (trueAll)
                {
                    Debug.Log(survivorDef.cachedName
                  + "\n hadSimulacrum | " + userProfile.HasAchievement("CLEAR_SIMU_" + upperName)
                  + "\n had Eclipse 4 | " + userProfile.HasAchievement("CLEAR_ECLIPSE_" + upperName)
                  + "\n had LunarScav | " + userProfile.HasAchievement("CLEAR_LUNARSCAV_" + upperName)
                  + "\n had  Voidling | " + userProfile.HasAchievement("CLEAR_VOIDLING_" + upperName));
                    userProfile.RevokeAchievement("CLEAR_SIMU_" + upperName);
                    userProfile.RevokeAchievement("CLEAR_ECLIPSE_" + upperName);
                    userProfile.RevokeAchievement("CLEAR_VOIDLING_" + upperName);
                    userProfile.RevokeAchievement("CLEAR_LUNARSCAV_" + upperName);
                }
            }

            WConfig.RemoveSkinUnlocks.Value = false;
            WConfig.RemoveAllTrackers.Value = false;
            WConfig.cfgSilentRelockReunlock.Value = false;
            userProfile.RequestEventualSave();
        }


        public static void CheckOld(UserProfile userProfile)
        {
            //If previous and has Not wave 50 then clearly needs wave 50
            //If previous and has Wave 50 then give AltBoss

            //Old wrong names
            #region Vanilla
            if (userProfile.HasAchievement("SIMU_SKIN_BANDIT") && !userProfile.HasAchievement("CLEAR_SIMU_BANDIT2"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_BANDIT2");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_BANDIT2"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_BANDIT2", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_ENGINEER") && !userProfile.HasAchievement("CLEAR_SIMU_ENGI"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_ENGINEER");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_ENGI"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_ENGI", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_Captain") && !userProfile.HasAchievement("CLEAR_SIMU_CAPTAIN"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_Captain");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_CAPTAIN"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_CAPTAIN", false);
                }
            }
            #endregion
            #region Enforcer Gang
            if (userProfile.HasAchievement("SIMU_SKIN_HAND") && !userProfile.HasAchievement("CLEAR_SIMU_HANDOVERCLOCKED"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_HAND");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_HANDOVERCLOCKED"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_HANDOVERCLOCKED", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_SNIPER") && !userProfile.HasAchievement("CLEAR_SIMU_SNIPERCLASSIC"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_SNIPER");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_SNIPERCLASSIC"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_SNIPERCLASSIC", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_ROCKET") && !userProfile.HasAchievement("CLEAR_SIMU_ROCKETSURVIVOR"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_ROCKET");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_ROCKETSURVIVOR"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_ROCKETSURVIVOR", false);
                }
            }
            #endregion
            #region Robert
            if (userProfile.HasAchievement("SIMU_SKIN_PALADIN") && !userProfile.HasAchievement("CLEAR_SIMU_ROBPALADIN"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_PALADIN");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_ROBPALADIN"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_ROBPALADIN", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_RAVAGER") && !userProfile.HasAchievement("CLEAR_SIMU_ROB_RAVAGER_BODY_NAME"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_RAVAGER");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_ROB_RAVAGER_BODY_NAME"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_ROB_RAVAGER_BODY_NAME", false);
                }
            }
            #endregion
            #region Other
            if (userProfile.HasAchievement("SIMU_SKIN_ARSONIST") && !userProfile.HasAchievement("CLEAR_SIMU_POPCORN_ARSONIST_BODY_"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_ARSONIST");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_POPCORN_ARSONIST_BODY_"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_POPCORN_ARSONIST_BODY_", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_EXECUTIONER") && !userProfile.HasAchievement("CLEAR_SIMU_SURVIVOREXECUTIONER2"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_EXECUTIONER");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_SURVIVOREXECUTIONER2"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_SURVIVOREXECUTIONER2", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_CHEF") && !userProfile.HasAchievement("CLEAR_SIMU_GNOMECHEF"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_CHEF");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_GNOMECHEF"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_GNOMECHEF", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_NEM_ENFORCER") && !userProfile.HasAchievement("CLEAR_SIMU_NEMESISENFORCER"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_NEM_ENFORCER");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_NEMESISENFORCER"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_NEMESISENFORCER", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_PILOT") && !userProfile.HasAchievement("CLEAR_SIMU_MOFFEINPILOT"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_PILOT");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_MOFFEINPILOT"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_MOFFEINPILOT", false);
                }
            }
            #endregion
        }

        public static void UpdateTier2Objective_Specific(UserProfile userProfile, SurvivorDef survivorDef, int token)
        {
            string name = survivorDef.cachedName.ToUpperInvariant();
            AchievementDef achieve = AchievementManager.GetAchievementDef("CLEAR_BOTH_" + name);
            if (achieve != null)
            {
                achieve.descriptionToken = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_TIER2_" + token), Language.GetString(survivorDef.displayNameToken));
            }
        }

        public static void UpdateTier2_ForAll(UserProfile userProfile)
        {
            Debug.Log("UpdateTier2_ForAll");
            if (userProfile == null)
            {
                LocalUser localUser = LocalUserManager.GetFirstLocalUser();
                userProfile = localUser.userProfile;
            }
            if (userProfile == null)
            {
                Debug.LogError("NO LOCAL USER");
                return;
            }
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {
                //bool hasAltBoss = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
                //bool hasSimu = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                string upperName = survivorDef.cachedName.ToUpperInvariant();
                string survivorName = Language.GetString(survivorDef.displayNameToken);
                AchievementDef achieve = AchievementManager.GetAchievementDef("CLEAR_BOTH_" + upperName);
                if (achieve == null)
                {
                    continue;
                }
                if (userProfile.HasAchievement(achieve.identifier))
                {
                    achieve.descriptionToken = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_TIER2_0"), survivorName);
                    continue;
                }
                int token = 0;
                int unlocks = 0;
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
                if (unlocks >= 2)
                {
                    achieve.descriptionToken = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_TIER2_0"), survivorName);
                }
                else
                {
                    achieve.descriptionToken = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_TIER2_" + token), survivorName);
                }
            }

        }

        public static void AutogenerateTokens()
        {
            Debug.Log("AutogenerateTokens");
            //string based = Language.GetString("ACHIEVEMENT_BASE");

            foreach (SurvivorDef survivor in SurvivorCatalog.survivorDefs)
            {
                string nameT = survivor.cachedName.ToUpperInvariant();

                string token_name_one = string.Format("ACHIEVEMENT_CLEAR_ANY_{0}_NAME", nameT);
                string token_name_two = string.Format("ACHIEVEMENT_CLEAR_BOTH_{0}_NAME", nameT);

                string token_desc_one = string.Format("ACHIEVEMENT_CLEAR_ANY_{0}_DESCRIPTION", nameT);

                /* string token_name_simu = string.Format("ACHIEVEMENT_CLEAR_SIMU_{0}_NAME", nameT);
                 string token_name_eclipse = string.Format("ACHIEVEMENT_CLEAR_ECLIPSE_{0}_NAME", nameT);  
                 string token_name_lunarScav = string.Format("ACHIEVEMENT_CLEAR_LUNARSCAV_{0}_NAME", nameT);
                 string token_name_voidling = string.Format("ACHIEVEMENT_CLEAR_VOIDLING_{0}_NAME", nameT);


                 string token_desc_two = string.Format("ACHIEVEMENT_CLEAR_BOTH_{0}_DESCRIPTION", nameT);
                 string token_desc_twoboss = string.Format("ACHIEVEMENT_CLEAR_ALTBOSS_{0}_DESCRIPTION", nameT);

                 string token_desc_simu = string.Format("ACHIEVEMENT_CLEAR_SIMU_{0}_DESCRIPTION", nameT);
                 string token_desc_eclipse = string.Format("ACHIEVEMENT_CLEAR_ECLIPSE_{0}_DESCRIPTION", nameT);             
                 string token_desc_lunarScav = string.Format("ACHIEVEMENT_CLEAR_LUNARSCAV_{0}_DESCRIPTION", nameT);
                 string token_desc_voidling = string.Format("ACHIEVEMENT_CLEAR_VOIDLING_{0}_DESCRIPTION", nameT);*/

                string name = Language.GetString(survivor.displayNameToken);

                string name_one = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ANY_NAME"), name);
                string name_two = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_BOTH_NAME"), name);

                string desc_one = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_TIER1"), name);
                /*
               string name_simu = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_SIMU_NAME"), name);
               string name_eclipse = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ECLIPSE_NAME"), name);
               string name_lunarScav = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_LUNARSCAV_NAME"), name);
               string name_Eclipse = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_VOIDLING_NAME"), name);
                  
              
               string desc_two = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_BOTH_DESCRIPTION"), name);
               string desc_altBoss = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ALTBOSS_DESCRIPTION"), name);

               string desc_Simu = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_SIMU_DESCRIPTION"), name);
               string desc_Eclipse = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ECLIPSE_DESCRIPTION"), name);
               string desc_LunarScav = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_LUNARSCAV_DESCRIPTION"), name);
               string desc_Voidling = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_VOIDLING_DESCRIPTION"), name);*/

                LanguageAPI.Add(token_name_one, name_one);
                LanguageAPI.Add(token_name_two, name_two);
                /*LanguageAPI.Add(token_name_simu, name_simu);
                LanguageAPI.Add(token_name_lunarScav, name_lunarScav);
                LanguageAPI.Add(token_name_voidling, name_Eclipse);
                LanguageAPI.Add(token_name_eclipse, name_eclipse);*/


                LanguageAPI.Add(token_desc_one, desc_one);
                //LanguageAPI.Add(token_desc_two, desc_two);
                /*LanguageAPI.Add(token_desc_simu, desc_Simu);
                LanguageAPI.Add(token_desc_lunarScav, desc_LunarScav);
                LanguageAPI.Add(token_desc_voidling, desc_Voidling);
                LanguageAPI.Add(token_desc_eclipse, desc_Eclipse);*/



            }

        }

        public static UnlockableDef[] AutogenerateUnlockableDefs(On.RoR2.RoR2Content.orig_CreateEclipseUnlockablesForSurvivor orig, SurvivorDef survivorDef, int minEclipseLevel, int maxEclipseLevel)
        {
            UnlockableDef[] temp = orig(survivorDef, minEclipseLevel, maxEclipseLevel);

            string nameT = survivorDef.cachedName.ToUpperInvariant();
            string token1 = string.Format("ACHIEVEMENT_CLEAR_ANY_{0}_NAME", nameT);
            string token3 = string.Format("ACHIEVEMENT_CLEAR_BOTH_{0}_NAME", nameT);

            //string token_Simu = string.Format("ACHIEVEMENT_CLEAR_SIMU_{0}_NAME", nameT);    
            //string token4 = string.Format("ACHIEVEMENT_CLEAR_LUNARSCAV_{0}_NAME", nameT);
            //string token5 = string.Format("ACHIEVEMENT_CLEAR_VOIDLING_{0}_NAME", nameT);
            //string token7 = string.Format("ACHIEVEMENT_CLEAR_ECLIPSE_{0}_NAME", nameT);

            UnlockableDef unlockable_First = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockable_First.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.First";
            unlockable_First.hidden = false;
            unlockable_First.nameToken = token1;

            UnlockableDef unlockableDef_Both = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Both.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.Both";
            unlockableDef_Both.hidden = false;
            unlockableDef_Both.nameToken = token3;

            UnlockableDef unlockableDef_Simu = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Simu.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.Simu";
            unlockableDef_Simu.hidden = true;
            //unlockableDef_Simu.nameToken = token_Simu;

            UnlockableDef unlockableDef_Lunar = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Lunar.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.LunarScav";
            unlockableDef_Lunar.hidden = true;
            //unlockableDef_Lunar.nameToken = token4;

            UnlockableDef unlockableDef_Voidling = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Voidling.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.Voidling";
            unlockableDef_Voidling.hidden = true;
            //unlockableDef_Voidling.nameToken = token5;

            /*UnlockableDef unlockableDef_Eclipse = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Eclipse.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.Eclipse";
            unlockableDef_Eclipse.hidden = true;*/
            //unlockableDef_Eclipse.nameToken = token7;

            UnlockableDef[] skinUnlocks = new UnlockableDef[]
            {
                unlockable_First,
                unlockableDef_Both,
                unlockableDef_Simu,
                unlockableDef_Lunar,
                unlockableDef_Voidling,
            };
            return HG.ArrayUtils.Join(temp, skinUnlocks);
        }

        public static void AssignUnlockables()
        {
            if (SkinCatalog.skinsByBody.Length == 0)
            {
                return; //No
            }
            ExpansionDef DLC2 = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();
            Debug.Log("AssignUnlockables");
            bool noUnlocks = WConfig.cfgUnlockAll.Value;
            for (int i = 0; i < SurvivorCatalog.survivorDefs.Length; i++)
            {
                Debug.Log(SurvivorCatalog.survivorDefs[i]);
                BodyIndex bodyIndex = SurvivorCatalog.GetBodyIndexFromSurvivorIndex(SurvivorCatalog.survivorDefs[i].survivorIndex);
                if (bodyIndex != BodyIndex.None)
                {
                    string name = SurvivorCatalog.survivorDefs[i].cachedName;
                    SkinDef[] skinDefs = SkinCatalog.skinsByBody[(int)bodyIndex];
                    for (int skin = 2; skin < skinDefs.Length; skin++)
                    {
                        if (skinDefs[skin] is SkinDefMakeOnApply)
                        {

                            UnlockableDef unlockable = null;
                            if (skinDefs[skin].name.EndsWith("_1"))
                            {
                                unlockable = UnlockableCatalog.GetUnlockableDef("Skins." + name + ".Wolfo.First");
                            }
                            else if (skinDefs[skin].name.EndsWith("_DLC2"))
                            {
                                unlockable = UnlockableCatalog.GetUnlockableDef("Skins." + name + ".Wolfo.Both");
                                if (unlockable)
                                {
                                    unlockable.requiredExpansion = DLC2;
                                }
                            }
                            /*else if (skinDefs[skin].name.EndsWith("_DLC3"))
                            {
                                unlockable = UnlockableCatalog.GetUnlockableDef("Skins." + name + ".Wolfo.Both");
                            }*/
                            if (unlockable)
                            {
                                if (!unlockable.achievementIcon)
                                {
                                    unlockable.achievementIcon = skinDefs[skin].icon;
                                }
                                if (noUnlocks)
                                {
                                    skinDefs[skin].unlockableDef = null;
                                }
                                else
                                {
                                    skinDefs[skin].unlockableDef = unlockable;
                                }
                            }
                        }
                    }
                }

            }

            //Manual assigning

            UnlockableDef Merc = UnlockableCatalog.GetUnlockableDef("Skins.Merc.Wolfo.First");
            UnlockableDef Merc2 = UnlockableCatalog.GetUnlockableDef("Skins.Merc.Wolfo.Both");
            UnlockableDef Commando = UnlockableCatalog.GetUnlockableDef("Skins.Commando.Wolfo.First");
            UnlockableDef TeslaTrooper = UnlockableCatalog.GetUnlockableDef("Skins.TeslaTrooper.Wolfo.First");
            UnlockableDef Desolator = UnlockableCatalog.GetUnlockableDef("Skins.Desolator.Wolfo.First");

            if (!Merc)
            {
                Debug.Log("Failed to generate Merc Unlock, How?");
                return;
            }

            Merc2.achievementIcon = SkinsMerc.green_SKIN.icon;
            if (TeslaTrooper)
            {
                TeslaTrooper.achievementIcon = H.GetIcon("mod/Tesla/colorsTesla");
            }
            if (Desolator)
            {
                Desolator.achievementIcon = H.GetIcon("mod/Tesla/colorsDesolator");
            }
            if (noUnlocks)
            {
                Merc = null;
                Merc2 = null;
                Commando = null;
                TeslaTrooper = null;
                Desolator = null;

            }

            SkinsMerc.red_SKIN.unlockableDef = Merc;
            SkinsMerc.green_SKIN.unlockableDef = Merc2;
            Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion().unlockableDef = Commando;

            if (TeslaTrooper)
            {
                for (int i = 8; i < TeslaDesolatorColors.teslaColors.variants.Length; i++)
                {
                    TeslaDesolatorColors.teslaColors.variants[i].unlockableDef = TeslaTrooper;
                }
            }
            if (Desolator)
            {
                for (int i = 8; i < TeslaDesolatorColors.desolatorColors.variants.Length; i++)
                {
                    TeslaDesolatorColors.desolatorColors.variants[i].unlockableDef = Desolator;
                }
            }


            System.GC.Collect(); //
        }

        public static bool HideUnimplementedUnlocks(On.RoR2.UI.LogBook.LogBookController.orig_CanSelectAchievementEntry orig, AchievementDef achievementDef, System.Collections.Generic.Dictionary<RoR2.ExpansionManagement.ExpansionDef, bool> expansionAvailability)
        {
            //if (achievementDef.identifier.StartsWith("CLEAR")){}
            UnlockableDef def = UnlockableCatalog.GetUnlockableDef(achievementDef.unlockableRewardIdentifier);
            if (def && def.hidden)
            {
                Debug.Log("Hiding : " + achievementDef.identifier);
                return false;
            }
            else if (!def)
            {
                Debug.Log("Hiding : " + achievementDef.identifier);
                return false;
            }
            return orig(achievementDef, expansionAvailability);
        }
    }

}