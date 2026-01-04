using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsCommando
    {
        public static GameObject CommandoDashJetsBlue = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoDashJets.prefab").WaitForCompletion(), "CommandoDashJetsBlue", false);


        internal static void Start()
        {
            SkinDef skinCommandoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Commando/skinCommandoAlt.asset").WaitForCompletion();
            SkinDef skinCommandoAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Commando/skinCommandoAltColossus.asset").WaitForCompletion();

            Unused_SotVSkin();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinCommandoWolfoProvi_1",
                nameToken = "SIMU_SKIN_COMMANDO",
                icon = H.GetIcon("base/commando_provi"),
                original = skinCommandoAlt,
            }, new System.Action<SkinDefMakeOnApply>(CommandoSkin_Provi));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                nameToken = "SIMU_SKIN_COMMANDO_COLOSSUS",
                name = "skinCommandoAltColossus_DLC2",
                icon = H.GetIcon("base/commando_dlc2"),
                original = skinCommandoAltColossus,
            }, new System.Action<SkinDefMakeOnApply>(Commando_AltColossus));


            On.EntityStates.Commando.DodgeState.OnEnter += DodgeState_OnEnter;
            On.EntityStates.Commando.SlideState.OnEnter += SlideState_OnEnter;
        }

        internal static void Commando_AltColossus(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matCommandoAltColossus = CloneMat(ref newRenderInfos, 2);

            matCommandoAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/Colossus/texCommandoAltColossusDiffuse.png");
            matCommandoAltColossus.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/Colossus/texRampBeetleQueen.png")); //0.8491 0.5267 0.1402 1
            matCommandoAltColossus.SetFloat("_FlowEmissionStrength", 2);

            newRenderInfos[0].defaultMaterial = matCommandoAltColossus;
            newRenderInfos[1].defaultMaterial = matCommandoAltColossus;
            newRenderInfos[2].defaultMaterial = matCommandoAltColossus;

            GameObject CommandoGrenadeProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeProjectile.prefab").WaitForCompletion();
            GameObject CommandoGrenadeGhost = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeGhost.prefab").WaitForCompletion(), "CommandoGrenadeGhostPROVI", false);

            CommandoGrenadeGhost.transform.GetChild(0).GetComponent<MeshRenderer>().material = matCommandoAltColossus;
            CommandoGrenadeGhost.transform.GetChild(4).GetComponent<Light>().color = new Color(0.34f, 0.67f, 1f);//0.8679 0.6413 0.2334 1

            newSkinDef.skinDefParams.projectileGhostReplacements = new SkinDefParams.ProjectileGhostReplacement[]
            {
                new SkinDefParams.ProjectileGhostReplacement
                {
                    projectilePrefab = CommandoGrenadeProjectile,
                    projectileGhostReplacementPrefab = CommandoGrenadeGhost,
                }
            };

        }

        #region Colossus Red Idea
        /*
        internal static void Commando_AltColossusRed(SkinDef skinCommandoAltColossus)
        {
   
            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[skinCommandoAltColossus.rendererInfos.Length];
            System.Array.Copy(skinCommandoAltColossus.rendererInfos, newRenderInfos, skinCommandoAltColossus.rendererInfos.Length);

            Material matCommandoAltColossus = CloneMat(ref CommandoAltColossus.rendererInfos[2].defaultMaterial);


            Texture2D texCommandoAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/ColossusRed/texCommandoAltColossusDiffuse.png");
            texCommandoAltColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampBeetleQueen = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/ColossusRed/texRampBeetleQueen.png");
            texRampBeetleQueen.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCommandoAltColossusEmissive = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/ColossusRed/texCommandoAltColossusEmissive.png");
            texCommandoAltColossusEmissive.wrapMode = TextureWrapMode.Clamp;


            matCommandoAltColossus.mainTexture = texCommandoAltColossusDiffuse;
            //matCommandoAltColossus.SetTexture("_FlowHeightmap", texCommandoAltColossusEmissive);
            matCommandoAltColossus.SetTexture("_FlowHeightRamp", texRampBeetleQueen); //0.8491 0.5267 0.1402 1
            matCommandoAltColossus.SetFloat("_FlowEmissionStrength", 2);

            newRenderInfos[0].defaultMaterial = matCommandoAltColossus;
            newRenderInfos[1].defaultMaterial = matCommandoAltColossus;
            newRenderInfos[2].defaultMaterial = matCommandoAltColossus;
            //



            GameObject CommandoGrenadeProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeProjectile.prefab").WaitForCompletion();
            GameObject CommandoGrenadeGhost = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeGhost.prefab").WaitForCompletion(), "CommandoGrenadeGhostPROVI", false);

            CommandoGrenadeGhost.transform.GetChild(0).GetComponent<MeshRenderer>().material = matCommandoAltColossus;
            CommandoGrenadeGhost.transform.GetChild(4).GetComponent<Light>().color = new Color(0.34f, 0.67f, 1f);//0.8679 0.6413 0.2334 1

            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinCommandoAltColossusWolfoRed_DLC2",
                NameToken = "SIMU_SKIN_COMMANDO_COLOSSUSRED",
                Icon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/ColossusRed/commando.png")),
                BaseSkins = skinCommandoAltColossus.baseSkins,
                MeshReplacements = skinCommandoAltColossus.meshReplacements,
                RendererInfos = newRenderInfos,
                RootObject = skinCommandoAltColossus.rootObject,
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
                {
                    new SkinDef.ProjectileGhostReplacement
                    {
                        projectilePrefab = CommandoGrenadeProjectile,
                        projectileGhostReplacementPrefab = CommandoGrenadeGhost,
                    }
                },
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), SkinInfo);
        }


        */
        #endregion

        internal static void Unused_SotVSkin()
        {
            SkinDef UnusedCommandoSkin = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion();
            H.AddSkinToCharacterPublic(UnusedCommandoSkin);

            //This removes Limb removal, not good but better than bugged ones I guess
            //I have no idea how to actually fix his Right Calf being both Calfs
            Material matCommandoDualiesMarine = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matCommandoDualiesMarine.mat").WaitForCompletion();
            matCommandoDualiesMarine.DisableKeyword("LIMBREMOVAL");

            UnusedCommandoSkin.nameToken = "SIMU_SKIN_COMMANDO_SOTV";

        }

        internal static void CommandoSkin_Provi(SkinDefMakeOnApply newSkinDef)
        {

            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matCommandoDualiesAlt = CloneMat(ref newRenderInfos, 2);
            Material matCommandoDualiesAltGUN = CloneMat(ref newRenderInfos, 2);

            matCommandoDualiesAlt.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/texCommandoPaletteDiffuse.png");
            //matCommandoDualiesAlt.SetColor("_EmColor", new Color(0.15f, 0.775f, 0.96f)); //0.8491 0.5267 0.1402 1
            matCommandoDualiesAlt.color = new Color(1.1f, 1f, 0.8f);
            matCommandoDualiesAlt.SetColor("_EmColor", new Color(0.2f, 0.9f, 0.9f)); //0.8491 0.5267 0.1402 1

            matCommandoDualiesAltGUN.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/texCommandoPaletteDiffuseGUN.png");
            matCommandoDualiesAltGUN.color = new Color(1.2f, 1.4f, 1.4f);
            matCommandoDualiesAltGUN.SetColor("_EmColor", new Color(1f, 2f, 3f));
            //matCommandoDualiesAltGUN.SetTexture("_EmTex", null);

            newRenderInfos[0].defaultMaterial = matCommandoDualiesAltGUN;
            newRenderInfos[1].defaultMaterial = matCommandoDualiesAltGUN;
            newRenderInfos[2].defaultMaterial = matCommandoDualiesAlt;


            GameObject CommandoGrenadeProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeProjectile.prefab").WaitForCompletion();
            GameObject CommandoGrenadeGhost = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeGhost.prefab").WaitForCompletion(), "CommandoGrenadeGhostPROVI", false);

            CommandoGrenadeGhost.transform.GetChild(0).GetComponent<MeshRenderer>().material = matCommandoDualiesAlt;
            CommandoGrenadeGhost.transform.GetChild(4).GetComponent<Light>().color = new Color(0.34f, 0.67f, 1f);//0.8679 0.6413 0.2334 1


            CommandoDashJetsBlue.GetComponent<Light>().color = new Color(0, 0.7f, 1f); //1 0.7045 0.051 1
            CommandoDashJetsBlue.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 0.425f, 1f);

            var colorOverLifetime = CommandoDashJetsBlue.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
            colorOverLifetime.enabled = false;

            CommandoDashJetsBlue.transform.GetChild(3).GetComponent<ParticleSystem>().startColor = new Color(0, 0.3f, 0.3f); //0.2925 0.2922 0 1
            CommandoDashJetsBlue.transform.GetChild(4).GetComponent<ParticleSystem>().startColor = new Color(0.3f, 0.8f, 1f, 1); //1 0.4941 0.3098 1



            newSkinDef.skinDefParams.projectileGhostReplacements = new SkinDefParams.ProjectileGhostReplacement[]
               {
                    new SkinDefParams.ProjectileGhostReplacement
                    {
                        projectilePrefab = CommandoGrenadeProjectile,
                        projectileGhostReplacementPrefab = CommandoGrenadeGhost,
                    }
               };



        }

        private static void SlideState_OnEnter(On.EntityStates.Commando.SlideState.orig_OnEnter orig, EntityStates.Commando.SlideState self)
        {
            if (self.modelLocator.modelTransform.GetComponent<SkinDefAltColorTracker>())
            {
                GameObject jetOg = EntityStates.Commando.SlideState.jetEffectPrefab;
                EntityStates.Commando.SlideState.jetEffectPrefab = CommandoDashJetsBlue;
                orig(self);
                EntityStates.Commando.SlideState.jetEffectPrefab = jetOg;
                return;
            }
            else
            {
                orig(self);
            }
        }

        private static void DodgeState_OnEnter(On.EntityStates.Commando.DodgeState.orig_OnEnter orig, EntityStates.Commando.DodgeState self)
        {
            if (self.modelLocator.modelTransform.GetComponent<SkinDefAltColorTracker>())
            {
                GameObject jetOg = EntityStates.Commando.DodgeState.jetEffect;
                EntityStates.Commando.DodgeState.jetEffect = CommandoDashJetsBlue;
                orig(self);
                EntityStates.Commando.DodgeState.jetEffect = jetOg;
                return;
            }
            else
            {
                orig(self);
            }
        }


        [RegisterAchievement("CLEAR_ANY_COMMANDO", "Skins.Commando.Wolfo.First", null, 3, null)]
        public class CommandoClearAlt1 : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CommandoBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_COMMANDO", "Skins.Commando.Wolfo.Both", null, 3, null)]
        public class CommandoClearAlt2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CommandoBody");
            }
        }
    }
}