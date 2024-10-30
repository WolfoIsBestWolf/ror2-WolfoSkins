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
            //ModdedSkinBlack(PaladinBody);

            BodyIndex CharacterIndex = PaladinBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = PaladinBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPaladinDefault = modelSkinController.skins[0];
            SkinDef skinPaladinGreen = modelSkinController.skins[3];

            //0 matPaladinSword
            //1 matPaladin
            //2 matPaladinNkuhana
            //3 matPaladinNkuhana
            //4 matPaladinGMSword
            //5 matPaladin

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinPaladinDefault.rendererInfos.Length];
            System.Array.Copy(skinPaladinDefault.rendererInfos, NewRenderInfos, skinPaladinDefault.rendererInfos.Length);

            Material matPaladin = Object.Instantiate(skinPaladinDefault.rendererInfos[5].defaultMaterial);
            Material matPaladinSword = Object.Instantiate(skinPaladinDefault.rendererInfos[0].defaultMaterial);
            Material matPaladinGMOld = Object.Instantiate(skinPaladinDefault.rendererInfos[1].defaultMaterial);


            Texture2D texPaladin = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladin.png");
            texPaladin.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSword = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladinSword.png");
            texPaladinSword.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSwordEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladinSwordEmission.png");
            texPaladinSwordEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinGMOld = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Paladin/Yellow/texPaladinGMOld.png");
            texPaladinGMOld.wrapMode = TextureWrapMode.Repeat;


            matPaladin.mainTexture = texPaladin;
            matPaladin.SetColor("_EmColor", new Color(1.25f, 0.25f, 0.25f, 1f));
            matPaladinSword.mainTexture = texPaladinSword;
            matPaladinSword.SetTexture("_EmTex", texPaladinSwordEmission);
            matPaladinGMOld.mainTexture = texPaladinGMOld;

            NewRenderInfos[0].defaultMaterial = matPaladinSword;
            NewRenderInfos[1].defaultMaterial = matPaladinGMOld;
            NewRenderInfos[5].defaultMaterial = matPaladin;

            //mdlPaladin/Armature/spine/spine.001/spine.002/spine.003/spine.004/neck/head/EyeTrail/Trail

            //
            //
            RoR2.SkinDef.GameObjectActivation[] GameObjectActivations = new SkinDef.GameObjectActivation[skinPaladinDefault.gameObjectActivations.Length];
            skinPaladinDefault.gameObjectActivations.CopyTo(GameObjectActivations, 0);
   
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



            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length + 1];

            skinsNew[0] = modelSkinController.skins[0]; //D
            skinsNew[1] = modelSkinController.skins[1]; //M
            skinsNew[2] = modelSkinController.skins[2]; //GM
            skinsNew[3] = modelSkinController.skins[3]; //NK
            skinsNew[4] = modelSkinController.skins[4]; //C
            skinsNew[5] = newSkinDef;

            for (int i = 5; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i + 1] = modelSkinController.skins[i];
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)CharacterIndex] = skinsNew;
        }

        #region Unused Black
        /*
        internal static void ModdedSkinBlack(GameObject PaladinBody)
        {
            Debug.Log("Paladin Skins");
            BodyIndex CharacterIndex = PaladinBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = PaladinBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPaladinDefault = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinPaladinDefault.rendererInfos.Length];
            System.Array.Copy(skinPaladinDefault.rendererInfos, NewRenderInfos, skinPaladinDefault.rendererInfos.Length);

            Material matPaladin = Object.Instantiate(skinPaladinDefault.rendererInfos[13].defaultMaterial);
            Material matPaladinSword = Object.Instantiate(skinPaladinDefault.rendererInfos[0].defaultMaterial);
            Material matPaladinGMOld = Object.Instantiate(skinPaladinDefault.rendererInfos[1].defaultMaterial);


            Texture2D texPaladin = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
            texPaladin.LoadImage(Properties.Resources.texPaladinBLACK, true);
            texPaladin.filterMode = FilterMode.Point;
            texPaladin.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSword = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texPaladinSword.LoadImage(Properties.Resources.texPaladinSwordBLACK, true);
            texPaladinSword.filterMode = FilterMode.Bilinear;
            texPaladinSword.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinEmissionBLACK = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texPaladinEmissionBLACK.LoadImage(Properties.Resources.texPaladinEmissionBLACK, true);
            texPaladinEmissionBLACK.filterMode = FilterMode.Point;
            texPaladinEmissionBLACK.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinSwordEmission = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texPaladinSwordEmission.LoadImage(Properties.Resources.texPaladinSwordEmissionBLACK, true);
            texPaladinSwordEmission.filterMode = FilterMode.Bilinear;
            texPaladinSwordEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texPaladinGMOld = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texPaladinGMOld.LoadImage(Properties.Resources.texPaladinGMOldBLACK, true);
            texPaladinGMOld.filterMode = FilterMode.Bilinear;
            texPaladinGMOld.wrapMode = TextureWrapMode.Repeat;


            matPaladin.mainTexture = texPaladin;
            matPaladin.SetTexture("_EmTex", texPaladinEmissionBLACK);
            matPaladin.SetColor("_EmColor", new Color(1.25f, 0.25f, 0.25f, 1f));
            matPaladinSword.mainTexture = texPaladinSword;
            matPaladinSword.SetTexture("_EmTex", texPaladinSwordEmission);
            matPaladinGMOld.mainTexture = texPaladinGMOld;

            NewRenderInfos[0].defaultMaterial = matPaladinSword;
            NewRenderInfos[1].defaultMaterial = matPaladinGMOld;
            NewRenderInfos[13].defaultMaterial = matPaladin;

            RoR2.SkinDef.GameObjectActivation[] GameObjectActivations = new SkinDef.GameObjectActivation[skinPaladinDefault.gameObjectActivations.Length];
            skinPaladinDefault.gameObjectActivations.CopyTo(GameObjectActivations, 0);


            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinPaladinWolfo_BLACK";
            newSkinDef.nameToken = "SIMU_SKIN_PALADIN_BLACK";
            newSkinDef.icon = SkinIconS;
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
                    lightPath = "Armature/spine/spine.001/spine.002/spine.003/spine.004/neck/head/EyeTrail/Trail",
                }
            };



            SkinDef[] skinsNew = new SkinDef[modelSkinController.skins.Length + 1];

            skinsNew[0] = modelSkinController.skins[0]; //D
            skinsNew[1] = modelSkinController.skins[1]; //M
            skinsNew[2] = modelSkinController.skins[2]; //GM
            skinsNew[3] = modelSkinController.skins[3]; //NK
            skinsNew[4] = modelSkinController.skins[4]; //C
            skinsNew[5] = newSkinDef;

            for (int i = 5; i < modelSkinController.skins.Length; i++)
            {
                skinsNew[i + 1] = modelSkinController.skins[i];
            }
            modelSkinController.skins = skinsNew;
            BodyCatalog.skins[(int)CharacterIndex] = skinsNew;
        }
        */
        #endregion

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