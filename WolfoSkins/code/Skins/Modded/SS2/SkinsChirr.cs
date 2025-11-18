using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Mod
{
    public class SkinsChirr
    {
        internal static void ModdedSkin(GameObject ChirrBody)
        {
            BodyIndex ChirrIndex = ChirrBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ChirrBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinChirr = modelSkinController.skins[0];

            Debug.Log("Chirr Skins");
            SkinDef pink = SkinsPink(skinChirr);
            SkinDef orange = SkinsORANGE(skinChirr);


            //SkinCatalog.skinsByBody[(int)ChirrIndex] = modelSkinController.skins;
        }

        private static SkinDef skinDefPink;
        private static SkinDef skinDefOrange;


        internal static void AddProjectiles()
        {
            int catalogIndex = ProjectileCatalog.FindProjectileIndex("ChirrLeafProjectile");
            Debug.Log(catalogIndex);

            if (catalogIndex == -1)
            {
                Debug.LogWarning("NO Chirr Projectile");
                return;
            }

            GameObject ChirrLeaf = ProjectileCatalog.GetProjectilePrefab(catalogIndex);
            GameObject LeafGhost = R2API.PrefabAPI.InstantiateClone(ChirrLeaf.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "ChirrDartGhostPink", false);
            Material newMaterial = Object.Instantiate(LeafGhost.transform.GetChild(1).GetComponent<MeshRenderer>().material);
            newMaterial.color = new Color(0f, 0.9f, 0.6f); //0.9528 0.2735 0.0405 1
            LeafGhost.transform.GetChild(1).GetComponent<MeshRenderer>().material = newMaterial;
            LeafGhost.transform.GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0.2f, 0.7f, 0.5f, 0.8f); //0.9245 0.2588 0.1526 0.702

            skinDefPink.projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
                {
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectilePrefab = ChirrLeaf,
                        projectileGhostReplacementPrefab = LeafGhost,

                    }
                };

            GameObject LeafGhostORANGE = R2API.PrefabAPI.InstantiateClone(ChirrLeaf.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "ChirrDartGhostYellow", false);
            newMaterial = Object.Instantiate(LeafGhostORANGE.transform.GetChild(1).GetComponent<MeshRenderer>().material);
            newMaterial.color = new Color(1f, 0.9f, 0.4f); //0.9528 0.2735 0.0405 1
            LeafGhostORANGE.transform.GetChild(1).GetComponent<MeshRenderer>().material = newMaterial;
            LeafGhostORANGE.transform.GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0.9f, 0.8f, 0.5f, 0.8f); //0.9245 0.2588 0.1526 0.702

            skinDefOrange.projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
                {
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectilePrefab = ChirrLeaf,
                        projectileGhostReplacementPrefab = LeafGhostORANGE,

                    }
                };
            /*
            ProjectileGhostReplacementManager.SkinGhostPair skinGhostPair1 = new ProjectileGhostReplacementManager.SkinGhostPair
            {
                projectileGhost = LeafGhost,
                skinDef = skinDefPink
            };
            ProjectileGhostReplacementManager.SkinGhostPair skinGhostPair2 = new ProjectileGhostReplacementManager.SkinGhostPair
            {
                projectileGhost = LeafGhostORANGE,
                skinDef = skinDefOrange
            };

            if (ProjectileGhostReplacementManager.projectileToSkinGhostPairs[catalogIndex] == null)
            {
                ProjectileGhostReplacementManager.projectileToSkinGhostPairs[catalogIndex] = System.Array.Empty<ProjectileGhostReplacementManager.SkinGhostPair>();
            }
            HG.ArrayUtils.ArrayAppend<ProjectileGhostReplacementManager.SkinGhostPair>(ref ProjectileGhostReplacementManager.projectileToSkinGhostPairs[catalogIndex], skinGhostPair1);
            HG.ArrayUtils.ArrayAppend<ProjectileGhostReplacementManager.SkinGhostPair>(ref ProjectileGhostReplacementManager.projectileToSkinGhostPairs[catalogIndex], skinGhostPair2);
            */
        }


        internal static SkinDef SkinsPink(SkinDef skinChirr)
        {
            SkinDefAltColor newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinChirr_Pink_1",
                nameToken = "SIMU_SKIN_CHIRR",
                icon = H.GetIcon("mod/ss2/chirr_pink"),
                original = skinChirr,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material MatChirrBody = CloneMat(newRenderInfos, 0);
            MatChirrBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/texChirrDiffuse.png");
            return newSkinDef;
        }

        internal static SkinDef SkinsORANGE(SkinDef skinChirr)
        {
            SkinDefAltColor newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinChirr_BulkDetonator_1",
                nameToken = "SIMU_SKIN_CHIRR",
                icon = H.GetIcon("mod/ss2/chirr_orange"),
                original = skinChirr,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material MatChirrBody = CloneMat(newRenderInfos, 0);
            MatChirrBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/texChirrDiffuseORANGE.png");
            return newSkinDef;
        }


        [RegisterAchievement("CLEAR_ANY_CHIRR", "Skins.Chirr.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumChirr : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChirrBody");
            }
        }

    }
}