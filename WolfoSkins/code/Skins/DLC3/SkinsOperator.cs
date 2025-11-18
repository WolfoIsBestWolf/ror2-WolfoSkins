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
            
            DefaultColored(skinDroneTechDef);
            MasteryOrange(skinDroneTechDefAlt);

            //Default: Maybe yellow / red? 
            //Mastery Orange


            //0 Dronetech_body_mtl | DroneTech_Body  
            //1 Dronetech_body_mtl | DroneTech_Gun  
            //2 Dronetech_glow1 | Operator_Glow_01  
            //3 Dronetech_glow2 | Operator_Glow_02  //Backpack 
            //4 Dronetech_glow3 | Operator_Glow_03  //Backpack
            //5 Dronetech_glow4 | Operator_Glow_04      
        }

        internal static void DefaultColored(SkinDef skinDroneTechDef)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinOperatorDefault_Yellow",
                nameToken = "SIMU_SKIN_OPERATOR_YELLOW",
                icon = H.GetIcon("dlc3/operator_yellow"),
                original = skinDroneTechDef,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material Dronetech_body_mtl = CloneMat(newRenderInfos, 0);
            Material Dronetech_glow1 = CloneMat(newRenderInfos, 2);        
            Material Dronetech_glow2 = CloneMat(newRenderInfos, 3);
            Material Dronetech_glow3 = CloneMat(newRenderInfos, 4);       
            //Material Dronetech_glow4 = CloneMat(newRenderInfos, 5);
 
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
        }

        internal static void MasteryOrange(SkinDef skinDroneTechDefAlt)
        {
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinOperatorAlt1_Orange",
                nameToken = "SIMU_SKIN_OPERATOR_ORANGE",
                icon = H.GetIcon("dlc3/operator_orange"),
                original = skinDroneTechDefAlt,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matDroneTechDiffuse = CloneMat(newRenderInfos, 0);
            Material matDroneTechGlowAlt1 = CloneMat(newRenderInfos, 2);
            Material matDroneTechGlowAlt2 = CloneMat(newRenderInfos, 3);
            Material matDroneTechGlowAlt3 = CloneMat(newRenderInfos, 4);
            //Material Dronetech_glow4 = CloneMat(newRenderInfos, 5);

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
        }

        [RegisterAchievement("CLEAR_ANY_DRONETECH", "Skins.DroneTech.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumOperatorBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DroneTechBody");
            }
        }

        /*[RegisterAchievement("CLEAR_BOTH_DRONETECH", "Skins.DroneTech.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumOperatorBody2 : Achievement_TWO_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DroneTechBody");
            }
        }*/

    }
}