using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsRocket
    {
        internal static void ModdedSkin(GameObject RocketBody)
        {
            Debug.Log("Rocket Skins");
            SkinRED(RocketBody);
            SkinBLUE(RocketBody);
            Rocket_MasteryRed(RocketBody);
        }

        internal static void Rocket_MasteryRed(GameObject RocketBody)
        {
            BodyIndex RocketIndex = RocketBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RocketBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinRocket = modelSkinController.skins[1];

            //0 MatRocketBackpack
            //1 MatGrenade
            //2 MatGrenade
            //3 MatRocketBombardier
            //4 MatRocketBombardier
            //5 MatShovel
            //6 MatRocket

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRocket.rendererInfos.Length];
            System.Array.Copy(skinRocket.rendererInfos, NewRenderInfos, skinRocket.rendererInfos.Length);

            Material MatRocketBackpack = Object.Instantiate(skinRocket.rendererInfos[0].defaultMaterial);
            Material MatRocketBombardier = Object.Instantiate(skinRocket.rendererInfos[4].defaultMaterial);
            
            Texture2D TexRocketBackpack = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocketBackpack.png");
            TexRocketBackpack.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocket = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/RedMaster/texrocketbombardier.png");
            TexRocket.wrapMode = TextureWrapMode.Repeat;
 

            MatRocketBombardier.mainTexture = TexRocket;
            //MatRocketBombardier.color = new Color(1.1f,1.1f,0.9f,1f);
            MatRocketBombardier.SetColor("_EmColor", new Color(1.2f, 1.2f, 0.5f));
            //MatRocketBombardier.color = new Color(1, 0.8f, 0.8f);
            //MatRocketBombardier.SetTexture("_EmTex", TexRocketEmissive);

 

            //
            NewRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //NewRenderInfos[1].defaultMaterial = MatGrenade;
            //NewRenderInfos[2].defaultMaterial = MatGrenade;
            NewRenderInfos[3].defaultMaterial = MatRocketBombardier;
            NewRenderInfos[4].defaultMaterial = MatRocketBombardier;
            //NewRenderInfos[5].defaultMaterial = MatShovel;
            //NewRenderInfos[6].defaultMaterial = MatRocketProjectile;
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinRocketAltWolfo_Red_Simu",
                NameToken = "SIMU_SKIN_ROCKET_RED_MASTER",
                Icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/RedMaster/texrocketskinmastery.png")),
                BaseSkins = new SkinDef[] { skinRocket },
                RootObject = skinRocket.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinRocket.meshReplacements,
                GameObjectActivations = skinRocket.gameObjectActivations,
                ProjectileGhostReplacements = skinRocket.projectileGhostReplacements,
            };
            SkinDef RocketSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(RocketSkinDefNew);
            BodyCatalog.skins[(int)RocketIndex] = BodyCatalog.skins[(int)RocketIndex].Add(RocketSkinDefNew);
        }

        internal static void SkinRED(GameObject RocketBody)
        {
            BodyIndex RocketIndex = RocketBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RocketBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinRocket = modelSkinController.skins[0];

            //0 MatRocketBackpack
            //1 GrenadeMaterial (no texture)
            //2 MatGrenade
            //3 MatBlackBox
            //4 MatRocket
            //5 MatShovel
            //6 MatRocket

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRocket.rendererInfos.Length];
            System.Array.Copy(skinRocket.rendererInfos, NewRenderInfos, skinRocket.rendererInfos.Length);

            Material MatRocketBackpack = Object.Instantiate(skinRocket.rendererInfos[0].defaultMaterial);
            Material MatBlackBox = Object.Instantiate(skinRocket.rendererInfos[3].defaultMaterial);
            Material MatRocket = Object.Instantiate(skinRocket.rendererInfos[4].defaultMaterial);
            Material MatShovel = Object.Instantiate(skinRocket.rendererInfos[5].defaultMaterial);

            Texture2D TexRocketBackpack = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocketBackpack.png");
            TexRocketBackpack.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexBlackBox = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexBlackBox.png");
            TexBlackBox.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocket = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocket.png");
            TexRocket.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocketEmissive = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexRocketEmissive.png");
            TexRocketEmissive.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexShovel = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/TexShovel.png");
            TexShovel.wrapMode = TextureWrapMode.Repeat;


            MatRocketBackpack.mainTexture = TexRocketBackpack;
            MatBlackBox.mainTexture = TexBlackBox;
            MatBlackBox.color = new Color(1, 0.85f, 0f);
            MatRocket.mainTexture = TexRocket;
            MatRocket.color = new Color(1, 0.8f, 0.8f);
            MatRocket.SetTexture("_EmTex", TexRocketEmissive);
            MatRocket.SetTexture("_EmissionMap", TexRocketEmissive);
            MatShovel.mainTexture = TexShovel;

            //
            NewRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //NewRenderInfos[1].defaultMaterial = GrenadeMaterial;
            //NewRenderInfos[2].defaultMaterial = MatGrenade;
            NewRenderInfos[3].defaultMaterial = MatBlackBox;
            NewRenderInfos[4].defaultMaterial = MatRocket;
            NewRenderInfos[5].defaultMaterial = MatShovel;
            NewRenderInfos[6].defaultMaterial = MatRocket;
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinRocketWolfo_Red_Simu",
                NameToken = "SIMU_SKIN_ROCKET_RED",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Red/texRocketSkinDefault.png")),
                BaseSkins = new SkinDef[] { skinRocket },
                RootObject = skinRocket.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinRocket.meshReplacements,
                GameObjectActivations = skinRocket.gameObjectActivations,
                ProjectileGhostReplacements = skinRocket.projectileGhostReplacements,
            };
            SkinDef RocketSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(RocketSkinDefNew);
            BodyCatalog.skins[(int)RocketIndex] = BodyCatalog.skins[(int)RocketIndex].Add(RocketSkinDefNew);
        }

        internal static void SkinBLUE(GameObject RocketBody)
        {
            BodyIndex RocketIndex = RocketBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RocketBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinRocket = modelSkinController.skins[0];

            //0 MatRocketBackpack
            //1 GrenadeMaterial (no texture)
            //2 MatGrenade
            //3 MatBlackBox
            //4 MatRocket
            //5 MatShovel
            //6 MatRocket

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRocket.rendererInfos.Length];
            System.Array.Copy(skinRocket.rendererInfos, NewRenderInfos, skinRocket.rendererInfos.Length);

            Material MatRocketBackpack = Object.Instantiate(skinRocket.rendererInfos[0].defaultMaterial);
            Material MatBlackBox = Object.Instantiate(skinRocket.rendererInfos[3].defaultMaterial);
            Material MatRocket = Object.Instantiate(skinRocket.rendererInfos[4].defaultMaterial);
            Material MatShovel = Object.Instantiate(skinRocket.rendererInfos[5].defaultMaterial);
            Material MatGrenade = Object.Instantiate(skinRocket.rendererInfos[2].defaultMaterial);

            Texture2D TexRocketBackpack = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexRocketBackpackBLUE.png");
            TexRocketBackpack.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexBlackBox = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexBlackBoxBLUE.png");
            TexBlackBox.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocket = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexRocketBLUE.png");
            TexRocket.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexRocketEmissive = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexRocketEmissiveBLUE.png");
            TexRocketEmissive.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexShovel = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexShovelBLUE.png");
            TexShovel.wrapMode = TextureWrapMode.Repeat;

            Texture2D TexGrenade = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/TexGrenadeBLUE.png");
            TexGrenade.wrapMode = TextureWrapMode.Repeat;


            MatRocketBackpack.mainTexture = TexRocketBackpack;
            MatBlackBox.mainTexture = TexBlackBox;
            MatBlackBox.color = new Color(1, 1f, 1f);
            MatRocket.mainTexture = TexRocket;
            MatRocket.color = new Color(0.8f, 1f, 0.9f);
            MatRocket.SetTexture("_EmTex", TexRocketEmissive);
            MatRocket.SetTexture("_EmissionMap", TexRocketEmissive);
            MatRocketBackpack.color = new Color(0.8f, 1f, 0.8f);
            MatShovel.mainTexture = TexShovel;

            MatGrenade.mainTexture = TexGrenade;

            //
            NewRenderInfos[0].defaultMaterial = MatRocketBackpack;
            //NewRenderInfos[1].defaultMaterial = GrenadeMaterial;
            NewRenderInfos[2].defaultMaterial = MatGrenade;
            NewRenderInfos[3].defaultMaterial = MatBlackBox;
            NewRenderInfos[4].defaultMaterial = MatRocket;
            NewRenderInfos[5].defaultMaterial = MatShovel;
            NewRenderInfos[6].defaultMaterial = MatRocket;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinRocketWolfo_Blue_Simu",
                NameToken = "SIMU_SKIN_ROCKET_BLUE",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Rocket/Blue/texRocketSkinDefaultBLUE.png")),
                BaseSkins = new SkinDef[] { skinRocket },
                RootObject = skinRocket.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinRocket.meshReplacements,
                GameObjectActivations = skinRocket.gameObjectActivations,
                ProjectileGhostReplacements = skinRocket.projectileGhostReplacements,
            };
            SkinDef RocketSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(RocketSkinDefNew);
            BodyCatalog.skins[(int)RocketIndex] = BodyCatalog.skins[(int)RocketIndex].Add(RocketSkinDefNew);
        }


        [RegisterAchievement("CLEAR_ANY_ROCKETSURVIVOR", "Skins.RocketSurvivor.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumRocket : Achievement_AltBoss_Simu
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
