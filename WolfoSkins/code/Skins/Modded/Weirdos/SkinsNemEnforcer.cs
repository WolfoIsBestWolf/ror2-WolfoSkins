using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsNemEnforcer
    {
        internal static void ModdedSkin(GameObject BodyObject)
        {
            Debug.Log("Nemesis Enforcer Skins");

            BodyIndex CharacterIndex = BodyObject.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = BodyObject.GetComponentInChildren<ModelSkinController>();
            SkinDef skinDefault = modelSkinController.skins[0];

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinDefault.rendererInfos.Length];
            System.Array.Copy(skinDefault.rendererInfos, NewRenderInfos, skinDefault.rendererInfos.Length);

            Material matNemforcer = Object.Instantiate(skinDefault.rendererInfos[0].defaultMaterial);

            Texture2D texNemforcer = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/NemesisEnforcer/texNemforcer.png");
            texNemforcer.wrapMode = TextureWrapMode.Repeat;

            matNemforcer.mainTexture = texNemforcer;

            NewRenderInfos[0].defaultMaterial = matNemforcer;
            //NewRenderInfos[1].defaultMaterial = ;
            //NewRenderInfos[2].defaultMaterial = ;
            //NewRenderInfos[3].defaultMaterial = ;
            NewRenderInfos[4].defaultMaterial = matNemforcer;
            //
            //
            SkinDefInfo SkinInfo = new SkinDefInfo
            {
                Name = "skinNemesisEnforcerWolfo_Simu",
                NameToken = "SIMU_SKIN_NEM_ENFORCER",
                Icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/mod/NemesisEnforcer/skinIconNemEnforcer.png")),
                BaseSkins = skinDefault.baseSkins,
                RootObject = skinDefault.rootObject,
                RendererInfos = NewRenderInfos,
                MeshReplacements = skinDefault.meshReplacements,
                GameObjectActivations = skinDefault.gameObjectActivations,
            };

            SkinDef NewSkinDef = Skins.CreateNewSkinDef(SkinInfo);
            modelSkinController.skins = modelSkinController.skins.Add(NewSkinDef);
            BodyCatalog.skins[(int)CharacterIndex] = BodyCatalog.skins[(int)CharacterIndex].Add(NewSkinDef);
        }

        [RegisterAchievement("CLEAR_ANY_NEMESISENFORCER", "Skins.NemesisEnforcer.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumNemesisEnforcer : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("NemesisEnforcerBody");
            }
        }

    }
}