using R2API;
using RoR2;
using RoR2.Stats;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class Unlocks
    {
        internal static void Hooks()
        {
            Achievements.Hooks();
            On.RoR2.UI.LogBook.LogBookController.CanSelectAchievementEntry += HideUnimplementedUnlocks;

            On.RoR2.RoR2Content.CreateEclipseUnlockablesForSurvivor += AutogenerateUnlockableDefs;

            On.RoR2.LocalUserManager.AddMainUser_UserProfile += LocalUserManager_AddMainUser_UserProfile;

            On.RoR2.UnlockableCatalog.GenerateUnlockableMetaData += UnlockableCatalog_GenerateUnlockableMetaData;
            GameModeCatalog.availability.CallWhenAvailable(AutogenerateTokens);
        }

        private static void LocalUserManager_AddMainUser_UserProfile(On.RoR2.LocalUserManager.orig_AddMainUser_UserProfile orig, UserProfile userProfile)
        {
            orig(userProfile);

            //How to check if not first time runner
            //If the auto unlocker ran mod must've run before
            if (WConfig.cfgSilentRelockReunlock.Value && !WConfig.cfgRunAutoUnlocker.Value)
            {
                //Silent revoke and regrant everything upon bugged updates                
                LockEverything(userProfile);
                CheckForPreviouslyEarned(userProfile, true);
                WConfig.cfgSilentRelockReunlock.Value = false;
            }
            else
            {
                if (WConfig.cfgLockEverything.Value)
                {
                    LockEverything(userProfile);
                }
                if (WConfig.cfgRunAutoUnlocker.Value)
                {
                    CheckForPreviouslyEarned(userProfile, true);
                }         
            }          
            UpdateBothObjective_AtStartForAll(userProfile);           
        }

        private static void UnlockableCatalog_GenerateUnlockableMetaData(On.RoR2.UnlockableCatalog.orig_GenerateUnlockableMetaData orig, UnlockableDef[] unlockableDefs)
        {
            try
            {
                Unlocks.AssignUnlockables();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
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
            Debug.Log(userProfile);
            if (userProfile == null)
            {
                Debug.LogWarning("NO USER PROFILE");
                return;
            }           
            WConfig.cfgRunAutoUnlocker.Value = false;
            Debug.LogWarning("Checking for previously earned achievements " + userProfile.name);
            StatSheet statSheet = userProfile.statSheet;
            
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {
                string upperName = survivorDef.cachedName.ToUpperInvariant();

                bool hasLegacy = userProfile.HasAchievement("SIMU_SKIN_" + upperName);
                bool hasAltBoss = userProfile.HasAchievement("CLEAR_ALTBOSS_" + upperName);
                bool hasSimu = userProfile.HasAchievement("CLEAR_SIMU_" + upperName);
                bool hasEclipse = userProfile.HasAchievement("CLEAR_ECLIPSE_" + upperName);
                bool beatLunarScav = userProfile.HasAchievement("CLEAR_LUNARSCAV_" + upperName);
                bool beatVoidling = userProfile.HasAchievement("CLEAR_VOIDLING_" + upperName);

                //
                if (hasSimu)
                {
                    userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                }
                else
                {                    
                    string bodyName = survivorDef.bodyPrefab.name;
                    ulong simu_H = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedHard, bodyName);
                    ulong simu_N = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedNormal, bodyName);
                    ulong simu_E = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedEasy, bodyName);
                    if (simu_H >= 50 || simu_N >= 50 || simu_E >= 50)
                    {
                        hasSimu = true;
                        userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                        userProfile.AddAchievement("CLEAR_SIMU_" + upperName, false);
                    }
                }
                //
                //
                if (hasEclipse)
                {
                    userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Eclipse");
                }
                else if (userProfile.HasUnlockable("Eclipse." + survivorDef.cachedName + ".9"))
                {
                    hasEclipse = true;
                    userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Eclipse");
                    userProfile.AddAchievement("CLEAR_ECLIPSE_" + upperName, false);
                }
                //
                //
                if (beatLunarScav)
                {
                    userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.LunarScav");
                }
                else if (userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.LunarScav"))
                {
                    beatLunarScav = true;
                    userProfile.AddAchievement("CLEAR_LUNARSCAV_" + upperName, false);
                }
                if (beatVoidling)
                {
                    userProfile.AddUnlockToken("Skins." + survivorDef.cachedName + ".Wolfo.Voidling");
                }
                else if (userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Voidling"))
                {
                    beatVoidling = true;
                    userProfile.AddAchievement("CLEAR_VOIDLING_" + upperName, false);
                }
                //
                if (!hasAltBoss)
                {
                    if ((hasLegacy && !hasSimu) || beatLunarScav || beatVoidling)
                    {
                        hasAltBoss = true;
                        userProfile.AddAchievement("CLEAR_ALTBOSS_" + upperName, false);
                    }
                }

                Debug.Log(survivorDef.cachedName+"\n"+upperName+"\nHas autogenerated legacy: " + hasLegacy + "\nHas AltBoss: " + hasAltBoss + "\nBeat Simu: " + hasSimu + "\nBeat Eclipse: " + hasEclipse + "\nBeat LunarScav: " + beatLunarScav+"\nBeat Voidling: " + beatVoidling);
            }
            CheckOld(userProfile);
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {
                //Actual previous
                string upperName = survivorDef.cachedName.ToUpperInvariant();
                bool hasAltBoss = userProfile.HasAchievement("CLEAR_ALTBOSS_" + upperName);
                bool hasSimu = userProfile.HasAchievement("CLEAR_SIMU_" + upperName);

                if (hasSimu || hasAltBoss)
                {
                    UnlockableDef main_unlock = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.First");
                    if (main_unlock)
                    {
                        Debug.Log(main_unlock.cachedName);
                        userProfile.GrantUnlockable(main_unlock);
                    }
                    if (!userProfile.HasAchievement("CLEAR_ANY_" + upperName))
                    {
                        userProfile.AddAchievement("CLEAR_ANY_" + upperName, true);
                    }
                }
                if (hasSimu && hasAltBoss)
                {
                    UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Both");
                    if (safe)
                    {
                        Debug.Log(safe.cachedName);
                        userProfile.GrantUnlockable(safe);                       
                    }
                    if (!userProfile.HasAchievement("CLEAR_BOTH_" + upperName))
                    {
                        userProfile.AddAchievement("CLEAR_BOTH_" + upperName, true);
                    }
                }
            }

            if (clearView)
            {
                userProfile.ClearAllAchievementNotifications();//I guess?
                //userProfile.unviewedAchievementsList.Clear();
            }          
        }

        public static void LockEverything(UserProfile userProfile)
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
            WConfig.cfgLockEverything.Value = false;
            Debug.LogWarning("Removing all skin achievements and main unlockables " + userProfile.name);
  
            //Does not remove for survivors that don't exist I guess.
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {
                string upperName = survivorDef.cachedName.ToUpperInvariant();

                var First = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.First");
                var Both = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Both");
                var Simu = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                if (First)
                {
                    userProfile.RevokeUnlockable(First);
                }
                else
                {
                    Debug.LogWarning("Unlockables not properly generated for :" + survivorDef.cachedName);
                }
                if (Both)
                {
                    userProfile.RevokeUnlockable(Both);
                }
                if (Simu)
                {
                    userProfile.RevokeUnlockable(Simu);
                }
                
                userProfile.RevokeAchievement("CLEAR_ALTBOSS_" + upperName);
                userProfile.RevokeAchievement("CLEAR_SIMU_" + upperName);

                userProfile.RevokeAchievement("CLEAR_ANY_" + upperName);
                userProfile.RevokeAchievement("CLEAR_BOTH_" + upperName);
            }
            userProfile.ClearAllAchievementNotifications();
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
                //userProfile.AddUnlockToken("Skins.Bandit2.Wolfo.AltBoss");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_BANDIT"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_BANDIT", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_ENGINEER") && !userProfile.HasAchievement("CLEAR_SIMU_ENGI"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_ENGINEER");
                //userProfile.AddUnlockToken("Skins.Engi.Wolfo.AltBoss");
                if (!userProfile.HasAchievement("CLEAR_ALTBOSS_ENGI"))
                {
                    userProfile.AddAchievement("CLEAR_ALTBOSS_ENGI", false);
                }
            }
            if (userProfile.HasAchievement("SIMU_SKIN_Captain") && !userProfile.HasAchievement("CLEAR_SIMU_CAPTAIN"))
            {
                Debug.Log("Has manual Legacy : SIMU_SKIN_Captain");
                //userProfile.AddUnlockToken("Skins.Engi.Wolfo.AltBoss");
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

        public static void UpdateBothObjective_Specific(UserProfile userProfile, SurvivorDef survivorDef)
        {
            //bool beatAltBoss = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
            //bool beatSimu = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
            bool beatAltBoss = userProfile.HasAchievement("CLEAR_ALTBOSS_" + survivorDef.cachedName.ToUpperInvariant());
            bool beatSimu = userProfile.HasAchievement("CLEAR_SIMU_" + survivorDef.cachedName.ToUpperInvariant());

            string name = survivorDef.cachedName.ToUpperInvariant();
            AchievementDef achieve = AchievementManager.GetAchievementDef("CLEAR_BOTH_" + name);
            if (achieve != null)
            {       
                if (beatAltBoss && beatSimu)
                {
                    achieve.descriptionToken = "ACHIEVEMENT_CLEAR_BOTH_" + name + "_DESCRIPTION";
                }
                else if (beatAltBoss)
                {
                    achieve.descriptionToken = "ACHIEVEMENT_CLEAR_SIMU_" + name + "_DESCRIPTION";
                }
                else if (beatSimu)
                {
                    achieve.descriptionToken = "ACHIEVEMENT_CLEAR_ALTBOSS_" + name + "_DESCRIPTION";
                }
                else
                {
                    achieve.descriptionToken = "ACHIEVEMENT_CLEAR_BOTH_" + name + "_DESCRIPTION";
                }
            }
        }

        public static void UpdateBothObjective_AtStartForAll(UserProfile userProfile)
        {
            Debug.Log("UpdateBothObjective_AtStartForAll");
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
                bool hasAltBoss = userProfile.HasAchievement("CLEAR_ALTBOSS_" + upperName);
                bool hasSimu = userProfile.HasAchievement("CLEAR_SIMU_" + upperName);

                string name = survivorDef.cachedName.ToUpperInvariant();
                AchievementDef achieve = AchievementManager.GetAchievementDef("CLEAR_BOTH_"+ name);
                if (achieve != null)
                {
                    if (hasAltBoss && hasSimu)
                    {
                        achieve.descriptionToken = "ACHIEVEMENT_CLEAR_BOTH_" + name + "_DESCRIPTION";
                    }
                    else if (hasAltBoss)
                    {
                        achieve.descriptionToken = "ACHIEVEMENT_CLEAR_SIMU_" + name + "_DESCRIPTION";
                    }
                    else if (hasSimu)
                    {
                        achieve.descriptionToken = "ACHIEVEMENT_CLEAR_ALTBOSS_" + name + "_DESCRIPTION";
                    }
                    else
                    {
                        achieve.descriptionToken = "ACHIEVEMENT_CLEAR_BOTH_" + name + "_DESCRIPTION";
                    }
                    //Debug.Log(achieve.identifier);
                    //Debug.Log(altBoss);
                    //Debug.Log(simu);
                    //Debug.Log(achieve.descriptionToken);
                }
                else
                {
                    //Debug.Log("No Achivement for CLEAR_BOTH_" + name);
                }
            }

        }

        public static void AutogenerateTokens()
        {
            Debug.Log("AutogenerateTokens");
            //string based = Language.GetString("ACHIEVEMENT_BASE");
            string[] langs = new string[] { "en", "ru" };

            foreach (SurvivorDef survivor in SurvivorCatalog.survivorDefs)
            {
                string nameT = survivor.cachedName.ToUpperInvariant();

                string token_any = string.Format("ACHIEVEMENT_CLEAR_ANY_{0}_NAME", nameT);
                string token_both = string.Format("ACHIEVEMENT_CLEAR_BOTH_{0}_NAME", nameT);

                string token_simu = string.Format("ACHIEVEMENT_CLEAR_SIMU_{0}_NAME", nameT);
                string token_eclipse = string.Format("ACHIEVEMENT_CLEAR_ECLIPSE_{0}_NAME", nameT);  
                string token4 = string.Format("ACHIEVEMENT_CLEAR_LUNARSCAV_{0}_NAME", nameT);
                string token5 = string.Format("ACHIEVEMENT_CLEAR_VOIDLING_{0}_NAME", nameT);

                string token_any2 = string.Format("ACHIEVEMENT_CLEAR_ANY_{0}_DESCRIPTION", nameT);
                string token_both2 = string.Format("ACHIEVEMENT_CLEAR_BOTH_{0}_DESCRIPTION", nameT);
                string token_altboss2 = string.Format("ACHIEVEMENT_CLEAR_ALTBOSS_{0}_DESCRIPTION", nameT);

                string token_simu2 = string.Format("ACHIEVEMENT_CLEAR_SIMU_{0}_DESCRIPTION", nameT);
                string token_eclipse2 = string.Format("ACHIEVEMENT_CLEAR_ECLIPSE_{0}_DESCRIPTION", nameT);             
                string token44 = string.Format("ACHIEVEMENT_CLEAR_LUNARSCAV_{0}_DESCRIPTION", nameT);
                string token55 = string.Format("ACHIEVEMENT_CLEAR_VOIDLING_{0}_DESCRIPTION", nameT);

                for (int i = 0; i < langs.Length; i++)
                {
                    string name = Language.GetString(survivor.displayNameToken, langs[i]);

                    string string_any = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ANY_NAME", langs[i]), name);
                    string string_both = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_BOTH_NAME", langs[i]), name);

                    string string_simu = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_SIMU_NAME", langs[i]), name);
                    string string_eclipse = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ECLIPSE_NAME", langs[i]), name);
                    string string_lunar = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_LUNARSCAV_NAME", langs[i]), name);
                    string string_void = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_VOIDLING_NAME", langs[i]), name);
                    //
                    //
                    string string_any2 = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ANY_DESCRIPTION", langs[i]), name);
                    string string_both2 = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_BOTH_DESCRIPTION", langs[i]), name);
                    string string_altboss2 = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ALTBOSS_DESCRIPTION", langs[i]), name);

                    string string_simu2 = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_SIMU_DESCRIPTION", langs[i]), name);
                    string string_eclipse2 = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_ECLIPSE_DESCRIPTION", langs[i]), name);
                    string string44 = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_LUNARSCAV_DESCRIPTION", langs[i]), name);
                    string string55 = string.Format(Language.GetString("ACHIEVEMENT_CLEAR_VOIDLING_DESCRIPTION", langs[i]), name);

                    LanguageAPI.Add(token_any, string_any, langs[i]);
                    LanguageAPI.Add(token_simu, string_simu, langs[i]);
                    LanguageAPI.Add(token4, string_lunar, langs[i]);
                    LanguageAPI.Add(token5, string_void, langs[i]);
                    LanguageAPI.Add(token_eclipse, string_eclipse, langs[i]);
                    LanguageAPI.Add(token_both, string_both, langs[i]);

                    LanguageAPI.Add(token_any2, string_any2, langs[i]);
                    LanguageAPI.Add(token_simu2, string_simu2, langs[i]);
                    LanguageAPI.Add(token_altboss2, string_altboss2, langs[i]);
                    LanguageAPI.Add(token44, string44, langs[i]);
                    LanguageAPI.Add(token55, string55, langs[i]);
                    LanguageAPI.Add(token_eclipse2, string_eclipse2, langs[i]);
                    LanguageAPI.Add(token_both2, string_both2, langs[i]);
                }

            }

        }

        public static UnlockableDef[] AutogenerateUnlockableDefs(On.RoR2.RoR2Content.orig_CreateEclipseUnlockablesForSurvivor orig, SurvivorDef survivorDef, int minEclipseLevel, int maxEclipseLevel)
        {
            UnlockableDef[] temp = orig(survivorDef, minEclipseLevel, maxEclipseLevel);

            string nameT = survivorDef.cachedName.ToUpperInvariant();
            string token1 = string.Format("ACHIEVEMENT_CLEAR_ANY_{0}_NAME", nameT);
            string token3 = string.Format("ACHIEVEMENT_CLEAR_BOTH_{0}_NAME", nameT);

            string token_Simu = string.Format("ACHIEVEMENT_CLEAR_SIMU_{0}_NAME", nameT);    
            string token4 = string.Format("ACHIEVEMENT_CLEAR_LUNARSCAV_{0}_NAME", nameT);
            string token5 = string.Format("ACHIEVEMENT_CLEAR_VOIDLING_{0}_NAME", nameT);
            string token7 = string.Format("ACHIEVEMENT_CLEAR_ECLIPSE_{0}_NAME", nameT);

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
            unlockableDef_Simu.hidden = false;
            unlockableDef_Simu.nameToken = token_Simu;

            UnlockableDef unlockableDef_Lunar = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Lunar.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.LunarScav";
            unlockableDef_Lunar.hidden = false;
            unlockableDef_Lunar.nameToken = token4;

            UnlockableDef unlockableDef_Voidling = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Voidling.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.Voidling";
            unlockableDef_Voidling.hidden = false;
            unlockableDef_Voidling.nameToken = token5;

            UnlockableDef unlockableDef_Eclipse = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Eclipse.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.Eclipse";
            unlockableDef_Eclipse.hidden = false;
            unlockableDef_Eclipse.nameToken = token7;


            temp = temp.Add(unlockable_First, unlockableDef_Both, unlockableDef_Simu, unlockableDef_Lunar, unlockableDef_Voidling, unlockableDef_Eclipse);
            //Gets put into EclipseCache but that doesn't seem to do anything.
            return temp;
        }

        public static void AssignUnlockables()
        {
            Debug.Log("AssignUnlockables");

            bool noUnlocks = WConfig.cfgUnlockAll.Value;
            for (int i = 0; i < SurvivorCatalog.survivorDefs.Length; i++)
            {
                //Debug.LogWarning(SurvivorCatalog.survivorDefs[i]);
                GameObject Body = SurvivorCatalog.survivorDefs[i].bodyPrefab;
                BodyIndex Index = Body.GetComponent<CharacterBody>().bodyIndex;
                SkinDef[] skinDefs = BodyCatalog.skins[(int)Index];
                //Debug.Log(SurvivorCatalog.survivorDefs[i]);
                for (int skin = 0; skin < skinDefs.Length; skin++)
                {
                    //Debug.Log(skinDefs[skin].name);
                    UnlockableDef unlockable = null;
                    if (skinDefs[skin].name.EndsWith("_Simu"))
                    {
                        unlockable = UnlockableCatalog.GetUnlockableDef("Skins." + SurvivorCatalog.survivorDefs[i].cachedName + ".Wolfo.First");                 
                    }
                    else if (skinDefs[skin].name.EndsWith("_AltBoss"))
                    {
                        unlockable = UnlockableCatalog.GetUnlockableDef("Skins." + SurvivorCatalog.survivorDefs[i].cachedName + ".Wolfo.Both");
                    }
                    else if (skinDefs[skin].name.EndsWith("_Any"))
                    {
                        unlockable = UnlockableCatalog.GetUnlockableDef("Skins." + SurvivorCatalog.survivorDefs[i].cachedName + ".Wolfo.First");
                    }
                    if (unlockable)
                    {
                        if (!unlockable.achievementIcon)
                        {
                            unlockable.achievementIcon = skinDefs[skin].icon;
                        }
                        if (!noUnlocks)
                        {
                            skinDefs[skin].unlockableDef = unlockable;
                        }                
                    }
                }
            }

            //Manual assigning
            SkinDef UnusedCommandoSkin = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion();
            UnlockableDef Commando = UnlockableCatalog.GetUnlockableDef("Skins.Commando.Wolfo.First");
            UnusedCommandoSkin.unlockableDef = Commando;

            UnlockableDef Merc = UnlockableCatalog.GetUnlockableDef("Skins.Merc.Wolfo.First");
            UnlockableDef Merc2 = UnlockableCatalog.GetUnlockableDef("Skins.Merc.Wolfo.Both");

            SkinsMerc.red_SKIN.unlockableDef = Merc;
            SkinsMerc.green_SKIN.unlockableDef = Merc2;
            Merc2.achievementIcon = SkinsMerc.green_SKIN.icon;

            UnlockableDef TeslaTrooper = UnlockableCatalog.GetUnlockableDef("Skins.TeslaTrooper.Wolfo.First");
            UnlockableDef Desolator = UnlockableCatalog.GetUnlockableDef("Skins.Desolator.Wolfo.First");
            if (TeslaTrooper)
            {
                for (int i = 8; i < TeslaDesolatorColors.teslaColors.variants.Length; i++)
                {
                    TeslaDesolatorColors.teslaColors.variants[i].unlockableDef = TeslaTrooper;
                }
                TeslaTrooper.achievementIcon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/skinIconTesla.png"));
            }
            if(Desolator)
            {
                for (int i = 8; i < TeslaDesolatorColors.desolatorColors.variants.Length; i++)
                {
                    TeslaDesolatorColors.desolatorColors.variants[i].unlockableDef = Desolator;
                }
                Desolator.achievementIcon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/skinIconDesolator.png"));
            }

    
            System.GC.Collect(); //
        }

        public static bool HideUnimplementedUnlocks(On.RoR2.UI.LogBook.LogBookController.orig_CanSelectAchievementEntry orig, AchievementDef achievementDef, System.Collections.Generic.Dictionary<RoR2.ExpansionManagement.ExpansionDef, bool> expansionAvailability)
        {
            if (achievementDef.identifier.StartsWith("CLEAR"))
            {
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
            }
            return orig(achievementDef, expansionAvailability);
        }
    }

}