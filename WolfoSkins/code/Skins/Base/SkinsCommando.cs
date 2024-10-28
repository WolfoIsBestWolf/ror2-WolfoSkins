using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsCommando
    {
        public static GameObject CommandoDashJetsBlue = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoDashJets.prefab").WaitForCompletion(), "CommandoDashJetsBlue", false);


        internal static void Start()
        {
            CommandoSkin_Unused();
            CommandoSkin_Provi();
            Commando_AltColossus();

            On.EntityStates.Commando.DodgeState.OnEnter += DodgeState_OnEnter;
            On.EntityStates.Commando.SlideState.OnEnter += SlideState_OnEnter;
        }

        internal static void Commando_AltColossus()
        {
            SkinDef skinCommandoAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Commando/skinCommandoAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinCommandoAltColossus.rendererInfos.Length];
            System.Array.Copy(skinCommandoAltColossus.rendererInfos, NewRenderInfos, skinCommandoAltColossus.rendererInfos.Length);

            Material matCommandoAltColossus = Object.Instantiate(skinCommandoAltColossus.rendererInfos[2].defaultMaterial);


            Texture2D texCommandoAltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/Colossus/texCommandoAltColossusDiffuse.png");
            texCommandoAltColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampBeetleQueen = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/Colossus/texRampBeetleQueen.png");
            texRampBeetleQueen.wrapMode = TextureWrapMode.Clamp;

            matCommandoAltColossus.mainTexture = texCommandoAltColossusDiffuse;
            matCommandoAltColossus.SetTexture("_FlowHeightRamp", texRampBeetleQueen); //0.8491 0.5267 0.1402 1
            matCommandoAltColossus.SetFloat("_FlowEmissionStrength", 2);

            NewRenderInfos[0].defaultMaterial = matCommandoAltColossus;
            NewRenderInfos[1].defaultMaterial = matCommandoAltColossus;
            NewRenderInfos[2].defaultMaterial = matCommandoAltColossus;
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCommandoAltColossusWolfo_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_COMMANDO_COLOSSUS";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/Colossus/commando.png"));
            newSkinDef.baseSkins = skinCommandoAltColossus.baseSkins;
            newSkinDef.meshReplacements = skinCommandoAltColossus.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCommandoAltColossus.rootObject;


            GameObject CommandoGrenadeProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeProjectile.prefab").WaitForCompletion();
            GameObject CommandoGrenadeGhost = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeGhost.prefab").WaitForCompletion(), "CommandoGrenadeGhostPROVI", false);

            CommandoGrenadeGhost.transform.GetChild(0).GetComponent<MeshRenderer>().material = matCommandoAltColossus;
            CommandoGrenadeGhost.transform.GetChild(4).GetComponent<Light>().color = new Color(0.34f, 0.67f, 1f);//0.8679 0.6413 0.2334 1

            newSkinDef.projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
            {
                new SkinDef.ProjectileGhostReplacement
                {
                    projectilePrefab = CommandoGrenadeProjectile,
                    projectileGhostReplacementPrefab = CommandoGrenadeGhost,
                }
            };


            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), newSkinDef);
        }

        internal static void CommandoSkin_Unused()
        {
            SkinDef UnusedCommandoSkin = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion();
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), UnusedCommandoSkin);

            //This removes Limb removal, not good but better than bugged ones I guess
            //I have no idea how to actually fix his Right Calf being both Calfs
            Material matCommandoDualiesMarine = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matCommandoDualiesMarine.mat").WaitForCompletion();
            matCommandoDualiesMarine.DisableKeyword("LIMBREMOVAL");

            UnusedCommandoSkin.nameToken = "SIMU_SKIN_COMMANDO_SOTV";

            /*Texture2D skinIconCommando = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinIconCommando.LoadImage(Properties.Resources.skinIconCommandoSOTV, true);
            skinIconCommando.filterMode = FilterMode.Bilinear;
            Sprite skinIconCommandoS = Sprite.Create(skinIconCommando, WRect.rec128, WRect.half);*/
        }

        internal static void CommandoSkin_Provi()
        {
            SkinDef skinCommandoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Commando/skinCommandoAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinCommandoAlt.rendererInfos.Length];
            System.Array.Copy(skinCommandoAlt.rendererInfos, NewRenderInfos, skinCommandoAlt.rendererInfos.Length);


            Texture2D texCommandoPaletteDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/texCommandoPaletteDiffuse.png");
            texCommandoPaletteDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCommandoPaletteDiffuseGUN = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/texCommandoPaletteDiffuseGUN.png");
            texCommandoPaletteDiffuseGUN.wrapMode = TextureWrapMode.Clamp;


            Material matCommandoDualiesAlt = Object.Instantiate(skinCommandoAlt.rendererInfos[2].defaultMaterial);
            Material matCommandoDualiesAltGUN = Object.Instantiate(skinCommandoAlt.rendererInfos[2].defaultMaterial);

            matCommandoDualiesAlt.mainTexture = texCommandoPaletteDiffuse;
            //matCommandoDualiesAlt.SetColor("_EmColor", new Color(0.15f, 0.775f, 0.96f)); //0.8491 0.5267 0.1402 1
            matCommandoDualiesAlt.color = new Color(1.1f, 1f, 0.8f);
            matCommandoDualiesAlt.SetColor("_EmColor", new Color(0.2f, 0.9f, 0.9f)); //0.8491 0.5267 0.1402 1

            matCommandoDualiesAltGUN.mainTexture = texCommandoPaletteDiffuseGUN;
            matCommandoDualiesAltGUN.color = new Color(1.2f, 1.4f, 1.4f);
            matCommandoDualiesAltGUN.SetColor("_EmColor", new Color(1f,2f,3f));
            //matCommandoDualiesAltGUN.SetTexture("_EmTex", null);

            NewRenderInfos[0].defaultMaterial = matCommandoDualiesAltGUN;
            NewRenderInfos[1].defaultMaterial = matCommandoDualiesAltGUN;
            NewRenderInfos[2].defaultMaterial = matCommandoDualiesAlt;
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCommandoWolfoProvi_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_COMMANDO";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Commando/skinIconCommando.png"));
            newSkinDef.baseSkins = skinCommandoAlt.baseSkins;
            newSkinDef.meshReplacements = skinCommandoAlt.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCommandoAlt.rootObject;


            GameObject CommandoGrenadeProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeProjectile.prefab").WaitForCompletion();
            GameObject CommandoGrenadeGhost = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoGrenadeGhost.prefab").WaitForCompletion(), "CommandoGrenadeGhostPROVI", false);

            CommandoGrenadeGhost.transform.GetChild(0).GetComponent<MeshRenderer>().material = matCommandoDualiesAlt;
            CommandoGrenadeGhost.transform.GetChild(4).GetComponent<Light>().color = new Color(0.34f, 0.67f, 1f);//0.8679 0.6413 0.2334 1

            newSkinDef.projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[]
            {
                new SkinDef.ProjectileGhostReplacement
                {
                    projectilePrefab = CommandoGrenadeProjectile,
                    projectileGhostReplacementPrefab = CommandoGrenadeGhost,
                }
            };


            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), newSkinDef);

            CommandoDashJetsBlue.GetComponent<Light>().color = new Color(0, 0.7f, 1f); //1 0.7045 0.051 1
            CommandoDashJetsBlue.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 0.425f, 1f);

            var colorOverLifetime = CommandoDashJetsBlue.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
            colorOverLifetime.enabled = false;

            CommandoDashJetsBlue.transform.GetChild(3).GetComponent<ParticleSystem>().startColor = new Color(0, 0.3f, 0.3f); //0.2925 0.2922 0 1
            CommandoDashJetsBlue.transform.GetChild(4).GetComponent<ParticleSystem>().startColor = new Color(0.3f, 0.8f, 1f, 1); //1 0.4941 0.3098 1


      

        }

        private static void SlideState_OnEnter(On.EntityStates.Commando.SlideState.orig_OnEnter orig, EntityStates.Commando.SlideState self)
        {
            if (self.modelLocator.modelTransform.GetComponent<SkinDefWolfoTracker>())
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
            if(self.modelLocator.modelTransform.GetComponent<SkinDefWolfoTracker>())
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


        [RegisterAchievement("CLEAR_ANY_COMMANDO", "Skins.Commando.Wolfo.First", null, 5, null)]
        public class CommandoClearAlt1: Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CommandoBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_COMMANDO", "Skins.Commando.Wolfo.Both", null, 5, null)]
        public class CommandoClearAlt2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CommandoBody");
            }
        }
    }
}