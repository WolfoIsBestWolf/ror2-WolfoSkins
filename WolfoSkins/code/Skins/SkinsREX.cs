using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsREX
    {
        internal static void Start()
        {

        }


        internal static void TreebotSkin()
        {
            //Blue Tree Bot
            Texture2D texTreebotBlueFlowerDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texTreebotBlueFlowerDiffuse.LoadImage(Properties.Resources.texTreebotBlueFlowerDiffuse, true);
            texTreebotBlueFlowerDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueFlowerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueLeafDiffuse = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texTreebotBlueLeafDiffuse.LoadImage(Properties.Resources.texTreebotBlueLeafDiffuse, true);
            texTreebotBlueLeafDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueTreeBarkDiffuse = new Texture2D(256, 1024, TextureFormat.DXT5, false);
            texTreebotBlueTreeBarkDiffuse.LoadImage(Properties.Resources.texTreebotBlueTreeBarkDiffuse, true);
            texTreebotBlueTreeBarkDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texTreebotBlueSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texTreebotBlueSkinIcon.LoadImage(Properties.Resources.texTreebotBlueSkinIcon, true);
            texTreebotBlueSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texTreebotBlueSkinIconS = Sprite.Create(texTreebotBlueSkinIcon, WRect.rec128, WRect.half);

            SkinDef GreenFlowerRex = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];


            CharacterModel.RendererInfo[] REXBlueRenderInfos = new CharacterModel.RendererInfo[4];
            System.Array.Copy(GreenFlowerRex.rendererInfos, REXBlueRenderInfos, 4);

            Material matREXBlueRobot = Object.Instantiate(GreenFlowerRex.rendererInfos[0].defaultMaterial);
            Material matREXBlueFlower = Object.Instantiate(GreenFlowerRex.rendererInfos[1].defaultMaterial);
            Material matREXBlueLeaf = Object.Instantiate(GreenFlowerRex.rendererInfos[2].defaultMaterial);
            Material matREXBlueBark = Object.Instantiate(GreenFlowerRex.rendererInfos[3].defaultMaterial);

            matREXBlueRobot.color = new Color(0.65f, 0.65f, 0.65f, 1);
            matREXBlueRobot.SetColor("_EmColor", new Color(0.7f, 0.7f, 1.4f, 1));

            matREXBlueFlower.mainTexture = texTreebotBlueFlowerDiffuse;
            matREXBlueLeaf.mainTexture = texTreebotBlueLeafDiffuse;
            //matREXBlueBark.mainTexture = texTreebotBlueTreeBarkDiffuse;
            matREXBlueBark.color = new Color32(190, 175, 200, 255);

            REXBlueRenderInfos[0].defaultMaterial = matREXBlueRobot;
            REXBlueRenderInfos[1].defaultMaterial = matREXBlueFlower;
            REXBlueRenderInfos[2].defaultMaterial = matREXBlueLeaf;
            REXBlueRenderInfos[3].defaultMaterial = matREXBlueBark;
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_TREEBOT", "Lepton Lily");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_TREEBOT_NAME", "REX: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_TREEBOT_DESCRIPTION", "As REX"+ Unlocks.unlockCondition);

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_TREEBOT_NAME";
            unlockableDef.cachedName = "Skins.Treebot.Wolfo";
            unlockableDef.achievementIcon = texTreebotBlueSkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            //
            R2API.SkinDefInfo BlueFlowerRexInfo = new R2API.SkinDefInfo
            {
                BaseSkins = GreenFlowerRex.baseSkins,
                NameToken = "SIMU_SKIN_TREEBOT",
                UnlockableDef = unlockableDef,
                RootObject = GreenFlowerRex.rootObject,
                RendererInfos = REXBlueRenderInfos,
                Name = "skinTreebotWolfo",
                Icon = texTreebotBlueSkinIconS,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody"), BlueFlowerRexInfo);
            TreebotSkin2(unlockableDef);
        }

        internal static void TreebotSkin2(UnlockableDef unlockableDef)
        {
            //Blue Tree Bot
            Texture2D texTreebotBlueFlowerDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texTreebotBlueFlowerDiffuse.LoadImage(Properties.Resources.texTreebotBlueFlowerDiffuseINV, true);
            texTreebotBlueFlowerDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueFlowerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueLeafDiffuse = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texTreebotBlueLeafDiffuse.LoadImage(Properties.Resources.texTreebotBlueLeafDiffuseINV, true);
            texTreebotBlueLeafDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueTreeBarkDiffuse = new Texture2D(256, 1024, TextureFormat.DXT5, false);
            texTreebotBlueTreeBarkDiffuse.LoadImage(Properties.Resources.texTreebotBlueTreeBarkDiffuseINV, true);
            texTreebotBlueTreeBarkDiffuse.filterMode = FilterMode.Bilinear;
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texTreebotBlueSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texTreebotBlueSkinIcon.LoadImage(Properties.Resources.texTreebotBlueSkinIconINV, true);
            texTreebotBlueSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texTreebotBlueSkinIconS = Sprite.Create(texTreebotBlueSkinIcon, WRect.rec128, WRect.half);

            SkinDef GreenFlowerRex = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];


            CharacterModel.RendererInfo[] REXBlueRenderInfos = new CharacterModel.RendererInfo[4];
            System.Array.Copy(GreenFlowerRex.rendererInfos, REXBlueRenderInfos, 4);

            Material matTreebotMetal = Object.Instantiate(GreenFlowerRex.rendererInfos[0].defaultMaterial);
            Material matTreebotTreeFlower = Object.Instantiate(GreenFlowerRex.rendererInfos[1].defaultMaterial);
            Material matTreebotTreeLeaf = Object.Instantiate(GreenFlowerRex.rendererInfos[2].defaultMaterial);
            Material matREXBlueBark = Object.Instantiate(GreenFlowerRex.rendererInfos[3].defaultMaterial);

            matTreebotMetal.color = new Color(0.4f, 0.4f, 0.4f, 1);
            matTreebotMetal.SetColor("_EmColor", new Color(1.4f, 0.7f, 0.7f, 1));

            matTreebotTreeFlower.mainTexture = texTreebotBlueFlowerDiffuse;
            matTreebotTreeLeaf.mainTexture = texTreebotBlueLeafDiffuse;
            matREXBlueBark.mainTexture = texTreebotBlueTreeBarkDiffuse;
            matREXBlueBark.color = new Color32(200, 175, 190, 255);

            REXBlueRenderInfos[0].defaultMaterial = matTreebotMetal;
            REXBlueRenderInfos[1].defaultMaterial = matTreebotTreeFlower;
            REXBlueRenderInfos[2].defaultMaterial = matTreebotTreeLeaf;
            REXBlueRenderInfos[3].defaultMaterial = matREXBlueBark;
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_TREEBOT_ALT", "Midnight Lily");

            //
            R2API.SkinDefInfo BlueFlowerRexInfo = new R2API.SkinDefInfo
            {
                BaseSkins = GreenFlowerRex.baseSkins,
                NameToken = "SIMU_SKIN_TREEBOT_ALT",
                UnlockableDef = unlockableDef,
                RootObject = GreenFlowerRex.rootObject,
                RendererInfos = REXBlueRenderInfos,
                Name = "skinTreebotWolfoInv",
                Icon = texTreebotBlueSkinIconS,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody"), BlueFlowerRexInfo);
        }

        [RegisterAchievement("SIMU_SKIN_TREEBOT", "Skins.Treebot.Wolfo", "RescueTreebot", 5, null)]
        public class ClearSimulacrumTreebotBody : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TreebotBody");
            }
        }

        internal static void PrismAchievement()
        {
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_TREEBOT_NAME", "REX" + Unlocks.unlockNamePrism);
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_TREEBOT_DESCRIPTION", "As REX" + Unlocks.unlockConditionPrism);
            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_TREEBOT_NAME";
            unlockableDef.cachedName = "Skins.Treebot.Wolfo.Prism";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
        }

        /*[RegisterAchievement("PRISM_SKIN_TREEBOT", "Skins.Treebot.Wolfo.Prism", null, 5, null)]
        public class AchievementPrismaticDissoTreebot2Body : AchievementPrismaticDisso
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TreebotBody");
            }
        }*/
    }
}