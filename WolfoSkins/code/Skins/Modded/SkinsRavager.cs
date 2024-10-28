using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
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
            
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinRavager.rendererInfos.Length];
            System.Array.Copy(skinRavager.rendererInfos, NewRenderInfos, skinRavager.rendererInfos.Length);

            Material matBody = Object.Instantiate(skinRavager.rendererInfos[0].defaultMaterial);
            Material matSword = Object.Instantiate(skinRavager.rendererInfos[1].defaultMaterial);
            Material matImpBoss = Object.Instantiate(skinRavager.rendererInfos[2].defaultMaterial);
            
            Texture2D texBody = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexBody.png");
            texBody.wrapMode = TextureWrapMode.Repeat;

            Texture2D texBodyEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexBodyEmission.png");
            texBodyEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSword = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexSword.png");
            texSword.wrapMode = TextureWrapMode.Repeat;

            Texture2D texSwordEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/RAVAGERtexSwordEmission.png");
            texSwordEmission.wrapMode = TextureWrapMode.Repeat;

            matBody.mainTexture = texBody;
            matBody.SetTexture("_EmTex", texBodyEmission);

            matSword.mainTexture = texSword;
            matSword.SetTexture("_EmTex", texSwordEmission);

            matImpBoss.color = new Color(1f, 0.8f, 0.7f); //0.5 0.5 0.4 1
            matImpBoss.DisableKeyword("FORCE_SPEC");
            matImpBoss.SetColor("_EmColor", Color.red);

            NewRenderInfos[0].defaultMaterial = matBody;
            NewRenderInfos[1].defaultMaterial = matSword;
            NewRenderInfos[2].defaultMaterial = matImpBoss;
            //
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinRavagerWolfo_Simu",
                NameToken = "SIMU_SKIN_RAVAGER",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/Ravager/skinRavagerIcon.png")),
                BaseSkins = new SkinDef[] { skinRavager },
                RootObject = skinRavager.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinRavager.meshReplacements,
                GameObjectActivations = skinRavager.gameObjectActivations,
                ProjectileGhostReplacements = skinRavager.projectileGhostReplacements,
            };
            SkinDef RavagerSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);


            modelSkinController.skins = modelSkinController.skins.Add(RavagerSkinDefNew);
            BodyCatalog.skins[(int)RavagerIndex] = BodyCatalog.skins[(int)RavagerIndex].Add(RavagerSkinDefNew);
        }

        [RegisterAchievement("CLEAR_ANY_ROB_RAVAGER_BODY_NAME", "Skins.ROB_RAVAGER_BODY_NAME.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumRobRavager : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("RobRavagerBody");
            }
        }

    }
}