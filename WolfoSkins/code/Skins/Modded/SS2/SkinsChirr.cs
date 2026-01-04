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

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinChirr_Pink_1",
                nameToken = "SIMU_SKIN_CHIRR",
                icon = H.GetIcon("mod/ss2/chirr_pink"),
                original = skinChirr,
            }, new System.Action<SkinDefMakeOnApply>(Default_Pink));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinChirr_BulkDetonator_1",
                nameToken = "SIMU_SKIN_CHIRR_ORANGE",
                icon = H.GetIcon("mod/ss2/chirr_orange"),
                original = skinChirr,
            }, new System.Action<SkinDefMakeOnApply>(Default_Orange));
 
        }

 


        internal static void MakeChirrProjectile(SkinDef skin, Color color, Color trail)
        {
            int catalogIndex = ProjectileCatalog.FindProjectileIndex("ChirrLeafProjectile");
            if (catalogIndex != -1)
            {
                GameObject ChirrLeaf = ProjectileCatalog.GetProjectilePrefab(catalogIndex);

                GameObject ChirrDartGhost = R2API.PrefabAPI.InstantiateClone(ChirrLeaf.GetComponent<RoR2.Projectile.ProjectileController>().ghostPrefab, "ChirrDartGhost_Alt", false);
                Material newMaterial = Object.Instantiate(ChirrDartGhost.transform.GetChild(1).GetComponent<MeshRenderer>().material);
                newMaterial.color = color; //0.9528 0.2735 0.0405 1
                ChirrDartGhost.transform.GetChild(1).GetComponent<MeshRenderer>().material = newMaterial;
                ChirrDartGhost.transform.GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = trail; //0.9245 0.2588 0.1526 0.702

                //0.9528 0.2735 0.0405 1
                //0.9245 0.2588 0.1526 0.702

                skin.skinDefParams.projectileGhostReplacements = new SkinDefParams.ProjectileGhostReplacement[]
                {
                    new SkinDefParams.ProjectileGhostReplacement
                    {
                        projectilePrefab = ChirrLeaf,
                        projectileGhostReplacementPrefab = ChirrDartGhost,

                    }
                };
            }
        }


        internal static void Default_Pink(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material MatChirrBody = CloneMat(ref newRenderInfos, 0);
            MatChirrBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/texChirrDiffuse.png");
            MakeChirrProjectile(newSkinDef, new Color(0f, 0.9f, 0.6f), new Color(0.2f, 0.8f, 0.5f));
        }

        internal static void Default_Orange(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material MatChirrBody = CloneMat(ref newRenderInfos, 0);
            MatChirrBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Chirr/texChirrDiffuseORANGE.png");
            MakeChirrProjectile(newSkinDef, new Color(1f, 0.8f, 0.2f), new Color(0.9f, 0.75f, 0.3f)); 
        }


        [RegisterAchievement("CLEAR_ANY_CHIRR", "Skins.Chirr.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumChirr : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ChirrBody");
            }
        }

    }
}