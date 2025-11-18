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
            Debug.Log("Executioner Skins");
            //GameModeCatalog.availability.CallWhenAvailable(AddVFXLate);
            //For some reason needs to be called hella later?
            RoR2Application.onLoad += AddVFXLate;
     
            ExecutionerBodyS = ExecutionerBody;

            BodyIndex ExecutionerIndex = ExecutionerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ExecutionerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinExecutioner = modelSkinController.skins[0];
            SkinDef skinExecutionerMastery = modelSkinController.skins[1];


            SkinDef red = ModdedSkinRED(skinExecutioner);
            SkinDef yellow = ModdedSkinYELLOW(skinExecutionerMastery);
            SkinDef nerd = ModdedSkinBLUE(skinExecutioner);
 
            //SkinCatalog.skinsByBody[(int)ExecutionerIndex] = modelSkinController.skins;
            //0 matExe2
            //1 matExe2Armor
            //2 matExe2Jumpkit
            //3 matExe2Gun
            //4 matExecutionerAxe
        }

        internal static SkinDef ModdedSkinRED(SkinDef skinExecutioner)
        {
            SkinDefAltColor newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinExecutioner_1",
                nameToken = "SIMU_SKIN_EXECUTIONER",
                icon = H.GetIcon("mod/ss2/executioner_red"),
                original = skinExecutioner,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matExe2 = CloneMat(newRenderInfos, 0);
            Material matExe2Armor = CloneMat(newRenderInfos, 1);
            Material matExe2Jumpkit = CloneMat(newRenderInfos, 2);
            Material matExe2Gun = CloneMat(newRenderInfos, 3);
            Material matExecutionerAxe = CloneMat(newRenderInfos, 4);

            Texture2D ExeTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeTex.png");
            Texture2D exeem = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/exeem.png");


            Color EmColor = new Color(1.5f, 0f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExe2.mainTexture = ExeTex;
            matExe2.SetTexture("_EmTex", exeem);
            matExe2.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Armor.mainTexture = ExeTex;
            matExe2Armor.SetTexture("_EmTex", exeem);
            matExe2Armor.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/exeRamp.png"));
            matExe2Armor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Jumpkit.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeKitTex.png");
            matExe2Jumpkit.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeKitEm.png"));
            matExe2Jumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Gun.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExegunTex.png");
            matExe2Gun.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExegunEmTex.png"));
            matExe2Gun.SetColor("_EmColor", EmColor);

            //matExecutionerAxe.mainTexture = ExeAxe4;
            matExecutionerAxe.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/texExeAxeRamp.png"));
            matExecutionerAxe.SetColor("_TintColor", EmColor); //0.0688 0.5619 0.9717 1

            newRenderInfos[0].defaultMaterial = matExe2;
            newRenderInfos[1].defaultMaterial = matExe2Armor;
            newRenderInfos[2].defaultMaterial = matExe2Jumpkit;
            newRenderInfos[3].defaultMaterial = matExe2Gun;
            newRenderInfos[4].defaultMaterial = matExecutionerAxe;


            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(2f, 0.2f, 0.2f, 0.75f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefAltColor.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };
            SkinDefRED = newSkinDef;
            return newSkinDef;
        }

        internal static SkinDef ModdedSkinBLUE(SkinDef skinExecutioner)
        {
            SkinDefAltColor newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinExecutioner_Blue_1",
                nameToken = "SIMU_SKIN_EXECUTIONER_BLUE",
                icon = H.GetIcon("mod/ss2/executioner_blue"),
                original = skinExecutioner,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matExe2 = CloneMat(newRenderInfos, 0);
            Material matExe2Armor = CloneMat(newRenderInfos, 1);
            Material matExe2Jumpkit = CloneMat(newRenderInfos, 2);
            Material matExe2Gun = CloneMat(newRenderInfos, 3);
            Material matExecutionerAxe = CloneMat(newRenderInfos, 4);

            Texture2D ExeTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeTexBLUE.png");
            Texture2D exeem = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/exeemBLUE.png");

            Color EmColor = new Color(0f, 1.1f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExe2.mainTexture = ExeTex;
            matExe2.SetTexture("_EmTex", exeem);
            matExe2.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Armor.mainTexture = ExeTex;
            matExe2Armor.SetTexture("_EmTex", exeem);
            matExe2Armor.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/exeRampBLUE.png"));
            matExe2Armor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Jumpkit.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeKitTexBLUE.png");
            matExe2Jumpkit.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeKitEmBLUE.png"));
            matExe2Jumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Gun.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExegunTexBLUE.png");
            matExe2Gun.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExegunEmTexBLUE.png"));
            matExe2Gun.SetColor("_EmColor", EmColor);

            //matExecutionerAxe.mainTexture = ExeAxe4;
            matExecutionerAxe.SetTexture("_RemapTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/texExeAxeRampBLUE.png"));
            matExecutionerAxe.SetColor("_TintColor", new Color(0.1f, 1f, 0.1f)); //0.0688 0.5619 0.9717 1

            newRenderInfos[0].defaultMaterial = matExe2;
            newRenderInfos[1].defaultMaterial = matExe2Armor;
            newRenderInfos[2].defaultMaterial = matExe2Jumpkit;
            newRenderInfos[3].defaultMaterial = matExe2Gun;
            newRenderInfos[4].defaultMaterial = matExecutionerAxe;


            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(0.2f, 2f, 0.2f, 0.75f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefAltColor.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };

            SkinDefBLUE = newSkinDef;
            return newSkinDef;
        }

        internal static SkinDef ModdedSkinYELLOW(SkinDef skinExecutioner)
        {
            SkinDefAltColor newSkinDef = H.CreateNewSkinW(new SkinInfo
            {
                name = "skinExecutionerMastery_1",
                nameToken = "SIMU_SKIN_EXECUTIONER_GOLD",
                icon = H.GetIcon("mod/ss2/executioner_gold"),
                original = skinExecutioner,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matExeMasteryBody = CloneMat(newRenderInfos, 0);
            Material matExeMasteryArmor = CloneMat(newRenderInfos, 1);
            Material matExeMasteryJumpkit = CloneMat(newRenderInfos, 2);
            Material matExeMasteryGun = CloneMat(newRenderInfos, 3);
            Material matExecutionerSword = CloneMat(newRenderInfos, 4);

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
            newSkinDef.changeMaterial = new SkinDefAltColor.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };
            SkinDefYELLOW = newSkinDef;
            return newSkinDef;
        }


  
        internal static void AddVFXLateOLD()
        {
            /*
            GameObject TracerIonBullet = null;
            GameObject TracerIonBulletRED = null;
            GameObject TracerIonBulletYELLOW = null;

            GameObject exeChargeGun = null;
            GameObject exeChargeGunRED = null;
            GameObject exeChargeGunYELLOW = null;

            GameObject exePlume = null;
            GameObject exePlumeRED = null;
            GameObject exePlumeYELLOW = null;

            GameObject exePlumeBig = null;
            GameObject exePlumeBigRED = null;
            GameObject exePlumeBigYELLOW = null;*/

            GameObject exeExhaust = null;
            GameObject exeExhaustRED = null;
            GameObject exeExhaustYELLOW = null;
            GameObject exeExhaustBLUE = null;

            GameObject ExecutionerAxeSlamEffect = null;
            GameObject ExecutionerAxeSlamEffectRED = null;
            GameObject ExecutionerAxeSlamEffectYELLOW = null;
            GameObject ExecutionerAxeSlamEffectBLUE = null;

            /*GameObject LightningFlash = null;
            GameObject LightningFlashRED = null;
            GameObject LightningFlashYELLOW = null;

            GameObject MuzzleflashFMJ = null;
            GameObject MuzzleflashFMJRED = null;
            GameObject MuzzleflashFMJYELLOW = null;*/

            for (int i = 0; i < EffectCatalog.entries.Length; i++)
            {
                //Debug.LogWarning(EffectCatalog.entries[i].prefab);
                if (EffectCatalog.entries[i].prefab)
                {
                    if (EffectCatalog.entries[i].prefab.name.EndsWith("exeExhaust"))
                    {
                        exeExhaust = EffectCatalog.entries[i].prefab;
                        exeExhaustRED = R2API.PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustRED", false);
                        exeExhaustYELLOW = R2API.PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustYELLOW", false);
                        exeExhaustBLUE = R2API.PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustBLUE", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("ExecutionerAxeSlamEffect"))
                    {
                        ExecutionerAxeSlamEffect = EffectCatalog.entries[i].prefab;
                        ExecutionerAxeSlamEffectRED = R2API.PrefabAPI.InstantiateClone(ExecutionerAxeSlamEffect, "ExecutionerAxeSlamEffectRED", false);
                        ExecutionerAxeSlamEffectYELLOW = R2API.PrefabAPI.InstantiateClone(ExecutionerAxeSlamEffect, "ExecutionerAxeSlamEffectYELLOW", false);
                        ExecutionerAxeSlamEffectBLUE = R2API.PrefabAPI.InstantiateClone(ExecutionerAxeSlamEffect, "ExecutionerAxeSlamEffectBLUE", false);
                    }
                    /*
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("TracerIonBullet"))
                    {
                        TracerIonBullet = EffectCatalog.entries[i].prefab;
                        TracerIonBulletRED = R2API.PrefabAPI.InstantiateClone(TracerIonBullet, "TracerIonBulletRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("exeChargeGun"))
                    {
                        exeChargeGun = EffectCatalog.entries[i].prefab;
                        exeChargeGunRED = R2API.PrefabAPI.InstantiateClone(exeChargeGun, "exeChargeGunRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("exePlumeBig"))
                    {
                        exePlumeBig = EffectCatalog.entries[i].prefab;
                        exePlumeBigRED = R2API.PrefabAPI.InstantiateClone(exePlumeBig, "exePlumeBigRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("exePlume"))
                    {
                        exePlume = EffectCatalog.entries[i].prefab;
                        exePlumeRED = R2API.PrefabAPI.InstantiateClone(exePlume, "exePlumeRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("LightningFlash"))
                    {
                        LightningFlash = EffectCatalog.entries[i].prefab;
                        LightningFlashRED = R2API.PrefabAPI.InstantiateClone(LightningFlash, "LightningFlashRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("MuzzleflashFMJ"))
                    {
                        MuzzleflashFMJ = EffectCatalog.entries[i].prefab;
                        MuzzleflashFMJRED = R2API.PrefabAPI.InstantiateClone(MuzzleflashFMJ, "MuzzleflashFMJRED", false);
                    }*/
                }

            }

            GameObject[] replacementsRED = new GameObject[]
            {
                //TracerIonBullet, TracerIonBulletRED,
                //exeChargeGun, exeChargeGunRED,
                //exePlume, exePlumeRED,
                //exePlumeBig, exePlumeBigRED,
                exeExhaust, exeExhaustRED,
                ExecutionerAxeSlamEffect, ExecutionerAxeSlamEffectRED,
                //LightningFlash, LightningFlashRED,
                //MuzzleflashFMJ, MuzzleflashFMJRED,
           };
            GameObject[] replacementsYELLOW = new GameObject[]
            {
                exeExhaust, exeExhaustYELLOW,
                ExecutionerAxeSlamEffect, ExecutionerAxeSlamEffectYELLOW,
            };

            GameObject[] replacementsBLUE = new GameObject[]
{
                exeExhaust, exeExhaustBLUE,
                ExecutionerAxeSlamEffect, ExecutionerAxeSlamEffectBLUE,
};

            EffectDef[] newEffects = new EffectDef[] {
                new EffectDef(exeExhaustRED),
                new EffectDef(ExecutionerAxeSlamEffectRED),
                new EffectDef(exeExhaustYELLOW),
                new EffectDef(ExecutionerAxeSlamEffectYELLOW),
             new EffectDef(exeExhaustBLUE),
                new EffectDef(ExecutionerAxeSlamEffectBLUE),
             };

            int EffectsLengthOld = EffectCatalog.entries.Length;
            System.Array.Resize(ref EffectCatalog.entries, EffectCatalog.entries.Length + newEffects.Length);

            for (int j = 0; j < newEffects.Length; j++)
            {
                ref EffectDef ptr = ref newEffects[j];
                ptr.index = (EffectIndex)j + EffectsLengthOld;
                ptr.prefabEffectComponent.effectIndex = ptr.index;
                EffectCatalog.entries[j + EffectsLengthOld] = ptr;
            }

            for (int i = 0; i < replacementsRED.Length; i++)
            {
                SkinVFX.AddSkinVFX(SkinDefRED, replacementsRED[i].GetComponent<EffectComponent>().effectIndex, replacementsRED[i + 1]);
                i++;
            }
            for (int i = 0; i < replacementsYELLOW.Length; i++)
            {
                SkinVFX.AddSkinVFX(SkinDefYELLOW, replacementsYELLOW[i].GetComponent<EffectComponent>().effectIndex, replacementsYELLOW[i + 1]);
                i++;
            }
            for (int i = 0; i < replacementsBLUE.Length; i++)
            {
                SkinVFX.AddSkinVFX(SkinDefBLUE, replacementsBLUE[i].GetComponent<EffectComponent>().effectIndex, replacementsBLUE[i + 1]);
                i++;
            }

            Material newMaterial = Object.Instantiate(exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            newMaterial.SetColor("_TintColor", new Color(0.95f, 0.25f, 0.25f, 0.334f)); //0.2625 0.4953 0.9434 0.3333
            exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = newMaterial;

            newMaterial = Object.Instantiate(exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetColor("_TintColor", new Color(0.7f, 0.1f, 0.1f, 1f)); //0.1169 0.4522 0.6698 1
            exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustRED.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustRED.transform.GetChild(2).GetComponent<Light>().color = new Color(1f, 0.2f, 0.2f); //0.2521 0.687 0.9717 1


            newMaterial = Object.Instantiate(exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            newMaterial.SetColor("_TintColor", new Color(0.95f, 0.95f, 0.25f, 0.334f)); //0.2625 0.4953 0.9434 0.3333
            exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = newMaterial;

            newMaterial = Object.Instantiate(exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetColor("_TintColor", new Color(0.7f, 0.7f, 0.1f, 1f)); //0.1169 0.4522 0.6698 1
            exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustYELLOW.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustYELLOW.transform.GetChild(2).GetComponent<Light>().color = new Color(1f, 1f, 0.2f); //0.2521 0.687 0.9717 1

            newMaterial = Object.Instantiate(exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            newMaterial.SetColor("_TintColor", new Color(0.25f, 0.95f, 0.25f, 0.334f)); //0.2625 0.4953 0.9434 0.3333
            exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = newMaterial;

            newMaterial = Object.Instantiate(exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetColor("_TintColor", new Color(0.1f, 0.7f, 0.1f, 1f)); //0.1169 0.4522 0.6698 1
            exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustBLUE.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustBLUE.transform.GetChild(2).GetComponent<Light>().color = new Color(0.2f, 1f, 0.2f); //0.2521 0.687 0.9717 1


            //AXE
            Texture2D texRampLightningRED = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/texRampLightningRED.png");
            texRampLightningRED.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampLightningYELLOW = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/texRampLightningGOLD.png");
            texRampLightningYELLOW.wrapMode = TextureWrapMode.Clamp;


            newMaterial = Object.Instantiate(ExecutionerAxeSlamEffectRED.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetTexture("_RemapTex", texRampLightningRED);
            ExecutionerAxeSlamEffectRED.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material = newMaterial;

            ExecutionerAxeSlamEffectRED.transform.GetChild(5).GetComponent<ParticleSystem>().startColor = new Color(1.5f, 0.3f, 0.3f); //0.5896 0.9395 1 1
            ExecutionerAxeSlamEffectRED.transform.GetChild(7).GetComponent<Light>().color = new Color(2f, 0.3f, 0.3f); //0.3821 0.8956 1 1
            //
            //
            newMaterial = Object.Instantiate(ExecutionerAxeSlamEffectYELLOW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetTexture("_RemapTex", texRampLightningYELLOW);
            ExecutionerAxeSlamEffectYELLOW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material = newMaterial;

            ExecutionerAxeSlamEffectYELLOW.transform.GetChild(5).GetComponent<ParticleSystem>().startColor = new Color(1.5f, 1.5f, 0.3f); //0.5896 0.9395 1 1
            ExecutionerAxeSlamEffectYELLOW.transform.GetChild(7).GetComponent<Light>().color = new Color(2f, 2f, 0.3f); //0.3821 0.8956 1 1


            /*
            LineRenderer lineRenderer = TracerIonBulletRED.GetComponent<LineRenderer>();
            lineRenderer.endColor = new Color(0.45f, 0.14f, 0.14f, 0.314f); //0 0.2824 0.451 0.3137
            lineRenderer.material = Object.Instantiate(lineRenderer.material);
            lineRenderer.material.SetColor("_TintColor", new Color(1f, 0.3f, 0.3f, 1f)); //0 0.7823 1 1*/

        }

        internal static void AddVFXLate()
        {
            /*
            GameObject TracerIonBullet = null;
            GameObject TracerIonBulletRED = null;
            GameObject TracerIonBulletYELLOW = null;

            GameObject exeChargeGun = null;
            GameObject exeChargeGunRED = null;
            GameObject exeChargeGunYELLOW = null;

            GameObject exePlume = null;
            GameObject exePlumeRED = null;
            GameObject exePlumeYELLOW = null;

            GameObject exePlumeBig = null;
            GameObject exePlumeBigRED = null;
            GameObject exePlumeBigYELLOW = null;*/

            //GameObject exeExhaust = null;
            /* GameObject exeExhaustRED = null;
             GameObject exeExhaustYELLOW = null;
             GameObject exeExhaustBLUE = null;*/

            /*GameObject ExecutionerAxeSlamEffect = null;
            GameObject ExecutionerAxeSlamEffectRED = null;
            GameObject ExecutionerAxeSlamEffectYELLOW = null;
            GameObject ExecutionerAxeSlamEffectBLUE = null;*/

            /*GameObject LightningFlash = null;
            GameObject LightningFlashRED = null;
            GameObject LightningFlashYELLOW = null;

            GameObject MuzzleflashFMJ = null;
            GameObject MuzzleflashFMJRED = null;
            GameObject MuzzleflashFMJYELLOW = null;*/



            Debug.Log("Executioner Skin VFX");

            GameObject exeExhaust = ExecutionerBodyS.GetComponent<SkillLocator>().utility.skillFamily.defaultSkillDef.activationState.stateType.GetFieldValue<GameObject>("dashEffect");
            Debug.Log("ExecExhaustEffect : " + exeExhaust);

            if (!exeExhaust)
            {
                Debug.LogWarning("Executioner Dust Effect could not be found");
                return;
            }

            GameObject exeExhaustRED = R2API.PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustRED", false);
            GameObject exeExhaustYELLOW = R2API.PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustYELLOW", false);
            GameObject exeExhaustBLUE = R2API.PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustBLUE", false);

            GameObject[] replacementsRED = new GameObject[]
            {
                //TracerIonBullet, TracerIonBulletRED,
                //exeChargeGun, exeChargeGunRED,
                //exePlume, exePlumeRED,
                //exePlumeBig, exePlumeBigRED,
                exeExhaust, exeExhaustRED,
                //ExecutionerAxeSlamEffect, ExecutionerAxeSlamEffectRED,
                //LightningFlash, LightningFlashRED,
                //MuzzleflashFMJ, MuzzleflashFMJRED,
           };
            GameObject[] replacementsYELLOW = new GameObject[]
            {
                exeExhaust, exeExhaustYELLOW,
                //ExecutionerAxeSlamEffect, ExecutionerAxeSlamEffectYELLOW,
            };

            GameObject[] replacementsBLUE = new GameObject[]
{
                exeExhaust, exeExhaustBLUE,
                //ExecutionerAxeSlamEffect, ExecutionerAxeSlamEffectBLUE,
};

            EffectDef[] newEffects = new EffectDef[] {
                new EffectDef(exeExhaustRED),
                //new EffectDef(ExecutionerAxeSlamEffectRED),
                new EffectDef(exeExhaustYELLOW),
                //new EffectDef(ExecutionerAxeSlamEffectYELLOW),
                new EffectDef(exeExhaustBLUE),
                //new EffectDef(ExecutionerAxeSlamEffectBLUE),
             };

            int EffectsLengthOld = EffectCatalog.entries.Length;
            System.Array.Resize(ref EffectCatalog.entries, EffectCatalog.entries.Length + newEffects.Length);

            for (int j = 0; j < newEffects.Length; j++)
            {
                ref EffectDef ptr = ref newEffects[j];
                ptr.index = (EffectIndex)j + EffectsLengthOld;
                ptr.prefabEffectComponent.effectIndex = ptr.index;
                EffectCatalog.entries[j + EffectsLengthOld] = ptr;
            }

            for (int i = 0; i < replacementsRED.Length; i++)
            {
                SkinVFX.AddSkinVFX(SkinDefRED, replacementsRED[i].GetComponent<EffectComponent>().effectIndex, replacementsRED[i + 1]);
                i++;
            }
            for (int i = 0; i < replacementsYELLOW.Length; i++)
            {
                SkinVFX.AddSkinVFX(SkinDefYELLOW, replacementsYELLOW[i].GetComponent<EffectComponent>().effectIndex, replacementsYELLOW[i + 1]);
                i++;
            }
            for (int i = 0; i < replacementsBLUE.Length; i++)
            {
                SkinVFX.AddSkinVFX(SkinDefBLUE, replacementsBLUE[i].GetComponent<EffectComponent>().effectIndex, replacementsBLUE[i + 1]);
                i++;
            }

            Material newMaterial = Object.Instantiate(exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            newMaterial.SetColor("_TintColor", new Color(0.95f, 0.25f, 0.25f, 0.334f)); //0.2625 0.4953 0.9434 0.3333
            exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = newMaterial;

            newMaterial = Object.Instantiate(exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetColor("_TintColor", new Color(0.7f, 0.1f, 0.1f, 1f)); //0.1169 0.4522 0.6698 1
            exeExhaustRED.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustRED.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustRED.transform.GetChild(2).GetComponent<Light>().color = new Color(1f, 0.2f, 0.2f); //0.2521 0.687 0.9717 1


            newMaterial = Object.Instantiate(exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            newMaterial.SetColor("_TintColor", new Color(0.95f, 0.95f, 0.25f, 0.334f)); //0.2625 0.4953 0.9434 0.3333
            exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = newMaterial;

            newMaterial = Object.Instantiate(exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetColor("_TintColor", new Color(0.7f, 0.7f, 0.1f, 1f)); //0.1169 0.4522 0.6698 1
            exeExhaustYELLOW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustYELLOW.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustYELLOW.transform.GetChild(2).GetComponent<Light>().color = new Color(1f, 1f, 0.2f); //0.2521 0.687 0.9717 1

            newMaterial = Object.Instantiate(exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial);
            newMaterial.SetColor("_TintColor", new Color(0.25f, 0.95f, 0.25f, 0.334f)); //0.2625 0.4953 0.9434 0.3333
            exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().trailMaterial = newMaterial;

            newMaterial = Object.Instantiate(exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetColor("_TintColor", new Color(0.1f, 0.7f, 0.1f, 1f)); //0.1169 0.4522 0.6698 1
            exeExhaustBLUE.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustBLUE.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().material = newMaterial;
            exeExhaustBLUE.transform.GetChild(2).GetComponent<Light>().color = new Color(0.2f, 1f, 0.2f); //0.2521 0.687 0.9717 1

            /*
            //AXE
            Texture2D texRampLightningRED = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampLightningRED.LoadImage(Properties.Resources.texRampLightningRED, true);
            texRampLightningRED.filterMode = FilterMode.Bilinear;
            texRampLightningRED.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampLightningYELLOW = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampLightningYELLOW.LoadImage(Properties.Resources.texRampLightningGOLD, true);
            texRampLightningYELLOW.filterMode = FilterMode.Bilinear;
            texRampLightningYELLOW.wrapMode = TextureWrapMode.Clamp;


            newMaterial = Object.Instantiate(ExecutionerAxeSlamEffectRED.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetTexture("_RemapTex", texRampLightningRED);
            ExecutionerAxeSlamEffectRED.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material = newMaterial;

            ExecutionerAxeSlamEffectRED.transform.GetChild(5).GetComponent<ParticleSystem>().startColor = new Color(1.5f, 0.3f, 0.3f); //0.5896 0.9395 1 1
            ExecutionerAxeSlamEffectRED.transform.GetChild(7).GetComponent<Light>().color = new Color(2f, 0.3f, 0.3f); //0.3821 0.8956 1 1
            //
            //
            newMaterial = Object.Instantiate(ExecutionerAxeSlamEffectYELLOW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material);
            newMaterial.SetTexture("_RemapTex", texRampLightningYELLOW);
            ExecutionerAxeSlamEffectYELLOW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>().material = newMaterial;

            ExecutionerAxeSlamEffectYELLOW.transform.GetChild(5).GetComponent<ParticleSystem>().startColor = new Color(1.5f, 1.5f, 0.3f); //0.5896 0.9395 1 1
            ExecutionerAxeSlamEffectYELLOW.transform.GetChild(7).GetComponent<Light>().color = new Color(2f, 2f, 0.3f); //0.3821 0.8956 1 1
            */

            /*
            LineRenderer lineRenderer = TracerIonBulletRED.GetComponent<LineRenderer>();
            lineRenderer.endColor = new Color(0.45f, 0.14f, 0.14f, 0.314f); //0 0.2824 0.451 0.3137
            lineRenderer.material = Object.Instantiate(lineRenderer.material);
            lineRenderer.material.SetColor("_TintColor", new Color(1f, 0.3f, 0.3f, 1f)); //0 0.7823 1 1*/

        }



        [RegisterAchievement("CLEAR_ANY_SURVIVOREXECUTIONER2", "Skins.survivorExecutioner2.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumExecutioner : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Executioner2Body");
            }
        }
    }
}