using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsRavager
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_RAVAGER", "Foolish");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_RAVAGER_NAME", "Ravager: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_RAVAGER_DESCRIPTION", "As Ravager" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_RAVAGER_NAME";
            unlockableDef.cachedName = "Skins.Ravager.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinRavagerIcon);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
           
        }

        internal static void ModdedSkin(GameObject RavagerBody)
        {
            Debug.Log("Ravager Skins");
            unlockableDef.hidden = false;
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            BodyIndex RavagerIndex = RavagerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RavagerBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            SkinDef skinRavager = modelSkinController.skins[0];

            //0 matRavager
            //1 matRavager
            //2 matRavager
            
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRavager.rendererInfos.Length];
            System.Array.Copy(skinRavager.rendererInfos, NewRenderInfos, skinRavager.rendererInfos.Length);

            Material matBody = Object.Instantiate(skinRavager.rendererInfos[0].defaultMaterial);
            Material matSword = Object.Instantiate(skinRavager.rendererInfos[1].defaultMaterial);
            Material matImpBoss = Object.Instantiate(skinRavager.rendererInfos[2].defaultMaterial);
            
            Texture2D texBody = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBody.LoadImage(Properties.Resources.RAVAGERtexBody, true);
            texBody.filterMode = FilterMode.Bilinear;
            texBody.wrapMode = TextureWrapMode.Repeat;

            Texture2D texBodyEmission = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBodyEmission.LoadImage(Properties.Resources.RAVAGERtexBodyEmission, true);
            texBodyEmission.filterMode = FilterMode.Bilinear;
            texBodyEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSword = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texSword.LoadImage(Properties.Resources.RAVAGERtexSword, true);
            texSword.filterMode = FilterMode.Bilinear;
            texSword.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSwordEmission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texSwordEmission.LoadImage(Properties.Resources.RAVAGERtexSwordEmission, true);
            texSwordEmission.filterMode = FilterMode.Bilinear;
            texSwordEmission.wrapMode = TextureWrapMode.Repeat;

            matBody.mainTexture = texBody;
            matBody.SetTexture("_EmTex", texBodyEmission);

            matSword.mainTexture = texSword;
            matSword.SetTexture("_EmTex", texSwordEmission);

            matImpBoss.color = new Color(1f, 0.8f, 0.7f); //0.5 0.5 0.4 1
            matImpBoss.DisableKeyword("FORCE_SPEC");
            matImpBoss.SetColor("_EmColor", Color.red);

            NewRenderInfos[0].defaultMaterial = matBody;
            NewRenderInfos[1].defaultMaterial = matSword;
            NewRenderInfos[2].defaultMaterial = matImpBoss;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinRavagerIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinRavagerWolfo",
                NameToken = "SIMU_SKIN_RAVAGER",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinRavager },
                RootObject = skinRavager.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinRavager.meshReplacements,
                GameObjectActivations = skinRavager.gameObjectActivations,
                ProjectileGhostReplacements = skinRavager.projectileGhostReplacements,
            };
            SkinDef RavagerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);


            modelSkinController.skins = modelSkinController.skins.Add(RavagerSkinDefNew);
            BodyCatalog.skins[(int)RavagerIndex] = BodyCatalog.skins[(int)RavagerIndex].Add(RavagerSkinDefNew);
        }

        [RegisterAchievement("SIMU_SKIN_RAVAGER", "Skins.Ravager.Wolfo", null, 5, null)]
        public class ClearSimulacrumRobRavager : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RobRavagerBody");
            }
        }

    }
}