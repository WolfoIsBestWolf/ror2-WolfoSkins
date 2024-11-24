using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine;
using RoR2;

namespace WolfoSkinsMod
{
    public class WConfig
    {
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Wolfo_Skins.cfg", true);

        public static ConfigEntry<bool> cfgUnlockAll;
        public static ConfigEntry<bool> cfgRunAutoUnlocker;
        public static ConfigEntry<bool> cfgLockEverything;
        public static ConfigEntry<bool> cfgClientHost;
        public static ConfigEntry<bool> cfgSort;
        public static ConfigEntry<bool> cfgLegacySkins;
        public static ConfigEntry<bool> cfgSilentRelockReunlock;
        public static ConfigEntry<bool> cfgClearAchievementView;
        public static ConfigEntry<bool> cfgFalseSonAttackColors;

        public static void InitConfig()
        {
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
               "Automatically grants Simu achievements for characters you already beat wave 50.\n\nGrants the AltBoss tracker if you have the old achievement but did not beat Wave 50.\n\nAnd just generally makes sure you didn't lose anything."
           );
            cfgLockEverything = ConfigFileUNSORTED.Bind(
               "Main",
               "Re-lock Everything",
               false,
               "Revoke all achievements related to this mod that you have.\n\nRemoves AltBoss/Simu tracker unlock so you need to reearn them or have the auto unlocker give them to you.\n\nDoes Not remove LunarScav/Voidling Tracker unlock."
            );
            cfgSort = ConfigFileUNSORTED.Bind(
                "Main",
                "Sort skins later",
                true,
                "Sort skins at the end of the skin list. This is mostly here in case it causes issues."
            );
            cfgSilentRelockReunlock = ConfigFileUNSORTED.Bind(
                "Other",
                "Silently ReLockReUnlock v2",
                true,
                "For whenever an update breaks smth"
            );
            cfgClearAchievementView = ConfigFileUNSORTED.Bind(
                "Other",
                "Clear Achievement Notifs",
                 false,
                "Clears any remaining notifiations if pressed in game."
            );
            cfgRunAutoUnlocker.SettingChanged += CfgRunAutoUnlocker_SettingChanged;
            cfgLockEverything.SettingChanged += CfgLockEverything_SettingChanged;
            cfgClearAchievementView.SettingChanged += CfgClearAchievementView_SettingChanged;
            RiskConfig();
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

        private static void CfgLockEverything_SettingChanged(object sender, System.EventArgs e)
        {
            if (cfgLockEverything.Value)
            {
                Unlocks.LockEverything(null);
                Unlocks.UpdateBothObjective_AtStartForAll(null);
            }
        }

        private static void CfgRunAutoUnlocker_SettingChanged(object sender, System.EventArgs e)
        {
            if (cfgRunAutoUnlocker.Value)
            {
                Unlocks.CheckForPreviouslyEarned(null, false);
                Unlocks.UpdateBothObjective_AtStartForAll(null);
            }
            
        }

        internal static void RiskConfig()
        {
            Sprite iconSprite = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/icon.png"));
            ModSettingsManager.SetModIcon(iconSprite);
            ModSettingsManager.SetModDescription("Too many skins");

            ModSettingsManager.AddOption(new CheckBoxOption(cfgRunAutoUnlocker, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgUnlockAll, true));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgLockEverything, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgClearAchievementView, false));

        }

    }
}