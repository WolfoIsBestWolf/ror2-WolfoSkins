using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API.Utils;

namespace WolfoSkinsMod
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
            On.RoR2.UI.MainMenu.BaseMainMenuScreen.Awake += ExecAddVFXLate;
            ExecutionerBodyS = ExecutionerBody;

            ModdedSkinRED(ExecutionerBody);
            ModdedSkinYELLOW(ExecutionerBody);
            ModdedSkinBLUE(ExecutionerBody);
        }

        internal static void ModdedSkinRED(GameObject ExecutionerBody)
        {
            BodyIndex ExecutionerIndex = ExecutionerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ExecutionerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinExecutioner = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinExecutioner.rendererInfos.Length];
            System.Array.Copy(skinExecutioner.rendererInfos, NewRenderInfos, skinExecutioner.rendererInfos.Length);

            //0 matExe2
            //1 matExe2Armor
            //2 matExe2Jumpkit
            //3 matExe2Gun
            //4 matExecutionerAxe

            Material matExe2 = Object.Instantiate(skinExecutioner.rendererInfos[0].defaultMaterial);
            Material matExe2Armor = Object.Instantiate(skinExecutioner.rendererInfos[1].defaultMaterial);
            Material matExe2Jumpkit = Object.Instantiate(skinExecutioner.rendererInfos[2].defaultMaterial);
            Material matExe2Gun = Object.Instantiate(skinExecutioner.rendererInfos[3].defaultMaterial);
            Material matExecutionerAxe = Object.Instantiate(skinExecutioner.rendererInfos[4].defaultMaterial);


            Texture2D ExeTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeTex.png");
            ExeTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D exeem = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/exeem.png");
            exeem.wrapMode = TextureWrapMode.Repeat;

            Texture2D exeRamp = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/exeRamp.png");
            exeRamp.wrapMode = TextureWrapMode.Clamp;

            Texture2D ExeKitTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeKitTex.png");
            ExeKitTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExeKitEm = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExeKitEm.png");
            ExeKitEm.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExegunTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExegunTex.png");
            ExegunTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExegunEmTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/ExegunEmTex.png");
            ExegunEmTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D texExeAxeRamp = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/texExeAxeRamp.png");
            texExeAxeRamp.wrapMode = TextureWrapMode.Clamp;

            Color EmColor = new Color(1.5f, 0f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExe2.mainTexture = ExeTex;
            matExe2.SetTexture("_EmTex", exeem);
            matExe2.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Armor.mainTexture = ExeTex;
            matExe2Armor.SetTexture("_EmTex", exeem);
            matExe2Armor.SetTexture("_FresnelRamp", exeRamp);
            matExe2Armor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Jumpkit.mainTexture = ExeKitTex;
            matExe2Jumpkit.SetTexture("_EmTex", exeem);
            matExe2Jumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Gun.mainTexture = ExegunTex;
            matExe2Gun.SetTexture("_EmTex", ExegunEmTex);
            matExe2Gun.SetColor("_EmColor", EmColor);

            //matExecutionerAxe.mainTexture = ExeAxe4;
            matExecutionerAxe.SetTexture("_RemapTex", texExeAxeRamp);
            matExecutionerAxe.SetColor("_TintColor", EmColor); //0.0688 0.5619 0.9717 1

            NewRenderInfos[0].defaultMaterial = matExe2;
            NewRenderInfos[1].defaultMaterial = matExe2Armor;
            NewRenderInfos[2].defaultMaterial = matExe2Jumpkit;
            NewRenderInfos[3].defaultMaterial = matExe2Gun;
            NewRenderInfos[4].defaultMaterial = matExecutionerAxe;

            //

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinExecutionerWolfo_Any";
            newSkinDef.nameToken = "SIMU_SKIN_EXECUTIONER";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Red/skinExecutionerIcon.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinExecutioner };
            newSkinDef.rootObject = skinExecutioner.rootObject;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.meshReplacements = skinExecutioner.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinExecutioner.projectileGhostReplacements;
            newSkinDef.gameObjectActivations = skinExecutioner.gameObjectActivations;

            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(2f, 0.2f, 0.2f, 0.75f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefWolfo.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };

            SkinDefRED = newSkinDef;
            modelSkinController.skins = modelSkinController.skins.Add(newSkinDef);
            BodyCatalog.skins[(int)ExecutionerIndex] = BodyCatalog.skins[(int)ExecutionerIndex].Add(newSkinDef);
        }

        private static void ExecAddVFXLate(On.RoR2.UI.MainMenu.BaseMainMenuScreen.orig_Awake orig, RoR2.UI.MainMenu.BaseMainMenuScreen self)
        {
            orig(self);
            AddVFXLate();
            On.RoR2.UI.MainMenu.BaseMainMenuScreen.Awake -= ExecAddVFXLate;
        }

        internal static void ModdedSkinBLUE(GameObject ExecutionerBody)
        {
            BodyIndex ExecutionerIndex = ExecutionerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ExecutionerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinExecutioner = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinExecutioner.rendererInfos.Length];
            System.Array.Copy(skinExecutioner.rendererInfos, NewRenderInfos, skinExecutioner.rendererInfos.Length);

            //0 matExe2
            //1 matExe2Armor
            //2 matExe2Jumpkit
            //3 matExe2Gun
            //4 matExecutionerAxe

            Material matExe2 = Object.Instantiate(skinExecutioner.rendererInfos[0].defaultMaterial);
            Material matExe2Armor = Object.Instantiate(skinExecutioner.rendererInfos[1].defaultMaterial);
            Material matExe2Jumpkit = Object.Instantiate(skinExecutioner.rendererInfos[2].defaultMaterial);
            Material matExe2Gun = Object.Instantiate(skinExecutioner.rendererInfos[3].defaultMaterial);
            Material matExecutionerAxe = Object.Instantiate(skinExecutioner.rendererInfos[4].defaultMaterial);


            Texture2D ExeTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeTexBLUE.png");
            ExeTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D exeem = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/exeemBLUE.png");
            exeem.wrapMode = TextureWrapMode.Repeat;

            Texture2D exeRamp = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/exeRampBLUE.png");
            exeRamp.wrapMode = TextureWrapMode.Clamp;

            Texture2D ExeKitTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeKitTexBLUE.png");
            ExeKitTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExeKitEm = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExeKitEmBLUE.png");
            ExeKitEm.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExegunTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExegunTexBLUE.png");
            ExegunTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExegunEmTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/ExegunEmTexBLUE.png");
            ExegunEmTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D texExeAxeRamp = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/texExeAxeRampBLUE.png");
            texExeAxeRamp.wrapMode = TextureWrapMode.Clamp;

            Color EmColor = new Color(0f, 1.1f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExe2.mainTexture = ExeTex;
            matExe2.SetTexture("_EmTex", exeem);
            matExe2.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Armor.mainTexture = ExeTex;
            matExe2Armor.SetTexture("_EmTex", exeem);
            matExe2Armor.SetTexture("_FresnelRamp", exeRamp);
            matExe2Armor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Jumpkit.mainTexture = ExeKitTex;
            matExe2Jumpkit.SetTexture("_EmTex", exeem);
            matExe2Jumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExe2Gun.mainTexture = ExegunTex;
            matExe2Gun.SetTexture("_EmTex", ExegunEmTex);
            matExe2Gun.SetColor("_EmColor", EmColor);

            //matExecutionerAxe.mainTexture = ExeAxe4;
            matExecutionerAxe.SetTexture("_RemapTex", texExeAxeRamp);
            matExecutionerAxe.SetColor("_TintColor", new Color(0.1f,1f,0.1f)); //0.0688 0.5619 0.9717 1

            NewRenderInfos[0].defaultMaterial = matExe2;
            NewRenderInfos[1].defaultMaterial = matExe2Armor;
            NewRenderInfos[2].defaultMaterial = matExe2Jumpkit;
            NewRenderInfos[3].defaultMaterial = matExe2Gun;
            NewRenderInfos[4].defaultMaterial = matExecutionerAxe;

            //

            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinExecutionerWolfo_Blue_Any";
            newSkinDef.nameToken = "SIMU_SKIN_EXECUTIONER_BLUE";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Blue/skinExecutionerIconBLUE.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinExecutioner };
            newSkinDef.rootObject = skinExecutioner.rootObject;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.meshReplacements = skinExecutioner.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinExecutioner.projectileGhostReplacements;
            newSkinDef.gameObjectActivations = skinExecutioner.gameObjectActivations;

            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(0.2f, 2f, 0.2f, 0.75f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefWolfo.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };

            SkinDefBLUE = newSkinDef;
            modelSkinController.skins = modelSkinController.skins.Add(newSkinDef);
            BodyCatalog.skins[(int)ExecutionerIndex] = BodyCatalog.skins[(int)ExecutionerIndex].Add(newSkinDef);
        }

        internal static void ModdedSkinYELLOW(GameObject ExecutionerBody)
        {
            BodyIndex ExecutionerIndex = ExecutionerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ExecutionerBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinExecutioner = modelSkinController.skins[1];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinExecutioner.rendererInfos.Length];
            System.Array.Copy(skinExecutioner.rendererInfos, NewRenderInfos, skinExecutioner.rendererInfos.Length);

            //0 matExe2
            //1 matExe2Armor
            //2 matExe2Jumpkit
            //3 matExe2Gun
            //4 matExecutionerAxe

            //YELLOW

            Material matExeMasteryBody = Object.Instantiate(skinExecutioner.rendererInfos[0].defaultMaterial);
            Material matExeMasteryArmor = Object.Instantiate(skinExecutioner.rendererInfos[1].defaultMaterial);
            Material matExeMasteryJumpkit = Object.Instantiate(skinExecutioner.rendererInfos[2].defaultMaterial);
            Material matExeMasteryGun = Object.Instantiate(skinExecutioner.rendererInfos[3].defaultMaterial);
            Material matExecutionerSword = Object.Instantiate(skinExecutioner.rendererInfos[4].defaultMaterial);


            Texture2D ExeSkinTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeSkinTexGOLD.png");
            ExeSkinTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExeSkinEM = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeSkinEMGOLD.png");
            ExeSkinEM.wrapMode = TextureWrapMode.Repeat;

            Texture2D SilverRamp = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/SilverRampGOLD.png");
            SilverRamp.wrapMode = TextureWrapMode.Clamp;

            Texture2D ExeMasteryKitTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeMasteryKitTexGOLD.png");
            ExeMasteryKitTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExeKitEm = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeKitEmGOLD.png");
            ExeKitEm.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExeMasterygunTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExeMasterygunTexGOLD.png");
            ExeMasterygunTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D ExegunEmTex = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/ExegunEmTexGOLD.png");
            ExegunEmTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D texExeAxeRampBlack = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/texExeAxeRampBlackGOLD.png");
            texExeAxeRampBlack.wrapMode = TextureWrapMode.Clamp;
 

            Color EmColor = new Color(1.3f, 1.3f, 0f, 1f);  //0.7098 0.8118 0.902 1

            matExeMasteryBody.mainTexture = ExeSkinTex;
            matExeMasteryBody.SetTexture("_EmTex", ExeSkinEM);
            matExeMasteryBody.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExeMasteryArmor.mainTexture = ExeSkinTex;
            matExeMasteryArmor.SetTexture("_EmTex", ExeSkinEM);
            matExeMasteryArmor.SetTexture("_FresnelRamp", SilverRamp);
            matExeMasteryArmor.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExeMasteryJumpkit.mainTexture = ExeMasteryKitTex;
            matExeMasteryJumpkit.SetTexture("_EmTex", ExeSkinEM);
            matExeMasteryJumpkit.SetColor("_EmColor", EmColor); //0.7098 0.8118 0.902 1

            matExeMasteryGun.mainTexture = ExeMasterygunTex;
            matExeMasteryGun.SetTexture("_EmTex", ExegunEmTex);
            matExeMasteryGun.SetColor("_EmColor", EmColor);

            //matExecutionerSword.mainTexture = ExeAxe4;
            matExecutionerSword.SetTexture("_RemapTex", texExeAxeRampBlack);
            matExecutionerSword.SetColor("_TintColor", new Color(1,1,0,1)); //0.0688 0.5619 0.9717 1

            NewRenderInfos[0].defaultMaterial = matExeMasteryBody;
            NewRenderInfos[1].defaultMaterial = matExeMasteryArmor;
            NewRenderInfos[2].defaultMaterial = matExeMasteryJumpkit;
            NewRenderInfos[3].defaultMaterial = matExeMasteryGun;
            NewRenderInfos[4].defaultMaterial = matExecutionerSword;

 //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinExecutionerMasteryWolfo_Any";
            newSkinDef.nameToken = "SIMU_SKIN_EXECUTIONER_GOLD";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Executioner/Gold/skinExecutionerIconGOLD.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinExecutioner };
            newSkinDef.rootObject = skinExecutioner.rootObject;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.meshReplacements = skinExecutioner.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinExecutioner.projectileGhostReplacements;
            newSkinDef.gameObjectActivations = skinExecutioner.gameObjectActivations;

            Material oldHuntress = Addressables.LoadAssetAsync<Material>("RoR2/Base/Huntress/matHuntressFlashBright.mat").WaitForCompletion();
            Material newHuntress = Object.Instantiate(oldHuntress);
            newHuntress.SetColor("_TintColor", new Color(2f, 2f, 0.8f)); //0.0191 1.1386 1.2973 1
            newSkinDef.changeMaterial = new SkinDefWolfo.MaterialChanger
            {
                targetMaterial = oldHuntress,
                replacementMaterial = newHuntress
            };

            SkinDefYELLOW = newSkinDef;
            modelSkinController.skins = modelSkinController.skins.Add(newSkinDef);
            BodyCatalog.skins[(int)ExecutionerIndex] = BodyCatalog.skins[(int)ExecutionerIndex].Add(newSkinDef);
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
                        exeExhaustRED = PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustRED", false);
                        exeExhaustYELLOW = PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustYELLOW", false);
                        exeExhaustBLUE = PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustBLUE", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("ExecutionerAxeSlamEffect"))
                    {
                        ExecutionerAxeSlamEffect = EffectCatalog.entries[i].prefab;
                        ExecutionerAxeSlamEffectRED = PrefabAPI.InstantiateClone(ExecutionerAxeSlamEffect, "ExecutionerAxeSlamEffectRED", false);
                        ExecutionerAxeSlamEffectYELLOW = PrefabAPI.InstantiateClone(ExecutionerAxeSlamEffect, "ExecutionerAxeSlamEffectYELLOW", false);
                        ExecutionerAxeSlamEffectBLUE = PrefabAPI.InstantiateClone(ExecutionerAxeSlamEffect, "ExecutionerAxeSlamEffectBLUE", false);
                    }
                    /*
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("TracerIonBullet"))
                    {
                        TracerIonBullet = EffectCatalog.entries[i].prefab;
                        TracerIonBulletRED = PrefabAPI.InstantiateClone(TracerIonBullet, "TracerIonBulletRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("exeChargeGun"))
                    {
                        exeChargeGun = EffectCatalog.entries[i].prefab;
                        exeChargeGunRED = PrefabAPI.InstantiateClone(exeChargeGun, "exeChargeGunRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("exePlumeBig"))
                    {
                        exePlumeBig = EffectCatalog.entries[i].prefab;
                        exePlumeBigRED = PrefabAPI.InstantiateClone(exePlumeBig, "exePlumeBigRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("exePlume"))
                    {
                        exePlume = EffectCatalog.entries[i].prefab;
                        exePlumeRED = PrefabAPI.InstantiateClone(exePlume, "exePlumeRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("LightningFlash"))
                    {
                        LightningFlash = EffectCatalog.entries[i].prefab;
                        LightningFlashRED = PrefabAPI.InstantiateClone(LightningFlash, "LightningFlashRED", false);
                    }
                    else if (EffectCatalog.entries[i].prefab.name.EndsWith("MuzzleflashFMJ"))
                    {
                        MuzzleflashFMJ = EffectCatalog.entries[i].prefab;
                        MuzzleflashFMJRED = PrefabAPI.InstantiateClone(MuzzleflashFMJ, "MuzzleflashFMJRED", false);
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
            Debug.Log("ExecExhaustEffect : "+exeExhaust);
 
            if (!exeExhaust)
            {
                Debug.LogWarning("Executioner Dust Effect could not be found");
                return;
            }

            GameObject exeExhaustRED = PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustRED", false);
            GameObject exeExhaustYELLOW = PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustYELLOW", false);
            GameObject exeExhaustBLUE = PrefabAPI.InstantiateClone(exeExhaust, "exeExhaustBLUE", false);

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
        public class ClearSimulacrumExecutioner : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Executioner2Body");
            }
        }
    }
}