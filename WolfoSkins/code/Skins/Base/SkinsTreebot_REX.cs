using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsTreebot_REX
    {
        internal static void Start()
        {
            TreebotSkin_Blue();
            TreebotSkin2();
            Treebot_AltColossus();
        }

        internal static void Treebot_AltColossus()
        {
            SkinDef GreenFlowerRex = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef skinTreebotAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Treebot/skinTreebotAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[5]; //Specific 5
            System.Array.Copy(skinTreebotAltColossus.rendererInfos, NewRenderInfos, skinTreebotAltColossus.rendererInfos.Length);

            Material matTreebotColossus = Object.Instantiate(skinTreebotAltColossus.rendererInfos[2].defaultMaterial);
            Material Vines = Object.Instantiate(GreenFlowerRex.rendererInfos[1].defaultMaterial);


            Texture2D texTreebotColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/Colossus/texTreebotColossusDiffuse.png");
            texTreebotColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotFlowerDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/Colossus/texTreebotFlowerDiffuse.png");
            texTreebotFlowerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampDroneFire = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/Colossus/texRampDroneFire.png");
            texRampDroneFire.wrapMode = TextureWrapMode.Clamp;

            matTreebotColossus.mainTexture = texTreebotColossusDiffuse;
            matTreebotColossus.SetColor("_EmColor", new Color(1f, 1f, 0.74f,1f)); //0.7379 0.9717 0.9458 1
            matTreebotColossus.SetFloat("_FresnelBoost", 2.4f);
            matTreebotColossus.SetTexture("_FresnelRamp", texRampDroneFire);
            Vines.mainTexture = texTreebotFlowerDiffuse;

            NewRenderInfos[0].defaultMaterial = matTreebotColossus;
            NewRenderInfos[1].defaultMaterial = matTreebotColossus;
            NewRenderInfos[2].defaultMaterial = matTreebotColossus;
            NewRenderInfos[3].defaultMaterial = matTreebotColossus;
            NewRenderInfos[4] = new CharacterModel.RendererInfo
            {
                defaultMaterial = Vines,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off,
                renderer = skinTreebotAltColossus.rootObject.transform.GetChild(5).GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>()
            };
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinTreebotAltColossusWolfo_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_TREEBOT_COLOSSUS";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/Colossus/REX.png"));
            newSkinDef.baseSkins = skinTreebotAltColossus.baseSkins;
            newSkinDef.meshReplacements = skinTreebotAltColossus.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinTreebotAltColossus.rootObject;


            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody"), newSkinDef);

        }

        internal static void TreebotSkin_Blue()
        {
            SkinDef GreenFlowerRex = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];

            CharacterModel.RendererInfo[] REXBlueRenderInfos = new CharacterModel.RendererInfo[4];
            System.Array.Copy(GreenFlowerRex.rendererInfos, REXBlueRenderInfos, 4);

            Material matREXBlueRobot = Object.Instantiate(GreenFlowerRex.rendererInfos[0].defaultMaterial);
            Material matREXBlueFlower = Object.Instantiate(GreenFlowerRex.rendererInfos[1].defaultMaterial);
            Material matREXBlueLeaf = Object.Instantiate(GreenFlowerRex.rendererInfos[2].defaultMaterial);
            Material matREXBlueBark = Object.Instantiate(GreenFlowerRex.rendererInfos[3].defaultMaterial);

            //Blue Tree Bot
            Texture2D texTreebotBlueFlowerDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/blue/texTreebotBlueFlowerDiffuse.png");
            texTreebotBlueFlowerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueLeafDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/blue/texTreebotBlueLeafDiffuse.png");
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueTreeBarkDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/blue/texTreebotBlueTreeBarkDiffuse.png");
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Repeat;

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

            SkinDefInfo BlueFlowerRexInfo = new SkinDefInfo
            {
                BaseSkins = GreenFlowerRex.baseSkins,
                MeshReplacements = GreenFlowerRex.meshReplacements,
                NameToken = "SIMU_SKIN_TREEBOT",
                RootObject = GreenFlowerRex.rootObject,
                RendererInfos = REXBlueRenderInfos,
                Name = "skinTreebotWolfo_Simu",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/blue/texTreebotBlueSkinIcon.png")),
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody"), BlueFlowerRexInfo);
        }

        internal static void TreebotSkin2()
        {
            SkinDef GreenFlowerRex = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];

            CharacterModel.RendererInfo[] REXBlueRenderInfos = new CharacterModel.RendererInfo[4];
            System.Array.Copy(GreenFlowerRex.rendererInfos, REXBlueRenderInfos, 4);

            Material matTreebotMetal = Object.Instantiate(GreenFlowerRex.rendererInfos[0].defaultMaterial);
            Material matTreebotTreeFlower = Object.Instantiate(GreenFlowerRex.rendererInfos[1].defaultMaterial);
            Material matTreebotTreeLeaf = Object.Instantiate(GreenFlowerRex.rendererInfos[2].defaultMaterial);
            Material matREXBlueBark = Object.Instantiate(GreenFlowerRex.rendererInfos[3].defaultMaterial);


            //Red Tree Bot
            Texture2D texTreebotBlueFlowerDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/red/texTreebotBlueFlowerDiffuseINV.png");
            texTreebotBlueFlowerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueLeafDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/red/texTreebotBlueLeafDiffuseINV.png");
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTreebotBlueTreeBarkDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/red/texTreebotBlueTreeBarkDiffuseINV.png");
            texTreebotBlueLeafDiffuse.wrapMode = TextureWrapMode.Repeat;

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
 
            //
            SkinDefInfo BlueFlowerRexInfo = new SkinDefInfo
            {
                BaseSkins = GreenFlowerRex.baseSkins,
                MeshReplacements = GreenFlowerRex.meshReplacements,
                NameToken = "SIMU_SKIN_TREEBOT_ALT",
                RootObject = GreenFlowerRex.rootObject,
                RendererInfos = REXBlueRenderInfos,
                Name = "skinTreebotWolfoInv_Simu",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/red/texTreebotBlueSkinIconINV.png")),
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/TreebotBody"), BlueFlowerRexInfo);
        }

        [RegisterAchievement("CLEAR_ANY_TREEBOT", "Skins.Treebot.Wolfo.First", "RescueTreebot", 5, null)]
        public class ClearSimulacrumTreebotBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TreebotBody");
            }
        }


        [RegisterAchievement("CLEAR_BOTH_TREEBOT", "Skins.Treebot.Wolfo.Both", "RescueTreebot", 5, null)]
        public class ClearSimulacrumTreebotBody2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TreebotBody");
            }
        }

    }
}