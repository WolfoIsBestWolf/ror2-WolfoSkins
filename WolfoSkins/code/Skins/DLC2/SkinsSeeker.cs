using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsSeeker
    {
        internal static void Start()
        {
            LanguageAPI.Add("SIMU_SKIN_SEEKER", "Autumn");


            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_SEEKER_NAME", "Seeker: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_SEEKER_DESCRIPTION", "As Seeker" + Unlocks.unlockCondition);

            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_SEEKER_NAME";
            unlockableDef.cachedName = "Skins.Seeker.Wolfo";
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            //
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
 
        }

        [RegisterAchievement("SIMU_SKIN_SEEKER", "Skins.Seeker.Wolfo", null, 5, null)]
        public class ClearSimulacrumSeekerBody : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("SeekerBody");
            }
        }

    }
}