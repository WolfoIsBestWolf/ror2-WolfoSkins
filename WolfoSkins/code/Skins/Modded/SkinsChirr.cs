using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsChirr
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            LanguageAPI.Add("SIMU_SKIN_CHIRR", "Bloom");
            LanguageAPI.Add("SIMU_SKIN_CHIRR_ORANGE", "Detonator");

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHIRR_NAME", "Chirr: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHIRR_DESCRIPTION", "As Chirr" + Unlocks.unlockCondition);

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CHIRR_NAME";
            unlockableDef.cachedName = "Skins.Chirr.Wolfo";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.skinIconChirr);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
        }

        internal static void ModdedSkin(GameObject ChirrBody)
        {
            Debug.Log("Chirr Skins");
            unlockableDef.hidden = false;
            SkinsPink(ChirrBody);
            SkinsORANGE(ChirrBody);

            GameModeCatalog.availability.CallWhenAvailable(AddVFXLate);
            //On.RoR2.ProjectileGhostReplacementManager.Init += ProjectileGhostReplacementManager_Init;
        }

        private static SkinDef skinDefPink;
        private static SkinDef skinDefOrange;


        internal static void AddVFXLate()
        {
            int catalogIndex = ProjectileCatalog.FindProjectileIndex("ChirrLeafProjectile");
            Debug.Log(catalogIndex);
            
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
        }


        internal static void SkinsPink(GameObject ChirrBody)
        {
            BodyIndex ChirrIndex = ChirrBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ChirrBody.GetComponentInChildren<ModelSkinController>();
            SkinDef skinChirr = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChirr.rendererInfos.Length];
            System.Array.Copy(skinChirr.rendererInfos, NewRenderInfos, skinChirr.rendererInfos.Length);

            Material MatChirrBody = Object.Instantiate(skinChirr.rendererInfos[0].defaultMaterial);

            Texture2D texChirrDiffuse = new Texture2D(2048, 2048, TextureFormat.RGBA32, false);
            texChirrDiffuse.LoadImage(Properties.Resources.texChirrDiffuse, true);
            texChirrDiffuse.filterMode = FilterMode.Bilinear;
            texChirrDiffuse.wrapMode = TextureWrapMode.Clamp;

            MatChirrBody.mainTexture = texChirrDiffuse;

            NewRenderInfos[0].defaultMaterial = MatChirrBody;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconChirr, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinChirrWolfo_Pink",
                NameToken = "SIMU_SKIN_CHIRR",
                Icon = SkinIconS,
                BaseSkins = skinChirr.baseSkins,
                RootObject = skinChirr.rootObject,
                UnlockableDef = unlockableDef,
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

            Texture2D texChirrDiffuse = new Texture2D(2048, 2048, TextureFormat.RGBA32, false);
            texChirrDiffuse.LoadImage(Properties.Resources.texChirrDiffuseORANGE, true);
            texChirrDiffuse.filterMode = FilterMode.Bilinear;
            texChirrDiffuse.wrapMode = TextureWrapMode.Clamp;

            MatChirrBody.mainTexture = texChirrDiffuse;

            NewRenderInfos[0].defaultMaterial = MatChirrBody;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconChirrORANGE, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinChirrWolfo_ORANGE",
                NameToken = "SIMU_SKIN_CHIRR_ORANGE",
                Icon = SkinIconS,
                BaseSkins = skinChirr.baseSkins,
                RootObject = skinChirr.rootObject,
                UnlockableDef = unlockableDef,
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


        [RegisterAchievement("SIMU_SKIN_CHIRR", "Skins.Chirr.Wolfo", null, null)]
        public class ClearSimulacrumChirr : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChirrBody");
            }
        }

    }
}