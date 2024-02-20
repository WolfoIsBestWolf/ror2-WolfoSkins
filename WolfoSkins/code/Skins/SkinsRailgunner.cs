using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsRailGunner
    {
        internal static void Start()
        {

        }


        internal static void RailGunnerSkins()
        {
            SkinDef skinRailGunnerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerDefault.asset").WaitForCompletion();
            SkinDef skinRailGunnerAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRailGunnerDefault.rendererInfos.Length];
            System.Array.Copy(skinRailGunnerDefault.rendererInfos, NewRenderInfos, skinRailGunnerDefault.rendererInfos.Length);

            //0 matRailGun
            //1 matRailGun
            //2 matRailGunnerBase
            //3 matRailgunnerTrim
            //4 matRailGun
            //5 matRailgunnerLED
            //6 matRailgunnerLED
            //7 matRailgunnerLED
            //8 matRailgunnerPistolLaser
            //9 matRailgunnerLED
            //10 matRailgunnerLED
            //11 matRailgunBackpackIdle
            Material matRailGun = Object.Instantiate(skinRailGunnerDefault.rendererInfos[0].defaultMaterial);
            Material matRailGunnerBase = Object.Instantiate(skinRailGunnerDefault.rendererInfos[2].defaultMaterial);
            Material matRailgunnerTrim = Object.Instantiate(skinRailGunnerDefault.rendererInfos[3].defaultMaterial);
            //Material matRailgunnerLED = Object.Instantiate(skinRailGunnerDefault.rendererInfos[5].defaultMaterial);

            Texture2D texRailGunnerDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texRailGunnerDiffuse.LoadImage(Properties.Resources.texRailGunnerDiffuse, true);
            texRailGunnerDiffuse.filterMode = FilterMode.Bilinear;
            texRailGunnerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTrimSheetMilitaryLightMetal = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            texTrimSheetMilitaryLightMetal.LoadImage(Properties.Resources.texTrimSheetMilitaryLightMetal, true);
            texTrimSheetMilitaryLightMetal.filterMode = FilterMode.Bilinear;
            texTrimSheetMilitaryLightMetal.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetMilitaryLightMetal.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texRailgunEmission = new Texture2D(64, 128, TextureFormat.DXT1, false);
            texRailgunEmission.LoadImage(Properties.Resources.texRailgunEmission, true);
            texRailgunEmission.filterMode = FilterMode.Bilinear;
            texRailgunEmission.wrapMode = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeU = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeV = TextureWrapMode.Clamp;
            texRailgunEmission.wrapModeW = TextureWrapMode.Repeat;

            Texture2D texTrimSheetRailGunnerMetalBody = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            texTrimSheetRailGunnerMetalBody.LoadImage(Properties.Resources.texTrimSheetRailGunnerMetalBody, true);
            texTrimSheetRailGunnerMetalBody.filterMode = FilterMode.Bilinear;
            texTrimSheetRailGunnerMetalBody.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetRailGunnerMetalBody.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetRailGunnerMetalBody.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetRailGunnerMetalBody.wrapModeW = TextureWrapMode.Clamp;

            matRailGun.mainTexture = texTrimSheetMilitaryLightMetal;
            matRailGun.SetTexture("_EmTex", texRailgunEmission);

            matRailGunnerBase.mainTexture = texRailGunnerDiffuse;

            matRailgunnerTrim.mainTexture = texTrimSheetRailGunnerMetalBody;
            matRailgunnerTrim.SetColor("_EmColor", new Color(0.3f,0.7f,0.8f)); //0.8868 0 0.2423 1
            //EM is white

            NewRenderInfos[0].defaultMaterial = matRailGun;
            NewRenderInfos[1].defaultMaterial = matRailGun;
            NewRenderInfos[2].defaultMaterial = matRailGunnerBase;
            NewRenderInfos[3].defaultMaterial = matRailgunnerTrim;
            NewRenderInfos[4].defaultMaterial = matRailGun;
            //NewRenderInfos[5].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[6].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[7].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[8].defaultMaterial = matRailgunnerPistolLaser;
            //NewRenderInfos[9].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[10].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[11].defaultMaterial = matRailgunBackpackIdle;
            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinRailGunnerDefault.meshReplacements.Length];
            skinRailGunnerDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[2].mesh = skinRailGunnerAlt.meshReplacements[2].mesh;

            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconRailgunner, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_RAILGUNNER", "Cold");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_RAILGUNNER_NAME", "Railgunner: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_RAILGUNNER_DESCRIPTION", "As Railgunner"+ Unlocks.unlockCondition);

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "SIMU_SKIN_RAILGUNNER";
            unlockableDef.cachedName = "Skins.RailGunner.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            if (WConfig.cfgUnlockAll.Value)
            {
                unlockableDef = null;
            }
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunnerWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinRailGunnerAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = skinRailGunnerAlt.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinRailGunnerDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
            RailGunnerSkinsCamper(unlockableDef);
            //RailGunnerSkinsSniper(unlockableDef);
        }

        internal static void RailGunnerSkinsCamper(UnlockableDef unlockableDef)
        {
            SkinDef skinRailGunnerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerDefault.asset").WaitForCompletion();
            SkinDef skinRailGunnerAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRailGunnerDefault.rendererInfos.Length];
            System.Array.Copy(skinRailGunnerDefault.rendererInfos, NewRenderInfos, skinRailGunnerDefault.rendererInfos.Length);

            //0 matRailGun
            //1 matRailGun
            //2 matRailGunnerBase
            //3 matRailgunnerTrim
            //4 matRailGun
            //5 matRailgunnerLED
            //6 matRailgunnerLED
            //7 matRailgunnerLED
            //8 matRailgunnerPistolLaser
            //9 matRailgunnerLED
            //10 matRailgunnerLED
            //11 matRailgunBackpackIdle
            Material matRailGun = Object.Instantiate(skinRailGunnerDefault.rendererInfos[0].defaultMaterial);
            Material matRailGunBACKPACK = Object.Instantiate(skinRailGunnerDefault.rendererInfos[0].defaultMaterial);
            Material matRailGunnerBase = Object.Instantiate(skinRailGunnerDefault.rendererInfos[2].defaultMaterial);
            Material matRailgunnerTrim = Object.Instantiate(skinRailGunnerDefault.rendererInfos[3].defaultMaterial);
            //Material matRailgunnerLED = Object.Instantiate(skinRailGunnerDefault.rendererInfos[5].defaultMaterial);

            Texture2D texRailGunnerDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texRailGunnerDiffuse.LoadImage(Properties.Resources.texRailGunnerDiffuseCAMPER, true);
            texRailGunnerDiffuse.filterMode = FilterMode.Bilinear;
            texRailGunnerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTrimSheetMilitaryLightMetal = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            texTrimSheetMilitaryLightMetal.LoadImage(Properties.Resources.RailGunnerCamperMetalGun, true);
            texTrimSheetMilitaryLightMetal.filterMode = FilterMode.Bilinear;
            texTrimSheetMilitaryLightMetal.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetMilitaryLightMetal.wrapModeW = TextureWrapMode.Clamp;

            Texture2D RailGunnerCamperMetalBackpack = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            RailGunnerCamperMetalBackpack.LoadImage(Properties.Resources.RailGunnerCamperMetalBackpack, true);
            RailGunnerCamperMetalBackpack.filterMode = FilterMode.Bilinear;
            RailGunnerCamperMetalBackpack.wrapMode = TextureWrapMode.Repeat;
            RailGunnerCamperMetalBackpack.wrapModeU = TextureWrapMode.Repeat;
            RailGunnerCamperMetalBackpack.wrapModeV = TextureWrapMode.Clamp;
            RailGunnerCamperMetalBackpack.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texRailgunEmission = new Texture2D(64, 128, TextureFormat.DXT1, false);
            texRailgunEmission.LoadImage(Properties.Resources.texRailgunEmissionCAMPER, true);
            texRailgunEmission.filterMode = FilterMode.Bilinear;
            texRailgunEmission.wrapMode = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeU = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeV = TextureWrapMode.Clamp;
            texRailgunEmission.wrapModeW = TextureWrapMode.Repeat;

            Texture2D texTrimSheetRailGunnerMetalBody = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            texTrimSheetRailGunnerMetalBody.LoadImage(Properties.Resources.RailGunnerCamperMetalBody, true);
            texTrimSheetRailGunnerMetalBody.filterMode = FilterMode.Bilinear;
            texTrimSheetRailGunnerMetalBody.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetRailGunnerMetalBody.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetRailGunnerMetalBody.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetRailGunnerMetalBody.wrapModeW = TextureWrapMode.Clamp;

            matRailGun.mainTexture = texTrimSheetMilitaryLightMetal;
            matRailGun.SetTexture("_EmTex", texRailgunEmission);

            matRailGunBACKPACK.mainTexture = RailGunnerCamperMetalBackpack;
            matRailGunBACKPACK.SetTexture("_EmTex", texRailgunEmission);

            matRailGunnerBase.mainTexture = texRailGunnerDiffuse;

            matRailgunnerTrim.mainTexture = texTrimSheetRailGunnerMetalBody;
            matRailgunnerTrim.SetColor("_EmColor", new Color(0.8f, 0.7f, 0.3f)); //0.8868 0 0.2423 1
            //EM is white

            NewRenderInfos[0].defaultMaterial = matRailGunBACKPACK;
            NewRenderInfos[1].defaultMaterial = matRailGunBACKPACK;
            NewRenderInfos[2].defaultMaterial = matRailGunnerBase;
            NewRenderInfos[3].defaultMaterial = matRailgunnerTrim;
            NewRenderInfos[4].defaultMaterial = matRailGun;
            //NewRenderInfos[5].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[6].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[7].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[8].defaultMaterial = matRailgunnerPistolLaser;
            //NewRenderInfos[9].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[10].defaultMaterial = matRailgunnerLED;
            //NewRenderInfos[11].defaultMaterial = matRailgunBackpackIdle;
            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinRailGunnerDefault.meshReplacements.Length];
            skinRailGunnerDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            //MeshReplacements[2].mesh = skinRailGunnerAlt.meshReplacements[2].mesh;

            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconRailgunnerCAMPER, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            LanguageAPI.Add("SIMU_SKIN_RAILGUNNER2", "Forest");

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunnerWolfoCamper";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER2";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinRailGunnerAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = skinRailGunnerDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinRailGunnerDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
        }

        internal static void RailGunnerSkinsSniper(UnlockableDef unlockableDef)
        {
            SkinDef skinRailGunnerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerDefault.asset").WaitForCompletion();
            SkinDef skinRailGunnerAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRailGunnerDefault.rendererInfos.Length];
            System.Array.Copy(skinRailGunnerDefault.rendererInfos, NewRenderInfos, skinRailGunnerDefault.rendererInfos.Length);

            //0 matRailGun
            //1 matRailGun
            //2 matRailGunnerBase
            //3 matRailgunnerTrim
            //4 matRailGun
            Material matRailGun = Object.Instantiate(skinRailGunnerDefault.rendererInfos[0].defaultMaterial);
            Material matRailGunBACKPACK = Object.Instantiate(skinRailGunnerDefault.rendererInfos[0].defaultMaterial);
            Material matRailGunnerBase = Object.Instantiate(skinRailGunnerDefault.rendererInfos[2].defaultMaterial);
            Material matRailgunnerTrim = Object.Instantiate(skinRailGunnerDefault.rendererInfos[3].defaultMaterial);
            //Material matRailgunnerLED = Object.Instantiate(skinRailGunnerDefault.rendererInfos[5].defaultMaterial);

            Texture2D texRailGunnerDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texRailGunnerDiffuse.LoadImage(Properties.Resources.texRailGunnerDiffuseSNIPER, true);
            texRailGunnerDiffuse.filterMode = FilterMode.Bilinear;
            texRailGunnerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTrimSheetMilitaryLightMetal = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            texTrimSheetMilitaryLightMetal.LoadImage(Properties.Resources.RailGunnerMetalGunSNIPER, true);
            texTrimSheetMilitaryLightMetal.filterMode = FilterMode.Bilinear;
            texTrimSheetMilitaryLightMetal.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetMilitaryLightMetal.wrapModeW = TextureWrapMode.Clamp;

            Texture2D RailGunnerCamperMetalBackpack = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            RailGunnerCamperMetalBackpack.LoadImage(Properties.Resources.RailGunnerMetalBackpackSNIPER, true);
            RailGunnerCamperMetalBackpack.filterMode = FilterMode.Bilinear;
            RailGunnerCamperMetalBackpack.wrapMode = TextureWrapMode.Repeat;
            RailGunnerCamperMetalBackpack.wrapModeU = TextureWrapMode.Repeat;
            RailGunnerCamperMetalBackpack.wrapModeV = TextureWrapMode.Clamp;
            RailGunnerCamperMetalBackpack.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texRailgunEmission = new Texture2D(64, 128, TextureFormat.DXT1, false);
            texRailgunEmission.LoadImage(Properties.Resources.texRailgunEmissionSNIPER, true);
            texRailgunEmission.filterMode = FilterMode.Bilinear;
            texRailgunEmission.wrapMode = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeU = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeV = TextureWrapMode.Clamp;
            texRailgunEmission.wrapModeW = TextureWrapMode.Repeat;

            Texture2D texTrimSheetRailGunnerMetalBody = new Texture2D(512, 1024, TextureFormat.DXT1, false);
            texTrimSheetRailGunnerMetalBody.LoadImage(Properties.Resources.RailGunnerMetalBodySNIPER, true);
            texTrimSheetRailGunnerMetalBody.filterMode = FilterMode.Bilinear;
            texTrimSheetRailGunnerMetalBody.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetRailGunnerMetalBody.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetRailGunnerMetalBody.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetRailGunnerMetalBody.wrapModeW = TextureWrapMode.Clamp;

            matRailGun.mainTexture = texTrimSheetMilitaryLightMetal;
            matRailGun.SetTexture("_EmTex", texRailgunEmission);

            matRailGunBACKPACK.mainTexture = RailGunnerCamperMetalBackpack;
            matRailGunBACKPACK.SetTexture("_EmTex", texRailgunEmission);

            matRailGunnerBase.mainTexture = texRailGunnerDiffuse;

            matRailgunnerTrim.mainTexture = texTrimSheetRailGunnerMetalBody;
            matRailgunnerTrim.SetColor("_EmColor", new Color(0.9797f, 0.533f, 1f, 1)); //0.8868 0 0.2423 1

            NewRenderInfos[0].defaultMaterial = matRailGunBACKPACK;
            NewRenderInfos[1].defaultMaterial = matRailGunBACKPACK;
            NewRenderInfos[2].defaultMaterial = matRailGunnerBase;
            NewRenderInfos[3].defaultMaterial = matRailgunnerTrim;
            NewRenderInfos[4].defaultMaterial = matRailGun;

            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinRailGunnerDefault.meshReplacements.Length];
            skinRailGunnerDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[2].mesh = skinRailGunnerAlt.meshReplacements[2].mesh;

            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconRailgunnerSNIPER, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            LanguageAPI.Add("SIMU_SKIN_RAILGUNNER_SNIPER", "Markswoman");

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunnerWolfo_Sniper";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER_SNIPER";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinRailGunnerAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = skinRailGunnerDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinRailGunnerDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
        }


        [RegisterAchievement("SIMU_SKIN_RAILGUNNER", "Skins.RailGunner.Wolfo", null, null)]
        public class ClearSimulacrumCrocoBody : AchievementSimuVoidTwisted
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RailgunnerBody");
            }
        }
    }
}