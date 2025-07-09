using RoR2;
using UnityEngine;
using static WolfoSkinsMod.H;


namespace WolfoSkinsMod.Mod
{
    public class SkinsRavager
    {
        internal static void ModdedSkin(GameObject RavagerBody)
        {
            Debug.Log("Ravager Skins");
            BodyIndex RavagerIndex = RavagerBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = RavagerBody.transform.GetChild(0).GetChild(2).GetComponent<ModelSkinController>();
            SkinDef skinRavager = modelSkinController.skins[0];

            //0 matRavager
            //1 matRavager
            //2 matRavager
            SkinDef newSkinDef = H.CreateNewSkin(new SkinInfo
            {
                name = "skinRavager_1",
                nameToken = "SIMU_SKIN_RAVAGER",
                icon = H.GetIcon("mod/ravager"),
                original = skinRavager,
            });
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matBody = CloneMat(newRenderInfos, 0);
            Material matSword = CloneMat(newRenderInfos, 1);
            Material matImpBoss = CloneMat(newRenderInfos, 2);


            matBody.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexBody.png");
            matBody.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexBodyEmission.png"));

            matSword.mainTexture = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexSword.png");
            matSword.SetTexture("_EmTex", Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexSwordEmission.png"));

            matImpBoss.color = new Color(1f, 0.8f, 0.7f); //0.5 0.5 0.4 1
            matImpBoss.DisableKeyword("FORCE_SPEC");
            matImpBoss.SetColor("_EmColor", Color.red);

            newRenderInfos[0].defaultMaterial = matBody;
            newRenderInfos[1].defaultMaterial = matSword;
            newRenderInfos[2].defaultMaterial = matImpBoss;


            SkinCatalog.skinsByBody[(int)RavagerIndex] = modelSkinController.skins;
        }

        [RegisterAchievement("CLEAR_ANY_ROB_RAVAGER_BODY_NAME", "Skins.ROB_RAVAGER_BODY_NAME.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumRobRavager : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RobRavagerBody");
            }
        }

    }
}