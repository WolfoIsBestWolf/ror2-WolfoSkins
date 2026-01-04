/*
using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;


namespace WolfoSkinsMod.Mod
{
    public class SkinsNemEnforcer
    {
        internal static void ModdedSkin(GameObject BodyObject)
        {
            Debug.Log("Nemesis Enforcer Skins");

            BodyIndex CharacterIndex = BodyObject.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = BodyObject.GetComponentInChildren<ModelSkinController>();
            SkinDef skinDefault = modelSkinController.skins[0];

            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinNemesisEnforcer_1",
                nameToken = "SIMU_SKIN_NEM_ENFORCER",
                icon = H.GetIcon("mod/nem_enforcer"),
                original = skinDefault,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;
            Material matNemforcer = CloneMat(ref newRenderInfos, 0);

            matNemforcer.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/NemesisEnforcer/texNemforcer.png");

            newRenderInfos[0].defaultMaterial = matNemforcer;
            //newRenderInfos[1].defaultMaterial = ;
            //newRenderInfos[2].defaultMaterial = ;
            //newRenderInfos[3].defaultMaterial = ;
            newRenderInfos[4].defaultMaterial = matNemforcer;
            //
 
            //SkinCatalog.skinsByBody[(int)CharacterIndex] = modelSkinController.skins;
        }

        [RegisterAchievement("CLEAR_ANY_NEMESISENFORCER", "Skins.NemesisEnforcer.Wolfo.First", null, 3, null)]
        public class ClearSimulacrumNemesisEnforcer : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("NemesisEnforcerBody");
            }
        }

    }
}
*/