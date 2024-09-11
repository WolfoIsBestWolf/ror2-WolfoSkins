using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsChef
    {
        internal static void Start()
        {
            LanguageAPI.Add("SIMU_SKIN_CHEF", "Autumn");


            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_NAME", "Chef: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_DESCRIPTION", "As Chef" + Unlocks.unlockCondition);

            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CHEF_NAME";
            unlockableDef.cachedName = "Skins.Chef.Wolfo";
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            //
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
 
        }

        [RegisterAchievement("SIMU_SKIN_CHEF", "Skins.Chef.Wolfo", null, 5, null)]
        public class ClearSimulacrumChefBody : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChefBody");
            }
        }

    }
}