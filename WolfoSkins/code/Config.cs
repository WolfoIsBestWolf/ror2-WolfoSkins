using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RoR2;
using UnityEngine;

namespace WolfoSkinsMod
{
    public class WConfig
    {
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Wolfo_Skins.cfg", true);

        public static ConfigEntry<bool> cfgUnlockAll;
        public static ConfigEntry<bool> cfgRunAutoUnlocker;
        public static ConfigEntry<bool> RemoveSkinUnlocks;
        public static ConfigEntry<bool> cfgClientHost;
        public static ConfigEntry<bool> cfgSort;
        public static ConfigEntry<bool> cfgLegacySkins;
        public static ConfigEntry<bool> cfgSilentRelockReunlock;
        public static ConfigEntry<bool> cfgClearAchievementView;
        public static ConfigEntry<bool> cfgDump;


        public static ConfigEntry<bool> RemoveAllTrackers;
        public static ConfigEntry<bool> cfgTest;

        public static void InitConfig()
        {
            cfgTest = ConfigFileUNSORTED.Bind(
                "Test",
                "cfgTest",
                false,
                "Don't"
            );
            RemoveAllTrackers = ConfigFileUNSORTED.Bind(
                "Test",
                "Remove all unlock trackers",
                false,
                "Testing "
            );

            cfgUnlockAll = ConfigFileUNSORTED.Bind(
                "Main",
                "Unlock all skins",
                false,
                "Makes skins not require unlocks.\n\nUnlocks and Achievements remain to be collected."
            );

            cfgRunAutoUnlocker = ConfigFileUNSORTED.Bind(
               "Main",
               "Check for missing unlocks",
               true,
               "Checks for Eclipse 4+ and Simu Wave 50 completion."
           );
            RemoveSkinUnlocks = ConfigFileUNSORTED.Bind(
               "Main",
               "Re-lock Everything",
               false,
               "Revoke all achievements related to this mod that you have.\n\nRemoves AltBoss/Simu tracker unlock so you need to reearn them or have the auto unlocker give them to you.\n\nDoes Not remove LunarScav/Voidling Tracker unlock."
            );
            cfgSort = ConfigFileUNSORTED.Bind(
                "Main",
                "Sort Skins",
                false,
                "Sort skins at the end of the skin list. This is mostly here in case it causes issues."
            );
            cfgSilentRelockReunlock = ConfigFileUNSORTED.Bind(
                "Other",
                "Silently ReLockReUnlock v3",
                true,
                "For whenever an update adds, changes or breaks something."
            );
            cfgClearAchievementView = ConfigFileUNSORTED.Bind(
                "Main",
                "Clear Achievement Notifs",
                 false,
                "Clears any remaining notifiations if pressed in game."
            );
            cfgDump = ConfigFileUNSORTED.Bind(
                "Test",
                "cfgDump",
                 false,
                "cfgDump"
            );
            cfgRunAutoUnlocker.SettingChanged += CfgRunAutoUnlocker_SettingChanged;

            cfgClearAchievementView.SettingChanged += CfgClearAchievementView_SettingChanged;
            RemoveAllTrackers.SettingChanged += RemoveAllIdentifiers_SettingChanged;
            RemoveSkinUnlocks.SettingChanged += RemoveAllIdentifiers_SettingChanged;
            cfgUnlockAll.SettingChanged += CfgUnlockAll_SettingChanged;
            cfgDump.SettingChanged += CfgDump_SettingChanged;
        }

        private static void CfgDump_SettingChanged(object sender, System.EventArgs e)
        {
            if (cfgDump.Value == true)
            {
                H.DumpInfo();
            }
            cfgDump.Value = false;
        }

        private static void CfgUnlockAll_SettingChanged(object sender, System.EventArgs e)
        {
            Unlocks.AssignUnlockables();
        }

        private static void RemoveAllIdentifiers_SettingChanged(object sender, System.EventArgs e)
        {
            if (RemoveSkinUnlocks.Value || RemoveAllTrackers.Value)
            {
                Unlocks.LockEverything(null, RemoveAllTrackers.Value);
                Unlocks.UpdateTier2_ForAll(null);
            }

        }

        private static void CfgClearAchievementView_SettingChanged(object sender, System.EventArgs e)
        {
            LocalUser localUser = LocalUserManager.GetFirstLocalUser();
            if (localUser == null)
            {
                Debug.LogError("NO LOCAL USER");
                return;
            }
            UserProfile userProfile = localUser.userProfile;
            if (userProfile == null)
            {
                Debug.LogError("NO LOCAL USER");
                return;
            }
            userProfile.ClearAllAchievementNotifications();
            cfgClearAchievementView.Value = false;
        }



        private static void CfgRunAutoUnlocker_SettingChanged(object sender, System.EventArgs e)
        {
            if (cfgRunAutoUnlocker.Value)
            {
                Unlocks.CheckForPreviouslyEarned(null, false);
                Unlocks.UpdateTier2_ForAll(null);
            }

        }

        internal static void RiskConfig()
        {
            ModSettingsManager.SetModIcon(Assets.Bundle.LoadAsset<Sprite>("Assets/Skins/icon.png"));
            ModSettingsManager.SetModDescription("Too many skins");


            ModSettingsManager.AddOption(new CheckBoxOption(cfgUnlockAll, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgSort, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgRunAutoUnlocker, false));
            ModSettingsManager.AddOption(new CheckBoxOption(RemoveSkinUnlocks, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgClearAchievementView, false));
            ModSettingsManager.AddOption(new CheckBoxOption(RemoveAllTrackers, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgTest, true));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgDump, false));

        }

    }
}