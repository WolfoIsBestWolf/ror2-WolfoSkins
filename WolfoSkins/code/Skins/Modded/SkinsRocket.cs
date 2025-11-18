using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;


namespace WolfoSkinsMod.Mod
{
    public class SkinsRocket
    {
        internal static void ModdedSkin(GameObject RocketBody)
        {
            Debug.Log("Rocket Skins");
            BodyIndex RocketIndex = RocketBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RocketBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinRocket = modelSkinController.skins[0];
            SkinDef skinRocketMastery = modelSkinController.skins[1];


            SkinDef red = SkinRED(skinRocket);
            SkinDef blu = SkinBLUE(skinRocket);
            SkinDef redM = Rocket_MasteryRed(skinRocketMastery);
 
            //SkinCatalog.skinsByBody[(int)RocketIndex] = modelSkinController.skins;

            //0 MatRocketBackpack
            //1 MatGrenade
            //2 MatGrenade
            //3 MatRocketBombardier
            //4 MatRocketBombardier
            //5 MatShovel
            //6 MatRocket

        }

        internal static SkinDef Rocket_MasteryRed(SkinDef skinRocketAlt)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinRocketAlt_Red_1",
                nameToken = "SIMU_SKIN_ROCKET_RED_MASTER",
                icon = H.GetIcon("mod/rocket_redM"),
                original = skinRocketAlt,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material MatRocketBackpack = CloneMat(newRenderInfos, 0);
            Material MatRocketBombardier = CloneMat(newRenderInfos, 4);

            //Texture2D TexRocketBackpack = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocketBackpack.png");

            MatRocketBombardier.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/RedMaster/texrocketbombardier.png");
            //MatRocketBombardier.color = new Color(1.1f,1.1f,0.9f,1f);
            MatRocketBombardier.SetColor("_EmColor", new Color(1.2f, 1.2f, 0.5f));
            //MatRocketBombardier.color = new Color(1, 0.8f, 0.8f);
            //MatRocketBombardier.SetTexture("_EmTex", TexRocketEmissive);

            newRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //newRenderInfos[1].defaultMaterial = MatGrenade;
            //newRenderInfos[2].defaultMaterial = MatGrenade;
            newRenderInfos[3].defaultMaterial = MatRocketBombardier;
            newRenderInfos[4].defaultMaterial = MatRocketBombardier;
            //newRenderInfos[5].defaultMaterial = MatShovel;
            //newRenderInfos[6].defaultMaterial = MatRocketProjectile;

            return newSkinDef;
        }

        internal static SkinDef SkinRED(SkinDef skinRocket)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinRocket_Red_1",
                nameToken = "SIMU_SKIN_ROCKET_RED",
                icon = H.GetIcon("mod/rocket_red"),
                original = skinRocket,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material MatRocketBackpack = CloneMat(newRenderInfos, 0);
            Material MatBlackBox = CloneMat(newRenderInfos, 3);
            Material MatRocket = CloneMat(newRenderInfos, 4);
            Material MatShovel = CloneMat(newRenderInfos, 5);

            MatRocketBackpack.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocketBackpack.png");
            MatBlackBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexBlackBox.png");
            MatBlackBox.color = new Color(1, 0.85f, 0f);
            MatRocket.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocket.png");
            MatRocket.color = new Color(1, 0.8f, 0.8f);
            MatRocket.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocketEmissive.png"));
            //MatRocket.SetTexture("_EmissionMap", );
            MatShovel.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexShovel.png");

            newRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //newRenderInfos[1].defaultMaterial = GrenadeMaterial;
            //newRenderInfos[2].defaultMaterial = MatGrenade;
            newRenderInfos[3].defaultMaterial = MatBlackBox;
            newRenderInfos[4].defaultMaterial = MatRocket;
            newRenderInfos[5].defaultMaterial = MatShovel;
            newRenderInfos[6].defaultMaterial = MatRocket;

            return newSkinDef;
        }

        internal static SkinDef SkinBLUE(SkinDef skinRocket)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinRocket_Blue_1",
                nameToken = "SIMU_SKIN_ROCKET_BLUE",
                icon = H.GetIcon("mod/rocket_blue"),
                original = skinRocket,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material MatRocketBackpack = CloneMat(newRenderInfos, 0);
            Material MatBlackBox = CloneMat(newRenderInfos, 3);
            Material MatRocket = CloneMat(newRenderInfos, 4);
            Material MatShovel = CloneMat(newRenderInfos, 5);
            Material MatGrenade = CloneMat(newRenderInfos, 2);

            MatRocketBackpack.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexRocketBackpackBLUE.png");
            MatBlackBox.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexBlackBoxBLUE.png");
            MatBlackBox.color = new Color(1, 1f, 1f);
            MatRocket.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexRocketBLUE.png");
            MatRocket.color = new Color(0.8f, 1f, 0.9f);
            MatRocket.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexRocketEmissiveBLUE.png"));
            //MatRocket.SetTexture("_EmissionMap", TexRocketEmissive);
            MatRocketBackpack.color = new Color(0.8f, 1f, 0.8f);
            MatShovel.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexShovelBLUE.png");

            MatGrenade.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexGrenadeBLUE.png");

            newRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //newRenderInfos[1].defaultMaterial = GrenadeMaterial;
            newRenderInfos[2].defaultMaterial = MatGrenade;
            newRenderInfos[3].defaultMaterial = MatBlackBox;
            newRenderInfos[4].defaultMaterial = MatRocket;
            newRenderInfos[5].defaultMaterial = MatShovel;
            newRenderInfos[6].defaultMaterial = MatRocket;

            return newSkinDef;
        }


        [RegisterAchievement("CLEAR_ANY_ROCKETSURVIVOR", "Skins.RocketSurvivor.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumRocket : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RocketSurvivorBody");
            }
        }
        /*
        [RegisterAchievement("CLEAR_BOTH_ROCKETSURVIVOR", "Skins.RocketSurvivor.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumRocket2 : Achievement_AltBoss
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RocketSurvivorBody");
            }
        }
        */
    }
}
