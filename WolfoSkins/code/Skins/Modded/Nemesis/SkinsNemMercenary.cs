using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsNemMercenary
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            //NemMercenaryBody
            LanguageAPI.Add("SIMU_SKIN_NEM_MERCENARY", "Red");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_NEM_MERCENARY_NAME", "Nemesis Mercenary: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_NEM_MERCENARY_DESCRIPTION", "As Nemesis Mercenary" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_NEM_MERCENARY_NAME";
            unlockableDef.cachedName = "Skins.NemesisMercenary.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);

        }

        internal static void ModdedSkin(GameObject BodyObject)
        {
            Debug.Log("Nemesis Mercenary Skins");
            //unlockableDef.hidden = false;
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }


            BodyIndex CharacterIndex = BodyObject.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = BodyObject.GetComponentInChildren<ModelSkinController>();
            SkinDef skinDefault = modelSkinController.skins[0];
        }

        [RegisterAchievement("SIMU_SKIN_NEM_MERCENARY", "Skins.NemesisMercenary.Wolfo", null, 5, null)]
        public class ClearSimulacrumNemesisMercenary : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("NemMercBody");
            }
        }

    }
}