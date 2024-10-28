using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsBandit2
    {
        internal static void Start()
        {
            BanditSkin();
            Bandit_AltColossus();
            BanditSkinGreen();
            BanditSkinPurple();
        }

        internal static void Bandit_AltColossus()
        {
            SkinDef skinBandit2AltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Bandit2/skinBandit2AltColossus.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinBandit2AltColossus.rendererInfos.Length];
            System.Array.Copy(skinBandit2AltColossus.rendererInfos, NewRenderInfos, skinBandit2AltColossus.rendererInfos.Length);

            ////Mesh
            //0 : Shotgun
            //1 : Body
            //2 : Coat
            //3 : Pistol
            //4 : Accessories
            //5 : Arms
            //6 : Hat

            ////Materials
            //0 : matBandit2AltColossus
            //1 : matBandit2AltColossus
            //2 : matBandit2AltColossus
            //3 : matBandit2AltColossus
            //4 : matBandit2AltColossusWeapons
            //5 : matBandit2AltColossusWeapons
            //6 : matBandit2AltColossusWeapons : not Hat
            //7 : matBandit2AltColossusWeapons

            Material matBandit2AltColossus = Object.Instantiate(skinBandit2AltColossus.rendererInfos[0].defaultMaterial);
            Material matBandit2AltColossusWeapons = Object.Instantiate(skinBandit2AltColossus.rendererInfos[4].defaultMaterial);


            Texture2D texBandit2AltColossusDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Colossus/texBandit2AltColossusDiffuse.png");
            texBandit2AltColossusDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBandit2AltColossusWeaponsDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Colossus/texBandit2AltColossusWeaponsDiffuse.png");
            texBandit2AltColossusWeaponsDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBandit2AltColossusWeaponsEmissive = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Colossus/texBandit2AltColossusWeaponsEmissive.png");
            texBandit2AltColossusWeaponsEmissive.wrapMode = TextureWrapMode.Clamp;


            matBandit2AltColossus.mainTexture = texBandit2AltColossusDiffuse;
            matBandit2AltColossus.SetColor("_EmColor", new Color(1f, 0.5f, 0f, 1f)); //0.3915 1 0.8729 1
            matBandit2AltColossus.SetFloat("_EmPower", 2.22f); //0.89
            //matBandit2AltColossus.SetFloat("_NormalStrength", 2.22f); //0.89
            matBandit2AltColossus.SetFloat("_GreenChannelBias", -5f); //0.89
            //matBandit2AltColossus.SetTexture("_GreenChannelTex", null);
            //matBandit2AltColossus.SetColor("_EmColor", new Color32(255,118,76,255)); //0.3915 1 0.8729 1
            matBandit2AltColossusWeapons.mainTexture = texBandit2AltColossusWeaponsDiffuse;
            matBandit2AltColossusWeapons.SetTexture("_EmTex", texBandit2AltColossusWeaponsEmissive);

            NewRenderInfos[0].defaultMaterial = matBandit2AltColossus;
            NewRenderInfos[1].defaultMaterial = matBandit2AltColossus;
            NewRenderInfos[2].defaultMaterial = matBandit2AltColossus;
            NewRenderInfos[3].defaultMaterial = matBandit2AltColossus;
            NewRenderInfos[4].defaultMaterial = matBandit2AltColossusWeapons;
            NewRenderInfos[5].defaultMaterial = matBandit2AltColossusWeapons;
            NewRenderInfos[6].defaultMaterial = matBandit2AltColossusWeapons;
            NewRenderInfos[7].defaultMaterial = matBandit2AltColossusWeapons;
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinBandit2AltColossusWolfo_AltBoss";
            newSkinDef.nameToken = "SIMU_SKIN_BANDIT_COLOSSUS";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Colossus/icon.png"));
            newSkinDef.baseSkins = skinBandit2AltColossus.baseSkins;
            newSkinDef.meshReplacements = skinBandit2AltColossus.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinBandit2AltColossus.rootObject;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body"), newSkinDef);
        }

        internal static void BanditSkin()
        {
            //RoRR Red Bandit
            SkinDef BanditDefaultSkin = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef BanditAltSkin = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            CharacterModel.RendererInfo[] BanditRedRenderInfos = new CharacterModel.RendererInfo[8];
            System.Array.Copy(BanditDefaultSkin.rendererInfos, BanditRedRenderInfos, 8);

            Material matBanditRed1 = Object.Instantiate(BanditDefaultSkin.rendererInfos[0].defaultMaterial);
            //Material matBanditRed2      = Object.Instantiate(BanditDefaultSkin.rendererInfos[1].defaultMaterial);
            //Material matBanditRed3      = Object.Instantiate(BanditDefaultSkin.rendererInfos[2].defaultMaterial);
            Material matBandit2Coat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2CoatHat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2Shotgun = Object.Instantiate(BanditDefaultSkin.rendererInfos[4].defaultMaterial);
            Material matBandit2Knife = Object.Instantiate(BanditDefaultSkin.rendererInfos[5].defaultMaterial);
            //Material matBandit2Coat2    = Object.Instantiate(BanditDefaultSkin.rendererInfos[6].defaultMaterial);
            Material matBandit2Revolver = Object.Instantiate(BanditDefaultSkin.rendererInfos[7].defaultMaterial);


            Texture2D texBanditRedDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditRedDiffuse.png");
            texBanditRedDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedCoatDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditRedCoatDiffuse.png");
            texBanditRedCoatDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditRedEmission.png");
            texBanditRedEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditShotgunDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditShotgunDiffuse.png");
            texBanditShotgunDiffuse.wrapMode = TextureWrapMode.Clamp;

            //
            matBanditRed1.mainTexture = texBanditRedDiffuse;
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", texBanditRedEmission);
            matBanditRed1.SetColor("_EmColor", new Color(1.1f, 0.88f, 1.1f)); //0 0.3491 0.327 1
            //matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.12f, 0.19f)); //100 30 50
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.5f, 0.15f, 0.25f)); //100 30 50
            //matBandit2Coat.color = new Color(0.85f, 0.85f, 0.82f);
            matBandit2Coat.color = new Color(0.95f, 0.95f, 0.87f);

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2AccessoriesMesh //texBandit2Diffuse
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2ArmsMesh
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2BodyMesh
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;     //matBandit2Coat     //Bandit2CoatMesh        //texBandit2CoatDiffuse
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;     //matBandit2Shotgun  //BanditShotgunMesh      //texBanditShotgunDiffuse
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;     //matBandit2Knife    //BladeMesh              //texBanditShotgunDiffuse
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;     //matBandit2Coat     //Bandit2HatMesh
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;     //matBandit2Revolver //BanditPistolMesh       //texBanditShotgunDiffuse

            //
            RoR2.SkinDef.MeshReplacement[] BanditRedMesh = new SkinDef.MeshReplacement[BanditDefaultSkin.meshReplacements.Length];
            BanditDefaultSkin.meshReplacements.CopyTo(BanditRedMesh,0);

            BanditRedMesh[3] = BanditAltSkin.meshReplacements[2];
            //
            //

            //
            SkinDefInfo BanditRedSkinInfos = new SkinDefInfo
            {
                Name = "skinBandit2Wolfo_Simu",
                NameToken = "SIMU_SKIN_BANDIT",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditRedSkinIcon.png")),
                BaseSkins = BanditAltSkin.baseSkins,
                MeshReplacements = BanditRedMesh,
                ProjectileGhostReplacements = BanditDefaultSkin.projectileGhostReplacements,
                RendererInfos = BanditRedRenderInfos,
                RootObject = BanditDefaultSkin.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body"), BanditRedSkinInfos);      
        }

        internal static void BanditSkinPurple()
        {
            //RoRR Red Bandit
            SkinDef BanditDefaultSkin = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef BanditAltSkin = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            CharacterModel.RendererInfo[] BanditRedRenderInfos = new CharacterModel.RendererInfo[8];
            System.Array.Copy(BanditDefaultSkin.rendererInfos, BanditRedRenderInfos, 8);

            Material matBanditRed1 = Object.Instantiate(BanditDefaultSkin.rendererInfos[0].defaultMaterial);
            //Material matBanditRed2      = Object.Instantiate(BanditDefaultSkin.rendererInfos[1].defaultMaterial);
            //Material matBanditRed3      = Object.Instantiate(BanditDefaultSkin.rendererInfos[2].defaultMaterial);
            Material matBandit2Coat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2CoatHat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2Shotgun = Object.Instantiate(BanditDefaultSkin.rendererInfos[4].defaultMaterial);
            Material matBandit2Knife = Object.Instantiate(BanditDefaultSkin.rendererInfos[5].defaultMaterial);
            //Material matBandit2Coat2    = Object.Instantiate(BanditDefaultSkin.rendererInfos[6].defaultMaterial);
            Material matBandit2Revolver = Object.Instantiate(BanditDefaultSkin.rendererInfos[7].defaultMaterial);


            Texture2D texBanditRedDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditRedDiffusePURPLE.png");
            texBanditRedDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedCoatDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditRedCoatDiffusePURPLE.png");
            texBanditRedCoatDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditRedEmissionPURPLE.png");
            texBanditRedEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditShotgunDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditShotgunDiffusePURPLE.png");
            texBanditShotgunDiffuse.wrapMode = TextureWrapMode.Clamp;

            //
            matBanditRed1.mainTexture = texBanditRedDiffuse;
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", texBanditRedEmission);
            matBanditRed1.SetColor("_EmColor", new Color(1.2f, 1.2f, 0.5f)); //0 0.3491 0.327 1
            //matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.12f, 0.19f)); //100 30 50
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.8f, 0.7f, 0f)); //100 30 50
            //matBandit2Coat.color = new Color(0.85f, 0.85f, 0.82f);
            //matBandit2Coat.color = new Color(0.95f, 0.95f, 0.87f);

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2AccessoriesMesh //texBandit2Diffuse
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2ArmsMesh
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2BodyMesh
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;     //matBandit2Coat     //Bandit2CoatMesh        //texBandit2CoatDiffuse
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;     //matBandit2Shotgun  //BanditShotgunMesh      //texBanditShotgunDiffuse
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;     //matBandit2Knife    //BladeMesh              //texBanditShotgunDiffuse
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;     //matBandit2Coat     //Bandit2HatMesh
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;     //matBandit2Revolver //BanditPistolMesh       //texBanditShotgunDiffuse

            //
            RoR2.SkinDef.MeshReplacement[] BanditRedMesh = new SkinDef.MeshReplacement[BanditDefaultSkin.meshReplacements.Length];
            BanditDefaultSkin.meshReplacements.CopyTo(BanditRedMesh,0);

            BanditRedMesh[3] = BanditAltSkin.meshReplacements[2];
            //
            //
            //Unlockable
    

            SkinDefInfo BanditRedSkinInfos = new SkinDefInfo
            {
                Name = "skinBandit2WolfoPurple_Simu",
                NameToken = "SIMU_SKIN_BANDIT2",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/icon.png")),
                BaseSkins = BanditAltSkin.baseSkins,
                MeshReplacements = BanditRedMesh,
                ProjectileGhostReplacements = BanditDefaultSkin.projectileGhostReplacements,
                RendererInfos = BanditRedRenderInfos,
                RootObject = BanditDefaultSkin.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body"), BanditRedSkinInfos);
           
        }

        internal static void BanditSkinGreen()
        {
            //RoRR Red Bandit
            SkinDef BanditDefaultSkin = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef BanditAltSkin = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            CharacterModel.RendererInfo[] BanditRedRenderInfos = new CharacterModel.RendererInfo[8];
            System.Array.Copy(BanditDefaultSkin.rendererInfos, BanditRedRenderInfos, 8);

            Material matBanditRed1 = Object.Instantiate(BanditDefaultSkin.rendererInfos[0].defaultMaterial);
            //Material matBanditRed2      = Object.Instantiate(BanditDefaultSkin.rendererInfos[1].defaultMaterial);
            //Material matBanditRed3      = Object.Instantiate(BanditDefaultSkin.rendererInfos[2].defaultMaterial);
            Material matBandit2Coat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2CoatHat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2Shotgun = Object.Instantiate(BanditDefaultSkin.rendererInfos[4].defaultMaterial);
            Material matBandit2Knife = Object.Instantiate(BanditDefaultSkin.rendererInfos[5].defaultMaterial);
            //Material matBandit2Coat2    = Object.Instantiate(BanditDefaultSkin.rendererInfos[6].defaultMaterial);
            Material matBandit2Revolver = Object.Instantiate(BanditDefaultSkin.rendererInfos[7].defaultMaterial);


            Texture2D texBanditRedDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditRedDiffuseGREEN.png");
            texBanditRedDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedCoatDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditRedCoatDiffuseGREEN.png");
            texBanditRedCoatDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditRedEmissionGREEN.png");
            texBanditRedEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditShotgunDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditShotgunDiffuseGREEN.png");
            texBanditShotgunDiffuse.wrapMode = TextureWrapMode.Clamp;

            //
            matBanditRed1.mainTexture = texBanditRedDiffuse;
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", texBanditRedEmission);
            matBanditRed1.SetColor("_EmColor", new Color(1f, 1.2f, 0.7f)); //0 0.3491 0.327 1
            //matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.12f, 0.19f)); //100 30 50
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.5f, 0.15f)); //100 30 50
            //matBandit2Coat.color = new Color(0.95f, 0.95f, 0.87f);

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2AccessoriesMesh //texBandit2Diffuse
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2ArmsMesh
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2BodyMesh
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;     //matBandit2Coat     //Bandit2CoatMesh        //texBandit2CoatDiffuse
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;     //matBandit2Shotgun  //BanditShotgunMesh      //texBanditShotgunDiffuse
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;     //matBandit2Knife    //BladeMesh              //texBanditShotgunDiffuse
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;     //matBandit2Coat     //Bandit2HatMesh
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;     //matBandit2Revolver //BanditPistolMesh       //texBanditShotgunDiffuse

            //
            RoR2.SkinDef.MeshReplacement[] BanditRedMesh = new SkinDef.MeshReplacement[BanditDefaultSkin.meshReplacements.Length];
            BanditDefaultSkin.meshReplacements.CopyTo(BanditRedMesh,0);


            BanditRedMesh[3] = BanditAltSkin.meshReplacements[2];
            //
            //
            SkinDefInfo BanditRedSkinInfos = new SkinDefInfo
            {
                Name = "skinBandit2Wolfo_Green_Simu",
                NameToken = "SIMU_SKIN_BANDIT_GREEN",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/icon.png")),
                BaseSkins = BanditAltSkin.baseSkins,
                MeshReplacements = BanditRedMesh,
                ProjectileGhostReplacements = BanditDefaultSkin.projectileGhostReplacements,
                RendererInfos = BanditRedRenderInfos,
                RootObject = BanditDefaultSkin.rootObject,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body"), BanditRedSkinInfos);

        }

        [RegisterAchievement("CLEAR_ANY_BANDIT2", "Skins.Bandit2.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumBandit2Body : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Bandit2Body");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_BANDIT2", "Skins.Bandit2.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumBandit2Body2 : Achievement_AltBoss_AND_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Bandit2Body");
            }
        }
    }
}