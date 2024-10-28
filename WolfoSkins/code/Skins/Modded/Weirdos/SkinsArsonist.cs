using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsArsonist
    {
        private static SkinDef skinBlue;
        private static SkinDef skinBlueGM;

        internal static void AddProjectiles()
        {
            //artificerFireBolt //Main Attack

            int catalogIndex = ProjectileCatalog.FindProjectileIndex("strongFlare");
            Debug.Log(catalogIndex);

            GameObject FlareProjectile = ProjectileCatalog.GetProjectilePrefab(catalogIndex);
            GameObject FlareGhost = PrefabAPI.InstantiateClone(FlareProjectile.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "ChirrDartGhostPink", false);
            Material newMaterial = Object.Instantiate(FlareGhost.transform.GetChild(1).GetComponent<MeshRenderer>().material);
            newMaterial.color = new Color(0f, 0.9f, 0.6f); //0.9528 0.2735 0.0405 1
            FlareGhost.transform.GetChild(1).GetComponent<MeshRenderer>().material = newMaterial;
            FlareGhost.transform.GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0.2f, 0.7f, 0.5f, 0.8f); //0.9245 0.2588 0.1526 0.702

            skinBlue.projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
                {
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectilePrefab = FlareProjectile,
                        projectileGhostReplacementPrefab = FlareGhost,

                    }
                };
            skinBlueGM.projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
                {
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectilePrefab = FlareProjectile,
                        projectileGhostReplacementPrefab = FlareGhost,

                    }
                };


        }

        internal static void ModdedSkin(GameObject ArsonistBody)
        {
            ModdedSkinOrange(ArsonistBody);
            ModdedSkinBlue(ArsonistBody);
            ModdedSkinBlueGM(ArsonistBody);
        }


        internal static void ModdedSkinOrange(GameObject ArsonistBody)
        {
            Debug.Log("Arsonist Skins");

            BodyIndex ArsonistIndex = ArsonistBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ArsonistBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinArsonist = modelSkinController.skins[0];

            //0 matArsonist
            //1 matArsonistMetal
            //2 matArsonistMetal
            //3 matArsonistMetal
            //4 matArsonistMetal
            //5 matArsonistMetal
            //6 matArsonistMetal
            //7 null
            //8 matArsonistCloth

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinArsonist.rendererInfos.Length];
            System.Array.Copy(skinArsonist.rendererInfos, NewRenderInfos, skinArsonist.rendererInfos.Length);

            Material matArsonist = Object.Instantiate(skinArsonist.rendererInfos[0].defaultMaterial);
            Material matArsonistMetal = Object.Instantiate(skinArsonist.rendererInfos[1].defaultMaterial);
            Material matArsonistCloth = Object.Instantiate(skinArsonist.rendererInfos[8].defaultMaterial);

            Texture2D Arsonist_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/Arsonist_diffuse.png");
            Arsonist_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/ArsonistMetal_diffuse.png");
            ArsonistMetal_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/ArsonistMetal_emission.png");
            ArsonistMetal_emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistCloth_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/ArsonistCloth_diffuse.png");
            ArsonistCloth_diffuse.wrapMode = TextureWrapMode.Repeat;


            matArsonist.mainTexture = Arsonist_diffuse;

            matArsonistMetal.mainTexture = ArsonistMetal_diffuse;
            matArsonistMetal.SetTexture("_EmTex", ArsonistMetal_emission);
            matArsonistMetal.SetTexture("_EmissionMap", ArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matArsonistCloth.mainTexture = ArsonistCloth_diffuse;


            NewRenderInfos[0].defaultMaterial = matArsonist;
            NewRenderInfos[1].defaultMaterial = matArsonistMetal;
            NewRenderInfos[2].defaultMaterial = matArsonistMetal;
            NewRenderInfos[3].defaultMaterial = matArsonistMetal;
            NewRenderInfos[4].defaultMaterial = matArsonistMetal;
            NewRenderInfos[5].defaultMaterial = matArsonistMetal;
            NewRenderInfos[6].defaultMaterial = matArsonistMetal;
            //NewRenderInfos[7].defaultMaterial = null;
            NewRenderInfos[8].defaultMaterial = matArsonistCloth;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinArsonistWolfo_ORANGE_Simu",
                NameToken = "SIMU_SKIN_ARSONIST_ORANGE",
                Icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/skinArsonistIcon.png")),
                BaseSkins = new SkinDef[] { skinArsonist },
                RootObject = skinArsonist.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinArsonist.meshReplacements,
                GameObjectActivations = skinArsonist.gameObjectActivations,
                ProjectileGhostReplacements = skinArsonist.projectileGhostReplacements,
            };
            SkinDef ArsonistSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(ArsonistSkinDefNew);
            BodyCatalog.skins[(int)ArsonistIndex] = BodyCatalog.skins[(int)ArsonistIndex].Add(ArsonistSkinDefNew);
    
        }

        internal static void ModdedSkinBlue(GameObject ArsonistBody)
        {
            BodyIndex ArsonistIndex = ArsonistBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ArsonistBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinArsonist = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinArsonist.rendererInfos.Length];
            System.Array.Copy(skinArsonist.rendererInfos, NewRenderInfos, skinArsonist.rendererInfos.Length);

            Material matArsonist = Object.Instantiate(skinArsonist.rendererInfos[0].defaultMaterial);
            Material matArsonistMetal = Object.Instantiate(skinArsonist.rendererInfos[1].defaultMaterial);
            Material matArsonistCloth = Object.Instantiate(skinArsonist.rendererInfos[8].defaultMaterial);

            Texture2D Arsonist_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/Arsonist_diffuseBLUE.png");
            Arsonist_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/ArsonistMetal_diffuseBLUE.png");
            ArsonistMetal_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistMetal_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/ArsonistMetal_emissionBLUE.png");
            ArsonistMetal_emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D ArsonistCloth_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/ArsonistCloth_diffuseBLUE.png");
            ArsonistCloth_diffuse.wrapMode = TextureWrapMode.Repeat;



            matArsonist.mainTexture = Arsonist_diffuse;

            matArsonistMetal.mainTexture = ArsonistMetal_diffuse;
            matArsonistMetal.SetTexture("_EmTex", ArsonistMetal_emission);
            matArsonistMetal.SetTexture("_EmissionMap", ArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matArsonistCloth.mainTexture = ArsonistCloth_diffuse;


            NewRenderInfos[0].defaultMaterial = matArsonist;
            NewRenderInfos[1].defaultMaterial = matArsonistMetal;
            NewRenderInfos[2].defaultMaterial = matArsonistMetal;
            NewRenderInfos[3].defaultMaterial = matArsonistMetal;
            NewRenderInfos[4].defaultMaterial = matArsonistMetal;
            NewRenderInfos[5].defaultMaterial = matArsonistMetal;
            NewRenderInfos[6].defaultMaterial = matArsonistMetal;
            //NewRenderInfos[7].defaultMaterial = null;
            NewRenderInfos[8].defaultMaterial = matArsonistCloth;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinArsonistWolfo_BLUE_Simu",
                NameToken = "SIMU_SKIN_ARSONIST_BLUE",
                Icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/icon.png")),
                BaseSkins = new SkinDef[] { skinArsonist },
                RootObject = skinArsonist.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinArsonist.meshReplacements,
                GameObjectActivations = skinArsonist.gameObjectActivations,
                ProjectileGhostReplacements = skinArsonist.projectileGhostReplacements,
            };
            SkinDef ArsonistSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(ArsonistSkinDefNew);
            BodyCatalog.skins[(int)ArsonistIndex] = BodyCatalog.skins[(int)ArsonistIndex].Add(ArsonistSkinDefNew);

        }

        internal static void ModdedSkinBlueGM(GameObject ArsonistBody)
        {
            BodyIndex ArsonistIndex = ArsonistBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ArsonistBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinArsonist = modelSkinController.skins[2];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinArsonist.rendererInfos.Length];
            System.Array.Copy(skinArsonist.rendererInfos, NewRenderInfos, skinArsonist.rendererInfos.Length);

            //0 matNeoArsonistMetal
            //1 matNeoArsonistMetal
            //2 matNeoArsonistMetal
            //3 matNeoArsonistMetal
            //4 matNeoArsonistMetal
            //5 matNeoArsonistMetal
            //6 matNeoArsonistMetal
            //7 null
            //8 matArsonistCloth

            Material matNeoArsonistMetal = Object.Instantiate(skinArsonist.rendererInfos[0].defaultMaterial);
            Material matNeoArsonistCloth = Object.Instantiate(skinArsonist.rendererInfos[8].defaultMaterial);

 

            Texture2D NeoArsonistMetal_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/BlueGM/NeoArsonistMetal_diffuse.png");
            NeoArsonistMetal_diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D NeoArsonistMetal_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/BlueGM/NeoArsonistMetal_emission.png");
            NeoArsonistMetal_emission.wrapMode = TextureWrapMode.Repeat;

            Texture2D NeoArsonistCloth_diffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/BlueGM/NeoArsonistCloth_diffuse.png");
            NeoArsonistCloth_diffuse.wrapMode = TextureWrapMode.Repeat;


            matNeoArsonistMetal.mainTexture = NeoArsonistMetal_diffuse;
            matNeoArsonistMetal.SetTexture("_EmTex", NeoArsonistMetal_emission);
            matNeoArsonistMetal.SetTexture("_EmissionMap", NeoArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matNeoArsonistCloth.mainTexture = NeoArsonistCloth_diffuse;

            NewRenderInfos[0].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[1].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[2].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[3].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[4].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[5].defaultMaterial = matNeoArsonistMetal;
            NewRenderInfos[6].defaultMaterial = matNeoArsonistMetal;
            //NewRenderInfos[7].defaultMaterial = null;
            NewRenderInfos[8].defaultMaterial = matNeoArsonistCloth;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec256, WRect.half);
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinArsonistWolfo_GM_BLUE_Simu",
                NameToken = "SIMU_SKIN_ARSONIST_GM_BLUE",
                Icon = WRect.MakeIcon256(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/BlueGM/icon.png")),
                BaseSkins = new SkinDef[] { skinArsonist },
                RootObject = skinArsonist.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinArsonist.meshReplacements,
                GameObjectActivations = skinArsonist.gameObjectActivations,
                ProjectileGhostReplacements = skinArsonist.projectileGhostReplacements,
            };
            SkinDef ArsonistSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);

            modelSkinController.skins = modelSkinController.skins.Add(ArsonistSkinDefNew);
            BodyCatalog.skins[(int)ArsonistIndex] = BodyCatalog.skins[(int)ArsonistIndex].Add(ArsonistSkinDefNew);
        }


        [RegisterAchievement("CLEAR_ANY_POPCORN_ARSONIST_BODY_", "Skins.POPCORN_ARSONIST_BODY_.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumArsonistClassic : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ArsonistBody");
            }
        }

        /*[RegisterAchievement("CLEAR_BOTH_POPCORN_ARSONIST_BODY_", "Skins.POPCORN_ARSONIST_BODY_.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumArsonistClassic2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ArsonistBody");
            }
        }*/

    }
}