using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC1
{
    public class SkinsEnforcer
    {
        internal static void Start()
        {
            SkinDef skinEnforcerDefault = Addressables.LoadAssetAsync<SkinDef>(key: "7c9e1cf7cbd8832408011b029c1f7faa").WaitForCompletion();
            SkinDef skinEnforcerAlt = Addressables.LoadAssetAsync<SkinDef>(key: "b2b5e6044d531c7449abaf61933a15b8").WaitForCompletion();

        }

        internal static void Default_Red(SkinDefMakeOnApply newSkinDef)
        {

        }
        internal static void Mastery_Blue(SkinDefMakeOnApply newSkinDef)
        {

        }

        [RegisterAchievement("CLEAR_ANY_ENFORCER2", "Skins.Enforcer2.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumDrifterBodyBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return DLC4Content.BodyPrefabs.Enforcer2Body.bodyIndex;
            }
        }

  

    }
}