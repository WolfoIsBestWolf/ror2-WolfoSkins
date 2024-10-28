using R2API.Utils;
using RoR2;
using RoR2.Skills;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WolfoSkinsMod
{
    public class TeslaDesolatorColors
    {
        public static SkillFamily teslaColors;
        public static SkillFamily desolatorColors;


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void AddToTeslaTrooper(GameObject TeslaTrooperBody)
        {
            GameModeCatalog.availability.CallWhenAvailable(CopyToDisplay);
            On.RoR2.CharacterSelectSurvivorPreviewDisplayController.OnLoadoutChangedGlobal += FixColorsNotChanging;
            //
            Debug.Log("Tesla Trooper Colors");

            GenericSkill[] skills = TeslaTrooperBody.GetComponents<GenericSkill>();
            SkillFamily TeslaRecolors = skills[skills.Length - 1].skillFamily;


            SkillDef originalRed = TeslaRecolors.variants[0].skillDef;
            SkillDef dummySkillDef = skills[0].skillFamily.variants[0].skillDef;

            SkillDef skillNeonRed = GameObject.Instantiate(TeslaRecolors.variants[0].skillDef);
            skillNeonRed.skillName = "NeonRed";
            skillNeonRed.skillNameToken = "TESLA_COLOR_RED_NEON";
            skillNeonRed.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorRed.png"));
            SkillDef skillNeonBlue = GameObject.Instantiate(TeslaRecolors.variants[1].skillDef);
            skillNeonBlue.skillName = "NeonBlue";
            skillNeonBlue.skillNameToken = "TESLA_COLOR_BLUE_NEON";
            skillNeonBlue.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorBlue.png"));
            SkillDef skillStrongBlue = GameObject.Instantiate(TeslaRecolors.variants[2].skillDef);
            skillStrongBlue.skillName = "strongblue";
            skillStrongBlue.skillNameToken = "TESLA_COLOR_BLUE_STRONG";
            skillStrongBlue.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaSimpleBlue.png"));
            SkillDef skillNeonCyan = GameObject.Instantiate(TeslaRecolors.variants[1].skillDef);
            skillNeonCyan.skillName = "NeonCyan";
            skillNeonCyan.skillNameToken = "TESLA_COLOR_CYAN_NEON";
            skillNeonCyan.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorCyan.png"));
            SkillDef skillNeonPurple = GameObject.Instantiate(TeslaRecolors.variants[6].skillDef);
            skillNeonPurple.skillName = "NeonPurple";
            skillNeonPurple.skillNameToken = "TESLA_COLOR_PINK_NEON";
            skillNeonPurple.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorPurple.png"));
            SkillDef skillWhite = GameObject.Instantiate(TeslaRecolors.variants[6].skillDef);
            skillWhite.skillName = "white";
            skillWhite.skillNameToken = "TESLA_COLOR_WHITE";
            skillWhite.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaSimpleWhite.png"));

 
            SkillFamily.Variant varWhite = new SkillFamily.Variant
            {
                skillDef = skillWhite,
            };
            SkillFamily.Variant varNeonRed = new SkillFamily.Variant
            {
                skillDef = skillNeonRed,
            };
            SkillFamily.Variant varNormalBlue = new SkillFamily.Variant
            {
                skillDef = skillStrongBlue,
            };
            SkillFamily.Variant varNeonBlue = new SkillFamily.Variant
            {
                skillDef = skillNeonBlue,
            };
            SkillFamily.Variant varNeonCyan = new SkillFamily.Variant
            {
                skillDef = skillNeonCyan,
            };
            SkillFamily.Variant varNeonPurple = new SkillFamily.Variant
            {
                skillDef = skillNeonPurple,
            };

            teslaColors = TeslaRecolors;
            TeslaRecolors.variants = TeslaRecolors.variants.Add(varWhite, varNormalBlue, varNeonBlue, varNeonCyan, varNeonPurple, varNeonRed);


            RA2Mod.General.Components.SkinRecolorController controller = TeslaTrooperBody.GetComponentInChildren<RA2Mod.General.Components.SkinRecolorController>();

            RA2Mod.General.Components.Recolor[] newRecolors = controller.Recolors;
            //controller.Recolors.CopyTo(newRecolors, 0);

            float mult1 = 2.5f;
            float multLamps = 1.25f;

            Color RedMult = new Color(4f, 2f, 2f, 2f);
            Color PinkMult = new Color(2f, 2f, 2.5f, 2f);
            //Upper Armor
            //Body Suit/Pants
            //Armor Rim around face
            //Lights
            Color White = new Color(2, 2, 2, 1);


            RA2Mod.General.Components.Recolor rcWhite = new RA2Mod.General.Components.Recolor
            {
                recolorName = "white",
                colors = new Color[]
{
                    Color.white,
                    Color.white,
                    new Color(0.75f,0.75f,0.75f,1f),
                    new Color(0.5f,1f,1f)
}
            };
            RA2Mod.General.Components.Recolor normalBlue = new RA2Mod.General.Components.Recolor
            {
                recolorName = "strongblue",
                colors = new Color[]
{
                    controller.Recolors[1].colors[0]*1.6f,
                    controller.Recolors[1].colors[1]*1.5f,
                    controller.Recolors[1].colors[2],
                    controller.Recolors[1].colors[3]*1.1f
}
            };
            RA2Mod.General.Components.Recolor neonRed = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonred",
                colors = new Color[]
                {
                    new Color(2.5f, 0.55f, 0.55f, 1), //2.8392 0.549 0.5412 1
                    new Color(1.5f, 0.6f, 0.6f, 1),
                    White,
                    controller.Recolors[0].colors[3]*multLamps
                }
            };

            RA2Mod.General.Components.Recolor neonBlue = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonblue",
                colors = new Color[]
    {
                    new Color(1.25f, 1.25f, 2.5f, 1),
                    new Color(0.75f, 0.75f, 1f, 1),
                    White,
                    controller.Recolors[1].colors[3]*multLamps
    }
            };
            RA2Mod.General.Components.Recolor neonCyan = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neoncyan",
                colors = new Color[]
    {
                    new Color(0.6f, 1.8f, 1.8f, 1),
                    new Color(0.6f, 1f, 1f, 1),
                    White,
                    controller.Recolors[5].colors[3]*multLamps
    }
            };
            RA2Mod.General.Components.Recolor neonPurple = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonpurple",
                colors = new Color[]
    {
                    new Color(1.6f, 1.0f, 1.8f, 1),
                    new Color(0.9f, 0.5f, 0.9f, 1),
                    White,
                    controller.Recolors[6].colors[3]*multLamps
    }
            };
            newRecolors = newRecolors.Add(rcWhite, normalBlue, neonBlue, neonCyan, neonPurple, neonRed);
            controller.SetFieldValue<RA2Mod.General.Components.Recolor[]>("recolors", newRecolors);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void AddToDesolator(GameObject DesolatorBody)
        {
            Debug.Log("Desolator Colors");
            GenericSkill[] skills = DesolatorBody.GetComponents<GenericSkill>();
            SkillFamily TeslaRecolors = skills[skills.Length - 1].skillFamily;

            SkillDef skillStrongGreen = GameObject.Instantiate(TeslaRecolors.variants[3].skillDef);
            skillStrongGreen.skillName = "greenstrong";
            skillStrongGreen.skillNameToken = "TESLA_COLOR_GREEN_STRONG";
            skillStrongGreen.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaSimpleGreen.png"));
            SkillDef skillNeonGreen = GameObject.Instantiate(TeslaRecolors.variants[2].skillDef);
            skillNeonGreen.skillName = "NeonGreen";
            skillNeonGreen.skillNameToken = "TESLA_COLOR_GREEN_NEON";
            skillNeonGreen.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorGreen.png"));
            SkillDef skillNeonYellow = GameObject.Instantiate(TeslaRecolors.variants[3].skillDef);
            skillNeonYellow.skillName = "NeonYellow";
            skillNeonYellow.skillNameToken = "TESLA_COLOR_YELLOW_NEON";
            skillNeonYellow.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorYellow.png"));
            SkillDef skillNeonOrange = GameObject.Instantiate(TeslaRecolors.variants[4].skillDef);
            skillNeonOrange.skillName = "NeonOrange";
            skillNeonOrange.skillNameToken = "TESLA_COLOR_ORANGE_NEON";
            skillNeonOrange.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorOrange.png"));
            SkillDef skillSafteyYellow = GameObject.Instantiate(TeslaRecolors.variants[4].skillDef);
            skillSafteyYellow.skillName = "SafetyYellow";
            skillSafteyYellow.skillNameToken = "TESLA_COLOR_YELLOW_SAFE";
            skillSafteyYellow.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaColorYellowGreen.png"));
            SkillDef skillWhite = GameObject.Instantiate(TeslaRecolors.variants[4].skillDef);
            skillWhite.skillName = "white";
            skillWhite.skillNameToken = "TESLA_COLOR_GRAY";
            skillWhite.icon = WRect.MakeIcon32(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/TeslaDeso/teslaSimpleGray.png"));


            SkillFamily.Variant varWhite = new SkillFamily.Variant
            {
                skillDef = skillWhite,
            };
            SkillFamily.Variant varGreener = new SkillFamily.Variant
            {
                skillDef = skillStrongGreen,
            };
            SkillFamily.Variant varNeonGreen = new SkillFamily.Variant
            {
                skillDef = skillNeonGreen,
            };
            SkillFamily.Variant varNeonYellow = new SkillFamily.Variant
            {
                skillDef = skillNeonYellow,
            };
            SkillFamily.Variant varNeonOrange = new SkillFamily.Variant
            {
                skillDef = skillNeonOrange,
            };
            SkillFamily.Variant varNeonSafetyYellow = new SkillFamily.Variant
            {
                skillDef = skillSafteyYellow,
            };

            desolatorColors = TeslaRecolors;


            TeslaRecolors.variants = TeslaRecolors.variants.Add(varWhite, varGreener, varNeonGreen, varNeonSafetyYellow, varNeonYellow, varNeonOrange);

            RA2Mod.General.Components.SkinRecolorController controller = DesolatorBody.GetComponentInChildren<RA2Mod.General.Components.SkinRecolorController>();

            RA2Mod.General.Components.Recolor[] newRecolors = controller.Recolors;
            //controller.Recolors.CopyTo(newRecolors, 0);

            int mult1 = 5;
            float multTank = 1.5f;

            //Main Clothing
            //Tank
            //Nothing?
            //Nothing?


            RA2Mod.General.Components.Recolor rcWhite = new RA2Mod.General.Components.Recolor
            {
                recolorName = "white",
                colors = new Color[]
    {
                    new Color(0.7f,0.7f,0.7f),
                    new Color(0.4f,0.5f,0f),
                    Color.white,
                    Color.white
    }
            };
            RA2Mod.General.Components.Recolor normalGreen = new RA2Mod.General.Components.Recolor
            {
                recolorName = "greenstrong",
                colors = new Color[]
{
                    new Color(0.7f,1f,0.2f),
                    new Color(0.1f,0.3f,0.1f),
                    new Color(0.4f,1f,0.2f),
                    new Color(0.4f,1f,0.2f)
}
            };
            RA2Mod.General.Components.Recolor neonGreen = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neongreen",
                colors = new Color[]
    {
                    new Color(0,2.25f,0),
                    new Color(0,0.5f,0),
                    new Color(0,2f,0),
                    new Color(0,2f,0)
    }
            };
            RA2Mod.General.Components.Recolor SafteyYellow = new RA2Mod.General.Components.Recolor
            {
                recolorName = "safetyyellow",
                colors = new Color[]
{
                     new Color(1.5f,2.2f,0),
                     new Color(0.35f,0.5f,0),
                     new Color(1.5f,3f,0),
                     new Color(1.5f,3f,0),
}
            };
            RA2Mod.General.Components.Recolor NeonYellow = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonyellow",
                colors = new Color[]
    {
                    new Color(2.2f,2.05f,0), 
                    new Color(0.4f,0.375f,0),
                    new Color(2,1.8f,0),
                    new Color(2,1.8f,0)
    }
            };
            RA2Mod.General.Components.Recolor NeonOrange = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonorange",
                colors = new Color[]
    {
                     new Color(2.5f,1.25f,0),
                     new Color(0.5f,0.25f,0),
                     new Color(3,1.5f,0),
                     new Color(3,1.5f,0),
    }
            };

            newRecolors = newRecolors.Add(rcWhite, normalGreen, neonGreen, SafteyYellow, NeonYellow, NeonOrange);
            controller.SetFieldValue<RA2Mod.General.Components.Recolor[]>("recolors", newRecolors);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void AddToTeslaTower(GameObject TeslaTowerBody)
        {
            Debug.Log("Tesla Tower Colors");
            RA2Mod.General.Components.SkinRecolorController controller = TeslaTowerBody.GetComponentInChildren<RA2Mod.General.Components.SkinRecolorController>();
            RA2Mod.General.Components.Recolor[] newRecolors = controller.Recolors;

            float mult1 = 1.5f;

            Color RedMult = new Color(1.75f, 1.25f, 1.25f);
            Color PinkMult = new Color(1.5f, 1f, 1.75f);
            //Bottom Prongs
            //Nothing?

            RA2Mod.General.Components.Recolor rcWhite = new RA2Mod.General.Components.Recolor
            {
                recolorName = "white",
                colors = new Color[]
{
                    Color.white,
                    Color.white,
}
            };
            RA2Mod.General.Components.Recolor normalBlue = new RA2Mod.General.Components.Recolor
            {
                recolorName = "strongblue",
                colors = new Color[]
{
                    controller.Recolors[1].colors[0]*1.1f,
                    controller.Recolors[1].colors[1]*1.1f,
}
            };

            RA2Mod.General.Components.Recolor neonRed = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonred",
                colors = new Color[]
                {
                    controller.Recolors[0].colors[0]*RedMult,
                    controller.Recolors[0].colors[1]*RedMult,
                }
            };

            RA2Mod.General.Components.Recolor neonBlue = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonblue",
                colors = new Color[]
    {
                    controller.Recolors[1].colors[0]*mult1,
                    controller.Recolors[1].colors[1]*mult1,
    }
            };
            RA2Mod.General.Components.Recolor neonCyan = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neoncyan",
                colors = new Color[]
    {
                    controller.Recolors[5].colors[0]*mult1,
                    controller.Recolors[5].colors[1]*mult1,
    }
            };
            RA2Mod.General.Components.Recolor neonPurple = new RA2Mod.General.Components.Recolor
            {
                recolorName = "neonpurple",
                colors = new Color[]
    {
                    controller.Recolors[6].colors[0]*PinkMult,
                    controller.Recolors[6].colors[1]*PinkMult,
    }
            };
            newRecolors = newRecolors.Add(rcWhite, normalBlue, neonBlue, neonCyan, neonPurple, neonRed);
            controller.SetFieldValue<RA2Mod.General.Components.Recolor[]>("recolors", newRecolors);

            GameObject ScepterBody = BodyCatalog.FindBodyPrefab("TeslaTowerScepterBody");
            if (ScepterBody)
            {
                RA2Mod.General.Components.SkinRecolorController controllerScepter = ScepterBody.GetComponentInChildren<RA2Mod.General.Components.SkinRecolorController>();
                controllerScepter.SetFieldValue<RA2Mod.General.Components.Recolor[]>("recolors", controller.Recolors);
            } 
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void CopyToDisplay()
        {
            SurvivorDef Tesla = SurvivorCatalog.FindSurvivorDef("TeslaTrooper");
            SurvivorDef Desolator = SurvivorCatalog.FindSurvivorDef("Desolator");

            GameObject TeslaBody = BodyCatalog.FindBodyPrefab("TeslaTrooperBody");
            GameObject DesolatorBody = BodyCatalog.FindBodyPrefab("DesolatorBody");

            RA2Mod.General.Components.SkinRecolorController controller = Tesla.displayPrefab.GetComponent<RA2Mod.General.Components.SkinRecolorController>();
            RA2Mod.General.Components.Recolor[] newRecolors = TeslaBody.GetComponentInChildren<RA2Mod.General.Components.SkinRecolorController>().Recolors;
            controller.SetFieldValue<RA2Mod.General.Components.Recolor[]>("recolors", newRecolors);

            controller = Desolator.displayPrefab.GetComponent<RA2Mod.General.Components.SkinRecolorController>();
            newRecolors = DesolatorBody.GetComponentInChildren<RA2Mod.General.Components.SkinRecolorController>().Recolors;
            controller.SetFieldValue<RA2Mod.General.Components.Recolor[]>("recolors", newRecolors);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void FixColorsNotChanging(On.RoR2.CharacterSelectSurvivorPreviewDisplayController.orig_OnLoadoutChangedGlobal orig, CharacterSelectSurvivorPreviewDisplayController self, NetworkUser changedNetworkUser)
        {
            orig(self, changedNetworkUser);

            RA2Mod.General.Components.SkinRecolorController skinRecolorController = self.GetComponent<RA2Mod.General.Components.SkinRecolorController>();
            if (skinRecolorController)
            {
                if (changedNetworkUser != self.networkUser)
                {
                    return;
                }
                Loadout loadout = Loadout.RequestInstance();
                changedNetworkUser.networkLoadout.CopyLoadout(loadout);
                BodyIndex bodyIndex = BodyCatalog.FindBodyIndex(self.bodyPrefab);
                if (bodyIndex == BodyIndex.None)
                {
                    return;
                }
                uint skillIndex = self.currentLoadout.bodyLoadoutManager.GetSkillVariant(bodyIndex, 4);
                SkillFamily colorFamily = self.currentLoadout.bodyLoadoutManager.GetReadOnlyBodyLoadout(bodyIndex).GetSkillFamily(4);
                SkillDef color = colorFamily.variants[skillIndex].skillDef;

                if (color)
                {
                    skinRecolorController.SetRecolor(color.skillName.ToLowerInvariant());
                }
                  

            }
        }
    }
}