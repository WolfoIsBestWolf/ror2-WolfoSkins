using RoR2;
using RoR2.ContentManagement;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public struct SkinInfo
    {
        public SkinDef original;
        public Sprite icon;
        public string nameToken;
        public string name;
        public bool cloneMesh;
        public bool unsetMat;
        public bool w;
        public int extraRenders;
    }

    internal static class H
    {
        public static AsyncReferenceHandleUnloadType unloadType = AsyncReferenceHandleUnloadType.OnSceneUnload;
        public static SkinDefPrioritizeDirect CreateNewSkin(SkinInfo skinInfo)
        {
            if (!skinInfo.original)
            {
                Debug.LogWarning("Forgot to set original SkinDef for " + skinInfo.name);
            }
            SkinDefPrioritizeDirect skin = CloneSkinDefReal(skinInfo.original, skinInfo.w, skinInfo.cloneMesh, skinInfo.extraRenders);
            skin.name = skinInfo.name;
            skin.nameToken = skinInfo.nameToken;
            skin.icon = skinInfo.icon;
            if (skinInfo.unsetMat)
            {
                //UnsetAllMat(skin.skinDefParams.rendererInfos);
            }
            return skin;
        }
        public static SkinDefWolfo CreateNewSkinW(SkinInfo skinInfo)
        {
            skinInfo.w = true;
            return CreateNewSkin(skinInfo) as SkinDefWolfo;
        }
        public static CharacterModel.RendererInfo[] CreateNewSkinR(SkinInfo skinInfo)
        {
            return CreateNewSkin(skinInfo).skinDefParams.rendererInfos;
        }


        private static void AddSkinToCharacter(SkinDef skin)
        {
            ModelSkinController model = skin.rootObject.GetComponent<ModelSkinController>();
            HG.ArrayUtils.ArrayAppend(ref model.skins, skin);
        }

        public static void AddSkinToObject(GameObject body, SkinDef skin)
        {
            ModelSkinController model = body.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            HG.ArrayUtils.ArrayAppend(ref model.skins, skin);
        }

        public static void AddSkinToCharacterPublic(SkinDef skin)
        {
            ModelSkinController model = skin.rootObject.GetComponent<ModelSkinController>();
            HG.ArrayUtils.ArrayAppend(ref model.skins, skin);
        }


        public static void UnsetAllMat(CharacterModel.RendererInfo[] rendererInfos)
        {
            for (int i = 0; i < rendererInfos.Length; i++)
            {
                //rendererInfos[i].defaultMaterialAddress = null;
            }
        }
        public static Material CloneMat(CharacterModel.RendererInfo[] rendererInfos, int num, bool checkDuplicates)
        {
            Material newmat = null;
            var address = rendererInfos[num].defaultMaterialAddress;
            if (address != null)
            {
                newmat = GameObject.Instantiate(AssetAsyncReferenceManager<Material>.LoadAsset(rendererInfos[num].defaultMaterialAddress, unloadType).WaitForCompletion());
                for (int i = 0; i < rendererInfos.Length; i++)
                {
                    if (rendererInfos[i].defaultMaterialAddress == address)
                    {
                        rendererInfos[i].defaultMaterialAddress = null;
                    }
                }
            }
            return null;
        }


        public static Material CloneMat(CharacterModel.RendererInfo[] rendererInfos, int num)
        {
            Material newmat = null;
            var address = rendererInfos[num].defaultMaterialAddress;
            if (address != null && address.RuntimeKeyIsValid())
            {
                newmat = GameObject.Instantiate(AssetAsyncReferenceManager<Material>.LoadAsset(address, unloadType).WaitForCompletion());
            }
            else if (rendererInfos[num].defaultMaterial != null)
            {
                newmat = GameObject.Instantiate(rendererInfos[num].defaultMaterial);
            }
            rendererInfos[num].defaultMaterial = newmat;
            if (newmat == null)
            {
                Debug.LogWarning("Could not find Material " + rendererInfos[num].renderer + "|" + num);
            }
            return newmat;
        }

        public static Material CloneFromOriginal(SkinDefParams skinDefParams, int num)
        {
            Material newmat = null;
            var render = skinDefParams.rendererInfos[num];
            if (render.defaultMaterial != null)
            {
                newmat = GameObject.Instantiate(render.defaultMaterial);
            }
            else if (render.defaultMaterialAddress != null && render.defaultMaterialAddress.RuntimeKeyIsValid())
            {
                newmat = GameObject.Instantiate(AssetAsyncReferenceManager<Material>.LoadAsset(render.defaultMaterialAddress, unloadType).WaitForCompletion());
            }
            return newmat;
        }


        public static Sprite GetIcon(string name)
        {
            return Assets.Bundle.LoadAsset<Sprite>("Assets/Skins/Icons/" + name + ".png");
        }
        /*public static CharacterModel.RendererInfo[] CloneRenderExtra(SkinDef original, int bonus)
        {
            int lenght = original.rendererInfos.Length;
            CharacterModel.RendererInfo[] ClonedInfo = new CharacterModel.RendererInfo[lenght+bonus];
            Array.Copy(original.rendererInfos, ClonedInfo, lenght);
            return ClonedInfo;
        }
        public static CharacterModel.RendererInfo[] CloneRender(SkinDef original)
        {

            int lenght = original.rendererInfos.Length;
            CharacterModel.RendererInfo[] ClonedInfo = new CharacterModel.RendererInfo[lenght];
            Array.Copy(original.rendererInfos, ClonedInfo, lenght);
            return ClonedInfo;
        }
        public static SkinDef.MeshReplacement[] CloneMesh(SkinDef original)
        {
            int lenght = original.meshReplacements.Length;
            SkinDef.MeshReplacement[] ClonedInfo = new SkinDef.MeshReplacement[lenght];
            Array.Copy(original.meshReplacements, ClonedInfo, lenght);
            return ClonedInfo;
        }
        public static SkinDef.MinionSkinReplacement[] CloneMinion(SkinDef original)
        {
            int lenght = original.minionSkinReplacements.Length;
            SkinDef.MinionSkinReplacement[] ClonedInfo = new SkinDef.MinionSkinReplacement[lenght];
            Array.Copy(original.minionSkinReplacements, ClonedInfo, lenght);
            return ClonedInfo;
        }*/

        /* public static CharacterModel.RendererInfo[] CloneRender(SkinDefParams original)
         {
             int lenght = original.rendererInfos.Length;
             CharacterModel.RendererInfo[] ClonedInfo = new CharacterModel.RendererInfo[lenght];
             Array.Copy(original.rendererInfos, ClonedInfo, lenght);
             return ClonedInfo;
         }*/
        public static SkinDefParams.MeshReplacement[] CloneMesh(SkinDefParams original)
        {
            return HG.ArrayUtils.Clone(original.meshReplacements);
        }
        public static SkinDefParams.MinionSkinReplacement[] CloneMinion(SkinDefParams original)
        {
            return HG.ArrayUtils.Clone(original.minionSkinReplacements);
        }



        /*public static SkinDef CloneSkinDef(SkinDef original, bool mesh)
        {
            return CloneSkinDefReal(original, false, mesh, 0);
        }
        public static SkinDefWolfo CloneSkinDefW(SkinDef original, bool mesh)
        {
            return (SkinDefWolfo)CloneSkinDefReal(original, true, mesh, 0);
        }*/

        public static SkinDefPrioritizeDirect CloneSkinDefReal(SkinDef original, bool W, bool newMesh, int extraRenderers)
        {
            SkinDefParams originalParams = null;
            if (original.skinDefParamsAddress.RuntimeKeyIsValid())
            {
                originalParams = AssetAsyncReferenceManager<SkinDefParams>.LoadAsset(original.skinDefParamsAddress, unloadType).WaitForCompletion();
            }
            else if (original.skinDefParams)
            {
                originalParams = original.skinDefParams;
            }
            if (originalParams == null)
            {
                Debug.LogWarning("Cannot find SkinDefParams for " + original);
            }
            SkinDefPrioritizeDirect newSkinDef = null;
            if (W)
            {
                newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            }
            else
            {
                newSkinDef = ScriptableObject.CreateInstance<SkinDefPrioritizeDirect>();
            }
            SkinDefParams newParams = GameObject.Instantiate(originalParams);
            newSkinDef.skinDefParams = newParams;
            newSkinDef.skinDefParamsAddress = new AssetReferenceT<SkinDefParams>(""); //CANNOT BE NULL

            if (newSkinDef.baseSkins != null && newSkinDef.baseSkins.Length > 0)
            {
                newSkinDef.baseSkins = new SkinDef[]
                {
                    newSkinDef.baseSkins[0],
                    original,
                };
            }
            else
            {
                newSkinDef.baseSkins = new SkinDef[] { original };
            }
            newSkinDef.rootObject = original.rootObject;

            H.AddSkinToCharacter(newSkinDef);

            newParams.rendererInfos = HG.ArrayUtils.Clone(originalParams.rendererInfos);
            if (extraRenderers != 0)
            {
                System.Array.Resize(ref newParams.rendererInfos, newParams.rendererInfos.Length + extraRenderers);
            }
            if (newMesh)
            {
                newParams.meshReplacements = HG.ArrayUtils.Clone(originalParams.meshReplacements);
            }
            else
            {
                newParams.meshReplacements = originalParams.meshReplacements;
            }
            newParams.projectileGhostReplacements = originalParams.projectileGhostReplacements;
            newParams.gameObjectActivations = originalParams.gameObjectActivations;
            return newSkinDef;
        }



        public static void DumpInfo()
        {
            for (int surv = 0; surv < SurvivorCatalog.survivorIndexToBodyIndex.Length; surv++)
            {
                SkinDef[] array = SkinCatalog.skinsByBody[(int)SurvivorCatalog.survivorIndexToBodyIndex[surv]];
                Debug.Log(BodyCatalog.GetBodyPrefab(SurvivorCatalog.survivorIndexToBodyIndex[surv]));
                for (int S = 0; S < array.Length; S++)
                {
                    SkinDef skinDef = array[S];
                    Debug.Log(skinDef);

                    List<Material> materials = new List<Material>();
                    string renderInfo = "____________________\n" + skinDef.name;
                    string materialInfo = "____________________\n";
                    for (int r = 0; r < skinDef.rendererInfos.Length; r++)
                    {
                        Material mat = skinDef.rendererInfos[r].defaultMaterial;
                        if (mat == null)
                        {
                            continue;
                        }
                        renderInfo += "\n[" + r + "] " + mat.name + " | " + skinDef.rendererInfos[r].renderer.name;
                        if (!materials.Contains(mat))
                        {

                            materials.Add(mat);
                            if (mat.mainTexture != null)
                            {
                                materialInfo += mat.mainTexture.name + " | " + mat.mainTexture.wrapMode;
                            }
                            materialInfo += "\n";
                        }
                    }

                    Debug.Log(renderInfo);
                    Debug.Log(materialInfo);


                }

            }






        }


        public static SkinDefParams ReturnParams(this SkinDef skinDef)
        {
            if (skinDef.skinDefParams)
            {
                return skinDef.skinDefParams;
            }
            return AssetAsyncReferenceManager<SkinDefParams>.LoadAsset(skinDef.skinDefParamsAddress, unloadType).WaitForCompletion();
        }

    }

}