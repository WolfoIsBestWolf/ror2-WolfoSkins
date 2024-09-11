using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsPilot
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_PILOT_WHITE", "Winter");
            LanguageAPI.Add("SIMU_SKIN_PILOT_RED", "Militant");
            LanguageAPI.Add("SIMU_SKIN_PILOT_BLUE", "Courier ");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_PILOT_NAME", "Pilot"+Unlocks.unlockName);
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_PILOT_DESCRIPTION", "As Pilot" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_PILOT_NAME";
            unlockableDef.cachedName = "Skins.Pilot.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinIconPilot);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
           
        }

        internal static void ModdedSkin(GameObject PilotBody)
        {
            Debug.Log("Pilot Skins");
            unlockableDef.hidden = false;
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            SkinWHITE(PilotBody);
            SkinRED(PilotBody);
            SkinBLUE(PilotBody);
        }

        internal static void SkinWHITE(GameObject PilotBody)
        {
            BodyIndex CharacterIndex = PilotBody.GetComponent<CharacterBody>().bodyIndex;
            //ModelSkinController modelSkinController = PilotBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            ModelSkinController modelSkinController = PilotBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPilotDefault = modelSkinController.skins[0];

            //0 matPilotDefaultWeapon
            //1 matPilotDefaultWeapon
            //2 matPilotDefault1
            //3 matPilotDefault1
            //4 matPilotDefault2
            //5 matPilotDefaultWeapon
            //6 matPilotDefault2
            //7 matPilotDefault2
            //8 matPilotDefault2
            //9 matPilotDefault2
            //10 Parachute

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinPilotDefault.rendererInfos.Length];
            System.Array.Copy(skinPilotDefault.rendererInfos, NewRenderInfos, skinPilotDefault.rendererInfos.Length);

            Material matPilotDefaultWeapon = Object.Instantiate(skinPilotDefault.rendererInfos[0].defaultMaterial);
            Material matPilotDefault1 = Object.Instantiate(skinPilotDefault.rendererInfos[2].defaultMaterial);
            Material matPilotDefault2 = Object.Instantiate(skinPilotDefault.rendererInfos[4].defaultMaterial);
            Material Parachute = Object.Instantiate(skinPilotDefault.rendererInfos[10].defaultMaterial);

            Texture2D PIlotWeapon_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PIlotWeapon_diffuse.LoadImage(Properties.Resources.PIlotWeapon_diffuseWHITE, true);
            PIlotWeapon_diffuse.filterMode = FilterMode.Bilinear;
            PIlotWeapon_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlotWeapon_emission = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            PIlotWeapon_emission.LoadImage(Properties.Resources.PIlotWeapon_emissionWHITE, true);
            PIlotWeapon_emission.filterMode = FilterMode.Bilinear;
            PIlotWeapon_emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D Pilot_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            Pilot_diffuse.LoadImage(Properties.Resources.Pilot_diffuseWHITE, true);
            Pilot_diffuse.filterMode = FilterMode.Bilinear;
            Pilot_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlot2_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PIlot2_diffuse.LoadImage(Properties.Resources.PIlot2_diffuseWHITE, true);
            PIlot2_diffuse.filterMode = FilterMode.Bilinear;
            PIlot2_diffuse.wrapMode = TextureWrapMode.Clamp;

            /*Texture2D PIlot2_emission = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            PIlot2_emission.LoadImage(Properties.Resources.PIlot2_emission, true);
            PIlot2_emission.filterMode = FilterMode.Bilinear;
            PIlot2_emission.wrapMode = TextureWrapMode.Clamp;*/

            Texture2D PilotParachute_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PilotParachute_diffuse.LoadImage(Properties.Resources.PilotParachute_diffuseWHITE, true);
            PilotParachute_diffuse.filterMode = FilterMode.Bilinear;
            PilotParachute_diffuse.wrapMode = TextureWrapMode.Repeat;


            matPilotDefaultWeapon.mainTexture = PIlotWeapon_diffuse;
            //matPilotDefaultWeapon.SetTexture("_EmTex", PIlotWeapon_emission);
            //matPilotDefaultWeapon.SetTexture("_EmissionMap", PIlotWeapon_emission);
            matPilotDefaultWeapon.SetColor("_EmColor", new Color(1f, -2f, -2f)); //0.4764 0.7803 1 1

            matPilotDefault1.mainTexture = Pilot_diffuse;
            //matPilotDefault1.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault1.SetTexture("_EmissionMap", null);
            //matPilotDefault1.SetColor("_EmColor", new Color(0, 0, 0)); //0.2783 0.7029 1 1

            matPilotDefault2.mainTexture = PIlot2_diffuse;
            //matPilotDefault2.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault2.SetTexture("_EmissionMap", null);
            //matPilotDefault2.SetColor("_EmColor", new Color(0, 0, 0)); //0.4764 0.7803 1 1

            Parachute.mainTexture = PilotParachute_diffuse;

            NewRenderInfos[0].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[1].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[2].defaultMaterial = matPilotDefault1;
            NewRenderInfos[3].defaultMaterial = matPilotDefault1;
            NewRenderInfos[4].defaultMaterial = matPilotDefault2;
            NewRenderInfos[5].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[6].defaultMaterial = matPilotDefault2;
            NewRenderInfos[7].defaultMaterial = matPilotDefault2;
            NewRenderInfos[8].defaultMaterial = matPilotDefault2;
            NewRenderInfos[9].defaultMaterial = matPilotDefault2;
            NewRenderInfos[10].defaultMaterial = Parachute;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconPilotWHITE, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinPilotWolfo_White",
                NameToken = "SIMU_SKIN_PILOT_WHITE",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinPilotDefault },
                RootObject = skinPilotDefault.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinPilotDefault.meshReplacements,
                GameObjectActivations = skinPilotDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinPilotDefault.projectileGhostReplacements,
            };
            SkinDef SkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(SkinDefNew);
            BodyCatalog.skins[(int)CharacterIndex] = BodyCatalog.skins[(int)CharacterIndex].Add(SkinDefNew);
        }

        internal static void SkinRED(GameObject PilotBody)
        {
            BodyIndex CharacterIndex = PilotBody.GetComponent<CharacterBody>().bodyIndex;
            //ModelSkinController modelSkinController = PilotBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            ModelSkinController modelSkinController = PilotBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPilotDefault = modelSkinController.skins[0];

            //0 matPilotDefaultWeapon
            //1 matPilotDefaultWeapon
            //2 matPilotDefault1
            //3 matPilotDefault1
            //4 matPilotDefault2
            //5 matPilotDefaultWeapon
            //6 matPilotDefault2
            //7 matPilotDefault2
            //8 matPilotDefault2
            //9 matPilotDefault2
            //10 Parachute

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinPilotDefault.rendererInfos.Length];
            System.Array.Copy(skinPilotDefault.rendererInfos, NewRenderInfos, skinPilotDefault.rendererInfos.Length);

            Material matPilotDefaultWeapon = Object.Instantiate(skinPilotDefault.rendererInfos[0].defaultMaterial);
            Material matPilotDefault1 = Object.Instantiate(skinPilotDefault.rendererInfos[2].defaultMaterial);
            Material matPilotDefault2 = Object.Instantiate(skinPilotDefault.rendererInfos[4].defaultMaterial);
            Material Parachute = Object.Instantiate(skinPilotDefault.rendererInfos[10].defaultMaterial);

            Texture2D PIlotWeapon_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PIlotWeapon_diffuse.LoadImage(Properties.Resources.PIlotWeapon_diffuseRED, true);
            PIlotWeapon_diffuse.filterMode = FilterMode.Bilinear;
            PIlotWeapon_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlotWeapon_emission = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            PIlotWeapon_emission.LoadImage(Properties.Resources.PIlotWeapon_emissionRED, true);
            PIlotWeapon_emission.filterMode = FilterMode.Bilinear;
            PIlotWeapon_emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D Pilot_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            Pilot_diffuse.LoadImage(Properties.Resources.Pilot_diffuseRED, true);
            Pilot_diffuse.filterMode = FilterMode.Bilinear;
            Pilot_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlot2_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PIlot2_diffuse.LoadImage(Properties.Resources.PIlot2_diffuseRED, true);
            PIlot2_diffuse.filterMode = FilterMode.Bilinear;
            PIlot2_diffuse.wrapMode = TextureWrapMode.Clamp;

            /*Texture2D PIlot2_emission = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            PIlot2_emission.LoadImage(Properties.Resources.PIlot2_emission, true);
            PIlot2_emission.filterMode = FilterMode.Bilinear;
            PIlot2_emission.wrapMode = TextureWrapMode.Clamp;*/

            Texture2D PilotParachute_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PilotParachute_diffuse.LoadImage(Properties.Resources.PilotParachute_diffuseRED, true);
            PilotParachute_diffuse.filterMode = FilterMode.Bilinear;
            PilotParachute_diffuse.wrapMode = TextureWrapMode.Repeat;


            matPilotDefaultWeapon.mainTexture = PIlotWeapon_diffuse;
            //matPilotDefaultWeapon.SetTexture("_EmTex", PIlotWeapon_emission);
            //matPilotDefaultWeapon.SetTexture("_EmissionMap", PIlotWeapon_emission);
            matPilotDefaultWeapon.SetColor("_EmColor", new Color(0f, 3f, 1.5f)); //0.4764 0.7803 1 1

            matPilotDefault1.mainTexture = Pilot_diffuse;
            //matPilotDefault1.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault1.SetTexture("_EmissionMap", null);
            //matPilotDefault1.SetColor("_EmColor", new Color(0, 0, 0)); //0.2783 0.7029 1 1

            matPilotDefault2.mainTexture = PIlot2_diffuse;
            //matPilotDefault2.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault2.SetTexture("_EmissionMap", null);
            //matPilotDefault2.SetColor("_EmColor", new Color(0, 0, 0)); //0.4764 0.7803 1 1

            Parachute.mainTexture = PilotParachute_diffuse;

            NewRenderInfos[0].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[1].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[2].defaultMaterial = matPilotDefault1;
            NewRenderInfos[3].defaultMaterial = matPilotDefault1;
            NewRenderInfos[4].defaultMaterial = matPilotDefault2;
            NewRenderInfos[5].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[6].defaultMaterial = matPilotDefault2;
            NewRenderInfos[7].defaultMaterial = matPilotDefault2;
            NewRenderInfos[8].defaultMaterial = matPilotDefault2;
            NewRenderInfos[9].defaultMaterial = matPilotDefault2;
            NewRenderInfos[10].defaultMaterial = Parachute;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconPilotRED, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinPilotWolfo_Red",
                NameToken = "SIMU_SKIN_PILOT_RED",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinPilotDefault },
                RootObject = skinPilotDefault.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinPilotDefault.meshReplacements,
                GameObjectActivations = skinPilotDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinPilotDefault.projectileGhostReplacements,
            };
            SkinDef SkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(SkinDefNew);
            BodyCatalog.skins[(int)CharacterIndex] = BodyCatalog.skins[(int)CharacterIndex].Add(SkinDefNew);
        }


        internal static void SkinBLUE(GameObject PilotBody)
        {           
            BodyIndex CharacterIndex = PilotBody.GetComponent<CharacterBody>().bodyIndex;
            //ModelSkinController modelSkinController = PilotBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            ModelSkinController modelSkinController = PilotBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPilotDefault = modelSkinController.skins[0];

            //0 matPilotDefaultWeapon
            //1 matPilotDefaultWeapon
            //2 matPilotDefault1
            //3 matPilotDefault1
            //4 matPilotDefault2
            //5 matPilotDefaultWeapon
            //6 matPilotDefault2
            //7 matPilotDefault2
            //8 matPilotDefault2
            //9 matPilotDefault2
            //10 Parachute

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinPilotDefault.rendererInfos.Length];
            System.Array.Copy(skinPilotDefault.rendererInfos, NewRenderInfos, skinPilotDefault.rendererInfos.Length);

            Material matPilotDefaultWeapon = Object.Instantiate(skinPilotDefault.rendererInfos[0].defaultMaterial);
            Material matPilotDefault1 = Object.Instantiate(skinPilotDefault.rendererInfos[2].defaultMaterial);
            Material matPilotDefault2 = Object.Instantiate(skinPilotDefault.rendererInfos[4].defaultMaterial);
            Material Parachute = Object.Instantiate(skinPilotDefault.rendererInfos[10].defaultMaterial);

            Texture2D PIlotWeapon_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PIlotWeapon_diffuse.LoadImage(Properties.Resources.PIlotWeapon_diffuse, true);
            PIlotWeapon_diffuse.filterMode = FilterMode.Bilinear;
            PIlotWeapon_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlotWeapon_emission = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            PIlotWeapon_emission.LoadImage(Properties.Resources.PIlotWeapon_emission, true);
            PIlotWeapon_emission.filterMode = FilterMode.Bilinear;
            PIlotWeapon_emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D Pilot_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            Pilot_diffuse.LoadImage(Properties.Resources.Pilot_diffuse, true);
            Pilot_diffuse.filterMode = FilterMode.Bilinear;
            Pilot_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlot2_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PIlot2_diffuse.LoadImage(Properties.Resources.PIlot2_diffuse, true);
            PIlot2_diffuse.filterMode = FilterMode.Bilinear;
            PIlot2_diffuse.wrapMode = TextureWrapMode.Clamp;

            /*Texture2D PIlot2_emission = new Texture2D(2048, 2048, TextureFormat.DXT1, false);
            PIlot2_emission.LoadImage(Properties.Resources.PIlot2_emission, true);
            PIlot2_emission.filterMode = FilterMode.Bilinear;
            PIlot2_emission.wrapMode = TextureWrapMode.Clamp;*/

            Texture2D PilotParachute_diffuse = new Texture2D(2048, 2048, TextureFormat.DXT5, false);
            PilotParachute_diffuse.LoadImage(Properties.Resources.PilotParachute_diffuse, true);
            PilotParachute_diffuse.filterMode = FilterMode.Bilinear;
            PilotParachute_diffuse.wrapMode = TextureWrapMode.Repeat;


            matPilotDefaultWeapon.mainTexture = PIlotWeapon_diffuse;
            matPilotDefaultWeapon.SetTexture("_EmTex", PIlotWeapon_emission);
            matPilotDefaultWeapon.SetTexture("_EmissionMap", PIlotWeapon_emission);
            matPilotDefaultWeapon.SetColor("_EmColor", new Color(1,1,0)); //0.4764 0.7803 1 1

            matPilotDefault1.mainTexture = Pilot_diffuse;
            matPilotDefault1.SetTexture("_EmTex", null); //Em is full black atm
            matPilotDefault1.SetTexture("_EmissionMap", null);
            matPilotDefault1.SetColor("_EmColor", new Color(0, 0, 0)); //0.2783 0.7029 1 1

            matPilotDefault2.mainTexture = PIlot2_diffuse;
            matPilotDefault2.SetTexture("_EmTex", null); //Em is full black atm
            matPilotDefault2.SetTexture("_EmissionMap", null);
            matPilotDefault2.SetColor("_EmColor", new Color(0, 0, 0)); //0.4764 0.7803 1 1

            Parachute.mainTexture = PilotParachute_diffuse;

            NewRenderInfos[0].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[1].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[2].defaultMaterial = matPilotDefault1;
            NewRenderInfos[3].defaultMaterial = matPilotDefault1;
            NewRenderInfos[4].defaultMaterial = matPilotDefault2;
            NewRenderInfos[5].defaultMaterial = matPilotDefaultWeapon;
            NewRenderInfos[6].defaultMaterial = matPilotDefault2;
            NewRenderInfos[7].defaultMaterial = matPilotDefault2;
            NewRenderInfos[8].defaultMaterial = matPilotDefault2;
            NewRenderInfos[9].defaultMaterial = matPilotDefault2;
            NewRenderInfos[10].defaultMaterial = Parachute;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconPilot, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinPilotWolfo_Blue",
                NameToken = "SIMU_SKIN_PILOT_BLUE",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinPilotDefault },
                RootObject = skinPilotDefault.rootObject,
                UnlockableDef = unlockableDef,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinPilotDefault.meshReplacements,
                GameObjectActivations = skinPilotDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinPilotDefault.projectileGhostReplacements,
            };
            SkinDef SkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(SkinDefNew);
            BodyCatalog.skins[(int)CharacterIndex] = BodyCatalog.skins[(int)CharacterIndex].Add(SkinDefNew);
        }
        
        [RegisterAchievement("SIMU_SKIN_PILOT", "Skins.Pilot.Wolfo", null, 5, null)]
        public class ClearSimulacrumPilot : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MoffeinPilotBody");
            }
        }
        
    }
}