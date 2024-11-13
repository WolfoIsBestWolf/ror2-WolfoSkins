using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsPaladin
    {

        internal static void ModdedSkin(GameObject PaladinBody)
        {
            Debug.Log("Paladin Skins");
            SkinDef yellow = ModdedSkinYellow(PaladinBody);
            //SkinDef black = ModdedSkinBlack(PaladinBody);


            BodyIndex CharacterIndex = PaladinBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = PaladinBody.GetComponentInChildren<ModelSkinController>();
            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length + 1];

            skinsNew[0] = modelSkinController.skins[0]; //D
            skinsNew[1] = modelSkinController.skins[1]; //M
            skinsNew[2] = modelSkinController.skins[2]; //GM
            skinsNew[3] = modelSkinController.skins[3]; //NK
            skinsNew[4] = modelSkinController.skins[4]; //C
            skinsNew[5] = yellow;
            //skinsNew[6] = black;

            for (int i = 5; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i + 1] = modelSkinController.skins[i];
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)CharacterIndex] = skinsNew;
        }


        internal static SkinDef ModdedSkinYellow(GameObject PaladinBody)
        {
            ModelSkinController modelSkinController = PaladinBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPaladinDefault = modelSkinController.skins[0];

            //0 matPaladinSword
            //1 matPaladin : CAPE
            //2 matPaladinNkuhana
            //3 matPaladinNkuhana
            //4 matPaladinGMSword
            //5 matPaladin

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinPaladinDefault.rendererInfos.Length];
            System.Array.Copy(skinPaladinDefault.rendererInfos, NewRenderInfos, skinPaladinDefault.rendererInfos.Length);

            Material matPaladin = Object.Instantiate(skinPaladinDefault.rendererInfos[5].defaultMaterial);
            Material matPaladinSword = Object.Instantiate(skinPaladinDefault.rendererInfos[0].defaultMaterial);

            Texture2D texPaladin = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladin.png");
            texPaladin.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSword = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladinSword.png");
            texPaladinSword.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSwordEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladinSwordEmission.png");
            texPaladinSwordEmission.wrapMode = TextureWrapMode.Repeat;


            matPaladin.mainTexture = texPaladin;
            matPaladin.SetColor("_EmColor", new Color(1.25f, 0.25f, 0.25f, 1f));
            matPaladinSword.mainTexture = texPaladinSword;
            matPaladinSword.SetTexture("_EmTex", texPaladinSwordEmission);

            NewRenderInfos[0].defaultMaterial = matPaladinSword;
            NewRenderInfos[1].defaultMaterial = matPaladin;
            NewRenderInfos[5].defaultMaterial = matPaladin;

            //mdlPaladin/Armature/spine/spine.001/spine.002/spine.003/spine.004/neck/head/EyeTrail/Trail

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinPaladinWolfo_Any";
            newSkinDef.nameToken = "SIMU_SKIN_PALADIN";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/skinIconPaladin.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinPaladinDefault };
            newSkinDef.rootObject = skinPaladinDefault.rootObject;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.meshReplacements = skinPaladinDefault.meshReplacements;
            newSkinDef.gameObjectActivations = skinPaladinDefault.gameObjectActivations;
            newSkinDef.projectileGhostReplacements = skinPaladinDefault.projectileGhostReplacements;
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

        internal static SkinDef ModdedSkinBlack(GameObject PaladinBody)
        {
            ModelSkinController modelSkinController = PaladinBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPaladinDefault = modelSkinController.skins[0];

            //0 matPaladinSword
            //1 matPaladin : CAPE
            //2 matPaladinNkuhana
            //3 matPaladinNkuhana
            //4 matPaladinGMSword
            //5 matPaladin

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinPaladinDefault.rendererInfos.Length];
            System.Array.Copy(skinPaladinDefault.rendererInfos, NewRenderInfos, skinPaladinDefault.rendererInfos.Length);

            Material matPaladin = Object.Instantiate(skinPaladinDefault.rendererInfos[5].defaultMaterial);
            Material matPaladinSword = Object.Instantiate(skinPaladinDefault.rendererInfos[0].defaultMaterial);

            Texture2D texPaladin = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Black/texPaladin.png");
            texPaladin.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSword = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Black/texPaladinSword.png");
            texPaladinSword.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSwordEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Black/texPaladinSwordEmission.png");
            texPaladinSwordEmission.wrapMode = TextureWrapMode.Repeat;


            matPaladin.mainTexture = texPaladin;
            matPaladin.SetColor("_EmColor", new Color(1.25f, 0.25f, 0.25f, 1f));
            matPaladinSword.mainTexture = texPaladinSword;
            matPaladinSword.SetTexture("_EmTex", texPaladinSwordEmission);

            NewRenderInfos[0].defaultMaterial = matPaladinSword;
            NewRenderInfos[1].defaultMaterial = matPaladin;
            NewRenderInfos[5].defaultMaterial = matPaladin;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinPaladinWolfo_Any";
            newSkinDef.nameToken = "SIMU_SKIN_PALADIN_BLACK";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Black/skinIconPaladin.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinPaladinDefault };
            newSkinDef.rootObject = skinPaladinDefault.rootObject;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.meshReplacements = skinPaladinDefault.meshReplacements;
            newSkinDef.gameObjectActivations = skinPaladinDefault.gameObjectActivations;
            newSkinDef.projectileGhostReplacements = skinPaladinDefault.projectileGhostReplacements;
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
        public class ClearSimulacrumRobPaladinBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RobPaladinBody");
            }
        }

    }
}