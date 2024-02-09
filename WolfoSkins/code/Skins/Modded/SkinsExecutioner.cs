using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsExecutioner
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinExecutionerIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //Probably has to be added during awake

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_EXECUTIONER_NAME", "Executioner: Alternated");

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_EXECUTIONER_NAME";
            unlockableDef.cachedName = "Skins.Executioner.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
        }

        internal static void ModdedSkin(GameObject ExecutionerBody)
        {
            Debug.Log("Executioner Skins");
            BodyIndex ExecutionerIndex = ExecutionerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ExecutionerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinExecutioner = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinExecutioner.rendererInfos.Length];
            System.Array.Copy(skinExecutioner.rendererInfos, NewRenderInfos, skinExecutioner.rendererInfos.Length);

            //0 matExe2
            //1 matExe2Armor
            //2 matExe2Jumpkit
            //3 matExe2Gun
            //4 matExecutionerAxe

            Material matExe2 = Object.Instantiate(skinExecutioner.rendererInfos[0].defaultMaterial);
            Material matExe2Armor = Object.Instantiate(skinExecutioner.rendererInfos[1].defaultMaterial);
            Material matExe2Jumpkit = Object.Instantiate(skinExecutioner.rendererInfos[2].defaultMaterial);
            Material matExe2Gun = Object.Instantiate(skinExecutioner.rendererInfos[3].defaultMaterial);
            Material matExecutionerAxe = Object.Instantiate(skinExecutioner.rendererInfos[4].defaultMaterial);


            Texture2D ExeTex = new Texture2D(512, 512, TextureFormat.DXT5, false);
            ExeTex.LoadImage(Properties.Resources.ExeTex, true);
            ExeTex.filterMode = FilterMode.Bilinear;
            ExeTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D exeem = new Texture2D(512, 512, TextureFormat.DXT5, false);
            exeem.LoadImage(Properties.Resources.exeem, true);
            exeem.filterMode = FilterMode.Bilinear;
            exeem.wrapMode = TextureWrapMode.Repeat;

            Texture2D exeRamp = new Texture2D(256, 16, TextureFormat.DXT5, false);
            exeRamp.LoadImage(Properties.Resources.exeRamp, true);
            exeRamp.filterMode = FilterMode.Bilinear;
            exeRamp.wrapMode = TextureWrapMode.Clamp;

            Texture2D ExeKitTex = new Texture2D(512, 512, TextureFormat.DXT1, false);
            ExeKitTex.LoadImage(Properties.Resources.ExeKitTex, true);
            ExeKitTex.filterMode = FilterMode.Bilinear;
            ExeKitTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExeKitEm = new Texture2D(512, 512, TextureFormat.DXT1, false);
            ExeKitEm.LoadImage(Properties.Resources.ExeKitEm, true);
            ExeKitEm.filterMode = FilterMode.Bilinear;
            ExeKitEm.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExegunTex = new Texture2D(512, 512, TextureFormat.DXT1, false);
            ExegunTex.LoadImage(Properties.Resources.ExegunTex, true);
            ExegunTex.filterMode = FilterMode.Bilinear;
            ExegunTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExegunEmTex = new Texture2D(512, 512, TextureFormat.DXT1, false);
            ExegunEmTex.LoadImage(Properties.Resources.ExegunEmTex, true);
            ExegunEmTex.filterMode = FilterMode.Bilinear;
            ExegunEmTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExeAxe4 = new Texture2D(512, 512, TextureFormat.DXT5, false);
            ExeAxe4.LoadImage(Properties.Resources.ExeAxe4, true);
            ExeAxe4.filterMode = FilterMode.Bilinear;
            ExeAxe4.wrapMode = TextureWrapMode.Repeat;

            Texture2D texExeAxeRamp = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texExeAxeRamp.LoadImage(Properties.Resources.texExeAxeRamp, true);
            texExeAxeRamp.filterMode = FilterMode.Bilinear;
            texExeAxeRamp.wrapMode = TextureWrapMode.Clamp;




            Color EmColor = new Color(1.5f, 0f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExe2.mainTexture = ExeTex;
            matExe2.SetTexture("_EmTex", exeem);
            matExe2.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Armor.mainTexture = ExeTex;
            matExe2Armor.SetTexture("_EmTex", exeem);
            matExe2Armor.SetTexture("_FresnelRamp", exeRamp);
            matExe2Armor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Jumpkit.mainTexture = ExeKitTex;
            matExe2Jumpkit.SetTexture("_EmTex", exeem);
            matExe2Jumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Gun.mainTexture = ExegunTex;
            matExe2Gun.SetTexture("_EmTex", ExegunEmTex);
            matExe2Gun.SetColor("_EmColor", EmColor);

            matExecutionerAxe.mainTexture = ExeAxe4;
            matExecutionerAxe.SetTexture("_RemapTex", texExeAxeRamp);
            matExecutionerAxe.SetColor("_TintColor", EmColor); //0.0688 0.5619 0.9717 1

            NewRenderInfos[0].defaultMaterial = matExe2;
            NewRenderInfos[1].defaultMaterial = matExe2Armor;
            NewRenderInfos[2].defaultMaterial = matExe2Jumpkit;
            NewRenderInfos[3].defaultMaterial = matExe2Gun;
            NewRenderInfos[4].defaultMaterial = matExecutionerAxe;

            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinExecutionerIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_EXECUTIONER", "Edge");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_EXECUTIONER_DESCRIPTION", "As Executioner"+ WolfoSkins.unlockCondition);

            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinExecutionerWolfo",
                NameToken = "SIMU_SKIN_EXECUTIONER",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinExecutioner },
                RootObject = skinExecutioner.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinExecutioner.meshReplacements,
                GameObjectActivations = skinExecutioner.gameObjectActivations,
                ProjectileGhostReplacements = skinExecutioner.projectileGhostReplacements,           
            };
            SkinDef ExecutionerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(ExecutionerSkinDefNew);
            BodyCatalog.skins[(int)ExecutionerIndex] = BodyCatalog.skins[(int)ExecutionerIndex].Add(ExecutionerSkinDefNew);

        }



        [RegisterAchievement("SIMU_SKIN_EXECUTIONER", "Skins.Executioner.Wolfo", null, null)]
        public class ClearSimulacrumExecutioner : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Executioner2Body");
            }
        }
    }
}