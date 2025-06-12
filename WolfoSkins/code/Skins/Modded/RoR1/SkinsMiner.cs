using RoR2;
using System.Collections.Generic;
using UnityEngine;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod
{
    public class SkinsMiner
    {
        internal static void ModdedSkin(GameObject MinerBody)
        {
            Debug.Log("Miner Skins");

            //Find Diamond skin and add it
            //Sort Blacksmith skin earlier
            ModelSkinController modelSkinController = MinerBody.GetComponentInChildren<ModelSkinController>();
            BodyIndex MinerIndex = MinerBody.GetComponent<CharacterBody>().bodyIndex;

            SkinDef skinMinerDefault = modelSkinController.skins[0];
            SkinDef skinMinerMolten = modelSkinController.skins[1];
            SkinDef skinMinerBlacksmith = modelSkinController.skins[5];

            SkinDef skinDef1 = SkinsBlack(skinMinerMolten);
            SkinDef skinDef2 = SkinsGold(skinMinerBlacksmith); //Couldn't find Diamond skin so guess we'll just make it again lol
            SkinDef skinDef3 = SkinsEmerald(skinMinerDefault); //Couldn't find Diamond skin so guess we'll just make it again lol


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
            SkinCatalog.skinsByBody[(int)MinerIndex] = array;
        }

        internal static SkinDef SkinsBlack(SkinDef skinMinerMolten)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinMiner_Black_1",
                nameToken = "SIMU_SKIN_MINER",
                icon = H.GetIcon("mod/ror1/miner_black"),
                original = skinMinerMolten,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material MatMinerBody = CloneMat(newRenderInfos, 0);
            MatMinerBody.name = "MatMinerBody";

            MatMinerBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerMolten.png");
            MatMinerBody.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerMoltenEmission.png"));
            MatMinerBody.SetColor("_EmColor", new Color(1, 1, 0, 1));
            MatMinerBody.SetFloat("_EmPower", 4);

            return newSkinDef;
        }

        internal static SkinDef SkinsEmerald(SkinDef skinMinerDefault)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinMiner_Emerald_1",
                nameToken = "SIMU_SKIN_MINER_EMERALD",
                icon = H.GetIcon("mod/ror1/miner_green"),
                original = skinMinerDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material MatMinerBody = CloneMat(newRenderInfos, 0);
            MatMinerBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerDiamond.png");
            MatMinerBody.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerDiamondEmission.png"));
            MatMinerBody.SetFloat("_EmPower", 0.75f);
            MatMinerBody.color = new Color(0.6f, 1, 0.6f);

            return newSkinDef;
        }

        internal static SkinDef SkinsGold(SkinDef skinMinerBlacksmith)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinMiner_1",
                nameToken = "SIMU_SKIN_MINER_ORANGE",
                icon = H.GetIcon("mod/ror1/miner_orange"),
                original = skinMinerBlacksmith,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material MatMinerBody = CloneMat(newRenderInfos, 0);
            MatMinerBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerGold.png");
            MatMinerBody.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Miner/texMinerGoldEmission.png"));
            MatMinerBody.SetFloat("_EmPower", 1f);
            MatMinerBody.color = new Color(1f, 0.6f, 0.6f);
            MatMinerBody.SetColor("_EmColor", new Color(0.9569f, 0.7176f, 0.7176f, 1f));
            ;
            return newSkinDef;
        }


        [RegisterAchievement("CLEAR_ANY_MINER", "Skins.Miner.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumMiner : Achievement_ONE_THINGS
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