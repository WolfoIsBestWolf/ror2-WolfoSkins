using RoR2;

namespace WolfoSkinsMod
{
    public class TeslaDesolatorUnlocks
    {
        [RegisterAchievement("CLEAR_ANY_TESLATROOPER", "Skins.TeslaTrooper.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumTeslaTrooperClassic : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("TeslaTrooperBody");
            }
        }

        [RegisterAchievement("CLEAR_ANY_DESOLATOR", "Skins.Desolator.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumDesolatorClassic : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("DesolatorBody");
            }
        }

    }
}