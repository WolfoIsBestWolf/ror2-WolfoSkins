using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Mod
{
    public class SkinsPilot
    {
        internal static void ModdedSkin(GameObject PilotBody)
        {
            Debug.Log("Pilot Skins");
            BodyIndex CharacterIndex = PilotBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = PilotBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinPilotDefault = modelSkinController.skins[0];


            SkinDef white = SkinWHITE(skinPilotDefault);
            SkinDef red = SkinRED(skinPilotDefault);
            SkinDef blue = SkinBLUE(skinPilotDefault);
 
            SkinCatalog.skinsByBody[(int)CharacterIndex] = modelSkinController.skins;


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

        }

        internal static SkinDef SkinWHITE(SkinDef skinPilotDefault)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinPilot_White_1",
                nameToken = "SIMU_SKIN_PILOT_WHITE",
                icon = H.GetIcon("mod/pilot_white"),
                original = skinPilotDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matPilotDefaultWeapon = CloneMat(newRenderInfos, 0);
            Material matPilotDefault1 = CloneMat(newRenderInfos, 2);
            Material matPilotDefault2 = CloneMat(newRenderInfos, 4);
            Material Parachute = CloneMat(newRenderInfos, 10);

            matPilotDefaultWeapon.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PIlotWeapon_diffuseWHITE.png");
            //matPilotDefaultWeapon.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PIlotWeapon_emissionWHITE.png"));
            matPilotDefaultWeapon.SetColor("_EmColor", new Color(1f, -2f, -2f)); //0.4764 0.7803 1 1

            matPilotDefault1.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/Pilot_diffuseWHITE.png");
            //matPilotDefault1.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault1.SetTexture("_EmissionMap", null);
            //matPilotDefault1.SetColor("_EmColor", new Color(0, 0, 0)); //0.2783 0.7029 1 1

            matPilotDefault2.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PIlot2_diffuseWHITE.png");
            //matPilotDefault2.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault2.SetTexture("_EmissionMap", null);
            //matPilotDefault2.SetColor("_EmColor", new Color(0, 0, 0)); //0.4764 0.7803 1 1

            Parachute.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/White/PilotParachute_diffuseWHITE.png");

            newRenderInfos[0].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[1].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[2].defaultMaterial = matPilotDefault1;
            newRenderInfos[3].defaultMaterial = matPilotDefault1;
            newRenderInfos[4].defaultMaterial = matPilotDefault2;
            newRenderInfos[5].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[6].defaultMaterial = matPilotDefault2;
            newRenderInfos[7].defaultMaterial = matPilotDefault2;
            newRenderInfos[8].defaultMaterial = matPilotDefault2;
            newRenderInfos[9].defaultMaterial = matPilotDefault2;
            newRenderInfos[10].defaultMaterial = Parachute;

            return newSkinDef;
        }

        internal static SkinDef SkinRED(SkinDef skinPilotDefault)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinPilot_Red_1",
                nameToken = "SIMU_SKIN_PILOT_RED",
                icon = H.GetIcon("mod/pilot_red"),
                original = skinPilotDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matPilotDefaultWeapon = CloneMat(newRenderInfos, 0);
            Material matPilotDefault1 = CloneMat(newRenderInfos, 2);
            Material matPilotDefault2 = CloneMat(newRenderInfos, 4);
            Material Parachute = CloneMat(newRenderInfos, 10);

            //Texture2D PIlotWeapon_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PIlotWeapon_emissionRED.png");

            matPilotDefaultWeapon.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PIlotWeapon_diffuseRED.png");
            //matPilotDefaultWeapon.SetTexture("_EmTex", PIlotWeapon_emission);
            //matPilotDefaultWeapon.SetTexture("_EmissionMap", PIlotWeapon_emission);
            matPilotDefaultWeapon.SetColor("_EmColor", new Color(0f, 3f, 1.5f)); //0.4764 0.7803 1 1

            matPilotDefault1.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/Pilot_diffuseRED.png");
            //matPilotDefault1.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault1.SetTexture("_EmissionMap", null);
            //matPilotDefault1.SetColor("_EmColor", new Color(0, 0, 0)); //0.2783 0.7029 1 1

            matPilotDefault2.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PIlot2_diffuseRED.png");
            //matPilotDefault2.SetTexture("_EmTex", null); //Em is full black atm
            //matPilotDefault2.SetTexture("_EmissionMap", null);
            //matPilotDefault2.SetColor("_EmColor", new Color(0, 0, 0)); //0.4764 0.7803 1 1

            Parachute.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Red/PilotParachute_diffuseRED.png");

            newRenderInfos[0].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[1].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[2].defaultMaterial = matPilotDefault1;
            newRenderInfos[3].defaultMaterial = matPilotDefault1;
            newRenderInfos[4].defaultMaterial = matPilotDefault2;
            newRenderInfos[5].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[6].defaultMaterial = matPilotDefault2;
            newRenderInfos[7].defaultMaterial = matPilotDefault2;
            newRenderInfos[8].defaultMaterial = matPilotDefault2;
            newRenderInfos[9].defaultMaterial = matPilotDefault2;
            newRenderInfos[10].defaultMaterial = Parachute;

            return newSkinDef;
        }


        internal static SkinDef SkinBLUE(SkinDef skinPilotDefault)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinPilot_Blue_1",
                nameToken = "SIMU_SKIN_PILOT_BLUE",
                icon = H.GetIcon("mod/pilot_blue"),
                original = skinPilotDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matPilotDefaultWeapon = CloneMat(newRenderInfos, 0);
            Material matPilotDefault1 = CloneMat(newRenderInfos, 2);
            Material matPilotDefault2 = CloneMat(newRenderInfos, 4);
            Material Parachute = CloneMat(newRenderInfos, 10);

            matPilotDefaultWeapon.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PIlotWeapon_diffuse.png");
            matPilotDefaultWeapon.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PIlotWeapon_emission.png"));
            //matPilotDefaultWeapon.SetTexture("_EmissionMap", PIlotWeapon_emission);
            matPilotDefaultWeapon.SetColor("_EmColor", new Color(1, 1, 0)); //0.4764 0.7803 1 1

            matPilotDefault1.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/Pilot_diffuse.png");
            matPilotDefault1.SetTexture("_EmTex", null); //Em is full black atm
            matPilotDefault1.SetTexture("_EmissionMap", null);
            matPilotDefault1.SetColor("_EmColor", new Color(0, 0, 0)); //0.2783 0.7029 1 1

            matPilotDefault2.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PIlot2_diffuse.png");
            matPilotDefault2.SetTexture("_EmTex", null); //Em is full black atm
            matPilotDefault2.SetTexture("_EmissionMap", null);
            matPilotDefault2.SetColor("_EmColor", new Color(0, 0, 0)); //0.4764 0.7803 1 1

            Parachute.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Pilot/Blue/PilotParachute_diffuse.png");

            newRenderInfos[0].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[1].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[2].defaultMaterial = matPilotDefault1;
            newRenderInfos[3].defaultMaterial = matPilotDefault1;
            newRenderInfos[4].defaultMaterial = matPilotDefault2;
            newRenderInfos[5].defaultMaterial = matPilotDefaultWeapon;
            newRenderInfos[6].defaultMaterial = matPilotDefault2;
            newRenderInfos[7].defaultMaterial = matPilotDefault2;
            newRenderInfos[8].defaultMaterial = matPilotDefault2;
            newRenderInfos[9].defaultMaterial = matPilotDefault2;
            newRenderInfos[10].defaultMaterial = Parachute;

            return newSkinDef;
        }

        [RegisterAchievement("CLEAR_ANY_MOFFEINPILOT", "Skins.MoffeinPilot.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumPilot : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MoffeinPilotBody");
            }
        }

    }
}