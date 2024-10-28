using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public class SkinsVoidFiend
    {
        internal static void Start()
        {           
            VoidSkins();
            VoidSkinsIMP();
            VoidSkinsINV();
        }

        internal static void VoidSkins()
        {
            SkinDef skinVoidSurvivorDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/VoidSurvivor/skinVoidSurvivorDefault.asset").WaitForCompletion();
            
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinVoidSurvivorDefault.rendererInfos.Length+1];
            System.Array.Copy(skinVoidSurvivorDefault.rendererInfos, NewRenderInfos, skinVoidSurvivorDefault.rendererInfos.Length);

            Material matVoidSurvivorFlesh = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[0].defaultMaterial);
            Material matVoidSurvivorHead = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[1].defaultMaterial);
            Material matVoidSurvivorMetal = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[2].defaultMaterial);


            Texture2D texVoidSurvivorFleshDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Blue/texVoidSurvivorFleshDiffuse.png");
            texVoidSurvivorFleshDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texVoidSurvivorFleshEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Blue/texVoidSurvivorFleshEmission.png");
            texVoidSurvivorFleshEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampNullifierOffset = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Blue/texRampNullifierOffset.png");
            texRampNullifierOffset.wrapMode = TextureWrapMode.Clamp;


            matVoidSurvivorFlesh.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorFlesh.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorFlesh.SetColor("_EmColor", new Color(1,2,2));
            //matVoidSurvivorFlesh.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorHead.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorHead.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorHead.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorMetal.color = new Color(0.65f, 0.7f, 0.7f);
            matVoidSurvivorMetal.SetTexture("_FresnelRamp", texRampNullifierOffset);
            //matVoidSurvivorMetal.SetTexture("_PrintRamp", texRampNullifierOffset);


            NewRenderInfos[0].defaultMaterial = matVoidSurvivorFlesh;
            NewRenderInfos[1].defaultMaterial = matVoidSurvivorHead;
            NewRenderInfos[2].defaultMaterial = matVoidSurvivorMetal;
            NewRenderInfos[3] = new CharacterModel.RendererInfo
            {
                renderer = skinVoidSurvivorDefault.rendererInfos[0].renderer.transform.parent.GetChild(3).GetComponent<SkinnedMeshRenderer>(),
                defaultMaterial = matVoidSurvivorFlesh,
            };
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinVoidSurvivorWolfo_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_VOIDSURVIVOR";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Blue/skinIconVoidSurvivor.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinVoidSurvivorDefault };
            newSkinDef.meshReplacements = skinVoidSurvivorDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinVoidSurvivorDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab").WaitForCompletion(), newSkinDef);
        }

        internal static void VoidSkinsIMP()
        {
            SkinDef skinVoidSurvivorDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/VoidSurvivor/skinVoidSurvivorDefault.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfosIMP = new CharacterModel.RendererInfo[skinVoidSurvivorDefault.rendererInfos.Length + 1];
            System.Array.Copy(skinVoidSurvivorDefault.rendererInfos, NewRenderInfosIMP, skinVoidSurvivorDefault.rendererInfos.Length);

            Material matVoidSurvivorFleshIMP = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[0].defaultMaterial);
            Material matVoidSurvivorHeadIMP = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[1].defaultMaterial);
            Material matVoidSurvivorMetalIMP = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[2].defaultMaterial);


            Texture2D texVoidSurvivorFleshDiffuseIMP = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Red/texVoidSurvivorFleshDiffuseIMP.png");
            texVoidSurvivorFleshDiffuseIMP.wrapMode = TextureWrapMode.Clamp;

            Texture2D texVoidSurvivorFleshEmissionIMP = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Red/texVoidSurvivorFleshEmissionIMP.png");
            texVoidSurvivorFleshEmissionIMP.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampNullifierOffsetIMP = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Red/texRampNullifierOffsetIMP.png");
            texRampNullifierOffsetIMP.wrapMode = TextureWrapMode.Clamp;


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

            NewRenderInfosIMP[0].defaultMaterial = matVoidSurvivorFleshIMP;
            NewRenderInfosIMP[1].defaultMaterial = matVoidSurvivorHeadIMP;
            NewRenderInfosIMP[2].defaultMaterial = matVoidSurvivorMetalIMP;
            NewRenderInfosIMP[3] = new CharacterModel.RendererInfo
            {
                renderer = skinVoidSurvivorDefault.rendererInfos[0].renderer.transform.parent.GetChild(3).GetComponent<SkinnedMeshRenderer>(),
                defaultMaterial = matVoidSurvivorFleshIMP,
            };

            SkinDefWolfo newSkinDefIMP = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefIMP.name = "skinVoidSurvivorWolfo2_Simu";
            newSkinDefIMP.nameToken = "SIMU_SKIN_VOIDSURVIVOR2";
            newSkinDefIMP.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Red/skinIconVoidSurvivorIMP.png"));
            newSkinDefIMP.baseSkins = new SkinDef[] { skinVoidSurvivorDefault };
            newSkinDefIMP.meshReplacements = skinVoidSurvivorDefault.meshReplacements;
            newSkinDefIMP.rendererInfos = NewRenderInfosIMP;
            newSkinDefIMP.rootObject = skinVoidSurvivorDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab").WaitForCompletion(), newSkinDefIMP);
        }

        internal static void VoidSkinsINV()
        {
            SkinDef skinVoidSurvivorDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/VoidSurvivor/skinVoidSurvivorDefault.asset").WaitForCompletion();
            
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinVoidSurvivorDefault.rendererInfos.Length + 1];
            System.Array.Copy(skinVoidSurvivorDefault.rendererInfos, NewRenderInfos, skinVoidSurvivorDefault.rendererInfos.Length);

            Material matVoidSurvivorFlesh = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[0].defaultMaterial);
            Material matVoidSurvivorHead = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[1].defaultMaterial);
            Material matVoidSurvivorMetal = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[2].defaultMaterial);

            Texture2D texVoidSurvivorFleshDiffuse = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Yellow/texVoidSurvivorFleshDiffuseINV.png");
            texVoidSurvivorFleshDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texVoidSurvivorFleshEmission = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Yellow/texVoidSurvivorFleshEmissionINV.png");
            texVoidSurvivorFleshEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampNullifierOffset = Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Yellow/texRampNullifierOffsetINV.png");
            texRampNullifierOffset.wrapMode = TextureWrapMode.Clamp;

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

            NewRenderInfos[0].defaultMaterial = matVoidSurvivorFlesh;
            NewRenderInfos[1].defaultMaterial = matVoidSurvivorHead;
            NewRenderInfos[2].defaultMaterial = matVoidSurvivorMetal;
            NewRenderInfos[3] = new CharacterModel.RendererInfo
            {
                renderer = skinVoidSurvivorDefault.rendererInfos[0].renderer.transform.parent.GetChild(3).GetComponent<SkinnedMeshRenderer>(),
                defaultMaterial = matVoidSurvivorFlesh,
            };
            //
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinVoidSurvivorWolfo_Inv_Simu";
            newSkinDef.nameToken = "SIMU_SKIN_VOIDSURVIVOR_INV";
            newSkinDef.icon = WRect.MakeIcon(Assets.Bundle.LoadAsset<Texture2D>("Assets/Skins/dlc1/VoidSurvivor/Yellow/skinIconVoidSurvivorINV.png"));
            newSkinDef.baseSkins = new SkinDef[] { skinVoidSurvivorDefault };
            newSkinDef.meshReplacements = skinVoidSurvivorDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinVoidSurvivorDefault.rootObject;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab").WaitForCompletion(), newSkinDef);
        }

        [RegisterAchievement("CLEAR_ANY_VOIDSURVIVOR", "Skins.VoidSurvivor.Wolfo.First", "CompleteVoidEnding", 5, null)]
        public class ClearSimulacrumVoidSurvivorBody : Achievement_AltBoss_Simu
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            }
        }

 
    }
}