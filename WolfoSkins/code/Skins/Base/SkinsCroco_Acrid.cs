using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsCroco_Acrid
    {
        internal static void Start()
        {
            //Black Returns
            AcridSkinBlack();
            //Lunar Returns
            RoRR_Lunar();
            //Purple highlits      
            Acrid_AltColossus();
            //highlits      
            Acrid_AltColossus_Default();
            //Lemurian
            AcridSkinLemurian(); 
        }



        internal static void Acrid_AltColossus_Default()
        {
            SkinDef skinCrocoAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] renderInfo = new CharacterModel.RendererInfo[skinCrocoAltColossus.rendererInfos.Length];
            System.Array.Copy(skinCrocoAltColossus.rendererInfos, renderInfo, skinCrocoAltColossus.rendererInfos.Length);

            //0 matCrocoColossus
            //1 null
            //2 matCrocoDiseaseDrippings
            //3 matCrocoColossus


            Texture2D texCrocoColossusDiffus = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusDiffuse2.png");
            texCrocoColossusDiffus.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoColossusEmissive = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusEmissive.png");
            texCrocoColossusEmissive.wrapMode = TextureWrapMode.Clamp;


            Material matCrocoColossus = Object.Instantiate(skinCrocoAltColossus.rendererInfos[0].defaultMaterial);

            matCrocoColossus.mainTexture = texCrocoColossusDiffus;
            matCrocoColossus.SetTexture("_EmTex", texCrocoColossusEmissive); //texRampHealingVariant

            renderInfo[0].defaultMaterial = matCrocoColossus; //
            renderInfo[3].defaultMaterial = matCrocoColossus; //
            //

            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                NameToken = "CROCO_SKIN_ALT2_NAME2",
                Name = "skinCrocoAltColossusWolfoBaseAlt",
                BaseSkins = skinCrocoAltColossus.baseSkins,
                RootObject = skinCrocoAltColossus.rootObject,
                RendererInfos = renderInfo,
                MeshReplacements = skinCrocoAltColossus.meshReplacements,
                UnlockableDef = skinCrocoAltColossus.unlockableDef,
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/icon2.png")),
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CrocoBody"), SkinInfo2);
        }


        internal static void Acrid_AltColossus()
        {
            SkinDef skinCrocoAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAltColossus.asset").WaitForCompletion();
            skinCrocoAltColossus.runtimeSkin = null;
            //Texture2D skinCrocoAltColossus = Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Croco/skinCrocoAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] renderInfo = new CharacterModel.RendererInfo[skinCrocoAltColossus.rendererInfos.Length];
            System.Array.Copy(skinCrocoAltColossus.rendererInfos, renderInfo, skinCrocoAltColossus.rendererInfos.Length);

            //0 matCrocoColossus
            //1 null
            //2 matCrocoDiseaseDrippings
            //3 matCrocoColossus


            Texture2D texCrocoColossusDiffus = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusDiffuse.png");
            texCrocoColossusDiffus.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoColossusEmissive = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/texCrocoColossusEmissive.png");
            texCrocoColossusEmissive.wrapMode = TextureWrapMode.Clamp;


            Material matCrocoColossus = Object.Instantiate(skinCrocoAltColossus.rendererInfos[0].defaultMaterial);
            
            matCrocoColossus.mainTexture = texCrocoColossusDiffus;
            matCrocoColossus.SetTexture("_EmTex", texCrocoColossusEmissive); //texRampHealingVariant
            matCrocoColossus.SetTexture("_FlowHeightRamp", null); //texRampHealingVariant
            matCrocoColossus.SetColor("_EmColor", new Color(0.8f,0.45f,1f,1f));//1 0.4226 0.2235 1

            //
            renderInfo[0].defaultMaterial = matCrocoColossus; 
            renderInfo[3].defaultMaterial = matCrocoColossus; 
            //
            SkinDefInfo SkinInfo2 = new SkinDefInfo
            {
                NameToken = "SIMU_SKIN_CROCO_COLOSSUS",
                Name = "skinCrocoAltColossusWolfo_AltBoss",
                BaseSkins = skinCrocoAltColossus.baseSkins,
                RootObject = skinCrocoAltColossus.rootObject,
                RendererInfos = renderInfo,
                MeshReplacements = skinCrocoAltColossus.meshReplacements,
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Colossus/icon.png")),
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CrocoBody"), SkinInfo2);
        }

        internal static void RoRR_Lunar()
        {
            SkinDef skinCrocoDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoDefault.asset").WaitForCompletion();
            SkinDef skinCrocoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAlt.asset").WaitForCompletion();
            skinCrocoAlt.runtimeSkin = null;

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinCrocoDefault.rendererInfos.Length];
            System.Array.Copy(skinCrocoDefault.rendererInfos, NewRenderInfos, skinCrocoDefault.rendererInfos.Length);

            //0 matCroco
            //1 null
            //2 matCrocoDiseaseDrippings 
            //3 matCrocoSpine 

            Material matCroco = Object.Instantiate(skinCrocoDefault.rendererInfos[0].defaultMaterial);
            Material matCrocoDiseaseDrippings = Object.Instantiate(skinCrocoDefault.rendererInfos[2].defaultMaterial);
            Material matCrocoSpine = Object.Instantiate(skinCrocoDefault.rendererInfos[3].defaultMaterial);
 
            Texture2D texCrocoDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoDiffuse.png");
            texCrocoDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoEmission.png");
            texCrocoEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoPoisonMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoPoisonMask.png");
            texCrocoPoisonMask.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoSpinesDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texCrocoSpinesDiffuse.png");
            texCrocoSpinesDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampCrocoDiseaseDark = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/texRampCrocoDiseaseDark.png");
            texRampCrocoDiseaseDark.wrapMode = TextureWrapMode.Clamp;


            matCroco.mainTexture = texCrocoDiffuse;
            matCroco.SetTexture("_EmTex", texCrocoEmission);
            matCroco.SetTexture("_FlowHeightRamp", texRampCrocoDiseaseDark);
            matCroco.SetTexture("_FlowHeightmap", texCrocoPoisonMask);
            matCroco.SetColor("_EmColor", new Color(0.2f,0.8f,1f));

            matCrocoDiseaseDrippings.SetTexture("_FlowHeightmap", texCrocoPoisonMask);

            matCrocoSpine.mainTexture = texCrocoSpinesDiffuse;
            matCrocoSpine.SetTexture("_EmTex", texCrocoSpinesDiffuse);
            matCrocoSpine.SetColor("_EmColor", new Color(0.3f, 0.3f, 0.3f));

            NewRenderInfos[0].defaultMaterial = matCroco;
            //NewRenderInfos[1].defaultMaterial = null;
            NewRenderInfos[2].defaultMaterial = matCrocoDiseaseDrippings;
            NewRenderInfos[3].defaultMaterial = matCrocoSpine;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCrocoWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_CROCO";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lunar/skinIconCroco.png"));
            newSkinDef.baseSkins = skinCrocoAlt.baseSkins;
            newSkinDef.meshReplacements = skinCrocoDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCrocoDefault.rootObject;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CrocoBody"), newSkinDef);

        }

        internal static void AcridSkinBlack()
        {
            SkinDef skinCrocoDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoDefault.asset").WaitForCompletion();
            SkinDef skinCrocoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinCrocoDefault.rendererInfos.Length];
            System.Array.Copy(skinCrocoDefault.rendererInfos, NewRenderInfos, skinCrocoDefault.rendererInfos.Length);

            //0 matCroco
            //1 null
            //2 matCrocoDiseaseDrippings 
            //3 matCrocoSpine 


            Material matCroco = Object.Instantiate(skinCrocoDefault.rendererInfos[0].defaultMaterial);
            Material matCrocoDiseaseDrippings = Object.Instantiate(skinCrocoDefault.rendererInfos[2].defaultMaterial);
            Material matCrocoSpine = Object.Instantiate(skinCrocoDefault.rendererInfos[3].defaultMaterial);

            Texture2D texCrocoDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoDiffuseBLACK.png");
            texCrocoDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoEmissionBLACK.png");
            texCrocoEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoPoisonMask = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoPoisonMaskBLACK.png");
            texCrocoPoisonMask.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoSpinesDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoSpinesDiffuseBLACK.png");
            texCrocoSpinesDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoSpinesDiffuseBLACK_EM = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texCrocoSpinesDiffuseBLACK_EM.png");
            texCrocoSpinesDiffuseBLACK_EM.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampCrocoDiseaseDark = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/texRampCrocoDiseaseDarkBLACK.png");
            texRampCrocoDiseaseDark.wrapMode = TextureWrapMode.Clamp;


            matCroco.mainTexture = texCrocoDiffuse;
            matCroco.SetTexture("_EmTex", texCrocoEmission);
            matCroco.SetTexture("_FlowHeightRamp", texRampCrocoDiseaseDark);
            matCroco.SetTexture("_FlowHeightmap", texCrocoPoisonMask);
            matCroco.SetColor("_EmColor", new Color(0.3f, 1.2f, 0.2f));

            matCrocoDiseaseDrippings.SetTexture("_FlowHeightmap", texCrocoPoisonMask);

            matCrocoSpine.mainTexture = texCrocoSpinesDiffuse;
            matCrocoSpine.SetTexture("_EmTex", texCrocoSpinesDiffuseBLACK_EM);
            matCrocoSpine.SetColor("_EmColor", new Color(0.66f, 0.88f, 0.22f));

            NewRenderInfos[0].defaultMaterial = matCroco;
            //NewRenderInfos[1].defaultMaterial = null;
            NewRenderInfos[2].defaultMaterial = matCrocoDiseaseDrippings;
            NewRenderInfos[3].defaultMaterial = matCrocoSpine;

            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCrocoWolfoBlack_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_CROCO_BLACK";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Black/skinIconCrocoBLACK.png"));
            newSkinDef.baseSkins = skinCrocoAlt.baseSkins;
            newSkinDef.meshReplacements = skinCrocoDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCrocoDefault.rootObject;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CrocoBody"), newSkinDef);
        }

        internal static void AcridSkinLemurian()
        {
            SkinDef skinCrocoDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoDefault.asset").WaitForCompletion();
            //SkinDef skinCrocoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinCrocoDefault.rendererInfos.Length];
            System.Array.Copy(skinCrocoDefault.rendererInfos, NewRenderInfos, skinCrocoDefault.rendererInfos.Length);

            Material matCroco = Object.Instantiate(skinCrocoDefault.rendererInfos[0].defaultMaterial);

            Texture2D texCrocoDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lemurian/texCrocoDiffuseLEMURIAN.png");
            texCrocoDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lemurian/texCrocoEmissionLEMURIAN.png");
            texCrocoEmission.wrapMode = TextureWrapMode.Clamp;

            matCroco.mainTexture = texCrocoDiffuse;
            matCroco.SetTexture("_EmTex", texCrocoEmission);
            matCroco.SetTexture("_FlowHeightRamp", null);
            matCroco.SetTexture("_FlowHeightmap", null);
            matCroco.SetColor("_EmColor", new Color(0.8f, 0.8f, 0.8f));
            //matCroco.DisableKeyword("FLOWMAP");

            NewRenderInfos[0].defaultMaterial = matCroco;
            NewRenderInfos[1].defaultMaterial = null;
            NewRenderInfos[2].defaultMaterial = null;
            NewRenderInfos[3].defaultMaterial = null;

            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCrocoWolfoLemurian";
            newSkinDef.nameToken = "LEMURIAN_BODY_NAME";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Croco/Lemurian/skinIconCrocoLEMURIAN.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinCrocoDefault };
            newSkinDef.meshReplacements = skinCrocoDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCrocoDefault.rootObject;
            newSkinDef.disableThis = skinCrocoDefault.rootObject.transform.GetChild(7);

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CrocoBody"), newSkinDef);
        }


        [RegisterAchievement("CLEAR_ANY_CROCO", "Skins.Croco.Wolfo.First", "BeatArena", 5, null)]
        public class CrocoBodyAltEndings : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CrocoBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_CROCO", "Skins.Croco.Wolfo.Both", "BeatArena", 5, null)]
        public class CrocoBodyAltEndings2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CrocoBody");
            }
        }
    }
}