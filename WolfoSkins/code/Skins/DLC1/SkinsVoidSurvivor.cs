using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static WolfoSkinsMod.H;

namespace WolfoSkinsMod.DLC1
{
    public class SkinsVoidFiend
    {
        internal static void Start()
        {
            SkinDef skinVoidSurvivorDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/VoidSurvivor/skinVoidSurvivorDefault.asset").WaitForCompletion();
            SkinDef skinVoidSurvivorAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/VoidSurvivor/skinVoidSurvivorAlt.asset").WaitForCompletion();

            //Void Ally
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinVoidSurvivorAlly_1",
                nameToken = "SIMU_SKIN_VOIDSURVIVOR",
                icon = H.GetIcon("dlc1/voidfiend_blue"),
                original = skinVoidSurvivorDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Ally));
            //Imp
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinVoidSurvivorImp_1",
                nameToken = "SIMU_SKIN_VOIDSURVIVOR2",
                icon = H.GetIcon("dlc1/voidfiend_red"),
                original = skinVoidSurvivorDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_Imp));
            //
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinVoidSurvivorAltDark_1",
                nameToken = "SIMU_SKIN_VOIDSURVIVOR_ALTBLACK",
                icon = H.GetIcon("dlc1/voidfiend_dark"),
                original = skinVoidSurvivorAlt,
            }, new System.Action<SkinDefMakeOnApply>(Mastery_Voider));
            //Inverted-ish
            CreateEmptySkinForLaterCreation(new SkinInfo
            {
                name = "skinVoidSurvivorInv_1",
                nameToken = "SIMU_SKIN_VOIDSURVIVOR_INV",
                icon = H.GetIcon("dlc1/voidfiend_orange"),
                original = skinVoidSurvivorDefault,
            }, new System.Action<SkinDefMakeOnApply>(Default_InvertedShrimpFried));

        }

        internal static void Mastery_Voider(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matVoidSurvivorFlesh = CloneMat(ref newRenderInfos, 0);
            Material matVoidSurvivorHead = CloneMat(ref newRenderInfos, 1);
            Material matVoidSurvivorMetal = CloneMat(ref newRenderInfos, 2);

            matVoidSurvivorFlesh.color = new Color(1f, 0.667f, 0.778f, 1f);
            matVoidSurvivorFlesh.SetColor("_EmColor", new Color(0.7f, 0.2477f, 0.4481f, 1f));
            matVoidSurvivorFlesh.SetFloat("_FresnelPower", 5f);
            matVoidSurvivorHead.color = new Color(1f, 0.556f, 0.667f, 1f);
            matVoidSurvivorHead.SetColor("_EmColor", new Color(0.7f, 0.2477f, 0.4481f, 1f));
            matVoidSurvivorMetal.color = new Color(0.5f, 0.45f, 0.48f, 1f);

            newRenderInfos[0].defaultMaterial = matVoidSurvivorFlesh;
            newRenderInfos[1].defaultMaterial = matVoidSurvivorHead;
            newRenderInfos[2].defaultMaterial = matVoidSurvivorMetal;

        }


        internal static void Default_Ally(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matVoidSurvivorFlesh = CloneMat(ref newRenderInfos, 0);
            Material matVoidSurvivorHead = CloneMat(ref newRenderInfos, 1);
            Material matVoidSurvivorMetal = CloneMat(ref newRenderInfos, 2);

            Texture2D texVoidSurvivorFleshDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Blue/texVoidSurvivorFleshDiffuse.png");
            Texture2D texVoidSurvivorFleshEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Blue/texVoidSurvivorFleshEmission.png");
            Texture2D texRampNullifierOffset = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Blue/texRampNullifierOffset.png");

            matVoidSurvivorFlesh.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorFlesh.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorFlesh.SetColor("_EmColor", new Color(1, 2, 2));
            //matVoidSurvivorFlesh.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorHead.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorHead.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorHead.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorMetal.color = new Color(0.65f, 0.7f, 0.7f);
            matVoidSurvivorMetal.SetTexture("_FresnelRamp", texRampNullifierOffset);
            //matVoidSurvivorMetal.SetTexture("_PrintRamp", texRampNullifierOffset);


            newRenderInfos[0].defaultMaterial = matVoidSurvivorFlesh;
            newRenderInfos[1].defaultMaterial = matVoidSurvivorHead;
            newRenderInfos[2].defaultMaterial = matVoidSurvivorMetal;
            newRenderInfos[3].defaultMaterial = matVoidSurvivorFlesh;


        }

        internal static void Default_Imp(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matVoidSurvivorFleshIMP = CloneMat(ref newRenderInfos, 0);
            Material matVoidSurvivorHeadIMP = CloneMat(ref newRenderInfos, 1);
            Material matVoidSurvivorMetalIMP = CloneMat(ref newRenderInfos, 2);

            Texture2D texVoidSurvivorFleshDiffuseIMP = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Red/texVoidSurvivorFleshDiffuseIMP.png");
            Texture2D texVoidSurvivorFleshEmissionIMP = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Red/texVoidSurvivorFleshEmissionIMP.png");
            Texture2D texRampNullifierOffsetIMP = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Red/texRampNullifierOffsetIMP.png");


            matVoidSurvivorFleshIMP.mainTexture = texVoidSurvivorFleshDiffuseIMP;
            matVoidSurvivorFleshIMP.SetTexture("_EmTex", texVoidSurvivorFleshEmissionIMP);
            matVoidSurvivorFleshIMP.SetColor("_EmColor", new Color(2, 1, 1));
            matVoidSurvivorFleshIMP.SetTexture("_FresnelRamp", texRampNullifierOffsetIMP);

            matVoidSurvivorHeadIMP.mainTexture = texVoidSurvivorFleshDiffuseIMP;
            matVoidSurvivorHeadIMP.SetTexture("_EmTex", texVoidSurvivorFleshEmissionIMP);
            matVoidSurvivorHeadIMP.SetTexture("_FresnelRamp", texRampNullifierOffsetIMP);

            matVoidSurvivorMetalIMP.color = new Color(0.4f, 0.0f, 0.0f);
            matVoidSurvivorMetalIMP.SetTexture("_FresnelRamp", texRampNullifierOffsetIMP);
            matVoidSurvivorMetalIMP.SetTexture("_PrintRamp", texRampNullifierOffsetIMP);

            newRenderInfos[0].defaultMaterial = matVoidSurvivorFleshIMP;
            newRenderInfos[1].defaultMaterial = matVoidSurvivorHeadIMP;
            newRenderInfos[2].defaultMaterial = matVoidSurvivorMetalIMP;
            newRenderInfos[3].defaultMaterial = matVoidSurvivorFleshIMP;

        }

        internal static void Default_InvertedShrimpFried(SkinDefMakeOnApply newSkinDef)
        {
            CharacterModel.RendererInfo[] newRenderInfos = newSkinDef.skinDefParams.rendererInfos;

            Material matVoidSurvivorFlesh = CloneMat(ref newRenderInfos, 0);
            Material matVoidSurvivorHead = CloneMat(ref newRenderInfos, 1);
            Material matVoidSurvivorMetal = CloneMat(ref newRenderInfos, 2);

            Texture2D texVoidSurvivorFleshDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Yellow/texVoidSurvivorFleshDiffuseINV.png");
            Texture2D texVoidSurvivorFleshEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Yellow/texVoidSurvivorFleshEmissionINV.png");
            Texture2D texRampNullifierOffset = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Yellow/texRampNullifierOffsetINV.png");

            matVoidSurvivorFlesh.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorFlesh.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorFlesh.SetColor("_EmColor", new Color(2, 2, 1));
            matVoidSurvivorFlesh.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorHead.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorHead.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorHead.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorMetal.color = new Color(0.7f, 0.65f, 0.3f);
            matVoidSurvivorMetal.SetTexture("_FresnelRamp", texRampNullifierOffset);
            matVoidSurvivorMetal.SetTexture("_PrintRamp", texRampNullifierOffset);

            newRenderInfos[0].defaultMaterial = matVoidSurvivorFlesh;
            newRenderInfos[1].defaultMaterial = matVoidSurvivorHead;
            newRenderInfos[2].defaultMaterial = matVoidSurvivorMetal;
            newRenderInfos[3].defaultMaterial = matVoidSurvivorFlesh;

        }

        [RegisterAchievement("CLEAR_ANY_VOIDSURVIVOR", "Skins.VoidSurvivor.Wolfo.First", "CompleteVoidEnding", 3, null)]
        public class ClearSimulacrumVoidSurvivorBody : Achievement_ONE_THINGS
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            }
        }


    }
}