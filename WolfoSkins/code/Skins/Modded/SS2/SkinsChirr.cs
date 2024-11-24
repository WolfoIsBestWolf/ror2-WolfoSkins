using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsChirr
    {
        internal static void ModdedSkin(GameObject ChirrBody)
        {
            Debug.Log("Chirr Skins");
            SkinsPink(ChirrBody);
            SkinsORANGE(ChirrBody);

            //GameModeCatalog.availability.CallWhenAvailable(AddProjectiles);
            //On.RoR2.ProjectileGhostReplacementManager.Init += ProjectileGhostReplacementManager_Init;
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
            GameObject LeafGhost = PrefabAPI.InstantiateClone(ChirrLeaf.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "ChirrDartGhostPink", false);
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
            
            GameObject LeafGhostORANGE = PrefabAPI.InstantiateClone(ChirrLeaf.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "ChirrDartGhostYellow", false);
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


        internal static void SkinsPink(GameObject ChirrBody)
        {
            BodyIndex ChirrIndex = ChirrBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ChirrBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinChirr = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChirr.rendererInfos.Length];
            System.Array.Copy(skinChirr.rendererInfos, NewRenderInfos, skinChirr.rendererInfos.Length);

            Material MatChirrBody = Object.Instantiate(skinChirr.rendererInfos[0].defaultMaterial);

            Texture2D texChirrDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/texChirrDiffuse.png");
            texChirrDiffuse.wrapMode = TextureWrapMode.Clamp;

            MatChirrBody.mainTexture = texChirrDiffuse;

            NewRenderInfos[0].defaultMaterial = MatChirrBody;
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinChirrWolfo_Pink_Simu",
                NameToken = "SIMU_SKIN_CHIRR",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/skinIconChirr.png")),
                BaseSkins = skinChirr.baseSkins,
                RootObject = skinChirr.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinChirr.meshReplacements,
                GameObjectActivations = skinChirr.gameObjectActivations,
            };

            SkinDef ChirrSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);
            skinDefPink = ChirrSkinDefNew;
            modelSkinController.skins = modelSkinController.skins.Add(ChirrSkinDefNew);
            BodyCatalog.skins[(int)ChirrIndex] = BodyCatalog.skins[(int)ChirrIndex].Add(ChirrSkinDefNew);
        }

        internal static void SkinsORANGE(GameObject ChirrBody)
        {
            BodyIndex ChirrIndex = ChirrBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ChirrBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinChirr = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChirr.rendererInfos.Length];
            System.Array.Copy(skinChirr.rendererInfos, NewRenderInfos, skinChirr.rendererInfos.Length);

            Material MatChirrBody = Object.Instantiate(skinChirr.rendererInfos[0].defaultMaterial);

            Texture2D texChirrDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/texChirrDiffuseORANGE.png");
            texChirrDiffuse.wrapMode = TextureWrapMode.Clamp;

            MatChirrBody.mainTexture = texChirrDiffuse;

            NewRenderInfos[0].defaultMaterial = MatChirrBody;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinChirrWolfo_ORANGE_Simu",
                NameToken = "SIMU_SKIN_CHIRR_ORANGE",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/skinIconChirrORANGE.png")),
                BaseSkins = skinChirr.baseSkins,
                RootObject = skinChirr.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinChirr.meshReplacements,
                GameObjectActivations = skinChirr.gameObjectActivations,
                ProjectileGhostReplacements = skinChirr.projectileGhostReplacements,
            };

            SkinDef ChirrSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);
            skinDefOrange = ChirrSkinDefNew;
            modelSkinController.skins = modelSkinController.skins.Add(ChirrSkinDefNew);
            BodyCatalog.skins[(int)ChirrIndex] = BodyCatalog.skins[(int)ChirrIndex].Add(ChirrSkinDefNew);
        }


        [RegisterAchievement("CLEAR_ANY_CHIRR", "Skins.Chirr.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumChirr : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChirrBody");
            }
        }

    }
}