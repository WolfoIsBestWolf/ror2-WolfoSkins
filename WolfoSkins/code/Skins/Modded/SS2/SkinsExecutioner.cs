using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.Mod
{
    public class SkinsExecutioner
    {
        private static SkinDef SkinDefYELLOW;
        private static SkinDef SkinDefRED;
        private static SkinDef SkinDefBLUE;
        private static GameObject ExecutionerBodyS;

        internal static void ModdedSkin(GameObject ExecutionerBody)
        {
            return;
            Debug.Log("Executioner Skins");
            //GameModeCatalog.availability.CallWhenAvailable(AddVFXLate);
            //For some reason needs to be called hella later?
            //RoR2Application.onLoad += AddVFXLate;

            ExecutionerBodyS = ExecutionerBody;

            BodyIndex ExecutionerIndex = ExecutionerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ExecutionerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinExecutioner = modelSkinController.skins[0];
            SkinDef skinExecutionerMastery = modelSkinController.skins[1];


            SkinDef red = ModdedSkinRED(skinExecutioner);
            SkinDef yellow = ModdedSkinYELLOW(skinExecutionerMastery);
            SkinDef nerd = ModdedSkinBLUE(skinExecutioner);

            //0 matExecutioner2Armor / texExecutioner2Diffuse
            //1 matExecutioner2Cloth / texExecutioner2ClothDiffuse
            //2 matExe2Jumpkit / texExecutioner2KitD
            //3 matExe2Gun / texExecutioner2GunD
            //4 matExecutionerAxe / matExecutionerAxe
        }

        internal static SkinDef ModdedSkinRED(SkinDef skinExecutioner)
        {
            SkinDefEnhanced newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinExecutioner_1",
                nameToken = "SIMU_SKIN_EXECUTIONER",
                icon = H.GetIcon("mod/ss2/executioner_red"),
                original = skinExecutioner,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matExecutioner2Armor = CloneMat(ref newRenderInfos, 0);
            Material matExecutioner2Cloth = CloneMat(ref newRenderInfos, 1);
            Material matExe2Jumpkit = CloneMat(ref newRenderInfos, 2);
            Material matExe2Gun = CloneMat(ref newRenderInfos, 3);
            Material matExecutionerAxe = CloneMat(ref newRenderInfos, 4);

            Color EmColor = new Color(1.5f, 0f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExecutioner2Armor.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/texExecutioner2Diffuse.png");
            matExecutioner2Armor.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/texExecutioner2Emission.png"));
            matExecutioner2Armor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExecutioner2Cloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/texExecutioner2ClothDiffuse.png");

            matExe2Jumpkit.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeKitTex.png");
            matExe2Jumpkit.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeKitEm.png"));
            matExe2Jumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Gun.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExegunTex.png");
            matExe2Gun.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExegunEmTex.png"));
            matExe2Gun.SetColor("_EmColor", EmColor);

            //matExecutionerAxe.mainTexture = ExeAxe4;
            matExecutionerAxe.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/texExeAxeRamp.png"));
            matExecutionerAxe.SetColor("_TintColor", EmColor); //0.0688 0.5619 0.9717 1

            newRenderInfos[0].defaultMaterial = matExecutioner2Armor;
            newRenderInfos[1].defaultMaterial = matExecutioner2Cloth;
            newRenderInfos[2].defaultMaterial = matExe2Jumpkit;
            newRenderInfos[3].defaultMaterial = matExe2Gun;
            newRenderInfos[4].defaultMaterial = matExecutionerAxe;


            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(2f, 0.2f, 0.2f, 0.75f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefEnhanced.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };
            SkinDefRED = newSkinDef;
            return newSkinDef;
        }

        internal static SkinDef ModdedSkinBLUE(SkinDef skinExecutioner)
        {
            SkinDefEnhanced newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinExecutioner_Blue_1",
                nameToken = "SIMU_SKIN_EXECUTIONER_BLUE",
                icon = H.GetIcon("mod/ss2/executioner_blue"),
                original = skinExecutioner,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matExecutioner2Armor = CloneMat(ref newRenderInfos, 0);
            Material matExecutioner2Cloth = CloneMat(ref newRenderInfos, 1);
            Material matExe2Jumpkit = CloneMat(ref newRenderInfos, 2);
            Material matExe2Gun = CloneMat(ref newRenderInfos, 3);
            Material matExecutionerAxe = CloneMat(ref newRenderInfos, 4);

            Color EmColor = new Color(0f, 1.1f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExecutioner2Armor.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/texExecutioner2Diffuse.png");
            matExecutioner2Armor.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/texExecutioner2Emission.png"));
            matExecutioner2Armor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExecutioner2Cloth.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/texExecutioner2ClothDiffuse.png");

            matExe2Jumpkit.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeKitTexBLUE.png");
            matExe2Jumpkit.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeKitEmBLUE.png"));
            matExe2Jumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Gun.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExegunTexBLUE.png");
            matExe2Gun.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExegunEmTexBLUE.png"));
            matExe2Gun.SetColor("_EmColor", EmColor);

            //matExecutionerAxe.mainTexture = ExeAxe4;
            matExecutionerAxe.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/texExeAxeRampBLUE.png"));
            matExecutionerAxe.SetColor("_TintColor", new Color(0.1f, 1f, 0.1f)); //0.0688 0.5619 0.9717 1

            newRenderInfos[0].defaultMaterial = matExecutioner2Armor;
            newRenderInfos[1].defaultMaterial = matExecutioner2Cloth;
            newRenderInfos[2].defaultMaterial = matExe2Jumpkit;
            newRenderInfos[3].defaultMaterial = matExe2Gun;
            newRenderInfos[4].defaultMaterial = matExecutionerAxe;


            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(0.2f, 2f, 0.2f, 0.75f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefEnhanced.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };

            SkinDefBLUE = newSkinDef;
            return newSkinDef;
        }

        internal static SkinDef ModdedSkinYELLOW(SkinDef skinExecutioner)
        {
            SkinDefEnhanced newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinExecutionerMastery_1",
                nameToken = "SIMU_SKIN_EXECUTIONER_GOLD",
                icon = H.GetIcon("mod/ss2/executioner_gold"),
                original = skinExecutioner,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matExeMasteryBody = CloneMat(ref newRenderInfos, 0);
            Material matExeMasteryArmor = CloneMat(ref newRenderInfos, 1);
            Material matExeMasteryJumpkit = CloneMat(ref newRenderInfos, 2);
            Material matExeMasteryGun = CloneMat(ref newRenderInfos, 3);
            Material matExecutionerSword = CloneMat(ref newRenderInfos, 4);

            Texture2D ExeSkinEM = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeSkinEMGOLD.png");

            Color EmColor = new Color(1.3f, 1.3f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExeMasteryBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeSkinTexGOLD.png");
            matExeMasteryBody.SetTexture("_EmTex", ExeSkinEM);
            matExeMasteryBody.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExeMasteryArmor.mainTexture = matExeMasteryBody.mainTexture;
            matExeMasteryArmor.SetTexture("_EmTex", ExeSkinEM);
            matExeMasteryArmor.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/SilverRampGOLD.png"));
            matExeMasteryArmor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExeMasteryJumpkit.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeMasteryKitTexGOLD.png");
            matExeMasteryJumpkit.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeKitEmGOLD.png"));
            matExeMasteryJumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExeMasteryGun.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeMasterygunTexGOLD.png");
            matExeMasteryGun.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExegunEmTexGOLD.png"));
            matExeMasteryGun.SetColor("_EmColor", EmColor);

            //matExecutionerSword.mainTexture = ExeAxe4;
            matExecutionerSword.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/texExeAxeRampBlackGOLD.png"));
            matExecutionerSword.SetColor("_TintColor", new Color(1, 1, 0, 1)); //0.0688 0.5619 0.9717 1

            newRenderInfos[0].defaultMaterial = matExeMasteryBody;
            newRenderInfos[1].defaultMaterial = matExeMasteryArmor;
            newRenderInfos[2].defaultMaterial = matExeMasteryJumpkit;
            newRenderInfos[3].defaultMaterial = matExeMasteryGun;
            newRenderInfos[4].defaultMaterial = matExecutionerSword;

            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(2f, 2f, 0.8f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefEnhanced.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };
            SkinDefYELLOW = newSkinDef;
            return newSkinDef;
        }

        internal static void AddVFXLate()
        {
            Debug.Log("Executioner Skin VFX");

            GameObject ExecutionerDashEffect = ExecutionerBodyS.GetComponent<SkillLocator>().utility.skillFamily.defaultSkillDef.activationState.stateType.GetFieldValue<GameObject>("dashEffect");
            Debug.Log("ExecutionerDashEffect : " + ExecutionerDashEffect);

            if (!ExecutionerDashEffect)
            {
                Debug.LogWarning("Executioner Dust Effect could not be found");
                return;
            }
            //SkinVFX.AddSkinVFX(SkinDefRED, replacementsRED[i].GetComponent<EffectComponent>().effectIndex, replacementsRED[i + 1]);


            GameObject ExecutionerDashEffect_Red = R2API.PrefabAPI.InstantiateClone(ExecutionerDashEffect, "ExecutionerDashEffect_Red", false);
            GameObject ExecutionerDashEffect_Gold = R2API.PrefabAPI.InstantiateClone(ExecutionerDashEffect, "ExecutionerDashEffect_Gold", false);
       
            //matExeIonTrail
            //matExeCloud

            Material matExeIonTrail_Red = Object.Instantiate(ExecutionerDashEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            Material matExeCloud_Red = Object.Instantiate(ExecutionerDashEffect.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);

            #region Blue/Green
            GameObject ExecutionerDashEffect_Green = R2API.PrefabAPI.InstantiateClone(ExecutionerDashEffect, "ExecutionerDashEffect_Green", false);
            Material matExeIonTrail_Green = Object.Instantiate(ExecutionerDashEffect.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            Material matExeCloud_Green = Object.Instantiate(ExecutionerDashEffect.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);

            matExeIonTrail_Green.SetTexture("_RemapTex", null); //texRampHuntressSoft2
            matExeCloud_Green.SetColor("_TintColor", new Color(1f,1f,1f,1f)); //0.0318 0.2033 0.5189 1

            ExecutionerDashEffect_Green.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = matExeIonTrail_Green;
            ExecutionerDashEffect_Green.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = matExeCloud_Green;
            ExecutionerDashEffect_Green.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = matExeCloud_Green;
            #endregion


        }


        /*
        [RegisterAchievement("CLEAR_ANY_SURVIVOREXECUTIONER2", "Skins.survivorExecutioner2.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumExecutioner : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Executioner2Body");
            }
        }*/
    }



}