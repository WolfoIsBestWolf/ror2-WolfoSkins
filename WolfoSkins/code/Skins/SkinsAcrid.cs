using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsAcrid
    {
        internal static void AcridSkin()
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
 
            Texture2D texCrocoDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texCrocoDiffuse.LoadImage(Properties.Resources.texCrocoDiffuse, true);
            texCrocoDiffuse.filterMode = FilterMode.Bilinear;
            texCrocoDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoEmission = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texCrocoEmission.LoadImage(Properties.Resources.texCrocoEmission, true);
            texCrocoEmission.filterMode = FilterMode.Bilinear;
            texCrocoEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoPoisonMask = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texCrocoPoisonMask.LoadImage(Properties.Resources.texCrocoPoisonMask, true);
            texCrocoPoisonMask.filterMode = FilterMode.Bilinear;
            texCrocoPoisonMask.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoSpinesDiffuse = new Texture2D(32, 256, TextureFormat.DXT5, false);
            texCrocoSpinesDiffuse.LoadImage(Properties.Resources.texCrocoSpinesDiffuse, true);
            texCrocoSpinesDiffuse.filterMode = FilterMode.Bilinear;
            texCrocoSpinesDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampCrocoDiseaseDark = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampCrocoDiseaseDark.LoadImage(Properties.Resources.texRampCrocoDiseaseDark, true);
            texRampCrocoDiseaseDark.filterMode = FilterMode.Bilinear;
            texRampCrocoDiseaseDark.wrapMode = TextureWrapMode.Clamp;


            matCroco.mainTexture = texCrocoDiffuse;
            matCroco.SetTexture("_EmTex", texCrocoEmission);
            matCroco.SetTexture("_FlowHeightRamp", texRampCrocoDiseaseDark);
            matCroco.SetTexture("_FlowHeightmap", texCrocoPoisonMask);
            matCroco.SetColor("_EmColor", new Color(0.2f,0.8f,1f));

            matCrocoDiseaseDrippings.SetTexture("_FlowHeightmap", texCrocoPoisonMask);

            matCrocoSpine.mainTexture = texCrocoSpinesDiffuse;
            matCrocoSpine.SetTexture("_EmTex", texCrocoSpinesDiffuse);
            matCrocoSpine.SetColor("_EmColor", new Color(0.5f, 0.5f, 0.5f));

            NewRenderInfos[0].defaultMaterial = matCroco;
            //NewRenderInfos[1].defaultMaterial = null;
            NewRenderInfos[2].defaultMaterial = matCrocoDiseaseDrippings;
            NewRenderInfos[3].defaultMaterial = matCrocoSpine;

            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconCroco, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_CROCO", "Lunar");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CROCO_NAME", "Acrid: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CROCO_DESCRIPTION", "As Acrid"+ WolfoSkins.unlockCondition);

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CROCO_NAME";
            unlockableDef.cachedName = "Skins.Croco.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCrocoWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_CROCO";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinCrocoAlt.baseSkins;
            newSkinDef.meshReplacements = skinCrocoDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCrocoDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CrocoBody"), newSkinDef);
            AcridSkinLemurian(unlockableDef);
        }



        internal static void AcridSkinLemurian(UnlockableDef unlockableDef)
        {
            SkinDef skinCrocoDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoDefault.asset").WaitForCompletion();
            SkinDef skinCrocoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Croco/skinCrocoAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinCrocoDefault.rendererInfos.Length];
            System.Array.Copy(skinCrocoDefault.rendererInfos, NewRenderInfos, skinCrocoDefault.rendererInfos.Length);


            Material matCroco = Object.Instantiate(skinCrocoDefault.rendererInfos[0].defaultMaterial);

            Texture2D texCrocoDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texCrocoDiffuse.LoadImage(Properties.Resources.texCrocoDiffuseLEMURIAN, true);
            texCrocoDiffuse.filterMode = FilterMode.Bilinear;
            texCrocoDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCrocoEmission = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texCrocoEmission.LoadImage(Properties.Resources.texCrocoEmissionLEMURIAN, true);
            texCrocoEmission.filterMode = FilterMode.Bilinear;
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
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconCrocoLEMURIAN, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_CROCO_LEMURIAN", "Wannabe Lemurian");
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCrocoWolfoLemurian";
            newSkinDef.nameToken = "SIMU_SKIN_CROCO_LEMURIAN";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinCrocoAlt.baseSkins;
            newSkinDef.meshReplacements = skinCrocoAlt.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCrocoAlt.rootObject;
            newSkinDef.unlockableDef = unlockableDef;
            newSkinDef.gameObjectActivations = new SkinDef.GameObjectActivation[]
            {
                new SkinDef.GameObjectActivation
                {
                    gameObject = skinCrocoAlt.rootObject.transform.GetChild(7).gameObject,
                    shouldActivate = false
                }
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CrocoBody"), newSkinDef);
        }


        [RegisterAchievement("SIMU_SKIN_CROCO", "Skins.Croco.Wolfo", "BeatArena", null)]
        public class ClearSimulacrumCrocoBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CrocoBody");
            }
        }
    }
}