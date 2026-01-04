using EntityStates.FalseSon;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC2
{
    public class SkinsFalseSon
    {
        public static GameObject LunarSpikeGhost = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSon/LunarSpikeGhost.prefab").WaitForCompletion();
        public static GameObject LunarStakeGhost = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSon/LunarStakeGhost.prefab").WaitForCompletion();
        public static GameObject FalseSonGroundSlam = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSon/FalseSonGroundSlam.prefab").WaitForCompletion();
        //public static GameObject FalseSonMeteorGroundImpact = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSon/FalseSonMeteorGroundImpact.prefab").WaitForCompletion();

        internal static void Start()
        {
            SkinDef skinFalseSonDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/FalseSon/skinFalseSonDefault.asset").WaitForCompletion();
            SkinDef skinFalseSonAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/FalseSon/skinFalseSonAlt.asset").WaitForCompletion();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinFalseSon_Gold_1",
                nameToken = "SIMU_SKIN_FALSESON_GOLD",
                icon = H.GetIcon("dlc2/falseson_gold"),
                original = skinFalseSonDefault,
                extraRenders = 1
            }, new System.Action<SkinDefMakeOnApply>(Default_Gold));
            /*CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinFalseSon_Lunar_1",
                nameToken = "SIMU_SKIN_FALSESON_LUNAR",
                icon = H.GetIcon("dlc2/falseson_lunar"),
                original = skinFalseSonDefault,
                extraRenders = 1
            }, new System.Action<SkinDefMakeOnApply>(Default_Lunar));*/
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinFalseSonAlt_Provi_1",
                nameToken = "SIMU_SKIN_FALSESON_PROVI",
                icon = H.GetIcon("dlc2/falseson_provi"),
                original = skinFalseSonAlt,
                extraRenders = 1
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Providence));
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinFalseSonAlt_Provi2_1",
                nameToken = "SIMU_SKIN_FALSESON_PROVI2",
                icon = H.GetIcon("dlc2/falseson_provi_alt"),
                original = skinFalseSonAlt,
                extraRenders = 1
            }, new System.Action<SkinDefMakeOnApply>(Mastery_ProvidenceCyan));
            //0 matFalseSon
            //1 matFalseSonCloth / 
            //2 matFalseSon / Weapon

            //Lobby
            //Uses different texture, would not look good
            //Projectile
            On.EntityStates.FalseSon.LunarSpikes.FireLunarSpike += LunarSpikeAndStake;
            //Slam Grounded
            On.EntityStates.FalseSon.ChargedClubSwing.InitializeBlastAttackAsCharged += ChargedClubSwing_InitializeBlastAttackAsCharged;
            //Slam Airborne
            On.EntityStates.FalseSon.ClubGroundSlam.DetonateAuthority += ClubGroundSlam_DetonateAuthority;
            //Spawn Animation
            //On.EntityStates.FalseSonMeteorPod.Descent.OnExit += Descent_OnExit;

            //Do not reuse ghosts because we do not reAssign the material when the ghost is reused.
            LunarSpikeGhost.GetComponent<VFXAttributes>().DoNotPool = true;
            FalseSonGroundSlam.GetComponent<VFXAttributes>().DoNotPool = true;
            LunarStakeGhost.GetComponent<VFXAttributes>().DoNotPool = true;
        }



        public static Material ReturnMaterialFromEntityState(Transform modelTransform, int child)
        {
            return modelTransform.GetChild(child).GetComponent<Renderer>().material;
        }

        public static BodyIndex falseSonBodyIndex;
        private static void LunarSpikeAndStake(On.EntityStates.FalseSon.LunarSpikes.orig_FireLunarSpike orig, EntityStates.FalseSon.LunarSpikes self)
        {
            if (self.characterBody.bodyIndex == falseSonBodyIndex)
            {
                Material mat = ReturnMaterialFromEntityState(self.modelLocator.modelTransform, 0);
                if (self is LunarStake)
                {
                    LunarStakeGhost.transform.GetChild(0).GetComponent<Renderer>().material = mat;
                    LunarStakeGhost.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material = mat;
                }
                else
                {
                    LunarSpikeGhost.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = mat;
                    LunarSpikeGhost.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = mat;
                }
            }
            orig(self);
        }

        private static void ChargedClubSwing_InitializeBlastAttackAsCharged(On.EntityStates.FalseSon.ChargedClubSwing.orig_InitializeBlastAttackAsCharged orig, EntityStates.FalseSon.ChargedClubSwing self, ref BlastAttack blast)
        {
            Material mat = ReturnMaterialFromEntityState(self.modelLocator.modelTransform, 0);
            FalseSonGroundSlam.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = mat;
            orig(self, ref blast);
        }

        private static BlastAttack.Result ClubGroundSlam_DetonateAuthority(On.EntityStates.FalseSon.ClubGroundSlam.orig_DetonateAuthority orig, EntityStates.FalseSon.ClubGroundSlam self)
        {
            Material mat = ReturnMaterialFromEntityState(self.modelLocator.modelTransform, 0);
            FalseSonGroundSlam.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = mat;
            return orig(self);
        }

        /*private static void Descent_OnExit(On.EntityStates.FalseSonMeteorPod.Descent.orig_OnExit orig, EntityStates.FalseSonMeteorPod.Descent self)
        {
            Material mat = ReturnMaterialFromEntityState(self.GetComponent<VehicleSeat>().currentPassengerBody.modelLocator.modelTransform, 0);
            FalseSonMeteorGroundImpact.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<Renderer>().material = mat;
            orig(self);
        }*/

        internal static void Mastery_Providence(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matFalseSon = CloneMat(ref newRenderInfos, 0);
            Material matFalseSonCloth = CloneMat(ref newRenderInfos, 1);

            matFalseSon.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltDiffuse.png");
            matFalseSon.SetColor("_EmColor", new Color(0.55f, 1f, 0.85f));
            matFalseSon.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltFresnel.png"));
            matFalseSon.SetTexture("_FlowHeightmap", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltFlow.png"));
            matFalseSon.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texRampCaptainAirstrike.png"));

            matFalseSonCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltClothDiffuse.png");
            matFalseSonCloth.color = Color.white;
            matFalseSonCloth.SetTexture("_FlowHeightRamp", null);

            Material matFalseSonSword = Object.Instantiate(matFalseSon);
            matFalseSonSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltSwordDiffuse.png");
            matFalseSonSword.SetTexture("_FlowHeightmap", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltFlowSword.png"));

            newRenderInfos[0].defaultMaterial = matFalseSon;
            newRenderInfos[1].defaultMaterial = matFalseSonCloth;
            newRenderInfos[2].defaultMaterial = matFalseSonSword;
            newRenderInfos[3] = new CharacterModel.RendererInfo
            {
                defaultMaterial = matFalseSon,
                renderer = newSkinDef.rootObject.transform.Find("FSArmature/Root/Hips/Spine1/Spine2/Spine3/SpineEnd/L_Clav/L_Upperarm/L_Forearm/L_Hand/L_Hand_Object/L_Weapon/OverHeadSwingPoint/FSClubGroundTrail/Dust/Debris, 3D").GetComponent<ParticleSystemRenderer>()
            };
        }

        internal static void Mastery_ProvidenceCyan(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matFalseSon = CloneMat(ref newRenderInfos, 0);
            Material matFalseSonCloth = CloneMat(ref newRenderInfos, 1);

            matFalseSon.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi2/texFalseSonAltDiffuse.png");
            matFalseSon.SetColor("_EmColor", new Color(0.55f, 0.85f, 1f));
            matFalseSon.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi2/texFalseSonAltFresnel.png"));
            matFalseSon.SetTexture("_FlowHeightmap", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi2/texFalseSonAltFlow.png"));
            matFalseSon.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi2/texRampCaptainAirstrike.png"));

            matFalseSonCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi2/texFalseSonAltClothDiffuse.png");
            matFalseSonCloth.color = Color.white;
            matFalseSonCloth.SetTexture("_FlowHeightRamp", null);

            Material matFalseSonSword = Object.Instantiate(matFalseSon);
            matFalseSonSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi2/texFalseSonAltSwordDiffuse.png");
            matFalseSonSword.SetTexture("_FlowHeightmap", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi2/texFalseSonAltFlowSword.png"));

            newRenderInfos[0].defaultMaterial = matFalseSon;
            newRenderInfos[1].defaultMaterial = matFalseSonCloth;
            newRenderInfos[2].defaultMaterial = matFalseSonSword;
            newRenderInfos[3] = new CharacterModel.RendererInfo
            {
                defaultMaterial = matFalseSon,
                renderer = newSkinDef.rootObject.transform.Find("FSArmature/Root/Hips/Spine1/Spine2/Spine3/SpineEnd/L_Clav/L_Upperarm/L_Forearm/L_Hand/L_Hand_Object/L_Weapon/OverHeadSwingPoint/FSClubGroundTrail/Dust/Debris, 3D").GetComponent<ParticleSystemRenderer>()
            };
        }

        internal static void Default_Gold(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matFalseSon = CloneMat(ref newRenderInfos, 0);
            Material matFalseSonCloth = CloneMat(ref newRenderInfos, 1);

            matFalseSon.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Gold/texFalseSonDiffuse.png");
            //matFalseSon.color = new Color(1,1,0.666f); //I fucking hate how blue everything gets.
            matFalseSon.SetColor("_EmColor", new Color(1f, 0.8f, 0.8f));
            matFalseSon.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Gold/texFalseSonFresnel.png"));
            //matFalseSon.SetTexture("_FlowHeightRamp", matFalseSon.GetTexture("_FresnelRamp"));
            matFalseSonCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Gold/texFalseSonClothDiffuse.png");
            matFalseSonCloth.color = Color.white;

            newRenderInfos[0].defaultMaterial = matFalseSon;
            newRenderInfos[1].defaultMaterial = matFalseSonCloth;
            newRenderInfos[2].defaultMaterial = matFalseSon;
            newRenderInfos[3] = new CharacterModel.RendererInfo
            {
                defaultMaterial = matFalseSon,
                renderer = newSkinDef.rootObject.transform.Find("FSArmature/Root/Hips/Spine1/Spine2/Spine3/SpineEnd/L_Clav/L_Upperarm/L_Forearm/L_Hand/L_Hand_Object/L_Weapon/OverHeadSwingPoint/FSClubGroundTrail/Dust/Debris, 3D").GetComponent<ParticleSystemRenderer>()
            };
        }

        internal static void Default_Lunar(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matFalseSon = CloneMat(ref newRenderInfos, 0);
            Material matFalseSonCloth = CloneMat(ref newRenderInfos, 1);

            matFalseSon.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Lunar/texFalseSonDiffuse.png");
            matFalseSon.SetColor("_EmColor", new Color(1f, 0.8f, 0.8f));
            matFalseSon.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Lunar/texFalseSonFresnel.png"));
            //matFalseSon.SetTexture("_FlowHeightRamp", matFalseSon.GetTexture("_FresnelRamp"));
            matFalseSonCloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Lunar/texFalseSonClothDiffuse.png");
            matFalseSonCloth.color = Color.white;

            newRenderInfos[0].defaultMaterial = matFalseSon;
            newRenderInfos[1].defaultMaterial = matFalseSonCloth;
            newRenderInfos[2].defaultMaterial = matFalseSon;
            newRenderInfos[3] = new CharacterModel.RendererInfo
            {
                defaultMaterial = matFalseSon,
                renderer = newSkinDef.rootObject.transform.Find("FSArmature/Root/Hips/Spine1/Spine2/Spine3/SpineEnd/L_Clav/L_Upperarm/L_Forearm/L_Hand/L_Hand_Object/L_Weapon/OverHeadSwingPoint/FSClubGroundTrail/Dust/Debris, 3D").GetComponent<ParticleSystemRenderer>()
            };
        }


        [RegisterAchievement("CLEAR_ANY_FALSESON", "Skins.FalseSon.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumFalseSonBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("FalseSonBody");
            }
        }

        /*[RegisterAchievement("CLEAR_BOTH_FALSESON", "Skins.FalseSon.Wolfo.Both", null, 3, null)]
        public class ClearSimulacrumFalseSonBody2 : Achievement_AltBoss
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("FalseSonBody");
            }
        }*/
    }
}