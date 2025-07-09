using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC1
{
    public class SkinsRailGunner
    {
        internal static void Start()
        {
            SkinDef skinRailGunnerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerDefault.asset").WaitForCompletion();
            SkinDef skinRailGunnerAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerAlt.asset").WaitForCompletion();
            SkinDef skinRailGunnerAltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerAltColossus.asset").WaitForCompletion();

            Mixed_White(skinRailGunnerDefault, skinRailGunnerAlt.ReturnParams());

            Colossus(skinRailGunnerAltColossus);

            RailGunnerSkinsCamper(skinRailGunnerDefault, skinRailGunnerAltColossus.ReturnParams());
            //RailGunnerSkinsSniper();

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
        }

        internal static void Colossus(SkinDef skinRailGunnerAltColossus)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinRailGunnerAltColossus_DLC2",
                nameToken = "SIMU_SKIN_RAILGUNNER_COLOSSUS",
                icon = H.GetIcon("dlc1/railgunner_dlc2"),
                original = skinRailGunnerAltColossus,
                cloneMesh = false,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matRailGunnerAltColossusMetal = CloneMat(newRenderInfos, 0);
            Material matRailGunnerAltColossus = CloneMat(newRenderInfos, 2);

            Texture2D texRailgunEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Colossus/texRailgunEmission.png");
            texRailgunEmission.wrapModeV = TextureWrapMode.Clamp;

            matRailGunnerAltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Colossus/texRailGunnerAltColossusBaseDiffuse.png");
            matRailGunnerAltColossusMetal.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Colossus/texTrimSheetRailGunnerAltColossus.png");
            matRailGunnerAltColossusMetal.SetTexture("_EmTex", texRailgunEmission);

            newRenderInfos[0].defaultMaterial = matRailGunnerAltColossusMetal;
            newRenderInfos[1].defaultMaterial = matRailGunnerAltColossusMetal;
            newRenderInfos[2].defaultMaterial = matRailGunnerAltColossus;
            newRenderInfos[3].defaultMaterial = matRailGunnerAltColossusMetal;
            newRenderInfos[4].defaultMaterial = matRailGunnerAltColossusMetal;
        }

        internal static void Mixed_White(SkinDef skinRailGunnerDefault, SkinDefParams skinRailGunnerAlt)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinRailGunner_1",
                nameToken = "SIMU_SKIN_RAILGUNNER",
                icon = H.GetIcon("dlc1/railgunner_white"),
                original = skinRailGunnerDefault,
                cloneMesh = true,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            newSkinDef.skinDefParams.meshReplacements[2] = skinRailGunnerAlt.meshReplacements[2];

            Material matRailGun = CloneMat(newRenderInfos, 0);
            Material matRailGunnerBase = CloneMat(newRenderInfos, 2);
            Material matRailgunnerTrim = CloneMat(newRenderInfos, 3);
            //Material matRailgunnerLED = CloneMat(RailGunnerDefault.rendererInfos[5].defaultMaterial);

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

            matRailGunnerBase.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/White/texRailGunnerDiffuse.png");

            matRailgunnerTrim.mainTexture = texTrimSheetRailGunnerMetalBody;
            matRailgunnerTrim.SetColor("_EmColor", new Color(0.3f, 0.7f, 0.8f)); //0.8868 0 0.2423 1
            //EM is white

            newRenderInfos[0].defaultMaterial = matRailGun;
            newRenderInfos[1].defaultMaterial = matRailGun;
            newRenderInfos[2].defaultMaterial = matRailGunnerBase;
            newRenderInfos[3].defaultMaterial = matRailgunnerTrim;
            newRenderInfos[4].defaultMaterial = matRailGun;
            //newRenderInfos[5].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[6].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[7].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[8].defaultMaterial = matRailgunnerPistolLaser;
            //newRenderInfos[9].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[10].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[11].defaultMaterial = matRailgunBackpackIdle;

          
        }

        internal static void RailGunnerSkinsCamper(SkinDef skinRailGunnerDefault, SkinDefParams skinRailGunnerAltColossus)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinRailGunnerWolfoCamper_1",
                nameToken = "SIMU_SKIN_RAILGUNNER2",
                icon = H.GetIcon("dlc1/railgunner_green"),
                original = skinRailGunnerDefault,
                cloneMesh = true,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            newSkinDef.skinDefParams.meshReplacements[2] = skinRailGunnerAltColossus.meshReplacements[2];
           
            Material matRailGun = CloneMat(newRenderInfos, 0);
            Material matRailGunBACKPACK = CloneMat(newRenderInfos, 0);
            Material matRailGunnerBase = CloneMat(newRenderInfos, 2);
            Material matRailgunnerTrim = CloneMat(newRenderInfos, 3);
            //Material matRailgunnerLED = CloneMat(RailGunnerDefault.rendererInfos[5].defaultMaterial);


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

            matRailGunnerBase.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/Railgunner/Green/texRailGunnerDiffuseCAMPER.png");

            matRailgunnerTrim.mainTexture = texTrimSheetRailGunnerMetalBody;
            matRailgunnerTrim.SetColor("_EmColor", new Color(0.8f, 0.7f, 0.3f)); //0.8868 0 0.2423 1
            //EM is white

            newRenderInfos[0].defaultMaterial = matRailGunBACKPACK;
            newRenderInfos[1].defaultMaterial = matRailGunBACKPACK;
            newRenderInfos[2].defaultMaterial = matRailGunnerBase;
            newRenderInfos[3].defaultMaterial = matRailgunnerTrim;
            newRenderInfos[4].defaultMaterial = matRailGun;
            //newRenderInfos[5].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[6].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[7].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[8].defaultMaterial = matRailgunnerPistolLaser;
            //newRenderInfos[9].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[10].defaultMaterial = matRailgunnerLED;
            //newRenderInfos[11].defaultMaterial = matRailgunBackpackIdle;
           
        }

        #region Idk Sniper
        /*
        internal static void RailGunnerSkinsSniper()
        {
            SkinDef skinRailGunnerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerDefault.asset").WaitForCompletion();
            SkinDef skinRailGunnerAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/Railgunner/skinRailGunnerAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] newRenderInfos = new CharacterModel.RendererInfo[skinRailGunnerDefault.rendererInfos.Length];
            System.Array.Copy(skinRailGunnerDefault.rendererInfos, newRenderInfos, skinRailGunnerDefault.rendererInfos.Length);

            //0 matRailGun
            //1 matRailGun
            //2 matRailGunnerBase
            //3 matRailgunnerTrim
            //4 matRailGun
            Material matRailGun = CloneMat(RailGunnerDefault.rendererInfos[0].defaultMaterial);
            Material matRailGunBACKPACK = CloneMat(RailGunnerDefault.rendererInfos[0].defaultMaterial);
            Material matRailGunnerBase = CloneMat(RailGunnerDefault.rendererInfos[2].defaultMaterial);
            Material matRailgunnerTrim = CloneMat(RailGunnerDefault.rendererInfos[3].defaultMaterial);
            //Material matRailgunnerLED = CloneMat(RailGunnerDefault.rendererInfos[5].defaultMaterial);

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

            newRenderInfos[0].defaultMaterial = matRailGunBACKPACK;
            newRenderInfos[1].defaultMaterial = matRailGunBACKPACK;
            newRenderInfos[2].defaultMaterial = matRailGunnerBase;
            newRenderInfos[3].defaultMaterial = matRailgunnerTrim;
            newRenderInfos[4].defaultMaterial = matRailGun;

            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinRailGunnerDefault.meshReplacements.Length];
            skinRailGunnerDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[2].mesh = skinRailGunnerAlt.meshReplacements[2].mesh;

    

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinRailGunner_Sniper_1";
            newSkinDef.nameToken = "SIMU_SKIN_RAILGUNNER_SNIPER";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinRailGunnerAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = skinRailGunnerDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = newRenderInfos;
            newSkinDef.rootObject = skinRailGunnerDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/Railgunner/RailgunnerBody.prefab").WaitForCompletion(), newSkinDef);
        }
        */
        #endregion

        [RegisterAchievement("CLEAR_ANY_RAILGUNNER", "Skins.Railgunner.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumRailgunnerBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RailgunnerBody");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_RAILGUNNER", "Skins.Railgunner.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumRailgunnerBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RailgunnerBody");
            }
        }

    }
}