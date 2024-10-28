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
    public class Unlocks
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
                    Grant(".Wolfo.Simu", false);
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
                Grant(".Wolfo.LunarScav", true);
                System.Action<Run> action = unlockSkins_AltBoss;
                if (action == null)
                {
                    return;
                }
                action(Run.instance);
            };
            On.EntityStates.VoidRaidCrab.DeathState.OnEnter += Voidling_Unlock;
            On.EntityStates.BrotherMonster.TrueDeathState.OnEnter += Eclipse8_Unlock;

            //Run.onClientGameOverGlobal += BackupUnlocker;
            //
            On.RoR2.UI.LogBook.LogBookController.CanSelectAchievementEntry += HideUnimplementedUnlocks;

            //Idk which would be better
            //On.RoR2.UnlockableCatalog.SetUnlockableDefs += UnlockableCatalog_SetUnlockableDefs;
            On.RoR2.RoR2Content.CreateEclipseUnlockablesForSurvivor += AutogenerateUnlockableDefs;


            On.RoR2.LocalUserManager.AddMainUser_UserProfile += LocalUserManager_AddMainUser_UserProfile;


            On.RoR2.UnlockableCatalog.GenerateUnlockableMetaData += UnlockableCatalog_GenerateUnlockableMetaData;
            GameModeCatalog.availability.CallWhenAvailable(AutogenerateTokens);
           
        }

        private static void Voidling_Unlock(On.EntityStates.VoidRaidCrab.DeathState.orig_OnEnter orig, EntityStates.VoidRaidCrab.DeathState self)
        {
            orig(self);
            if (SceneInfo.instance && SceneInfo.instance.sceneDef.cachedName.Equals("voidraid"))
            {
                Grant(".Wolfo.Voidling", true);
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
                    //Grant(".Wolfo.Eclipse", false);
                }
            }
        }


        private static void LocalUserManager_AddMainUser_UserProfile(On.RoR2.LocalUserManager.orig_AddMainUser_UserProfile orig, UserProfile userProfile)
        {
            orig(userProfile);
            CheckForPreviouslyEarned(userProfile);
            LockEverything(userProfile);
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

 
        
 
        public static void Grant(string unlockable, bool isAltBoss)
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

                            //AltBoss
                            if (isAltBoss)
                            {
                                UnlockableDef AltBoss = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
                                if (!userProfile.HasUnlockable(AltBoss))
                                {
                                    networkUser.ServerHandleUnlock(AltBoss);
                                }
                                userProfile.GrantUnlockable(AltBoss);
                            }

                            //Main Unlockable
                            UnlockableDef unlockAlways = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName +".Wolfo.First");
                            if (unlockAlways)
                            {
                                Debug.Log("Granting Unlockable: Skins." + survivorDef.cachedName + ".Wolfo.First");
                                if (!userProfile.HasUnlockable(unlockAlways))
                                {
                                    networkUser.ServerHandleUnlock(unlockAlways);
                                }
                            }

                            //Both
                            bool altBoss = isAltBoss || userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
                            bool Simu = unlockable.EndsWith("Simu") || userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                            if (altBoss && Simu)
                            {
                                System.Action<Run> action = unlockSkins_Both;
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
                            }
                            UpdateBothObjective_Specific(userProfile, survivorDef);
                        }
                    }
                }
            }
        }
 
        public static void CheckForPreviouslyEarned(UserProfile userProfile)
        { 
            if (WConfig.cfgRunAutoUnlocker.Value == false)
            {
                return;
            }

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
                string bodyName = survivorDef.bodyPrefab.name;

                ulong simu_H = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedHard, bodyName);
                ulong simu_N = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedNormal, bodyName);
                ulong simu_E = statSheet.GetStatValueULong(PerBodyStatDef.highestInfiniteTowerWaveReachedEasy, bodyName);

                UnlockableDef simu_unlock = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                UnlockableDef altBoss_unlock = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
               
                if (simu_H >= 50 || simu_N >= 50 || simu_E >= 50)
                {               
                    if (simu_unlock)
                    {
                        Debug.Log(simu_unlock.cachedName);
                        userProfile.GrantUnlockable(simu_unlock);
                    }
                }
                else if (userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo"))
                {
                    //userProfile.RevokeUnlockable(simu_unlock);
                    //If they had old achievement but not wave 50 then giving them alt boss check
                    //If they did both Wave 50 and AltBoss cant track that
                    if (altBoss_unlock)
                    {
                        Debug.Log(altBoss_unlock.cachedName);
                        userProfile.GrantUnlockable(altBoss_unlock);
                    }
                }
                if (userProfile.HasUnlockable("Eclipse." + survivorDef.cachedName + ".9"))
                {
                    UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Eclipse");
                    if (safe)
                    {
                        Debug.Log(safe.cachedName);
                        userProfile.GrantUnlockable(safe);
                    }
                }
            }
            CheckOld(userProfile);
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {
                //Actual previous
                bool altBoss = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
                bool hasSimu = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                if (hasSimu || altBoss)
                {
                    UnlockableDef main_unlock = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.First");
                    if (main_unlock)
                    {
                        Debug.Log(main_unlock.cachedName);
                        if (!userProfile.HasAchievement("CLEAR_ANY_" + survivorDef.cachedName.ToUpperInvariant()))
                        {
                            userProfile.GrantUnlockable(main_unlock);
                            userProfile.AddAchievement("CLEAR_ANY_" + survivorDef.cachedName.ToUpperInvariant(), true);
                        }
                    }
                }
                if (hasSimu && altBoss)
                {
                    UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Both");
                    if (safe)
                    {
                        Debug.Log(safe.cachedName);
                        if (!userProfile.HasAchievement("CLEAR_BOTH_" + survivorDef.cachedName.ToUpperInvariant()))
                        {
                            userProfile.GrantUnlockable(safe);
                            userProfile.AddAchievement("CLEAR_BOTH_" + survivorDef.cachedName.ToUpperInvariant(), true);
                        }
                    }
                }
            }

        }

        public static void LockEverything(UserProfile userProfile)
        {
            if (WConfig.cfgLockEverything.Value == false)
            {
                return;
            }

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
            Debug.LogWarning("Removing all skin unlockables + achievements " + userProfile.name);
  
            foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
            {

                userProfile.RevokeUnlockable(UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.First"));
                userProfile.RevokeUnlockable(UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Both"));
                userProfile.RevokeUnlockable(UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss"));

                userProfile.RevokeUnlockable(UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Simu"));
                //userProfile.RevokeUnlockable(UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.LunarScav"));
                //userProfile.RevokeUnlockable(UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Voidling"));
                userProfile.RevokeUnlockable(UnlockableCatalog.GetUnlockableDef("Skins." + survivorDef.cachedName + ".Wolfo.Eclipse"));

                userProfile.RevokeAchievement("CLEAR_ANY_" + survivorDef.cachedName.ToUpperInvariant());
                userProfile.RevokeAchievement("CLEAR_BOTH_" + survivorDef.cachedName.ToUpperInvariant());
            }
        }


        public static void CheckOld(UserProfile userProfile)
        {
            //ChefMod : GnomeChef
            //Ravager : ROB_RAVAGER_BODY_NAME (I imagine he'd fix this at some point but blegh)

            //If previous and has Not wave 50 then clearly needs wave 50
            //If previous and has Wave 50 then give AltBoss

            #region Specific old
            //Old wrong names
            if (userProfile.HasUnlockable("Skins.Paladin.Wolfo") && !userProfile.HasUnlockable("Skins.RobPaladin.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.RobPaladin.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }
            if (userProfile.HasUnlockable("Skins.Arsonist.Wolfo") && !userProfile.HasUnlockable("Skins.POPCORN_ARSONIST_BODY_.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.POPCORN_ARSONIST_BODY_.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }
            if (userProfile.HasUnlockable("Skins.Ravager.Wolfo") && !userProfile.HasUnlockable("Skins.ROB_RAVAGER_BODY_NAME.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.ROB_RAVAGER_BODY_NAME.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }

            if (userProfile.HasUnlockable("Skins.Rocket.Wolfo") && !userProfile.HasUnlockable("Skins.RocketSurvivor.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.RocketSurvivor.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }
            if (userProfile.HasUnlockable("Skins.Sniper.Wolfo") && !userProfile.HasUnlockable("Skins.SniperClassic.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.SniperClassic.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }
            if (userProfile.HasUnlockable("Skins.Bandit.Wolfo") && !userProfile.HasUnlockable("Skins.Bandit2.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.Bandit2.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }
            if (userProfile.HasUnlockable("Skins.Engineer.Wolfo") && !userProfile.HasUnlockable("Skins.Engi.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.Engi.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }
            if (userProfile.HasUnlockable("Skins.Hand.Wolfo") && !userProfile.HasUnlockable("Skins.HANDOverclocked.Wolfo.Simu"))
            {
                UnlockableDef safe = UnlockableCatalog.GetUnlockableDef("Skins.HANDOverclocked.Wolfo.AltBoss");
                if (safe)
                {
                    Debug.Log(safe.cachedName);
                    userProfile.GrantUnlockable(safe);
                }
            }
            #endregion
        }

        public static void UpdateBothObjective_Specific(UserProfile userProfile, SurvivorDef survivorDef)
        {
            bool altBoss = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
            bool simu = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
            string name = survivorDef.cachedName.ToUpperInvariant();
            AchievementDef achieve = AchievementManager.GetAchievementDef("CLEAR_BOTH_" + name);
            if (achieve != null)
            {       
                if (altBoss && simu)
                {
                    achieve.descriptionToken = "ACHIEVEMENT_CLEAR_BOTH_" + name + "_DESCRIPTION";
                }
                else if (altBoss)
                {
                    achieve.descriptionToken = "ACHIEVEMENT_CLEAR_SIMU_" + name + "_DESCRIPTION";
                }
                else if (simu)
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
                bool altBoss = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.AltBoss");
                bool simu = userProfile.HasUnlockable("Skins." + survivorDef.cachedName + ".Wolfo.Simu");
                string name = survivorDef.cachedName.ToUpperInvariant();

                AchievementDef achieve = AchievementManager.GetAchievementDef("CLEAR_BOTH_"+ name);
                if (achieve != null)
                {
                    if (altBoss && simu)
                    {
                        achieve.descriptionToken = "ACHIEVEMENT_CLEAR_BOTH_" + name + "_DESCRIPTION";
                    }
                    else if (altBoss)
                    {
                        achieve.descriptionToken = "ACHIEVEMENT_CLEAR_SIMU_" + name + "_DESCRIPTION";
                    }
                    else if (simu)
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
                    Debug.LogWarning("No Achivement for CLEAR_BOTH_" + name);
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
                Debug.Log(survivor.cachedName);
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

            UnlockableDef unlockable_Old = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockable_Old.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo";
            unlockable_Old.hidden = true;
            unlockable_Old.nameToken = token1;

            UnlockableDef unlockable_First = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockable_First.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.First";
            unlockable_First.hidden = false;
            unlockable_First.nameToken = token1;

            UnlockableDef unlockableDef_Both = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_Both.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.Both";
            unlockableDef_Both.hidden = false;
            unlockableDef_Both.nameToken = token3;

            UnlockableDef unlockableDef_AltBoss = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef_AltBoss.cachedName = "Skins." + survivorDef.cachedName + ".Wolfo.AltBoss";
            unlockableDef_AltBoss.hidden = true;

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


            temp = temp.Add(unlockable_Old, unlockable_First, unlockableDef_Both, unlockableDef_AltBoss, unlockableDef_Simu, unlockableDef_Lunar, unlockableDef_Voidling, unlockableDef_Eclipse);
            //Gets put into EclipseCache but that doesn't seem to do anything.
            return temp;
        }

        public static void AssignUnlockables()
        {
            Debug.Log("AssignUnlockables");

            bool noUnlocks = WConfig.cfgUnlockAll.Value;

            Debug.Log(SurvivorCatalog.survivorDefs.Length);
            Debug.Log(UnlockableCatalog.nameToDefTable.Keys.Count);
            for (int i = 0; i < SurvivorCatalog.survivorDefs.Length; i++)
            {
                //Debug.LogWarning(SurvivorCatalog.survivorDefs[i]);
                GameObject Body = SurvivorCatalog.survivorDefs[i].bodyPrefab;
                BodyIndex Index = Body.GetComponent<CharacterBody>().bodyIndex;
                SkinDef[] skinDefs = BodyCatalog.skins[(int)Index];
                Debug.Log(SurvivorCatalog.survivorDefs[i]);
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


            SkinDef UnusedCommandoSkin = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion();
            UnlockableDef Commando = UnlockableCatalog.GetUnlockableDef("Skins.Commando.Wolfo");
            UnusedCommandoSkin.unlockableDef = Commando;

            UnlockableDef Merc = UnlockableCatalog.GetUnlockableDef("Skins.Merc.Wolfo");
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




    //Probably don't make a entirely new Eclipse8 achievement since Eclipse..Eclipse9 already exists
    public class Achievement_Simu : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            Unlocks.unlockSkins_Simu += this.Unlock;
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Unlocks.unlockSkins_Simu -= this.Unlock;
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
            Unlocks.unlockSkins_Both += this.Unlock;
            //Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Unlocks.unlockSkins_Both -= this.Unlock;
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
            Unlocks.unlockSkins_Simu += this.Unlock;
            Unlocks.unlockSkins_AltBoss += this.Unlock;
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            Unlocks.unlockSkins_Simu -= this.Unlock;
            Unlocks.unlockSkins_AltBoss -= this.Unlock;
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