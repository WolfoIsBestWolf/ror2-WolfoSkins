using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsMiner
    {
        private static UnlockableDef unlockableDef;
        private static UnlockableDef unlockableDefPRISM;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_MINER", "Nether");
            LanguageAPI.Add("SIMU_SKIN_MINER_STUPID", "Emerald");
            LanguageAPI.Add("SIMU_SKIN_MINER_RED", "Adamantite");
            LanguageAPI.Add("SIMU_SKIN_MINER_ORANGE", "Palladium");
            LanguageAPI.Add("SIMU_SKIN_MINER_GREEN", "Mythril");
            LanguageAPI.Add("SIMU_SKIN_MINER_BLUE", "Cobalt");
            LanguageAPI.Add("SIMU_SKIN_MINER_PINK", "Orichalcum");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_MINER_NAME", "Miner: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_MINER_DESCRIPTION", "As Miner" + Unlocks.unlockCondition);

            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_MINER_NAME", "Miner" + Unlocks.unlockNamePrism);
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_MINER_DESCRIPTION", "As Miner" + Unlocks.unlockConditionPrism);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_MINER_NAME";
            unlockableDef.cachedName = "Skins.Miner.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinIconMiner);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            unlockableDefPRISM = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDefPRISM.nameToken = "ACHIEVEMENT_PRISM_SKIN_MINER_NAME";
            unlockableDefPRISM.cachedName = "Skins.Miner.Wolfo.Prism";
            unlockableDefPRISM.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDefPRISM);
            unlockableDefPRISM.achievementIcon = WRect.MakeIcon(Properties.Resources.skinIconMinerDiamond);

            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
        }

        internal static void ModdedSkin(GameObject MinerBody)
        {
            Debug.Log("Miner Skins");
            unlockableDef.hidden = false;

            //Find Diamond skin and add it
            //Sort Blacksmith skin earlier
            SkinDef skinDef1 = SkinsBlack(MinerBody);
            SkinDef skinDef2 = SkinsGold(MinerBody); //Couldn't find Diamond skin so guess we'll just make it again lol
            SkinDef skinDef3 = SkinsEmerald(MinerBody); //Couldn't find Diamond skin so guess we'll just make it again lol

            ModelSkinController modelSkinController = MinerBody.GetComponentInChildren<ModelSkinController>();
            BodyIndex MinerIndex = MinerBody.GetComponent<CharacterBody>().bodyIndex;
            //SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length + 2];
            List<SkinDef> skinsNew = new List<SkinDef>();

            skinsNew.Add(modelSkinController.skins[0]); //D
            skinsNew.Add(modelSkinController.skins[1]); //M
            skinsNew.Add(modelSkinController.skins[2]); //GM
            skinsNew.Add(modelSkinController.skins[5]); //BlackSmith
            skinsNew.Add(skinDef1); //Black
            skinsNew.Add(skinDef2); //Colorful
            skinsNew.Add(skinDef3); //Colorful

            for (int i = 3; i < modelSkinController.skins.Length; i++)
            {
                if (!skinsNew.Contains(modelSkinController.skins[i]))
                {
                    skinsNew.Add(modelSkinController.skins[i]);
                }
            }

            SkinDef[] array = skinsNew.ToArray();
            modelSkinController.skins = array;
            BodyCatalog.skins[(int)MinerIndex] = array;
        }

        internal static SkinDef SkinsBlack(GameObject MinerBody)
        {       
            //BodyIndex MinerIndex = MinerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = MinerBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinMinerMolten = modelSkinController.skins[1];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMinerMolten.rendererInfos.Length];
            System.Array.Copy(skinMinerMolten.rendererInfos, NewRenderInfos, skinMinerMolten.rendererInfos.Length);

            Material MatMinerBody = Object.Instantiate(skinMinerMolten.rendererInfos[0].defaultMaterial);
            MatMinerBody.name = "MatMinerBody";

            Texture2D texMinerMolten = new Texture2D(4, 3, TextureFormat.RGB24, false);
            texMinerMolten.LoadImage(Properties.Resources.texMinerMolten, true);
            texMinerMolten.filterMode = FilterMode.Point;
            texMinerMolten.wrapMode = TextureWrapMode.Repeat;

            Texture2D texMinerMoltenEmission = new Texture2D(4, 3, TextureFormat.RGB24, false);
            texMinerMoltenEmission.LoadImage(Properties.Resources.texMinerMoltenEmission, true);
            texMinerMoltenEmission.filterMode = FilterMode.Point;
            texMinerMoltenEmission.wrapMode = TextureWrapMode.Repeat;

            MatMinerBody.mainTexture = texMinerMolten;
            MatMinerBody.SetTexture("_EmTex", texMinerMoltenEmission);
            MatMinerBody.SetColor("_EmColor", new Color(1, 1, 0, 1));

            NewRenderInfos[0].defaultMaterial = MatMinerBody;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconMiner, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinMinerWolfo_Black",
                NameToken = "SIMU_SKIN_MINER",
                Icon = SkinIconS,
                BaseSkins = skinMinerMolten.baseSkins,
                RootObject = skinMinerMolten.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinMinerMolten.meshReplacements,
                GameObjectActivations = skinMinerMolten.gameObjectActivations,
                ProjectileGhostReplacements = skinMinerMolten.projectileGhostReplacements,
            };
            return Skins.CreateNewSkinDef(SkinInfo);
            /*
            SkinDef MinerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);
            modelSkinController.skins = modelSkinController.skins.Add(MinerSkinDefNew);
            BodyCatalog.skins[(int)MinerIndex] = BodyCatalog.skins[(int)MinerIndex].Add(MinerSkinDefNew);*/
        }

        internal static SkinDef SkinsEmerald(GameObject MinerBody)
        {
            //BodyIndex MinerIndex = MinerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = MinerBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinMinerDefault = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMinerDefault.rendererInfos.Length];
            System.Array.Copy(skinMinerDefault.rendererInfos, NewRenderInfos, skinMinerDefault.rendererInfos.Length);

            Material MatMinerBody = Object.Instantiate(skinMinerDefault.rendererInfos[0].defaultMaterial);

            Texture2D texMinerMolten = new Texture2D(4, 3, TextureFormat.RGB24, false);
            texMinerMolten.LoadImage(Properties.Resources.texMinerDiamond, true);
            texMinerMolten.filterMode = FilterMode.Point;
            texMinerMolten.wrapMode = TextureWrapMode.Repeat;

            Texture2D texMinerMoltenEmission = new Texture2D(4, 3, TextureFormat.RGB24, false);
            texMinerMoltenEmission.LoadImage(Properties.Resources.texMinerDiamondEmission, true);
            texMinerMoltenEmission.filterMode = FilterMode.Point;
            texMinerMoltenEmission.wrapMode = TextureWrapMode.Repeat;

            MatMinerBody.mainTexture = texMinerMolten;
            MatMinerBody.SetTexture("_EmTex", texMinerMoltenEmission);
            MatMinerBody.SetFloat("_EmPower", 0.75f);
            MatMinerBody.color = new Color(0.6f, 1, 0.6f);

            NewRenderInfos[0].defaultMaterial = MatMinerBody;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconMinerDiamond, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinMiner_Stupid1",
                NameToken = "SIMU_SKIN_MINER_STUPID",
                Icon = SkinIconS,
                BaseSkins = skinMinerDefault.baseSkins,
                RootObject = skinMinerDefault.rootObject,
                UnlockableDef = unlockableDefPRISM,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinMinerDefault.meshReplacements,
                GameObjectActivations = skinMinerDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinMinerDefault.projectileGhostReplacements,
            };
            return Skins.CreateNewSkinDef(SkinInfo);
            /*
            SkinDef MinerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);
            modelSkinController.skins = modelSkinController.skins.Add(MinerSkinDefNew);
            BodyCatalog.skins[(int)MinerIndex] = BodyCatalog.skins[(int)MinerIndex].Add(MinerSkinDefNew);*/
        }

        internal static SkinDef SkinsGold(GameObject MinerBody)
        {
            //BodyIndex MinerIndex = MinerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = MinerBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinMinerDefault = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMinerDefault.rendererInfos.Length];
            System.Array.Copy(skinMinerDefault.rendererInfos, NewRenderInfos, skinMinerDefault.rendererInfos.Length);

            Material MatMinerBody = Object.Instantiate(skinMinerDefault.rendererInfos[0].defaultMaterial);

            Texture2D texMinerMolten = new Texture2D(4, 3, TextureFormat.RGB24, false);
            texMinerMolten.LoadImage(Properties.Resources.texMinerGold, true);
            texMinerMolten.filterMode = FilterMode.Point;
            texMinerMolten.wrapMode = TextureWrapMode.Repeat;

            Texture2D texMinerMoltenEmission = new Texture2D(4, 3, TextureFormat.RGB24, false);
            texMinerMoltenEmission.LoadImage(Properties.Resources.texMinerGoldEmission, true);
            texMinerMoltenEmission.filterMode = FilterMode.Point;
            texMinerMoltenEmission.wrapMode = TextureWrapMode.Repeat;

            MatMinerBody.mainTexture = texMinerMolten;
            MatMinerBody.SetTexture("_EmTex", texMinerMoltenEmission);
            MatMinerBody.SetFloat("_EmPower", 1f);
            MatMinerBody.color = new Color(1f, 0.6f, 0.6f);
            MatMinerBody.SetColor("_EmColor", new Color(0.9569f, 0.7176f, 0.7176f, 1f));

            NewRenderInfos[0].defaultMaterial = MatMinerBody;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconMinerGold, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinMiner_Gold2",
                NameToken = "SIMU_SKIN_MINER_ORANGE",
                Icon = SkinIconS,
                BaseSkins = skinMinerDefault.baseSkins,
                RootObject = skinMinerDefault.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinMinerDefault.meshReplacements,
                GameObjectActivations = skinMinerDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinMinerDefault.projectileGhostReplacements,
            };
            return Skins.CreateNewSkinDef(SkinInfo);
            /*
            SkinDef MinerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);
            modelSkinController.skins = modelSkinController.skins.Add(MinerSkinDefNew);
            BodyCatalog.skins[(int)MinerIndex] = BodyCatalog.skins[(int)MinerIndex].Add(MinerSkinDefNew);*/
        }


        [RegisterAchievement("SIMU_SKIN_MINER", "Skins.Miner.Wolfo", null, null)]
        public class ClearSimulacrumMiner : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MinerBody");
            }
        }

        [RegisterAchievement("PRISM_SKIN_MINER", "Skins.Miner.Wolfo.Prism", null, null)]
        public class AchievementPrismaticDissoMinerBody : AchievementPrismaticDisso
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MinerBody");
            }
        }
    }
}