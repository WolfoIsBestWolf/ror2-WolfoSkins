using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod.Base
{
    public class SkinsBandit2
    {
        internal static void Start()
        {
            SkinDef skinBandit2Default = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Bandit2/skinBandit2Default.asset").WaitForCompletion();
            SkinDefParams skinBandit2Alt = Addressables.LoadAssetAsync<SkinDefParams>(key: "RoR2/Base/Bandit2/skinBandit2Alt_params.asset").WaitForCompletion();
            SkinDef skinBandit2AltColossus = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Bandit2/skinBandit2AltColossus.asset").WaitForCompletion();

            Bandit_Red(skinBandit2Default, skinBandit2Alt);
            Bandit_AltColossus(skinBandit2AltColossus);
            Bandit_Green(skinBandit2Default, skinBandit2Alt);
            Bandit_Purple(skinBandit2Default, skinBandit2Alt);

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
        }

        internal static void Bandit_AltColossus(SkinDef skinBandit2AltColossus)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinBandit2AltColossus_DLC2",
                nameToken = "SIMU_SKIN_BANDIT_COLOSSUS",
                icon = H.GetIcon("bandit_dlc2"),
                original = skinBandit2AltColossus,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matBandit2AltColossus = H.CloneMat(newRenderInfos, 0);
            Material matBandit2AltColossusWeapons = H.CloneMat(newRenderInfos, 4);

            matBandit2AltColossus.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Colossus/texBandit2AltColossusDiffuse.png");
            matBandit2AltColossus.SetColor("_EmColor", new Color(1f, 0.5f, 0f, 1f)); //0.3915 1 0.8729 1
            matBandit2AltColossus.SetFloat("_EmPower", 2.22f); //0.89

            matBandit2AltColossus.SetFloat("_GreenChannelBias", -5f); //0.89

            matBandit2AltColossusWeapons.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Colossus/texBandit2AltColossusWeaponsDiffuse.png");
            matBandit2AltColossusWeapons.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Colossus/texBandit2AltColossusWeaponsEmissive.png"));

            newRenderInfos[0].defaultMaterial = matBandit2AltColossus;
            newRenderInfos[1].defaultMaterial = matBandit2AltColossus;
            newRenderInfos[2].defaultMaterial = matBandit2AltColossus;
            newRenderInfos[3].defaultMaterial = matBandit2AltColossus;
            newRenderInfos[4].defaultMaterial = matBandit2AltColossusWeapons;
            newRenderInfos[5].defaultMaterial = matBandit2AltColossusWeapons;
            newRenderInfos[6].defaultMaterial = matBandit2AltColossusWeapons;
            newRenderInfos[7].defaultMaterial = matBandit2AltColossusWeapons;

        }

        internal static void Bandit_Red(SkinDef skinBandit2Default, SkinDefParams skinBandit2Alt)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinBandit2R_Red_1",
                nameToken = "SIMU_SKIN_BANDIT",
                icon = H.GetIcon("bandit_red"),
                original = skinBandit2Default,
            });
            CharacterModel.RendererInfo[] BanditRedRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            newSkinDef.skinDefParams.meshReplacements[3] = skinBandit2Alt.meshReplacements[2];


            Material matBanditRed1 = H.CloneMat(BanditRedRenderInfos, 0);
            Material matBandit2Coat = H.CloneMat(BanditRedRenderInfos, 3);
            Material matBandit2CoatHat = H.CloneMat(BanditRedRenderInfos, 3);
            Material matBandit2Shotgun = H.CloneMat(BanditRedRenderInfos, 4);
            Material matBandit2Knife = H.CloneMat(BanditRedRenderInfos, 5);
            Material matBandit2Revolver = H.CloneMat(BanditRedRenderInfos, 7);


            Texture2D texBanditRedCoatDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditRedCoatDiffuse.png");
            Texture2D texBanditShotgunDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditShotgunDiffuse.png");

            matBanditRed1.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditRedDiffuse.png");
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Red/texBanditRedEmission.png"));
            matBanditRed1.SetColor("_EmColor", new Color(1.1f, 0.88f, 1.1f)); //0 0.3491 0.327 1
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.5f, 0.15f, 0.25f)); //100 30 50
            matBandit2Coat.color = new Color(0.95f, 0.95f, 0.87f);

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;




        }

        internal static void Bandit_Purple(SkinDef skinBandit2Default, SkinDefParams skinBandit2Alt)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinBandit2R_Purple_1",
                nameToken = "SIMU_SKIN_BANDIT2",
                icon = H.GetIcon("bandit_purple"),
                original = skinBandit2Default,
                cloneMesh = true,
            });

            CharacterModel.RendererInfo[] BanditRedRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            newSkinDef.skinDefParams.meshReplacements[3] = skinBandit2Alt.meshReplacements[2];

            Material matBanditRed1 = H.CloneMat(BanditRedRenderInfos, 0);
            Material matBandit2Coat = H.CloneMat(BanditRedRenderInfos, 3);
            Material matBandit2CoatHat = H.CloneMat(BanditRedRenderInfos, 3);
            Material matBandit2Shotgun = H.CloneMat(BanditRedRenderInfos, 4);
            Material matBandit2Knife = H.CloneMat(BanditRedRenderInfos, 5);
            Material matBandit2Revolver = H.CloneMat(BanditRedRenderInfos, 7);


            Texture2D texBanditRedCoatDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditRedCoatDiffusePURPLE.png");
            Texture2D texBanditShotgunDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditShotgunDiffusePURPLE.png");

            matBanditRed1.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditRedDiffusePURPLE.png");
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Purple/texBanditRedEmissionPURPLE.png"));
            matBanditRed1.SetColor("_EmColor", new Color(1.2f, 1.2f, 0.5f)); //0 0.3491 0.327 1
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.8f, 0.7f, 0f)); //100 30 50

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2AccessoriesMesh //texBandit2Diffuse
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2ArmsMesh
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2BodyMesh
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;     //matBandit2Coat     //Bandit2CoatMesh        //texBandit2CoatDiffuse
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;     //matBandit2Shotgun  //BanditShotgunMesh      //texBanditShotgunDiffuse
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;     //matBandit2Knife    //BladeMesh              //texBanditShotgunDiffuse
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;     //matBandit2Coat     //Bandit2HatMesh
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;     //matBandit2Revolver //BanditPistolMesh       //texBanditShotgunDiffuse

        }

        internal static void Bandit_Green(SkinDef skinBandit2Default, SkinDefParams skinBandit2Alt)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinBandit2R_Green_1",
                nameToken = "SIMU_SKIN_BANDIT_GREEN",
                icon = H.GetIcon("bandit_green"),
                original = skinBandit2Default,
                cloneMesh = true,
            });
            CharacterModel.RendererInfo[] BanditRedRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            newSkinDef.skinDefParams.meshReplacements[3] = skinBandit2Alt.meshReplacements[2];

            Material matBanditRed1 = H.CloneMat(BanditRedRenderInfos, 0);
            Material matBandit2Coat = H.CloneMat(BanditRedRenderInfos, 3);
            Material matBandit2CoatHat = H.CloneMat(BanditRedRenderInfos, 3);
            Material matBandit2Shotgun = H.CloneMat(BanditRedRenderInfos, 4);
            Material matBandit2Knife = H.CloneMat(BanditRedRenderInfos, 5);
            Material matBandit2Revolver = H.CloneMat(BanditRedRenderInfos, 7);


            Texture2D texBanditRedCoatDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditRedCoatDiffuseGREEN.png");
            Texture2D texBanditShotgunDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditShotgunDiffuseGREEN.png");

            matBanditRed1.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditRedDiffuseGREEN.png");
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/base/Bandit2/Green/texBanditRedEmissionGREEN.png"));
            matBanditRed1.SetColor("_EmColor", new Color(1f, 1.2f, 0.7f)); //0 0.3491 0.327 1
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.5f, 0.15f)); //100 30 50

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;

        }

        [RegisterAchievement("CLEAR_ANY_BANDIT2", "Skins.Bandit2.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumBandit2Body : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Bandit2Body");
            }
        }

        [RegisterAchievement("CLEAR_BOTH_BANDIT2", "Skins.Bandit2.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumBandit2Body2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Bandit2Body");
            }
        }
    }
}