using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Mod
{
    public class SkinsPaladin
    {

        internal static void ModdedSkin(GameObject PaladinBody)
        {
            Debug.Log("Paladin Skins");

            BodyIndex CharacterIndex = PaladinBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = PaladinBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPaladinDefault = modelSkinController.skins[0];

            SkinDef yellow = ModdedSkinYellow(skinPaladinDefault);
            //SkinDef black = ModdedSkinBlack(PaladinBody);

            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length];
            skinsNew[0] = modelSkinController.skins[0]; //D
            skinsNew[1] = modelSkinController.skins[1]; //M
            skinsNew[2] = modelSkinController.skins[2]; //GM
            skinsNew[3] = modelSkinController.skins[3]; //NK
            skinsNew[4] = modelSkinController.skins[4]; //C
            skinsNew[5] = yellow;
            //skinsNew[6] = black;

            for (int i = 6; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i] = modelSkinController.skins[i-1];
            }
            modelSkinController.skins = skinsNew;
            SkinCatalog.skinsByBody[(int)CharacterIndex] = skinsNew;

            //0 matPaladinSword
            //1 matPaladin : CAPE
            //2 matPaladinNkuhana
            //3 matPaladinNkuhana
            //4 matPaladinGMSword
            //5 matPaladin

        }


        internal static SkinDef ModdedSkinYellow(SkinDef skinPaladinDefault)
        {
            SkinDefWolfo newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinPaladin_1",
                nameToken = "SIMU_SKIN_PALADIN",
                icon = H.GetIcon("mod/paladin_yellow"),
                original = skinPaladinDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matPaladin = CloneMat(newRenderInfos, 5);
            Material matPaladinSword = CloneMat(newRenderInfos, 0);

            matPaladin.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladin.png");
            matPaladin.SetColor("_EmColor", new Color(1.25f, 0.25f, 0.25f, 1f));
            matPaladinSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladinSword.png");
            matPaladinSword.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladinSwordEmission.png"));

            newRenderInfos[0].defaultMaterial = matPaladinSword;
            newRenderInfos[1].defaultMaterial = matPaladin;
            newRenderInfos[5].defaultMaterial = matPaladin;

            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1f,0f,0f),
                    color2 = new Color(1f,0.5f,0.5f,0f),
                    lightPath = "Armature/base/spine.001/spine.002/spine.003/spine.004/neck/head/EyeTrail/Trail",
                }
            };
            return newSkinDef;
        }

        internal static SkinDef ModdedSkinBlack(SkinDef skinPaladinDefault)
        {
            SkinDefWolfo newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinPaladin_1",
                nameToken = "SIMU_SKIN_PALADIN_BLACK",
                icon = H.GetIcon("mod/paladin_black"),
                original = skinPaladinDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matPaladin = CloneMat(newRenderInfos, 5);
            Material matPaladinSword = CloneMat(newRenderInfos, 0);


            matPaladin.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Black/texPaladin.png");
            matPaladin.SetColor("_EmColor", new Color(1.25f, 0.25f, 0.25f, 1f));
            matPaladinSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Black/texPaladinSword.png");
            matPaladinSword.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Black/texPaladinSwordEmission.png"));

            newRenderInfos[0].defaultMaterial = matPaladinSword;
            newRenderInfos[1].defaultMaterial = matPaladin;
            newRenderInfos[5].defaultMaterial = matPaladin;

            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1f,0f,0f),
                    color2 = new Color(1f,0.5f,0.5f,0f),
                    lightPath = "Armature/base/spine.001/spine.002/spine.003/spine.004/neck/head/EyeTrail/Trail",
                }
            };
            return newSkinDef;
        }


        [RegisterAchievement("CLEAR_ANY_ROBPALADIN", "Skins.RobPaladin.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumRobPaladinBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RobPaladinBody");
            }
        }

    }
}