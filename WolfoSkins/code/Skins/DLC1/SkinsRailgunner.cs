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
            Mixed_White();

            Colossus();

            RailGunnerSkinsCamper();
            //RailGunnerSkinsSniper();
        }

        internal static void Colossus()
        {
            SkinDef skinRailGunnerAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerAltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRailGunnerAltColossus.rendererInfos.Length];
            System.Array.Copy(skinRailGunnerAltColossus.rendererInfos, NewRenderInfos, skinRailGunnerAltColossus.rendererInfos.Length);

            //0 : matRailGunnerAltColossusMetal
            //1 : matRailGunnerAltColossusMetal
            //2 : matRailGunnerAltColossus
            //3 : matRailGunnerAltColossusMetal
            //4 : matRailGunnerAltColossusMetal
            //5 : matRailgunnerLED
            //6 : matRailgunnerLED
            //7 : matRailgunnerLED
            //8 : matRailgunnerPistolLaser
            //9 : matRailgunnerLED
            //10 : matRailgunnerLED
            //11 : matRailgunBackpackIdle

            Material matRailGunnerAltColossusMetal = Object.Instantiate(skinRailGunnerAltColossus.rendererInfos[0].defaultMaterial);
            Material matRailGunnerAltColossus = Object.Instantiate(skinRailGunnerAltColossus.rendererInfos[2].defaultMaterial);


            Texture2D texTrimSheetRailGunnerAltColossus = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Colossus/texTrimSheetRailGunnerAltColossus.png");
            texTrimSheetRailGunnerAltColossus.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRailgunEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Colossus/texRailgunEmission.png");
            texRailgunEmission.wrapMode = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeU = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeV = TextureWrapMode.Clamp;
            texRailgunEmission.wrapModeW = TextureWrapMode.Repeat;

            Texture2D texRailGunnerAltColossusBaseDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Colossus/texRailGunnerAltColossusBaseDiffuse.png");
            texRailGunnerAltColossusBaseDiffuse.wrapMode = TextureWrapMode.Clamp;

            matRailGunnerAltColossus.mainTexture = texRailGunnerAltColossusBaseDiffuse;
            matRailGunnerAltColossusMetal.mainTexture = texTrimSheetRailGunnerAltColossus;           
            matRailGunnerAltColossusMetal.SetTexture("_EmTex", texRailgunEmission);

            NewRenderInfos[0].defaultMaterial = matRailGunnerAltColossusMetal;
            NewRenderInfos[1].defaultMaterial = matRailGunnerAltColossusMetal;
            NewRenderInfos[2].defaultMaterial = matRailGunnerAltColossus;
            NewRenderInfos[3].defaultMaterial = matRailGunnerAltColossusMetal;
            NewRenderInfos[4].defaultMaterial = matRailGunnerAltColossusMetal;
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunnerAltColossusWolfo_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER_COLOSSUS";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Colossus/railgunner.png"));
            newSkinDef.baseSkins = skinRailGunnerAltColossus.baseSkins;
            newSkinDef.meshReplacements = skinRailGunnerAltColossus.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinRailGunnerAltColossus.rootObject;


            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
        }

        internal static void Mixed_White()
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

            Texture2D texRailGunnerDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/White/texRailGunnerDiffuse.png");
            texRailGunnerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTrimSheetMilitaryLightMetal = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/White/texTrimSheetMilitaryLightMetal.png");
            texTrimSheetMilitaryLightMetal.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetMilitaryLightMetal.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texRailgunEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/White/texRailgunEmission.png");
            texRailgunEmission.wrapMode = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeU = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeV = TextureWrapMode.Clamp;
            texRailgunEmission.wrapModeW = TextureWrapMode.Repeat;

            Texture2D texTrimSheetRailGunnerMetalBody = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/White/texTrimSheetRailGunnerMetalBody.png");
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
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunnerWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/White/skinIconRailgunner.png"));
            newSkinDef.baseSkins = skinRailGunnerAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = skinRailGunnerAlt.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinRailGunnerDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
        }

        internal static void RailGunnerSkinsCamper()
        {
            SkinDef skinRailGunnerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerDefault.asset").WaitForCompletion();
            
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


            Texture2D texRailGunnerDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Green/texRailGunnerDiffuseCAMPER.png");
            texRailGunnerDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texTrimSheetMilitaryLightMetal = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Green/RailGunnerCamperMetalGun.png");
            texTrimSheetMilitaryLightMetal.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetMilitaryLightMetal.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetMilitaryLightMetal.wrapModeW = TextureWrapMode.Clamp;

            Texture2D RailGunnerCamperMetalBackpack = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Green/RailGunnerCamperMetalBackpack.png");
            RailGunnerCamperMetalBackpack.wrapMode = TextureWrapMode.Repeat;
            RailGunnerCamperMetalBackpack.wrapModeU = TextureWrapMode.Repeat;
            RailGunnerCamperMetalBackpack.wrapModeV = TextureWrapMode.Clamp;
            RailGunnerCamperMetalBackpack.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texRailgunEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Green/texRailgunEmissionCAMPER.png");
            texRailgunEmission.wrapMode = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeU = TextureWrapMode.Repeat;
            texRailgunEmission.wrapModeV = TextureWrapMode.Clamp;
            texRailgunEmission.wrapModeW = TextureWrapMode.Repeat;

            Texture2D texTrimSheetRailGunnerMetalBody = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Green/RailGunnerCamperMetalBody.png");
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
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunnerWolfoCamper_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER2";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Green/skinIconRailgunnerCAMPER.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinRailGunnerDefault };
            newSkinDef.meshReplacements = skinRailGunnerDefault.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinRailGunnerDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinRailGunnerDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
        }

        #region Idk Sniper
        /*
        internal static void RailGunnerSkinsSniper()
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
        

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunnerWolfo_Sniper_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER_SNIPER";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinRailGunnerAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = skinRailGunnerDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinRailGunnerDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
        }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_RAILGUNNER", "Skins.Railgunner.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumRailgunnerBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RailgunnerBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_RAILGUNNER", "Skins.Railgunner.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumRailgunnerBody2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RailgunnerBody");
            }
        }

    }
}