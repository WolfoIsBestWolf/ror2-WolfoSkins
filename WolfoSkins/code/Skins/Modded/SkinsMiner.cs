using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsMiner
    {
        internal static void ModdedSkin(GameObject MinerBody)
        {
            Debug.Log("Miner Skins");

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

            //RGB24
            Texture2D texMinerMolten = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerMolten.png");
            texMinerMolten.filterMode = FilterMode.Point;
            texMinerMolten.wrapMode = TextureWrapMode.Repeat;

            //RGB24
            Texture2D texMinerMoltenEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerMoltenEmission.png");
            texMinerMoltenEmission.filterMode = FilterMode.Point;
            texMinerMoltenEmission.wrapMode = TextureWrapMode.Repeat;

            MatMinerBody.mainTexture = texMinerMolten;
            MatMinerBody.SetTexture("_EmTex", texMinerMoltenEmission);
            MatMinerBody.SetColor("_EmColor", new Color(1, 1, 0, 1));
            MatMinerBody.SetFloat("_EmPower", 4);

            NewRenderInfos[0].defaultMaterial = MatMinerBody;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinMinerWolfo_Black_Simu",
                NameToken = "SIMU_SKIN_MINER",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/skinIconMiner.png")),
                BaseSkins = skinMinerMolten.baseSkins,
                RootObject = skinMinerMolten.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinMinerMolten.meshReplacements,
                GameObjectActivations = skinMinerMolten.gameObjectActivations,
                ProjectileGhostReplacements = skinMinerMolten.projectileGhostReplacements,
            };
            return Skins.CreateNewSkinDef(SkinInfo);
        }

        internal static SkinDef SkinsEmerald(GameObject MinerBody)
        {
            //BodyIndex MinerIndex = MinerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = MinerBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinMinerDefault = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMinerDefault.rendererInfos.Length];
            System.Array.Copy(skinMinerDefault.rendererInfos, NewRenderInfos, skinMinerDefault.rendererInfos.Length);

            Material MatMinerBody = Object.Instantiate(skinMinerDefault.rendererInfos[0].defaultMaterial);

            Texture2D texMinerMolten = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerDiamond.png");
            texMinerMolten.filterMode = FilterMode.Point;
            texMinerMolten.wrapMode = TextureWrapMode.Repeat;

            Texture2D texMinerMoltenEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerDiamondEmission.png");
            texMinerMoltenEmission.filterMode = FilterMode.Point;
            texMinerMoltenEmission.wrapMode = TextureWrapMode.Repeat;

            MatMinerBody.mainTexture = texMinerMolten;
            MatMinerBody.SetTexture("_EmTex", texMinerMoltenEmission);
            MatMinerBody.SetFloat("_EmPower", 0.75f);
            MatMinerBody.color = new Color(0.6f, 1, 0.6f);

            NewRenderInfos[0].defaultMaterial = MatMinerBody;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinMinerWolfo_Emerald_Simu",
                NameToken = "SIMU_SKIN_MINER_EMERALD",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/skinIconMinerDiamond.png")),
                BaseSkins = skinMinerDefault.baseSkins,
                RootObject = skinMinerDefault.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinMinerDefault.meshReplacements,
                GameObjectActivations = skinMinerDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinMinerDefault.projectileGhostReplacements,
            };
            return Skins.CreateNewSkinDef(SkinInfo);
        }

        internal static SkinDef SkinsGold(GameObject MinerBody)
        {
            //BodyIndex MinerIndex = MinerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = MinerBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinMinerDefault = modelSkinController.skins[5];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMinerDefault.rendererInfos.Length];
            System.Array.Copy(skinMinerDefault.rendererInfos, NewRenderInfos, skinMinerDefault.rendererInfos.Length);

            Material MatMinerBody = Object.Instantiate(skinMinerDefault.rendererInfos[0].defaultMaterial);

            Texture2D texMinerMolten = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerGold.png");
            texMinerMolten.filterMode = FilterMode.Point;
            texMinerMolten.wrapMode = TextureWrapMode.Repeat;

            Texture2D texMinerMoltenEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerGoldEmission.png");
            texMinerMoltenEmission.filterMode = FilterMode.Point;
            texMinerMoltenEmission.wrapMode = TextureWrapMode.Repeat;

            MatMinerBody.mainTexture = texMinerMolten;
            MatMinerBody.SetTexture("_EmTex", texMinerMoltenEmission);
            MatMinerBody.SetFloat("_EmPower", 1f);
            MatMinerBody.color = new Color(1f, 0.6f, 0.6f);
            MatMinerBody.SetColor("_EmColor", new Color(0.9569f, 0.7176f, 0.7176f, 1f));

            NewRenderInfos[0].defaultMaterial = MatMinerBody;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinMinerWolfo_Simu",
                NameToken = "SIMU_SKIN_MINER_ORANGE",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/skinIconMinerGold.png")),
                BaseSkins = skinMinerDefault.baseSkins,
                RootObject = skinMinerDefault.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinMinerDefault.meshReplacements,
                GameObjectActivations = skinMinerDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinMinerDefault.projectileGhostReplacements,
            };
            return Skins.CreateNewSkinDef(SkinInfo);
        }


        [RegisterAchievement("CLEAR_ANY_MINER", "Skins.Miner.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumMiner : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MinerBody");
            }
        }
        /*
        [RegisterAchievement("CLEAR_BOTH_MINER", "Skins.Miner.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumMiner2 : Achievement_AltBoss
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MinerBody");
            }
        }
        */
    }
}