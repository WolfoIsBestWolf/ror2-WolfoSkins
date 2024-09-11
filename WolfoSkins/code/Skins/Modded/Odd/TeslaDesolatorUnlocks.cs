using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class TeslaDesolatorUnlocks
    {
        public static UnlockableDef unlockableDefTesla;
        public static UnlockableDef unlockableDefDesolator;
     
        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_TESLATROOPER_NAME", "Tesla Trooper: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_TESLATROOPER_DESCRIPTION", "As Tesla Trooper" + Unlocks.unlockCondition);

            unlockableDefTesla = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDefTesla.nameToken = "ACHIEVEMENT_SIMU_SKIN_TESLATROOPER_NAME";
            unlockableDefTesla.cachedName = "Skins.TeslaTrooper.Wolfo";
            unlockableDefTesla.achievementIcon = WRect.MakeIcon(Properties.Resources.skinIconTesla);
            unlockableDefTesla.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDefTesla);

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_DESOLATOR_NAME", "Desolator: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_DESOLATOR_DESCRIPTION", "As Desolator" + Unlocks.unlockCondition);

            unlockableDefDesolator = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDefDesolator.nameToken = "ACHIEVEMENT_SIMU_SKIN_DESOLATOR_NAME";
            unlockableDefDesolator.cachedName = "Skins.Desolator.Wolfo";
            unlockableDefDesolator.achievementIcon = WRect.MakeIcon(Properties.Resources.skinIconDesolator);
            unlockableDefDesolator.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDefDesolator);

        }

        [RegisterAchievement("SIMU_SKIN_TESLATROOPER", "Skins.TeslaTrooper.Wolfo", null, 5, null)]
        public class ClearSimulacrumTeslaTrooperClassic : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TeslaTrooperBody");
            }
        }

        [RegisterAchievement("SIMU_SKIN_DESOLATOR", "Skins.Desolator.Wolfo", null, 5, null)]
        public class ClearSimulacrumDesolatorClassic : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DesolatorBody");
            }
        }

    }
}