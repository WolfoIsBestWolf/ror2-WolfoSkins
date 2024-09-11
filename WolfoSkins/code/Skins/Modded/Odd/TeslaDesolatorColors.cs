using R2API.Utils;
using RoR2;
using RoR2.Skills;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WolfoSkinsMod
{
    public class TeslaDesolatorColors
    {
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void AddToTeslaTrooper(GameObject TeslaTrooperBody)
        {
            GameModeCatalog.availability.CallWhenAvailable(CopyToDisplay);
            On.RoR2.CharacterSelectSurvivorPreviewDisplayController.OnLoadoutChangedGlobal += FixColorsNotChanging;
            //
            Debug.Log("Tesla Trooper Colors");
            TeslaDesolatorUnlocks.unlockableDefTesla.hidden = false;
            if (WConfig.cfgUnlockAll.Value)
            {
                TeslaDesolatorUnlocks.unlockableDefTesla = null;
                TeslaDesolatorUnlocks.unlockableDefDesolator = null;
            }
            GenericSkill[] skills = TeslaTrooperBody.GetComponents<GenericSkill>();
            SkillFamily TeslaRecolors = skills[skills.Length - 1].skillFamily;


            SkillDef originalRed = TeslaRecolors.variants[0].skillDef;
            SkillDef dummySkillDef = skills[0].skillFamily.variants[0].skillDef;

            SkillDef skillNeonRed = GameObject.Instantiate(TeslaRecolors.variants[0].skillDef);
            skillNeonRed.skillName = "NeonRed";
            skillNeonRed.skillNameToken = "Neon Red";
            skillNeonRed.icon = WRect.MakeIcon32(Properties.Resources.teslaColorRed);
            SkillDef skillNeonBlue = GameObject.Instantiate(TeslaRecolors.variants[1].skillDef);
            skillNeonBlue.skillName = "NeonBlue";
            skillNeonBlue.skillNameToken = "Neon Blue";
            skillNeonBlue.icon = WRect.MakeIcon32(Properties.Resources.teslaColorBlue);
            SkillDef skillStrongBlue = GameObject.Instantiate(TeslaRecolors.variants[2].skillDef);
            skillStrongBlue.skillName = "strongblue";
            skillStrongBlue.skillNameToken = "Strong Blue";
            skillStrongBlue.icon = WRect.MakeIcon32(Properties.Resources.teslaSimpleBlue);
            SkillDef skillNeonCyan = GameObject.Instantiate(TeslaRecolors.variants[1].skillDef);
            skillNeonCyan.skillName = "NeonCyan";
            skillNeonCyan.skillNameToken = "Neon Cyan";
            skillNeonCyan.icon = WRect.MakeIcon32(Properties.Resources.teslaColorCyan);
            SkillDef skillNeonPurple = GameObject.Instantiate(TeslaRecolors.variants[6].skillDef);
            skillNeonPurple.skillName = "NeonPurple";
            skillNeonPurple.skillNameToken = "Hot Pink";
            skillNeonPurple.icon = WRect.MakeIcon32(Properties.Resources.teslaColorPurple);
            SkillDef skillWhite = GameObject.Instantiate(TeslaRecolors.variants[6].skillDef);
            skillWhite.skillName = "white";
            skillWhite.skillNameToken = "White";
            skillWhite.icon = WRect.MakeIcon32(Properties.Resources.teslaSimpleWhite);

            SkillFamily.Variant varWhite = new SkillFamily.Variant
            {
                skillDef = skillWhite,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefTesla
            };
            SkillFamily.Variant varNeonRed = new SkillFamily.Variant
            {
                skillDef = skillNeonRed,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefTesla
            };
            SkillFamily.Variant varNormalBlue = new SkillFamily.Variant
            {
                skillDef = skillStrongBlue,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefTesla
            };
            SkillFamily.Variant varNeonBlue = new SkillFamily.Variant
            {
                skillDef = skillNeonBlue,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefTesla
            };
            SkillFamily.Variant varNeonCyan = new SkillFamily.Variant
            {
                skillDef = skillNeonCyan,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefTesla
            };
            SkillFamily.Variant varNeonPurple = new SkillFamily.Variant
            {
                skillDef = skillNeonPurple,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefTesla
            };

            TeslaRecolors.variants = TeslaRecolors.variants.Add(varWhite, varNormalBlue, varNeonBlue, varNeonCyan, varNeonPurple, varNeonRed);

            SkinRecolorController controller = TeslaTrooperBody.GetComponentInChildren<SkinRecolorController>();

            Recolor[] newRecolors = controller.Recolors;
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


            Recolor rcWhite = new Recolor
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
            Recolor normalBlue = new Recolor
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
            Recolor neonRed = new Recolor
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

            Recolor neonBlue = new Recolor
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
            Recolor neonCyan = new Recolor
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
            Recolor neonPurple = new Recolor
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
            controller.SetFieldValue<Recolor[]>("recolors", newRecolors);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void AddToDesolator(GameObject DesolatorBody)
        {
            Debug.Log("Desolator Colors");
            TeslaDesolatorUnlocks.unlockableDefDesolator.hidden = false;
            GenericSkill[] skills = DesolatorBody.GetComponents<GenericSkill>();
            SkillFamily TeslaRecolors = skills[skills.Length - 1].skillFamily;

            SkillDef skillStrongGreen = GameObject.Instantiate(TeslaRecolors.variants[3].skillDef);
            skillStrongGreen.skillName = "greenstrong";
            skillStrongGreen.skillNameToken = "Strong Green";
            skillStrongGreen.icon = WRect.MakeIcon32(Properties.Resources.teslaSimpleGreen);
            SkillDef skillNeonGreen = GameObject.Instantiate(TeslaRecolors.variants[2].skillDef);
            skillNeonGreen.skillName = "NeonGreen";
            skillNeonGreen.skillNameToken = "Neon Green";
            skillNeonGreen.icon = WRect.MakeIcon32(Properties.Resources.teslaColorGreen);
            SkillDef skillNeonYellow = GameObject.Instantiate(TeslaRecolors.variants[3].skillDef);
            skillNeonYellow.skillName = "NeonYellow";
            skillNeonYellow.skillNameToken = "Neon Yellow";
            skillNeonYellow.icon = WRect.MakeIcon32(Properties.Resources.teslaColorYellow);
            SkillDef skillNeonOrange = GameObject.Instantiate(TeslaRecolors.variants[4].skillDef);
            skillNeonOrange.skillName = "NeonOrange";
            skillNeonOrange.skillNameToken = "Neon Orange";
            skillNeonOrange.icon = WRect.MakeIcon32(Properties.Resources.teslaColorOrange);
            SkillDef skillSafteyYellow = GameObject.Instantiate(TeslaRecolors.variants[4].skillDef);
            skillSafteyYellow.skillName = "SafetyYellow";
            skillSafteyYellow.skillNameToken = "Safety Yellow";
            skillSafteyYellow.icon = WRect.MakeIcon32(Properties.Resources.teslaColorYellowGreen);
            SkillDef skillWhite = GameObject.Instantiate(TeslaRecolors.variants[4].skillDef);
            skillWhite.skillName = "white";
            skillWhite.skillNameToken = "Gray";
            skillWhite.icon = WRect.MakeIcon32(Properties.Resources.teslaSimpleGray);

            SkillFamily.Variant varWhite = new SkillFamily.Variant
            {
                skillDef = skillWhite,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefDesolator
            };
            SkillFamily.Variant varGreener = new SkillFamily.Variant
            {
                skillDef = skillStrongGreen,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefDesolator
            };
            SkillFamily.Variant varNeonGreen = new SkillFamily.Variant
            {
                skillDef = skillNeonGreen,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefDesolator
            };
            SkillFamily.Variant varNeonYellow = new SkillFamily.Variant
            {
                skillDef = skillNeonYellow,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefDesolator
            };
            SkillFamily.Variant varNeonOrange = new SkillFamily.Variant
            {
                skillDef = skillNeonOrange,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefDesolator
            };
            SkillFamily.Variant varNeonSafetyYellow = new SkillFamily.Variant
            {
                skillDef = skillSafteyYellow,
                unlockableDef = TeslaDesolatorUnlocks.unlockableDefDesolator
            };

            TeslaRecolors.variants = TeslaRecolors.variants.Add(varWhite, varGreener, varNeonGreen, varNeonSafetyYellow, varNeonYellow, varNeonOrange);

            SkinRecolorController controller = DesolatorBody.GetComponentInChildren<SkinRecolorController>();

            Recolor[] newRecolors = controller.Recolors;
            //controller.Recolors.CopyTo(newRecolors, 0);

            int mult1 = 5;
            float multTank = 1.5f;

            //Main Clothing
            //Tank
            //Nothing?
            //Nothing?


            Recolor rcWhite = new Recolor
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
            Recolor normalGreen = new Recolor
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
            Recolor neonGreen = new Recolor
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
            Recolor SafteyYellow = new Recolor
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
            Recolor NeonYellow = new Recolor
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
            Recolor NeonOrange = new Recolor
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
            controller.SetFieldValue<Recolor[]>("recolors", newRecolors);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void AddToTeslaTower(GameObject TeslaTowerBody)
        {
            Debug.Log("Tesla Tower Colors");
            SkinRecolorController controller = TeslaTowerBody.GetComponentInChildren<SkinRecolorController>();
            Recolor[] newRecolors = controller.Recolors;

            float mult1 = 1.5f;

            Color RedMult = new Color(1.75f, 1.25f, 1.25f);
            Color PinkMult = new Color(1.5f, 1f, 1.75f);
            //Bottom Prongs
            //Nothing?

            Recolor rcWhite = new Recolor
            {
                recolorName = "white",
                colors = new Color[]
{
                    Color.white,
                    Color.white,
}
            };
            Recolor normalBlue = new Recolor
            {
                recolorName = "strongblue",
                colors = new Color[]
{
                    controller.Recolors[1].colors[0]*1.1f,
                    controller.Recolors[1].colors[1]*1.1f,
}
            };

            Recolor neonRed = new Recolor
            {
                recolorName = "neonred",
                colors = new Color[]
                {
                    controller.Recolors[0].colors[0]*RedMult,
                    controller.Recolors[0].colors[1]*RedMult,
                }
            };

            Recolor neonBlue = new Recolor
            {
                recolorName = "neonblue",
                colors = new Color[]
    {
                    controller.Recolors[1].colors[0]*mult1,
                    controller.Recolors[1].colors[1]*mult1,
    }
            };
            Recolor neonCyan = new Recolor
            {
                recolorName = "neoncyan",
                colors = new Color[]
    {
                    controller.Recolors[5].colors[0]*mult1,
                    controller.Recolors[5].colors[1]*mult1,
    }
            };
            Recolor neonPurple = new Recolor
            {
                recolorName = "neonpurple",
                colors = new Color[]
    {
                    controller.Recolors[6].colors[0]*PinkMult,
                    controller.Recolors[6].colors[1]*PinkMult,
    }
            };
            newRecolors = newRecolors.Add(rcWhite, normalBlue, neonBlue, neonCyan, neonPurple, neonRed);
            controller.SetFieldValue<Recolor[]>("recolors", newRecolors);

            GameObject ScepterBody = BodyCatalog.FindBodyPrefab("TeslaTowerScepterBody");
            SkinRecolorController controllerScepter = ScepterBody.GetComponentInChildren<SkinRecolorController>();
            controllerScepter.SetFieldValue<Recolor[]>("recolors", controller.Recolors);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        internal static void CopyToDisplay()
        {
            SurvivorDef Tesla = SurvivorCatalog.FindSurvivorDef("TeslaTrooper");
            SurvivorDef Desolator = SurvivorCatalog.FindSurvivorDef("Desolator");

            GameObject TeslaBody = BodyCatalog.FindBodyPrefab("TeslaTrooperBody");
            GameObject DesolatorBody = BodyCatalog.FindBodyPrefab("DesolatorBody");

            SkinRecolorController controller = Tesla.displayPrefab.GetComponent<SkinRecolorController>();
            Recolor[] newRecolors = TeslaBody.GetComponentInChildren<SkinRecolorController>().Recolors;
            controller.SetFieldValue<Recolor[]>("recolors", newRecolors);

            controller = Desolator.displayPrefab.GetComponent<SkinRecolorController>();
            newRecolors = DesolatorBody.GetComponentInChildren<SkinRecolorController>().Recolors;
            controller.SetFieldValue<Recolor[]>("recolors", newRecolors);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void FixColorsNotChanging(On.RoR2.CharacterSelectSurvivorPreviewDisplayController.orig_OnLoadoutChangedGlobal orig, CharacterSelectSurvivorPreviewDisplayController self, NetworkUser changedNetworkUser)
        {
            orig(self, changedNetworkUser);

            SkinRecolorController skinRecolorController = self.GetComponent<SkinRecolorController>();
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