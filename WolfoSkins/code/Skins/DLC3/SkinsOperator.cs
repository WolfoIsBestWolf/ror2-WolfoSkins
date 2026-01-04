using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC1
{
    public class SkinsOperator
    {
        internal static void Start()
        {
            SkinDef skinDroneTechDef = Addressables.LoadAssetAsync<SkinDef>(key: "c1683d6a4220f7d419b0d46956297dde").WaitForCompletion();
            SkinDef skinDroneTechDefAlt = Addressables.LoadAssetAsync<SkinDef>(key: "74b51f66d2aca044fa361f6836b154af").WaitForCompletion();

            SkinDef skinDTGunnerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "510231c58a9944e45be8b90f5b41a4d0").WaitForCompletion();
            SkinDef skinDTHealDefault = Addressables.LoadAssetAsync<SkinDef>(key: "37af658961eb1d64b91d03d8098da0f2").WaitForCompletion();
            SkinDef skinDTHaulerDrone = Addressables.LoadAssetAsync<SkinDef>(key: "a725c698056caf04790c1f808f62d948").WaitForCompletion();

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinOperatorDefault_Yellow_1",
                nameToken = "SIMU_SKIN_OPERATOR_YELLOW",
                icon = H.GetIcon("dlc3/operator_yellow"),
                original = skinDroneTechDef,
                minionSkins = new SkinDef[]
                {
                    CreateNewSkin(new SkinInfo
                    {
                        name = "skinDTDroneHauler_Yellow",
                        original = skinDTHaulerDrone,
                    })
                },
            }, new System.Action<SkinDefMakeOnApply>(DefaultRedYellow));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinOperatorDefault_Blue_1",
                nameToken = "SIMU_SKIN_OPERATOR_BLUE",
                icon = H.GetIcon("dlc3/operator_blue"),
                original = skinDroneTechDef,
                minionSkins = new SkinDef[]
                {
                    CreateNewSkin(new SkinInfo
                    {
                        name = "skinDTDroneGun_Blue",
                        original = skinDTGunnerDefault,
                    }),
                    CreateNewSkin(new SkinInfo
                    {
                        name = "skinDTDroneHeal_Blue",
                        original = skinDTHealDefault,
                    }),
                    CreateNewSkin(new SkinInfo
                    {
                        name = "skinDTDroneHauler_Blue",
                        original = skinDTHaulerDrone,
                    })
                },
            }, new System.Action<SkinDefMakeOnApply>(DefaultGoldBlue));

            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinOperatorAlt1_Orange_1",
                nameToken = "SIMU_SKIN_OPERATOR_ORANGE",
                icon = H.GetIcon("dlc3/operator_orange"),
                original = skinDroneTechDefAlt,
                minionSkins = new SkinDef[]
                {
                    CreateNewSkin(new SkinInfo
                    {
                        name = "skinDTDroneHauler_Orange",
                        original = skinDTHaulerDrone,
                    })
                },
            }, new System.Action<SkinDefMakeOnApply>(MasteryOrange));


            //0 Dronetech_body_mtl | DroneTech_Body  
            //1 Dronetech_body_mtl | DroneTech_Gun  
            //2 Dronetech_glow1 | Operator_Glow_01  
            //3 Dronetech_glow2 | Operator_Glow_02  //Backpack 
            //4 Dronetech_glow3 | Operator_Glow_03  //Backpack
            //5 Dronetech_glow4 | Operator_Glow_04      
        }

        internal static void DefaultRedYellow(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material Dronetech_body_mtl = CloneMat(ref newRenderInfos, 0);
            Material Dronetech_glow1 = CloneMat(ref newRenderInfos, 2);
            Material Dronetech_glow2 = CloneMat(ref newRenderInfos, 3);
            Material Dronetech_glow3 = CloneMat(ref newRenderInfos, 4);
            //Material Dronetech_glow4 = CloneMat(ref newRenderInfos, 5);

            Dronetech_body_mtl.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Yellow/T_DroneTech_C.png");
            Dronetech_glow1.color = new Color(1f, 0.8f, 0f, 1f);//0 0.4424 0.566 1
            Dronetech_glow1.SetColor("_EmColor", new Color(1.1f, 0.88f, 0f)); //0.33363685, g: 0.6698113, b: 0.30647027
            Dronetech_glow2.color = new Color(0.555f, 0.555f, 0f, 1f);//0 0.4424 0.566 1
            Dronetech_glow2.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Yellow/texElusiveAntlersRamp.png")); //texElusiveAntlersRamp
            Dronetech_glow3.color = new Color(0.555f, 0.555f, 0f, 1f);//0 0.4424 0.566 1
            Dronetech_glow3.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Yellow/texElusiveAntlersRamp.png")); //texElusiveAntlersRamp

            newRenderInfos[0].defaultMaterial = Dronetech_body_mtl;
            newRenderInfos[1].defaultMaterial = Dronetech_body_mtl;
            newRenderInfos[2].defaultMaterial = Dronetech_glow1;
            newRenderInfos[3].defaultMaterial = Dronetech_glow2;
            newRenderInfos[4].defaultMaterial = Dronetech_glow3;

            Material matDroneNew = CloneMat(ref newSkinDef.minionSkins[0].skinDefParams.rendererInfos, 0, true);
            matDroneNew.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Yellow/texDTHauler.png");
            matDroneNew.SetColor("_EmColor", new Color(1.1f, 0.88f, 0f)); //1 0.3377 0.3377 1

            newSkinDef.minionSkins[0]._runtimeSkin = null;
            newSkinDef.minionSkins[0].Bake();
            newSkinDef.skinDefParams.minionSkinReplacements = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.minionSkinReplacements);
            newSkinDef.skinDefParams.minionSkinReplacements[2].minionSkin = newSkinDef.minionSkins[0];
            newSkinDef.minionSkins = null;
        }

        internal static void DefaultGoldBlue(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material Dronetech_body_mtl = CloneMat(ref newRenderInfos, 0);
            Material Dronetech_glow1 = CloneMat(ref newRenderInfos, 2);
            Material Dronetech_glow2 = CloneMat(ref newRenderInfos, 3);
            Material Dronetech_glow3 = CloneMat(ref newRenderInfos, 4);
            //Material Dronetech_glow4 = CloneMat(ref newRenderInfos, 5);

            Dronetech_body_mtl.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/T_DroneTech_C.png");
            Dronetech_body_mtl.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/T_DroneTech_C_EM.png"));
            Dronetech_body_mtl.SetColor("_EmColor", new Color(0, 1, 1)); //0.33363685, g: 0.6698113, b: 0.30647027
            Dronetech_body_mtl.SetFloat("_EmPower", 0.6f);
            Dronetech_body_mtl.SetTexture("_FresnelRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/texGeepRamp.png"));
            Dronetech_body_mtl.SetTexture("_FresnelMask", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/texFresnelMask.png"));

            Dronetech_glow1.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/T_DroneTech_Glow01_C.png");
            Dronetech_glow1.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/T_DroneTech_Glow01_C.png"));
            Dronetech_glow1.color = new Color(0, 1, 1);//0 0.4424 0.566 1
            Dronetech_glow1.SetColor("_EmColor", new Color(0, 1, 1)); //0.33363685, g: 0.6698113, b: 0.30647027

            //Dronetech_glow2.color = new Color(0.277f, 0.277f, 0.444f, 1f);//0 0.4424 0.566 1
            //Dronetech_glow2.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/texElusiveAntlersRamp.png")); //texElusiveAntlersRamp
            //Dronetech_glow3.color = new Color(0.277f, 0.277f, 0.444f, 1f);//0 0.4424 0.566 1
            //Dronetech_glow3.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/texElusiveAntlersRamp.png")); //texElusiveAntlersRamp

            newRenderInfos[0].defaultMaterial = Dronetech_body_mtl;
            newRenderInfos[1].defaultMaterial = Dronetech_body_mtl;
            newRenderInfos[2].defaultMaterial = Dronetech_glow1;
            //newRenderInfos[3].defaultMaterial = Dronetech_glow2;
            //newRenderInfos[4].defaultMaterial = Dronetech_glow3;

            Material matDroneGunner = CloneMat(ref newSkinDef.minionSkins[0].skinDefParams.rendererInfos, 0, true);
            matDroneGunner.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/texDiffdrone1_B.png");
            matDroneGunner.SetColor("_EmColor", Color.cyan); //1 0.3377 0.3377 1

            Material matDroneHealing = CloneMat(ref newSkinDef.minionSkins[1].skinDefParams.rendererInfos, 0, true);
            matDroneHealing.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/texDrone2_B.png");
            matDroneHealing.SetColor("_EmColor", Color.cyan); //1 0.3377 0.3377 1

            Material matDroneHauler = CloneMat(ref newSkinDef.minionSkins[2].skinDefParams.rendererInfos, 0, true);
            matDroneHauler.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Blue/texDTHauler.png");
            matDroneHauler.SetColor("_EmColor", Color.cyan); //1 0.3377 0.3377 1

            newSkinDef.minionSkins[0]._runtimeSkin = null;
            newSkinDef.minionSkins[0].Bake();
            newSkinDef.minionSkins[1]._runtimeSkin = null;
            newSkinDef.minionSkins[1].Bake();
            newSkinDef.minionSkins[2]._runtimeSkin = null;
            newSkinDef.minionSkins[2].Bake();
            newSkinDef.skinDefParams.minionSkinReplacements = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.minionSkinReplacements);
            newSkinDef.skinDefParams.minionSkinReplacements[0].minionSkin = newSkinDef.minionSkins[0];
            newSkinDef.skinDefParams.minionSkinReplacements[1].minionSkin = newSkinDef.minionSkins[1];
            newSkinDef.skinDefParams.minionSkinReplacements[2].minionSkin = newSkinDef.minionSkins[2];
            newSkinDef.minionSkins = null;
        }

        internal static void MasteryOrange(SkinDefMakeOnApply newSkinDef)
        {

            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matDroneTechDiffuse = CloneMat(ref newRenderInfos, 0);
            Material matDroneTechGlowAlt1 = CloneMat(ref newRenderInfos, 2);
            Material matDroneTechGlowAlt2 = CloneMat(ref newRenderInfos, 3);
            Material matDroneTechGlowAlt3 = CloneMat(ref newRenderInfos, 4);
            //Material Dronetech_glow4 = CloneMat(ref newRenderInfos, 5);

            matDroneTechDiffuse.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Orange/texDroneTechAltSkin.png");
            // matDroneTechGlowAlt1.color = new Color(0.9647f, 0.5667f, 0.3922f, 1f);//0.9647 0.0667 0.3922 1
            matDroneTechGlowAlt1.color = new Color(1f, 0.566f, 0.22f, 1f);//0.9647 0.0667 0.3922 1
            //matDroneTechGlowAlt1.SetColor("_EmColor", new Color(1f, 0.5555f, 0.3377f)); //1 0.3377 0.3377 1
            matDroneTechGlowAlt1.SetColor("_EmColor", new Color(1.22f, 0.666f, 0)); //1 0.3377 0.3377 1
            matDroneTechGlowAlt2.color = new Color(0.3585f, 0.2117f, 0.1505f, 1);//0.3585 0.1505 0.2117 1
            matDroneTechGlowAlt2.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Orange/texRampNullifier.png")); //texElusiveAntlersRamp
            matDroneTechGlowAlt3.color = new Color(0.4245f, 0.2709f, 0.1542f, 1f);//0.4245 0.1542 0.2209 1
            matDroneTechGlowAlt3.SetTexture("_FlowHeightRamp", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Orange/texRampNullifier.png")); //texElusiveAntlersRamp

            newRenderInfos[0].defaultMaterial = matDroneTechDiffuse;
            newRenderInfos[1].defaultMaterial = matDroneTechDiffuse;
            newRenderInfos[2].defaultMaterial = matDroneTechGlowAlt1;
            newRenderInfos[3].defaultMaterial = matDroneTechGlowAlt2;
            newRenderInfos[4].defaultMaterial = matDroneTechGlowAlt3;

            Material matDroneNew = CloneMat(ref newSkinDef.minionSkins[0].skinDefParams.rendererInfos, 0, true);
            matDroneNew.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc3/Operator/Orange/texDTHauler.png");
            matDroneNew.SetColor("_EmColor", new Color(1.22f, 0.666f, 0)); //1 0.3377 0.3377 1

            newSkinDef.minionSkins[0]._runtimeSkin = null;
            newSkinDef.minionSkins[0].Bake();
            newSkinDef.skinDefParams.minionSkinReplacements = HG.ArrayUtils.Clone(newSkinDef.skinDefParams.minionSkinReplacements);
            newSkinDef.skinDefParams.minionSkinReplacements[2].minionSkin = newSkinDef.minionSkins[0];
            newSkinDef.minionSkins = null;
        }


        [RegisterAchievement("CLEAR_ANY_DRONETECH", "Skins.DroneTech.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumOperatorBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DroneTechBody");
            }
        }

        /*[RegisterAchievement("CLEAR_BOTH_DRONETECH", "Skins.DroneTech.Wolfo.Both", null, 3, null)]
        public class ClearSimulacrumOperatorBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DroneTechBody");
            }
        }*/

    }
}