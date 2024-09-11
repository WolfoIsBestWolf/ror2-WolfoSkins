using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsNemEnforcer
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            //NemEnforcerBody
            LanguageAPI.Add("SIMU_SKIN_NEM_ENFORCER", "Reverted");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_NEM_ENFORCER_NAME", "Nemesis Enforcer: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_NEM_ENFORCER_DESCRIPTION", "As Nemesis Enforcer" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_NEM_ENFORCER_NAME";
            unlockableDef.cachedName = "Skins.NemesisEnforcer.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinIconNemEnforcer);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);

        }

        internal static void ModdedSkin(GameObject BodyObject)
        {
            Debug.Log("Nemesis Enforcer Skins");
            unlockableDef.hidden = false;
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            BodyIndex CharacterIndex = BodyObject.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = BodyObject.GetComponentInChildren<ModelSkinController>();
            SkinDef skinDefault = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinDefault.rendererInfos.Length];
            System.Array.Copy(skinDefault.rendererInfos, NewRenderInfos, skinDefault.rendererInfos.Length);

            Material matNemforcer = Object.Instantiate(skinDefault.rendererInfos[0].defaultMaterial);

            Texture2D texNemforcer = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texNemforcer.LoadImage(Properties.Resources.texNemforcer, true);
            texNemforcer.filterMode = FilterMode.Bilinear;
            texNemforcer.wrapMode = TextureWrapMode.Repeat;

            matNemforcer.mainTexture = texNemforcer;

            NewRenderInfos[0].defaultMaterial = matNemforcer;
            //NewRenderInfos[1].defaultMaterial = ;
            //NewRenderInfos[2].defaultMaterial = ;
            //NewRenderInfos[3].defaultMaterial = ;
            NewRenderInfos[4].defaultMaterial = matNemforcer;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconNemEnforcer, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinNemesisEnforcerWolfo",
                NameToken = "SIMU_SKIN_NEM_ENFORCER",
                Icon = SkinIconS,
                BaseSkins = skinDefault.baseSkins,
                RootObject = skinDefault.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinDefault.meshReplacements,
                GameObjectActivations = skinDefault.gameObjectActivations,
            };

            SkinDef NewSkinDef = Skins.CreateNewSkinDef(SkinInfo);
            modelSkinController.skins = modelSkinController.skins.Add(NewSkinDef);
            BodyCatalog.skins[(int)CharacterIndex] = BodyCatalog.skins[(int)CharacterIndex].Add(NewSkinDef);
        }

        [RegisterAchievement("SIMU_SKIN_NEM_ENFORCER", "Skins.NemesisEnforcer.Wolfo", null, 5, null)]
        public class ClearSimulacrumNemesisEnforcer : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("NemesisEnforcerBody");
            }
        }

    }
}