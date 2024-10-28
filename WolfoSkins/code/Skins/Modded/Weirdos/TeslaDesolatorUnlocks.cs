using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class TeslaDesolatorUnlocks
    {
        [RegisterAchievement("CLEAR_ANY_TESLATROOPER", "Skins.TeslaTrooper.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumTeslaTrooperClassic : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TeslaTrooperBody");
            }
        }

        [RegisterAchievement("CLEAR_ANY_DESOLATOR", "Skins.Desolator.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumDesolatorClassic : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DesolatorBody");
            }
        }

    }
}