using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsTreebot_REX
    {
        internal static void Start()
        {
            SkinDef skinTreebotAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Treebot/skinTreebotAlt.asset").WaitForCompletion();
            SkinDef skinTreebotAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Treebot/skinTreebotAltColossus.asset").WaitForCompletion();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinTreebotLepton_1",
                nameToken = "SIMU_SKIN_TREEBOT",
                icon = H.GetIcon("base/rex_blue"),
                original = skinTreebotAlt,
                extraRenders = 1
            }, new System.Action<SkinDefMakeOnApply>(Default_BlueWhite));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinTreebotFire_1",
                nameToken = "SIMU_SKIN_TREEBOT_ALT",
                icon = H.GetIcon("base/rex_red"),
                original = skinTreebotAlt,
                extraRenders = 1
            }, new System.Action<SkinDefMakeOnApply>(Default_FireBlack));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinTreebotAltColossus_DLC2",
                nameToken = "SIMU_SKIN_TREEBOT_COLOSSUS",
                icon = H.GetIcon("base/rex_dlc2"),
                original = skinTreebotAltColossus,
                extraRenders = 1
            }, new System.Action<SkinDefMakeOnApply>(Colossus_Orange));

        }

        internal static void Colossus_Orange(SkinDefMakeOnApply newSkinDef)
        {
            SkinDefParams skinTreebotAlt = Addressables.LoadAssetAsync<SkinDefParams>(key: "RoR2/Base/Treebot/skinTreebotAltColossus_params.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matTreebotColossus = CloneMat(ref newRenderInfos, 2);
            Material Vines = CloneFromOriginal(skinTreebotAlt, 1);

            matTreebotColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/Colossus/texTreebotColossusDiffuse.png");
            matTreebotColossus.SetColor("_EmColor", new Color(1f, 1f, 0.74f, 1f)); //0.7379 0.9717 0.9458 1
            matTreebotColossus.SetFloat("_FresnelBoost", 2.4f);
            matTreebotColossus.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/Colossus/texRampDroneFire.png"));
            Vines.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/Colossus/texTreebotFlowerDiffuse.png");

            newRenderInfos[0].defaultMaterial = matTreebotColossus;
            newRenderInfos[1].defaultMaterial = matTreebotColossus;
            newRenderInfos[2].defaultMaterial = matTreebotColossus;
            newRenderInfos[3].defaultMaterial = matTreebotColossus;
            newRenderInfos[4] = new CharacterModel.RendererInfo
            {
                defaultMaterial = Vines,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off,
                renderer = newSkinDef.rootObject.transform.GetChild(5).GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>()
            };
        }

        internal static void Default_BlueWhite(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matREXBlueRobot = CloneMat(ref newRenderInfos, 0);
            Material matREXBlueFlower = CloneMat(ref newRenderInfos, 1);
            Material matREXBlueLeaf = CloneMat(ref newRenderInfos, 2);
            Material matREXBlueBark = CloneMat(ref newRenderInfos, 3);

            matREXBlueRobot.color = new Color(0.65f, 0.65f, 0.65f, 1);
            matREXBlueRobot.SetColor("_EmColor", new Color(0.7f, 0.7f, 1.4f, 1));

            matREXBlueFlower.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/blue/texTreebotBlueFlowerDiffuse.png");
            matREXBlueLeaf.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/blue/texTreebotBlueLeafDiffuse.png");
            //matREXBlueBark.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/blue/texTreebotBlueTreeBarkDiffuse.png");
            matREXBlueBark.color = new Color32(190, 175, 200, 255);

            newRenderInfos[0].defaultMaterial = matREXBlueRobot;
            newRenderInfos[1].defaultMaterial = matREXBlueFlower;
            newRenderInfos[2].defaultMaterial = matREXBlueLeaf;
            newRenderInfos[3].defaultMaterial = matREXBlueBark;
            newRenderInfos[4] = new CharacterModel.RendererInfo
            {
                defaultMaterial = matREXBlueFlower,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off,
                renderer = newSkinDef.rootObject.transform.GetChild(5).GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>()
            };

        }

        internal static void Default_FireBlack(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matTreebotMetal = CloneMat(ref newRenderInfos, 0);
            Material matTreebotTreeFlower = CloneMat(ref newRenderInfos, 1);
            Material matTreebotTreeLeaf = CloneMat(ref newRenderInfos, 2);
            Material matREXBlueBark = CloneMat(ref newRenderInfos, 3);

            matTreebotMetal.color = new Color(0.4f, 0.4f, 0.4f, 1);
            matTreebotMetal.SetColor("_EmColor", new Color(1.4f, 0.7f, 0.7f, 1));

            matTreebotTreeFlower.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/red/texTreebotBlueFlowerDiffuseINV.png");
            matTreebotTreeLeaf.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/red/texTreebotBlueLeafDiffuseINV.png");
            matREXBlueBark.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Treebot/red/texTreebotBlueTreeBarkDiffuseINV.png");
            matREXBlueBark.color = new Color32(200, 175, 190, 255);

            newRenderInfos[0].defaultMaterial = matTreebotMetal;
            newRenderInfos[1].defaultMaterial = matTreebotTreeFlower;
            newRenderInfos[2].defaultMaterial = matTreebotTreeLeaf;
            newRenderInfos[3].defaultMaterial = matREXBlueBark;
            newRenderInfos[4] = new CharacterModel.RendererInfo
            {
                defaultMaterial = matTreebotTreeFlower,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off,
                renderer = newSkinDef.rootObject.transform.GetChild(5).GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>()
            };
        }

        [RegisterAchievement("CLEAR_ANY_TREEBOT", "Skins.Treebot.Wolfo.First", "RescueTreebot", 3, null)]
        public class ClearSimulacrumTreebotBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TreebotBody");
            }
        }


        [RegisterAchievement("CLEAR_BOTH_TREEBOT", "Skins.Treebot.Wolfo.Both", "RescueTreebot", 3, null)]
        public class ClearSimulacrumTreebotBody2 : Achievement_TWO_THINGS
        {

            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TreebotBody");
            }
        }

    }
}