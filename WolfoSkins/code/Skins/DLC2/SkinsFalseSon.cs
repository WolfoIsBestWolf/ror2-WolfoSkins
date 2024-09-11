using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsFalseSon
    {
        internal static void Start()
        {
            LanguageAPI.Add("SIMU_SKIN_FALSESON", "Autumn");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_FALSESON_NAME", "False Son: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_FALSESON_DESCRIPTION", "As False Son" + Unlocks.unlockCondition);

            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_FALSESON_NAME";
            unlockableDef.cachedName = "Skins.FalseSon.Wolfo";
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            //
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
 
        }

        [RegisterAchievement("SIMU_SKIN_FALSESON", "Skins.FalseSon.Wolfo", null, 5, null)]
        public class ClearSimulacrumFalseSonBody : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("FalseSonBody");
            }
        }

    }
}