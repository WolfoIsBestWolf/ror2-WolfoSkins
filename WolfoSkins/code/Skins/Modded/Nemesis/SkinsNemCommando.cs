using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsNemCommando
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            //NemCommandoBody
            LanguageAPI.Add("SIMU_SKIN_NEM_COMMANDO", "Orange");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_NEM_COMMANDO_NAME", "Nemesis Commando: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_NEM_COMMANDO_DESCRIPTION", "As Nemesis Commando" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_NEM_COMMANDO_NAME";
            unlockableDef.cachedName = "Skins.NemesisCommando.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);

        }

        internal static void ModdedSkin(GameObject BodyObject)
        {
            Debug.Log("Nemesis Commando Skins");

            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }

            //unlockableDef.hidden = false;
            BodyIndex CharacterIndex = BodyObject.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = BodyObject.GetComponentInChildren<ModelSkinController>();
            SkinDef skinDefault = modelSkinController.skins[0];
        }

        [RegisterAchievement("SIMU_SKIN_NEM_COMMANDO", "Skins.NemesisCommando.Wolfo", null, 5, null)]
        public class ClearSimulacrumNemesisCommando : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("NemCommandoBody");
            }
        }

    }
}