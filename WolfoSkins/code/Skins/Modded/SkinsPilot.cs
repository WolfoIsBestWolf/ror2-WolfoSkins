using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsPilot
    {
        internal static void ModdedSkin(GameObject PilotBody)
        {
            Debug.Log("Pilot Skins");
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

            Texture2D PIlotWeapon_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PIlotWeapon_diffuseWHITE.png");
            PIlotWeapon_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlotWeapon_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PIlotWeapon_emissionWHITE.png");
            PIlotWeapon_emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D Pilot_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/Pilot_diffuseWHITE.png");
            Pilot_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlot2_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PIlot2_diffuseWHITE.png");
            PIlot2_diffuse.wrapMode = TextureWrapMode.Clamp;

            /*Texture2D PIlot2_emission;
            PIlot2_emission.wrapMode = TextureWrapMode.Clamp;*/

            Texture2D PilotParachute_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PilotParachute_diffuseWHITE.png");
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
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinPilotWolfo_White_Simu",
                NameToken = "SIMU_SKIN_PILOT_WHITE",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/skinIconPilotWHITE.png")),
                BaseSkins = new SkinDef[] { skinPilotDefault },
                RootObject = skinPilotDefault.rootObject,
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

            Texture2D PIlotWeapon_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PIlotWeapon_diffuseRED.png");
            PIlotWeapon_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlotWeapon_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PIlotWeapon_emissionRED.png");
            PIlotWeapon_emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D Pilot_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/Pilot_diffuseRED.png");
            Pilot_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlot2_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PIlot2_diffuseRED.png");
            PIlot2_diffuse.wrapMode = TextureWrapMode.Clamp;

            /*Texture2D PIlot2_emission;
            PIlot2_emission.wrapMode = TextureWrapMode.Clamp;*/

            Texture2D PilotParachute_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PilotParachute_diffuseRED.png");
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
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinPilotWolfo_Red_Simu",
                NameToken = "SIMU_SKIN_PILOT_RED",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/skinIconPilotRED.png")),
                BaseSkins = new SkinDef[] { skinPilotDefault },
                RootObject = skinPilotDefault.rootObject,
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

            Texture2D PIlotWeapon_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PIlotWeapon_diffuse.png");
            PIlotWeapon_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlotWeapon_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PIlotWeapon_emission.png");
            PIlotWeapon_emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D Pilot_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/Pilot_diffuse.png");
            Pilot_diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D PIlot2_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PIlot2_diffuse.png");
            PIlot2_diffuse.wrapMode = TextureWrapMode.Clamp;

            /*Texture2D PIlot2_emission;
            PIlot2_emission.wrapMode = TextureWrapMode.Clamp;*/

            Texture2D PilotParachute_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PilotParachute_diffuse.png");
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
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinPilotWolfo_Blue_Simu",
                NameToken = "SIMU_SKIN_PILOT_BLUE",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/skinIconPilot.png")),
                BaseSkins = new SkinDef[] { skinPilotDefault },
                RootObject = skinPilotDefault.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinPilotDefault.meshReplacements,
                GameObjectActivations = skinPilotDefault.gameObjectActivations,
                ProjectileGhostReplacements = skinPilotDefault.projectileGhostReplacements,
            };
            SkinDef SkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(SkinDefNew);
            BodyCatalog.skins[(int)CharacterIndex] = BodyCatalog.skins[(int)CharacterIndex].Add(SkinDefNew);
        }
        
        [RegisterAchievement("CLEAR_ANY_MOFFEINPILOT", "Skins.MoffeinPilot.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumPilot : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MoffeinPilotBody");
            }
        }
        
    }
}