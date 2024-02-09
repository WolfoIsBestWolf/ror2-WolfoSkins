using BepInEx;
using BepInEx.Configuration;

namespace WolfoSkinsMod
{
    public class WConfig
    {
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Wolfo_Skins.cfg", true);

        public static ConfigEntry<bool> cfgUnlockAll;

        public static ConfigEntry<float> VoidlingHPMultiplier;
        public static ConfigEntry<float> VoidlingDamageMultiplier;
        public static ConfigEntry<bool> VoidlingLimitLevel99;

        public static ConfigEntry<bool> VoidlingNoScaleHPMulti;
        //public static ConfigEntry<bool> VoidlingNoScaleDMGMulti; //Scripted Combat doesn't give bonus bonus dmg in multi

        public static void InitConfig()
        {
            cfgUnlockAll = ConfigFileUNSORTED.Bind(
                "Main",
                "Unlock all skins",
                false,
                "Makes skins not require unlocks."
            );
            VoidlingHPMultiplier = ConfigFileUNSORTED.Bind(
                "Optional Voidling Nerfs",
                "HP Multiplier",
                1f,
                "Multiply Voidlings HP by this."
            );
            VoidlingDamageMultiplier = ConfigFileUNSORTED.Bind(
                "Optional Voidling Nerfs",
                "Dmg Multiplier",
                1f,
                "Multiply Voidlings HP by this."
            );
            VoidlingLimitLevel99 = ConfigFileUNSORTED.Bind(
                "Optional Voidling Nerfs",
                "Limit level to 99",
                true,
                "Voidling seems to be balanced around level 99 so if you play with a mod that uncaps the levels he gets way too powerful."
            );
            /*VoidlingNoScaleHPMulti = ConfigFileUNSORTED.Bind(
                "Optional Voidling Nerfs",
                "No extra extra health in Multiplayer",
                false,
                "Should Voidlings special scaling scale extra hard in Multiplayer.\nNormally Mithrix and Voidling get more special scaling the more players there are. Twisted Scavs don't seem to do that but that may be a bug."
            );*/
        }

    }
}