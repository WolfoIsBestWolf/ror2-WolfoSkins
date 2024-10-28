using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine;

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


        public static void InitConfig()
        {
            cfgUnlockAll = ConfigFileUNSORTED.Bind(
                "Main",
                "Unlock all skins",
                false,
                "Makes skins not require unlocks."
            );
           
            cfgRunAutoUnlocker = ConfigFileUNSORTED.Bind(
               "Main",
               "Run Auto Unlocker",
               true,
               "Automatically unlock Simu achievements for characters you already beat wave 50 and migrate old achievements from previous versions of the mod."
           );
            cfgLockEverything = ConfigFileUNSORTED.Bind(
               "Main",
               "Re-lock Everything",
               false,
               "Removes all achievements/unlocks related to this mod that you have."
            );
            cfgSort = ConfigFileUNSORTED.Bind(
                "Main",
                "Sort skins later",
                true,
                "Sort skins at the end of the skin list. This is mostly here in case it causes issues."
            );

            cfgRunAutoUnlocker.SettingChanged += CfgRunAutoUnlocker_SettingChanged;
            cfgLockEverything.SettingChanged += CfgLockEverything_SettingChanged;
            RiskConfig();
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
                Unlocks.CheckForPreviouslyEarned(null);
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

        }

    }
}