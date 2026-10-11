using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC1
{
    public class SkinsAcolyte
    {
        internal static void Start()
        {
            
        }
 
        [RegisterAchievement("CLEAR_ANY_ACOLYTE", "Skins.Acolyte.Wolfo.First", "ChampRushRitualCompleted", 3U, null)]
        public class ClearSimulacrumDrifterBodyBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("AcolyteBody");
            }
        }

  

    }
}