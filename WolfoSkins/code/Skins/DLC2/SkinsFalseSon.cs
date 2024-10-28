using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsFalseSon
    {
        internal static void Start()
        {
            Default_Alt();
            Mastery_Alt();
        }

        internal static void Mastery_Alt()
        {
            SkinDef skinFalseSonAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/FalseSon/skinFalseSonAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinFalseSonAlt.rendererInfos.Length];
            System.Array.Copy(skinFalseSonAlt.rendererInfos, NewRenderInfos, skinFalseSonAlt.rendererInfos.Length);

            //0 matFalseSon
            //1 matFalseSonCloth / 
            //2 matFalseSon / Weapon


            Material matFalseSon = Object.Instantiate(skinFalseSonAlt.rendererInfos[0].defaultMaterial);
            Material matFalseSonCloth = Object.Instantiate(skinFalseSonAlt.rendererInfos[1].defaultMaterial);

            Texture2D texFalseSonAltDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltDiffuse.png");
            texFalseSonAltDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texFalseSonAltClothDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltClothDiffuse.png");
            texFalseSonAltClothDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texFalseSonAltSwordDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltSwordDiffuse.png");
            texFalseSonAltSwordDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texFalseSonAltFlow = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltFlow.png");
            texFalseSonAltFlow.wrapMode = TextureWrapMode.Clamp;

            Texture2D texFalseSonAltFresnel = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltFresnel.png");
            texFalseSonAltFresnel.wrapMode = TextureWrapMode.Clamp;

            Texture2D texFalseSonAltFlowSword = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texFalseSonAltFlowSword.png");
            texFalseSonAltFlowSword.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampCaptainAirstrike = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/texRampCaptainAirstrike.png");
            texRampCaptainAirstrike.wrapMode = TextureWrapMode.Clamp;


            matFalseSon.mainTexture = texFalseSonAltDiffuse;
            matFalseSon.SetColor("_EmColor", new Color(0.55f, 1f, 0.85f));
            matFalseSon.SetTexture("_FresnelMask", texFalseSonAltFresnel);
            matFalseSon.SetTexture("_FlowHeightmap", texFalseSonAltFlow);
            matFalseSon.SetTexture("_FlowHeightRamp", texRampCaptainAirstrike);

            matFalseSonCloth.mainTexture = texFalseSonAltClothDiffuse;
            matFalseSonCloth.color = Color.white;
            matFalseSonCloth.SetTexture("_FlowHeightRamp", null);

            Material matFalseSonSword = Object.Instantiate(matFalseSon);
            matFalseSonSword.mainTexture = texFalseSonAltSwordDiffuse;
            matFalseSonSword.SetTexture("_FlowHeightmap", texFalseSonAltFlowSword);

            NewRenderInfos[0].defaultMaterial = matFalseSon;
            NewRenderInfos[1].defaultMaterial = matFalseSonCloth;
            NewRenderInfos[2].defaultMaterial = matFalseSonSword;

            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinFalseSonAltWolfo_Provi_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_FALSESON_PROVI";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Provi/icon.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinFalseSonAlt };
            newSkinDef.meshReplacements = skinFalseSonAlt.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinFalseSonAlt.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSon/FalseSonBody.prefab").WaitForCompletion(), newSkinDef);

        }


        internal static void Default_Alt()
        {
            SkinDef skinFalseSonDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC2/FalseSon/skinFalseSonDefault.asset").WaitForCompletion();
            
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinFalseSonDefault.rendererInfos.Length];
            System.Array.Copy(skinFalseSonDefault.rendererInfos, NewRenderInfos, skinFalseSonDefault.rendererInfos.Length);

            //0 matFalseSon
            //1 matFalseSonCloth / 
            //2 matFalseSon / Weapon

            Material matFalseSon = Object.Instantiate(skinFalseSonDefault.rendererInfos[0].defaultMaterial);
            Material matFalseSonCloth = Object.Instantiate(skinFalseSonDefault.rendererInfos[1].defaultMaterial);

            Texture2D texFalseSonDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Gold/texFalseSonDiffuse.png");
            texFalseSonDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texFalseSonFresnel = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Gold/texFalseSonFresnel.png");
            texFalseSonFresnel.wrapMode = TextureWrapMode.Clamp;

            Texture2D texFalseSonClothDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Gold/texFalseSonClothDiffuse.png");
            texFalseSonClothDiffuse.wrapMode = TextureWrapMode.Clamp;

            matFalseSon.mainTexture = texFalseSonDiffuse;
            matFalseSon.SetColor("_EmColor", new Color(1f,0.8f,0.8f));
            matFalseSon.SetTexture("_FresnelMask", texFalseSonFresnel);
            matFalseSon.SetTexture("_FlowHeightRamp", matFalseSon.GetTexture("_FresnelRamp"));
            matFalseSonCloth.mainTexture = texFalseSonClothDiffuse;
            matFalseSonCloth.color = Color.white;
            

            NewRenderInfos[0].defaultMaterial = matFalseSon;
            NewRenderInfos[1].defaultMaterial = matFalseSonCloth;
            NewRenderInfos[2].defaultMaterial = matFalseSon;
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinFalseSonWolfo_Gold_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_FALSESON_GOLD";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc2/FalseSon/Gold/icon.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinFalseSonDefault };
            newSkinDef.meshReplacements = skinFalseSonDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinFalseSonDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSon/FalseSonBody.prefab").WaitForCompletion(), newSkinDef);
        }

        [RegisterAchievement("CLEAR_ANY_FALSESON", "Skins.FalseSon.Wolfo.First", null, 5, null)]
        public class ClearSimulacrumFalseSonBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("FalseSonBody");
            }
        }

        /*[RegisterAchievement("CLEAR_BOTH_FALSESON", "Skins.FalseSon.Wolfo.Both", null, 5, null)]
        public class ClearSimulacrumFalseSonBody2 : Achievement_AltBoss
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("FalseSonBody");
            }
        }*/
    }
}