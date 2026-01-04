using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Base
{
    public class SkinsHuntress
    {
        internal static void Start()
        {
            SkinDef skinHuntressDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressDefault.asset").WaitForCompletion();
            SkinDef skinHuntressAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressAltColossus.asset").WaitForCompletion();


            //0 matBowString BowString
            //1 matHuntress Bow
            //2 matHuntressArrow 
            //3 matHuntressArrow 
            //4 matHuntressCharged Particle 
            //5 matHuntressArrow 
            //6 matHuntressArrow 
            //7 matHuntressArrow 
            //8 matHuntressArrow 
            //8 matHuntressArrow 
            //9 matHuntressArrow 
            //10 matHuntress 
            //11 matHuntressCape 

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinHuntress_1",
                nameToken = "SIMU_SKIN_HUNTRESS",
                icon = GetIcon("base/huntress_pink"),
                original = skinHuntressDefault,
                enhancedSkin = true
            }, new System.Action<SkinDefMakeOnApply>(Default_RoRRBlack));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinHuntressWolfoBee_1",
                nameToken = "SIMU_SKIN_HUNTRESS2",
                icon = GetIcon("base/huntress_yellow"),
                original = skinHuntressDefault,
                enhancedSkin = true,
                cloneMesh = true,
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Bee));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinHuntressAltColossus_DLC2",
                nameToken = "SIMU_SKIN_HUNTRESS_COLOSSUS",
                icon = GetIcon("base/huntress_dlc2"),
                original = skinHuntressAltColossus,
                enhancedSkin = true
            }, new System.Action<SkinDefMakeOnApply>(Colossus_DarkRed));

        }

        internal static void Colossus_DarkRed(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matHuntressAltColossus = CloneMat(ref newRenderInfos, 2);
            Material matHuntressAltColossusBowString = CloneMat(ref newRenderInfos, 3);
            Material matHuntressAltColossusArrow = CloneMat(ref newRenderInfos, 4);

            Texture2D texRampHuntressAltColossusArrow = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/Colossus/texRampHuntressAltColossusArrow.png");

            matHuntressAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/Colossus/texHuntressAltColossusDiffuse.png");
            matHuntressAltColossus.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/Colossus/texRampHuntressAltColossus.png"));
            matHuntressAltColossus.SetColor("_EmTex", new Color(1f, 0.5f, 0.5f)); //1 0.9296 0.3255 1

            matHuntressAltColossusBowString.SetTexture("_RemapTex", texRampHuntressAltColossusArrow);
            matHuntressAltColossusArrow.SetTexture("_RemapTex", texRampHuntressAltColossusArrow);

            newRenderInfos[0].defaultMaterial = matHuntressAltColossus;
            newRenderInfos[1].defaultMaterial = matHuntressAltColossus;
            newRenderInfos[2].defaultMaterial = matHuntressAltColossus;
            newRenderInfos[3].defaultMaterial = matHuntressAltColossusBowString;
            newRenderInfos[4].defaultMaterial = matHuntressAltColossusArrow;
            newRenderInfos[5].defaultMaterial = matHuntressAltColossusArrow;

        }

        internal static void Default_RoRRBlack(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matBowString = CloneMat(ref newRenderInfos, 0);
            Material matHuntress = CloneMat(ref newRenderInfos, 1);
            Material matHuntressBow = CloneMat(ref newRenderInfos, 1);
            Material matHuntressArrow = CloneMat(ref newRenderInfos, 2);
            Material matHuntressCharged = CloneMat(ref newRenderInfos, 4);
            Material matHuntressCape = CloneMat(ref newRenderInfos, 11);


            Texture2D texHuntressEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/pink/texHuntressEmission.png");

            Texture2D texRampHuntress = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/pink/texRampHuntress.png");

            matHuntress.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/pink/texHuntressDiffuse.png");
            matHuntress.SetTexture("_EmTex", texHuntressEmission);
            matHuntress.SetColor("_EmColor", new Color(2f, 1f, 1.6f));

            matHuntressBow.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/pink/texHuntressDiffuseBow.png");
            matHuntressBow.SetTexture("_EmTex", texHuntressEmission);
            matHuntressBow.SetColor("_EmColor", new Color(1f, 0.5f, 0.8f));
            //matHuntressBow.color = new Color(0.5f, 0.5f, 0.5f);

            //matHuntressCape.color = new Color(0.25f, 0.2f, 0.3f);
            //matHuntressCape.color = new Color(1f, 0.5f, 0.8f);
            matHuntressCape.color = new Color(0.8f, 0.4f, 0.6f);

            matBowString.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/pink/texRampHuntressSoft.png"));
            matHuntressArrow.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/pink/texRampHuntressSoft2.png"));
            matHuntressCharged.SetTexture("_RemapTex", texRampHuntress);

            newRenderInfos[0].defaultMaterial = matBowString;
            newRenderInfos[1].defaultMaterial = matHuntressBow;
            newRenderInfos[2].defaultMaterial = matHuntressArrow;
            newRenderInfos[3].defaultMaterial = matHuntressArrow;
            newRenderInfos[4].defaultMaterial = matHuntressCharged;
            newRenderInfos[5].defaultMaterial = matHuntressArrow;
            newRenderInfos[6].defaultMaterial = matHuntressArrow;
            newRenderInfos[7].defaultMaterial = matHuntressArrow;
            newRenderInfos[8].defaultMaterial = matHuntressArrow;
            newRenderInfos[9].defaultMaterial = matHuntressArrow;
            newRenderInfos[10].defaultMaterial = matHuntress;
            newRenderInfos[11].defaultMaterial = matHuntressCape;
            //


            GameObject HuntressArrowRain = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Huntress/HuntressArrowRain.prefab").WaitForCompletion();
            GameObject HuntressArrowRainWolfo1 = R2API.PrefabAPI.InstantiateClone(HuntressArrowRain, "HuntressArrowRainWolfo1", false);

            Material matHuntressAreaIndicatorActive = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Material matHuntressFlash = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material);

            matHuntressAreaIndicatorActive.SetTexture("_RemapTex", texRampHuntress);
            matHuntressFlash.SetTexture("_RemapTex", texRampHuntress);

            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matHuntressAreaIndicatorActive;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>().material = matHuntressCharged;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);//HITBOX 
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(4).gameObject.SetActive(false); //HITBOX
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(5).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material = matHuntressFlash;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Light>().color = new Color(0.8f, 0.2f, 0.8f);
            //
            //

            GameObject DisplayEliteRabbitEars = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DisplayEliteRabbitEars.prefab").WaitForCompletion();
            GameObject DisplayEliteRabbitEarsNew = R2API.PrefabAPI.InstantiateClone(DisplayEliteRabbitEars, "HuntressRabbitEars", false);
            SkinnedMeshRenderer RabbitMesh = DisplayEliteRabbitEarsNew.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();

            Material matRabbitEars = Object.Instantiate(RabbitMesh.material);

            matRabbitEars.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/pink/texRabbitEarsDiffuse.png");
            matRabbitEars.color = new Color(0.6f, 0.3f, 0.45f);
            matRabbitEars.SetTexture("_FresnelRamp", null);
            RabbitMesh.material = matRabbitEars;
            DisplayEliteRabbitEarsNew.GetComponent<ItemDisplay>().rendererInfos[0].defaultMaterial = matRabbitEars;

            (newSkinDef as SkinDefEnhanced).addGameObjects = new ItemDisplayRule[]
            {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = DisplayEliteRabbitEarsNew,
                            childName = "Head",
                            localPos = new Vector3(0f, 0.3f, -0.05f),
                            localAngles = new Vector3(340f,0f,0f),
                            localScale = new Vector3(0.8f,0.8f,0.8f),
                            limbMask = LimbFlags.None
                        },
            };

            ReplaceArrowRainVFX replaceArrowRainVFX = HuntressArrowRain.AddComponent<ReplaceArrowRainVFX>();
            replaceArrowRainVFX.skinDef = newSkinDef;
            replaceArrowRainVFX.newVFX = HuntressArrowRainWolfo1.transform.GetChild(0).gameObject;

        }

        internal static void Mastery_Bee(SkinDefMakeOnApply newSkinDef)
        {
            SkinDefParams skinHuntressAlt = Addressables.LoadAssetAsync<SkinDefParams>(key: "RoR2/Base/Huntress/skinHuntressAlt_params.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            newSkinDef.skinDefParams.meshReplacements[1] = skinHuntressAlt.meshReplacements[1];

            Material matBowString = CloneMat(ref newRenderInfos, 0);
            Material matHuntress = CloneMat(ref newRenderInfos, 1);
            Material matHuntressBow = CloneMat(ref newRenderInfos, 1);
            Material matHuntressArrow = CloneMat(ref newRenderInfos, 2);
            Material matHuntressCharged = CloneMat(ref newRenderInfos, 4);
            Material matHuntressCapeALT = Object.Instantiate(Addressables.LoadAssetAsync<Material>(key: "fdcb95c014a8f754495f6e564bf17354").WaitForCompletion());

            Texture2D texHuntressEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texHuntressEmission.png");

            Texture2D texRampHuntress = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texRampHuntress.png");
            Texture2D texHuntressScarfDiffuseAlt = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texHuntressScarfDiffuseAlt.png");


            matHuntress.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texHuntressDiffuse.png");
            matHuntress.SetTexture("_EmTex", texHuntressEmission);
            matHuntress.SetColor("_EmColor", new Color(2f, 2f, 0f));

            matHuntressBow.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texHuntressDiffuseBow.png");
            matHuntressBow.SetTexture("_EmTex", texHuntressEmission);
            matHuntressBow.SetColor("_EmColor", new Color(1f, 1f, 0f));
            //matHuntressBow.color = new Color(0.5f, 0.5f, 0.5f);

            //matHuntressCapeALT.color = new Color(0.8f, 0.7f, 0.4f);
            matHuntressCapeALT.mainTexture = texHuntressScarfDiffuseAlt;

            matBowString.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texRampHuntressSoftYELLOW.png"));
            matHuntressArrow.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texRampHuntressSoft2YELLOW.png"));
            matHuntressCharged.SetTexture("_RemapTex", texRampHuntress);

            newRenderInfos[0].defaultMaterial = matBowString;
            newRenderInfos[1].defaultMaterial = matHuntressBow;
            newRenderInfos[2].defaultMaterial = matHuntressArrow;
            newRenderInfos[3].defaultMaterial = matHuntressArrow;
            newRenderInfos[4].defaultMaterial = matHuntressCharged;
            newRenderInfos[5].defaultMaterial = matHuntressArrow;
            newRenderInfos[6].defaultMaterial = matHuntressArrow;
            newRenderInfos[7].defaultMaterial = matHuntressArrow;
            newRenderInfos[8].defaultMaterial = matHuntressArrow;
            newRenderInfos[9].defaultMaterial = matHuntressArrow;
            newRenderInfos[10].defaultMaterial = matHuntress;
            newRenderInfos[11].defaultMaterial = matHuntressCapeALT;

            GameObject HuntressArrowRain = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Huntress/HuntressArrowRain.prefab").WaitForCompletion();
            GameObject HuntressArrowRainWolfo1 = R2API.PrefabAPI.InstantiateClone(HuntressArrowRain, "HuntressArrowRainWolfoYellow", false);

            Material matHuntressAreaIndicatorActive = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Material matHuntressFlash = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material);

            matHuntressAreaIndicatorActive.SetTexture("_RemapTex", texRampHuntress);
            matHuntressFlash.SetTexture("_RemapTex", texRampHuntress);

            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matHuntressAreaIndicatorActive;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>().material = matHuntressCharged;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);//HITBOX 
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(4).gameObject.SetActive(false); //HITBOX
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(5).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material = matHuntressFlash;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Light>().color = new Color(0.8f, 0.2f, 0.8f);
            //


            GameObject DisplayShieldBug = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShieldOnly/DisplayShieldBug.prefab").WaitForCompletion();
            GameObject DisplayShieldBugNew = R2API.PrefabAPI.InstantiateClone(DisplayShieldBug, "HuntressBeeHead", false);

            GameObject DisplayBugWings = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Jetpack/DisplayBugWings.prefab").WaitForCompletion();
            GameObject DisplayBugWingsNew = R2API.PrefabAPI.InstantiateClone(DisplayBugWings, "HuntressBeeWings", false);

            ItemDisplay itemDisplayAntler = DisplayShieldBugNew.GetComponent<ItemDisplay>();
            ItemDisplay itemDisplayWings = DisplayBugWingsNew.GetComponent<ItemDisplay>();

            Material matShieldBug = Object.Instantiate(itemDisplayAntler.rendererInfos[0].defaultMaterial);

            matShieldBug.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texShieldBugDiffuse.png");
            matShieldBug.color = new Color(0.5f, 0.5f, 0.5f); //0.2123 1 0.9836 1
            matShieldBug.SetColor("_EmColor", new Color(0, 0, 0));
            matShieldBug.SetTexture("_EmTex", null);

            itemDisplayAntler.rendererInfos[0].renderer.material = matShieldBug;
            itemDisplayAntler.rendererInfos[0].defaultMaterial = matShieldBug;


            Material matBugWings = Object.Instantiate(itemDisplayWings.rendererInfos[0].defaultMaterial);

            matBugWings.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Huntress/yellow/texBugWingDiffuse.png");
            matBugWings.SetTexture("_FresnelRamp", null);
            matBugWings.color = new Color(1, 1, 0.9f);

            itemDisplayWings.transform.GetChild(0).GetChild(0).GetChild(2).localScale = new Vector3(0, 0, 0);
            itemDisplayWings.transform.GetChild(0).GetChild(0).GetChild(3).localScale = new Vector3(0, 0, 0);

            GameObject.Destroy(itemDisplayWings.transform.GetChild(0).GetComponent<AkGameObj>());
            GameObject.Destroy(itemDisplayWings.transform.GetChild(0).GetComponent<AnimationEvents>());
            GameObject.Destroy(itemDisplayWings.transform.GetChild(0).GetComponent<Animator>());

            itemDisplayWings.rendererInfos[0].renderer.material = matBugWings;
            itemDisplayWings.rendererInfos[0].defaultMaterial = matBugWings;

            (newSkinDef as SkinDefEnhanced).addGameObjects = new ItemDisplayRule[]
            {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = DisplayShieldBugNew,
                            childName = "Head",
                            localPos = new Vector3(0.088f, 0.26f, -0.04f),
                            localAngles = new Vector3(358.7521f, 269.4822f, 15.7501f),
                            localScale = new Vector3(0.2f, 0.2f, 0.2f),
                            limbMask = LimbFlags.None
                        },
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = DisplayShieldBugNew,
                            childName = "Head",
                            localPos = new Vector3(-0.096f, 0.2595f, -0.0404f),
                            localAngles = new Vector3(1.5158f, 270.3596f, 15.752f),
                            localScale = new Vector3(0.2f, 0.2f, -0.2f),
                            limbMask = LimbFlags.None
                        },
                        /*new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = DisplayBugWingsNew,
                            childName = "Chest",
                            localPos = new Vector3(0f, 0.2f, -0.05f),
                            localAngles = new Vector3(310,0,0),
                            localScale = new Vector3(0.2f, 0.1f, 0.08f),
                            limbMask = LimbFlags.None
                        },*/
            };

            ReplaceArrowRainVFX replaceArrowRainVFX = HuntressArrowRain.AddComponent<ReplaceArrowRainVFX>();
            replaceArrowRainVFX.skinDef = newSkinDef;
            replaceArrowRainVFX.newVFX = HuntressArrowRainWolfo1.transform.GetChild(0).gameObject;

        }


        public class ReplaceArrowRainVFX : MonoBehaviour
        {
            public GameObject newVFX;
            public SkinDef skinDef;

            public void FixedUpdate()
            {

                GameObject owner = this.gameObject.GetComponent<RoR2.Projectile.ProjectileController>().owner;
                if (owner)
                {
                    CharacterBody body = owner.GetComponent<CharacterBody>();
                    //Debug.Log(SkinCatalog.GetBodySkinDef(body.bodyIndex, (int)body.skinIndex));
                    if (SkinCatalog.GetBodySkinDef(body.bodyIndex, (int)body.skinIndex) == skinDef)
                    {
                        this.gameObject.transform.GetChild(0).GetChild(3).parent = this.transform;
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        Instantiate(newVFX, this.transform);
                    }
                    Destroy(this);
                }
            }
        }


        [RegisterAchievement("CLEAR_ANY_HUNTRESS", "Skins.Huntress.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumHuntressBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HuntressBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_HUNTRESS", "Skins.Huntress.Wolfo.Both", null, 3, null)]
        public class ClearSimulacrumHuntressBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HuntressBody");
            }
        }

    }
}