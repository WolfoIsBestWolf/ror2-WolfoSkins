using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsHuntress
    {
        internal static void Start()
        {

        }


        internal static void HuntressSkin()
        {
            SkinDef skinHuntressDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressDefault.asset").WaitForCompletion();
            SkinDef skinHuntressAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinHuntressDefault.rendererInfos.Length];
            System.Array.Copy(skinHuntressDefault.rendererInfos, NewRenderInfos, skinHuntressDefault.rendererInfos.Length);

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

            Material matBowString = Object.Instantiate(skinHuntressDefault.rendererInfos[0].defaultMaterial);
            Material matHuntress = Object.Instantiate(skinHuntressDefault.rendererInfos[1].defaultMaterial);
            Material matHuntressBow = Object.Instantiate(skinHuntressDefault.rendererInfos[1].defaultMaterial);
            Material matHuntressArrow = Object.Instantiate(skinHuntressDefault.rendererInfos[2].defaultMaterial);
            Material matHuntressCharged = Object.Instantiate(skinHuntressDefault.rendererInfos[4].defaultMaterial);
            Material matHuntressCape = Object.Instantiate(skinHuntressDefault.rendererInfos[11].defaultMaterial);

            Texture2D texHuntressDiffuse = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texHuntressDiffuse.LoadImage(Properties.Resources.texHuntressDiffuse, true);
            texHuntressDiffuse.filterMode = FilterMode.Bilinear;
            texHuntressDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texHuntressDiffuseBow = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texHuntressDiffuseBow.LoadImage(Properties.Resources.texHuntressDiffuseBow, true);
            texHuntressDiffuseBow.filterMode = FilterMode.Bilinear;
            texHuntressDiffuseBow.wrapMode = TextureWrapMode.Clamp;

            Texture2D texHuntressEmission = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texHuntressEmission.LoadImage(Properties.Resources.texHuntressEmission, true);
            texHuntressEmission.filterMode = FilterMode.Bilinear;
            texHuntressEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntress = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntress.LoadImage(Properties.Resources.texRampHuntress, true);
            texRampHuntress.filterMode = FilterMode.Point;
            texRampHuntress.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressSoft = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntressSoft.LoadImage(Properties.Resources.texRampHuntressSoft, true);
            texRampHuntressSoft.filterMode = FilterMode.Point;
            texRampHuntressSoft.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressSoft2 = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntressSoft2.LoadImage(Properties.Resources.texRampHuntressSoft2, true);
            texRampHuntressSoft2.filterMode = FilterMode.Point;
            texRampHuntressSoft2.wrapMode = TextureWrapMode.Clamp;

            matHuntress.mainTexture = texHuntressDiffuse;
            matHuntress.SetTexture("_EmTex", texHuntressEmission);
            matHuntress.SetColor("_EmColor", new Color(2f,1f,2f));

            matHuntressBow.mainTexture = texHuntressDiffuseBow;
            matHuntressBow.SetTexture("_EmTex", texHuntressEmission);
            matHuntressBow.SetColor("_EmColor", new Color(1f, 0.5f, 1f));
            //matHuntressBow.color = new Color(0.5f, 0.5f, 0.5f);

            //matHuntressCape.color = new Color(0.25f, 0.2f, 0.3f);
            //matHuntressCape.color = new Color(1f, 0.5f, 0.8f);
            matHuntressCape.color = new Color(0.8f, 0.4f, 0.6f);

            matBowString.SetTexture("_RemapTex", texRampHuntressSoft);
            matHuntressArrow.SetTexture("_RemapTex", texRampHuntressSoft2);
            matHuntressCharged.SetTexture("_RemapTex", texRampHuntress);

            NewRenderInfos[0].defaultMaterial = matBowString;     // 
            NewRenderInfos[1].defaultMaterial = matHuntressBow;     //
            NewRenderInfos[2].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[3].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[4].defaultMaterial = matHuntressCharged;     //
            NewRenderInfos[5].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[6].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[7].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[8].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[9].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[10].defaultMaterial = matHuntress;
            NewRenderInfos[11].defaultMaterial = matHuntressCape;
            //


            GameObject HuntressArrowRain = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Huntress/HuntressArrowRain.prefab").WaitForCompletion();
            GameObject HuntressArrowRainWolfo1 = PrefabAPI.InstantiateClone(HuntressArrowRain, "HuntressArrowRainWolfo1", false);

            Material matHuntressAreaIndicatorActive = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Material matHuntressFlash = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material);

            matHuntressAreaIndicatorActive.SetTexture("_RemapTex", texRampHuntress);
            matHuntressFlash.SetTexture("_RemapTex", texRampHuntress);

            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matHuntressAreaIndicatorActive ;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow; 
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>().material = matHuntressCharged;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);//HITBOX 
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(4).gameObject.SetActive(false); //HITBOX
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(5).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material = matHuntressFlash;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Light>().color = new Color(0.8f,0.2f,0.8f);
             
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconHuntress, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_HUNTRESS", "Amor");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HUNTRESS_NAME", "Huntress: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HUNTRESS_DESCRIPTION", "As Huntress"+ Unlocks.unlockCondition);

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_HUNTRESS_NAME";
            unlockableDef.cachedName = "Skins.Huntress.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinHuntressWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_HUNTRESS";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinHuntressAlt.baseSkins;
            newSkinDef.meshReplacements = skinHuntressDefault.meshReplacements;
            //newSkinDef.projectileGhostReplacements = Projectiles;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinHuntressDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            GameObject DisplayEliteRabbitEars = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DisplayEliteRabbitEars.prefab").WaitForCompletion();
            GameObject DisplayEliteRabbitEarsNew = R2API.PrefabAPI.InstantiateClone(DisplayEliteRabbitEars, "HuntressRabbitEars", false);
            SkinnedMeshRenderer RabbitMesh = DisplayEliteRabbitEarsNew.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();

            Material matRabbitEars = Object.Instantiate(RabbitMesh.material);

            Texture2D texRabbitEarsDiffuse = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texRabbitEarsDiffuse.LoadImage(Properties.Resources.texRabbitEarsDiffuse, true);
            texRabbitEarsDiffuse.filterMode = FilterMode.Bilinear;
            texRabbitEarsDiffuse.wrapMode = TextureWrapMode.Clamp;

            matRabbitEars.mainTexture = texRabbitEarsDiffuse;
            matRabbitEars.color = new Color(0.6f,0.3f,0.6f);
            matRabbitEars.SetTexture("_FresnelRamp", null);
            RabbitMesh.material = matRabbitEars;
            DisplayEliteRabbitEarsNew.GetComponent<ItemDisplay>().rendererInfos[0].defaultMaterial = matRabbitEars;

            newSkinDef.addGameObjects = new ItemDisplayRule[]
            {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = DisplayEliteRabbitEarsNew,
                            childName = "Head",
                            localPos = new Vector3(0f, 0.3f, -0.05f),
                            localAngles = new Vector3(340f,0f,0f),
                            localScale = new Vector3(0.7f,0.7f,0.7f),
                            limbMask = LimbFlags.None
                        },
            };

            ReplaceArrowRainVFX replaceArrowRainVFX = HuntressArrowRain.AddComponent<ReplaceArrowRainVFX>();
            replaceArrowRainVFX.skinDef = newSkinDef;
            replaceArrowRainVFX.newVFX = HuntressArrowRainWolfo1.transform.GetChild(0).gameObject;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/HuntressBody"), newSkinDef);
            HuntressSkinYellow(unlockableDef);
        }

        internal static void HuntressSkinYellow(UnlockableDef unlockableDef)
        {
            SkinDef skinHuntressDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressDefault.asset").WaitForCompletion();
            SkinDef skinHuntressAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinHuntressDefault.rendererInfos.Length];
            System.Array.Copy(skinHuntressDefault.rendererInfos, NewRenderInfos, skinHuntressDefault.rendererInfos.Length);

            Material matBowString = Object.Instantiate(skinHuntressDefault.rendererInfos[0].defaultMaterial);
            Material matHuntress = Object.Instantiate(skinHuntressDefault.rendererInfos[1].defaultMaterial);
            Material matHuntressBow = Object.Instantiate(skinHuntressDefault.rendererInfos[1].defaultMaterial);
            Material matHuntressArrow = Object.Instantiate(skinHuntressDefault.rendererInfos[2].defaultMaterial);
            Material matHuntressCharged = Object.Instantiate(skinHuntressDefault.rendererInfos[4].defaultMaterial);
            Material matHuntressCapeALT = Object.Instantiate(skinHuntressAlt.rendererInfos[2].defaultMaterial);

            Texture2D texHuntressDiffuse = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texHuntressDiffuse.LoadImage(Properties.Resources.texHuntressDiffuseYELLOW, true);
            texHuntressDiffuse.filterMode = FilterMode.Bilinear;
            texHuntressDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texHuntressDiffuseBow = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texHuntressDiffuseBow.LoadImage(Properties.Resources.texHuntressDiffuseBowYELLOW, true);
            texHuntressDiffuseBow.filterMode = FilterMode.Bilinear;
            texHuntressDiffuseBow.wrapMode = TextureWrapMode.Clamp;

            Texture2D texHuntressEmission = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texHuntressEmission.LoadImage(Properties.Resources.texHuntressEmissionYELLOW, true);
            texHuntressEmission.filterMode = FilterMode.Bilinear;
            texHuntressEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntress = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntress.LoadImage(Properties.Resources.texRampHuntressYELLOW, true);
            texRampHuntress.filterMode = FilterMode.Point;
            texRampHuntress.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressSoft = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntressSoft.LoadImage(Properties.Resources.texRampHuntressSoftYELLOW, true);
            texRampHuntressSoft.filterMode = FilterMode.Point;
            texRampHuntressSoft.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressSoft2 = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntressSoft2.LoadImage(Properties.Resources.texRampHuntressSoft2YELLOW, true);
            texRampHuntressSoft2.filterMode = FilterMode.Point;
            texRampHuntressSoft2.wrapMode = TextureWrapMode.Clamp;

            Texture2D texHuntressScarfDiffuseAlt = new Texture2D(64, 32, TextureFormat.DXT1, false);
            texHuntressScarfDiffuseAlt.LoadImage(Properties.Resources.texHuntressScarfDiffuseAltYELLOW, true);
            texHuntressScarfDiffuseAlt.filterMode = FilterMode.Bilinear;
            texHuntressScarfDiffuseAlt.wrapMode = TextureWrapMode.Repeat;


            matHuntress.mainTexture = texHuntressDiffuse;
            matHuntress.SetTexture("_EmTex", texHuntressEmission);
            matHuntress.SetColor("_EmColor", new Color(2f, 2f, 0f));

            matHuntressBow.mainTexture = texHuntressDiffuseBow;
            matHuntressBow.SetTexture("_EmTex", texHuntressEmission);
            matHuntressBow.SetColor("_EmColor", new Color(1f, 1f, 0f));
            //matHuntressBow.color = new Color(0.5f, 0.5f, 0.5f);

            //matHuntressCapeALT.color = new Color(0.8f, 0.7f, 0.4f);
            matHuntressCapeALT.mainTexture = texHuntressScarfDiffuseAlt;

            matBowString.SetTexture("_RemapTex", texRampHuntressSoft);
            matHuntressArrow.SetTexture("_RemapTex", texRampHuntressSoft2);
            matHuntressCharged.SetTexture("_RemapTex", texRampHuntress);

            NewRenderInfos[0].defaultMaterial = matBowString; 
            NewRenderInfos[1].defaultMaterial = matHuntressBow;
            NewRenderInfos[2].defaultMaterial = matHuntressArrow;
            NewRenderInfos[3].defaultMaterial = matHuntressArrow;
            NewRenderInfos[4].defaultMaterial = matHuntressCharged;
            NewRenderInfos[5].defaultMaterial = matHuntressArrow;
            NewRenderInfos[6].defaultMaterial = matHuntressArrow;
            NewRenderInfos[7].defaultMaterial = matHuntressArrow;
            NewRenderInfos[8].defaultMaterial = matHuntressArrow;
            NewRenderInfos[9].defaultMaterial = matHuntressArrow;
            NewRenderInfos[10].defaultMaterial = matHuntress;
            NewRenderInfos[11].defaultMaterial = matHuntressCapeALT;
       
            GameObject HuntressArrowRain = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Huntress/HuntressArrowRain.prefab").WaitForCompletion();
            GameObject HuntressArrowRainWolfo1 = PrefabAPI.InstantiateClone(HuntressArrowRain, "HuntressArrowRainWolfoYellow", false);

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
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinHuntressAlt.meshReplacements.Length];
            skinHuntressAlt.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[0].mesh = skinHuntressDefault.meshReplacements[0].mesh;

            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconHuntressYELLOW, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_HUNTRESS2", "Bee");

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinHuntressWolfoBee";
            newSkinDef.nameToken = "SIMU_SKIN_HUNTRESS2";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinHuntressAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinHuntressDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            GameObject DisplayShieldBug = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShieldOnly/DisplayShieldBug.prefab").WaitForCompletion();
            GameObject DisplayShieldBugNew = R2API.PrefabAPI.InstantiateClone(DisplayShieldBug, "HuntressBeeHead", false);

            GameObject DisplayBugWings = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Jetpack/DisplayBugWings.prefab").WaitForCompletion();
            GameObject DisplayBugWingsNew = R2API.PrefabAPI.InstantiateClone(DisplayBugWings, "HuntressBeeWings", false);

            ItemDisplay itemDisplayAntler = DisplayShieldBugNew.GetComponent<ItemDisplay>();
            ItemDisplay itemDisplayWings = DisplayBugWingsNew.GetComponent<ItemDisplay>();

            Material matShieldBug = Object.Instantiate(itemDisplayAntler.rendererInfos[0].defaultMaterial);
           
            Texture2D texShieldBugDiffuse = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texShieldBugDiffuse.LoadImage(Properties.Resources.texShieldBugDiffuse, true);
            texShieldBugDiffuse.filterMode = FilterMode.Bilinear;
            texShieldBugDiffuse.wrapMode = TextureWrapMode.Repeat;

            matShieldBug.mainTexture = texShieldBugDiffuse;
            matShieldBug.color = new Color(0.5f, 0.5f, 0.5f); //0.2123 1 0.9836 1
            matShieldBug.SetColor("_EmColor", new Color(0, 0, 0));
            matShieldBug.SetTexture("_EmTex", null);

            itemDisplayAntler.rendererInfos[0].renderer.material = matShieldBug;
            itemDisplayAntler.rendererInfos[0].defaultMaterial = matShieldBug;

            //
            Material matBugWings = Object.Instantiate(itemDisplayWings.rendererInfos[0].defaultMaterial);

            Texture2D texBugWingDiffuse = new Texture2D(1024, 512, TextureFormat.DXT5, false);
            texBugWingDiffuse.LoadImage(Properties.Resources.texBugWingDiffuse, true);
            texBugWingDiffuse.filterMode = FilterMode.Bilinear;
            texBugWingDiffuse.wrapMode = TextureWrapMode.Repeat;

            matBugWings.mainTexture = texBugWingDiffuse;
            matBugWings.SetTexture("_FresnelRamp", null);
            matBugWings.color = new Color(1, 1, 0.9f);

            itemDisplayWings.transform.GetChild(0).GetChild(0).GetChild(2).localScale = new Vector3(0, 0, 0);
            itemDisplayWings.transform.GetChild(0).GetChild(0).GetChild(3).localScale = new Vector3(0, 0, 0);

            GameObject.Destroy(itemDisplayWings.transform.GetChild(0).GetComponent<AkGameObj>());
            GameObject.Destroy(itemDisplayWings.transform.GetChild(0).GetComponent<AnimationEvents>());
            GameObject.Destroy(itemDisplayWings.transform.GetChild(0).GetComponent<Animator>());

            itemDisplayWings.rendererInfos[0].renderer.material = matBugWings;
            itemDisplayWings.rendererInfos[0].defaultMaterial = matBugWings;


            newSkinDef.addGameObjects = new ItemDisplayRule[]
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

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/HuntressBody"), newSkinDef);
        }

        public class ReplaceArrowRainVFX : MonoBehaviour
        {
            public GameObject newVFX;
            public SkinDef skinDef;

            public void FixedUpdate()
            {   
                GameObject owner = this.gameObject.GetComponent<RoR2.Projectile.ProjectileController>().owner;
                //Debug.LogWarning(owner);
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


        [RegisterAchievement("SIMU_SKIN_HUNTRESS", "Skins.Huntress.Wolfo", null, 5, null)]
        public class ClearSimulacrumHuntressBody : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HuntressBody");
            }
        }

        internal static void PrismAchievement()
        {
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_HUNTRESS_NAME", "Huntress" + Unlocks.unlockNamePrism);
            LanguageAPI.Add("ACHIEVEMENT_PRISM_SKIN_HUNTRESS_DESCRIPTION", "As Huntress" + Unlocks.unlockConditionPrism);
            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_HUNTRESS_NAME";
            unlockableDef.cachedName = "Skins.Huntress.Wolfo.Prism";
            unlockableDef.achievementIcon = WRect.MakeIcon(Properties.Resources.placeHolder);
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
        }

        /*[RegisterAchievement("PRISM_SKIN_HUNTRESS", "Skins.Huntress.Wolfo.Prism", null, 5, null)]
        public class AchievementPrismaticDissoHuntress2Body : AchievementPrismaticDisso
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HuntressBody");
            }
        }*/
    }
}