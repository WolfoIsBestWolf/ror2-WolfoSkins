using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Mod
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
            GameObject FlareGhost = R2API.PrefabAPI.InstantiateClone(FlareProjectile.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "ChirrDartGhostPink", false);
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
            Debug.Log("Arsonist Skins");

            BodyIndex ArsonistIndex = ArsonistBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ArsonistBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinArsonist = modelSkinController.skins[0];
            SkinDef skinArsonistGM = modelSkinController.skins[2];

            SkinDef orange = ModdedSkinOrange(skinArsonist);
            SkinDef blue = ModdedSkinBlue(skinArsonist);
            SkinDef blueGM = ModdedSkinBlueGM(skinArsonistGM);

 
            //SkinCatalog.skinsByBody[(int)ArsonistIndex] = modelSkinController.skins;

            //0 matArsonist
            //1 matArsonistMetal
            //2 matArsonistMetal
            //3 matArsonistMetal
            //4 matArsonistMetal
            //5 matArsonistMetal
            //6 matArsonistMetal
            //7 null
            //8 matArsonistCloth

            //0 matNeoArsonistMetal
            //1 matNeoArsonistMetal
            //2 matNeoArsonistMetal
            //3 matNeoArsonistMetal
            //4 matNeoArsonistMetal
            //5 matNeoArsonistMetal
            //6 matNeoArsonistMetal
            //7 null
            //8 matArsonistCloth
        }


        internal static SkinDef ModdedSkinOrange(SkinDef skinArsonist)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinArsonist_ORANGE_1",
                nameToken = "SIMU_SKIN_ARSONIST_ORANGE",
                icon = H.GetIcon("mod/arsonist_orange"),
                original = skinArsonist,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matArsonist = CloneMat(newRenderInfos, 0);
            Material matArsonistMetal = CloneMat(newRenderInfos, 1);
            Material matArsonistCloth = CloneMat(newRenderInfos, 8);

            Texture2D ArsonistMetal_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/ArsonistMetal_emission.png");

            matArsonist.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/Arsonist_diffuse.png");

            matArsonistMetal.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/ArsonistMetal_diffuse.png");
            matArsonistMetal.SetTexture("_EmTex", ArsonistMetal_emission);
            matArsonistMetal.SetTexture("_EmissionMap", ArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matArsonistCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Orange/ArsonistCloth_diffuse.png");


            newRenderInfos[0].defaultMaterial = matArsonist;
            newRenderInfos[1].defaultMaterial = matArsonistMetal;
            newRenderInfos[2].defaultMaterial = matArsonistMetal;
            newRenderInfos[3].defaultMaterial = matArsonistMetal;
            newRenderInfos[4].defaultMaterial = matArsonistMetal;
            newRenderInfos[5].defaultMaterial = matArsonistMetal;
            newRenderInfos[6].defaultMaterial = matArsonistMetal;
            //newRenderInfos[7].defaultMaterial = null;
            newRenderInfos[8].defaultMaterial = matArsonistCloth;

            return newSkinDef;
        }

        internal static SkinDef ModdedSkinBlue(SkinDef skinArsonist)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinArsonist_BLUE_1",
                nameToken = "SIMU_SKIN_ARSONIST_BLUE",
                icon = H.GetIcon("mod/arsonist_blue"),
                original = skinArsonist,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matArsonist = CloneMat(newRenderInfos, 0);
            Material matArsonistMetal = CloneMat(newRenderInfos, 1);
            Material matArsonistCloth = CloneMat(newRenderInfos, 8);

            Texture2D ArsonistMetal_emission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/ArsonistMetal_emissionBLUE.png");

            matArsonist.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/Arsonist_diffuseBLUE.png");

            matArsonistMetal.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/ArsonistMetal_diffuseBLUE.png");
            matArsonistMetal.SetTexture("_EmTex", ArsonistMetal_emission);
            matArsonistMetal.SetTexture("_EmissionMap", ArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matArsonistCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/Blue/ArsonistCloth_diffuseBLUE.png");


            newRenderInfos[0].defaultMaterial = matArsonist;
            newRenderInfos[1].defaultMaterial = matArsonistMetal;
            newRenderInfos[2].defaultMaterial = matArsonistMetal;
            newRenderInfos[3].defaultMaterial = matArsonistMetal;
            newRenderInfos[4].defaultMaterial = matArsonistMetal;
            newRenderInfos[5].defaultMaterial = matArsonistMetal;
            newRenderInfos[6].defaultMaterial = matArsonistMetal;
            //newRenderInfos[7].defaultMaterial = null;
            newRenderInfos[8].defaultMaterial = matArsonistCloth;

            return newSkinDef;
        }

        internal static SkinDef ModdedSkinBlueGM(SkinDef skinArsonist)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinArsonist_GM_BLUE_1",
                nameToken = "SIMU_SKIN_ARSONIST_GM_BLUE",
                icon = H.GetIcon("mod/arsonist_blueGM"),
                original = skinArsonist,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matNeoArsonistMetal = CloneMat(newRenderInfos, 0);
            Material matNeoArsonistCloth = CloneMat(newRenderInfos, 8);

            matNeoArsonistMetal.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/BlueGM/NeoArsonistMetal_diffuse.png");
            matNeoArsonistMetal.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/BlueGM/NeoArsonistMetal_emission.png"));
            //matNeoArsonistMetal.SetTexture("_EmissionMap", NeoArsonistMetal_emission);
            //matArsonistMetal.SetColor("_EmColor", new Color(1f, 1f, 1f));

            matNeoArsonistCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Arsonist/BlueGM/NeoArsonistCloth_diffuse.png");

            newRenderInfos[0].defaultMaterial = matNeoArsonistMetal;
            newRenderInfos[1].defaultMaterial = matNeoArsonistMetal;
            newRenderInfos[2].defaultMaterial = matNeoArsonistMetal;
            newRenderInfos[3].defaultMaterial = matNeoArsonistMetal;
            newRenderInfos[4].defaultMaterial = matNeoArsonistMetal;
            newRenderInfos[5].defaultMaterial = matNeoArsonistMetal;
            newRenderInfos[6].defaultMaterial = matNeoArsonistMetal;
            //newRenderInfos[7].defaultMaterial = null;
            newRenderInfos[8].defaultMaterial = matNeoArsonistCloth;
            //
            return newSkinDef;
        }


        [RegisterAchievement("CLEAR_ANY_POPCORN_ARSONIST_BODY_", "Skins.POPCORN_ARSONIST_BODY_.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumArsonistClassic : Achievement_ONE_THINGS
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