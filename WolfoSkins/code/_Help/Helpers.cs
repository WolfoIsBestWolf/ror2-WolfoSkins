using RoR2;
using RoR2.ContentManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WolfoSkinsMod
{
    public struct SkinInfo
    {
        public SkinDef original;
        public SkinDef[] minionSkins;
        public Sprite icon;
        public string nameToken;
        public string name;
        public bool enhancedSkin;
        public bool cloneMesh;

        public int extraRenders;
        //public Action method;
    }

    internal static class H
    {
        public static AsyncReferenceHandleUnloadType unloadType = AsyncReferenceHandleUnloadType.OnSceneUnload;

        public static SkinDefMakeOnApply CreateEmptySkinForLaterCreation(SkinInfo skinInfo, Action<SkinDefMakeOnApply> method)
        {
            SkinDefMakeOnApply newSkinDef = null;
            if (skinInfo.enhancedSkin)
            {
                // Debug.LogWarning("NOT SUPPORTED YET");
                newSkinDef = ScriptableObject.CreateInstance<SkinDefEnhanced>();
            }
            else
            {
                newSkinDef = ScriptableObject.CreateInstance<SkinDefMakeOnApply>();
            }
            newSkinDef.name = skinInfo.name;
            newSkinDef.nameToken = skinInfo.nameToken;
            newSkinDef.icon = skinInfo.icon;
            newSkinDef.cloneMesh = skinInfo.cloneMesh;
            newSkinDef.extraRenders = skinInfo.extraRenders;
            newSkinDef.creationMethod = method;
            newSkinDef.baseSkin = skinInfo.original;
            newSkinDef.minionSkins = skinInfo.minionSkins;
            newSkinDef.rootObject = skinInfo.original.rootObject;
            newSkinDef.skinDefParams = ScriptableObject.CreateInstance<SkinDefParams>();
            AddSkinToCharacter(newSkinDef);
            return newSkinDef;
        }

        public static SkinDef CreateNewSkin(SkinInfo skinInfo)
        {
            if (!skinInfo.original)
            {
                Debug.LogWarning("Forgot to set original SkinDef for " + skinInfo.name);
            }
            SkinDef newSkinDef = null;
            if (skinInfo.enhancedSkin)
            {
                //newSkinDef = ScriptableObject.CreateInstance<SkinDefAltColor>();
                newSkinDef = ScriptableObject.CreateInstance<SkinDefEnhanced>();
            }
            else
            {
                newSkinDef = ScriptableObject.CreateInstance<SkinDefMakeOnApply>();
            }

            CloneSkinDefReal(newSkinDef, skinInfo.original, skinInfo.cloneMesh, skinInfo.extraRenders);
            AddSkinToCharacter(newSkinDef);
            newSkinDef.name = skinInfo.name;
            newSkinDef.nameToken = skinInfo.nameToken;
            newSkinDef.icon = skinInfo.icon;
            return newSkinDef;
        }
        public static SkinDefEnhanced CreateNewSkinW(SkinInfo skinInfo)
        {
            skinInfo.enhancedSkin = true;
            return CreateNewSkin(skinInfo) as SkinDefEnhanced;
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



        public static Material CloneMat(ref CharacterModel.RendererInfo[] rendererInfos, int num, bool unset = false)
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
            if (unset)
            {
                rendererInfos[num].defaultMaterialAddress = null;
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

        public static SkinDef CloneSkinDefReal(SkinDef newSkinDef, SkinDef original, bool newMesh, int extraRenderers)
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
                SkinCatalog.ValidateParams(original);
                originalParams = original.skinDefParams;
            }
            if (originalParams == null)
            {
                Debug.LogWarning("Cannot find SkinDefParams for " + original);
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
                    var skinDefParams = skinDef.ReturnParams();
                    for (int r = 0; r < skinDefParams.rendererInfos.Length; r++)
                    {
                        Material mat = skinDefParams.rendererInfos[r].ReturnMaterial();
                        if (mat == null)
                        {
                            continue;
                        }
                        renderInfo += "\n[" + r + "] " + mat.name + " | " + skinDefParams.rendererInfos[r].renderer.name;
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
            SkinCatalog.ValidateParams(skinDef);
            if (skinDef.skinDefParams)
            {
                return skinDef.skinDefParams;
            }
            return AssetAsyncReferenceManager<SkinDefParams>.LoadAsset(skinDef.skinDefParamsAddress, unloadType).WaitForCompletion();
        }
        public static Material ReturnMaterial(this CharacterModel.RendererInfo render)
        {
            if (render.defaultMaterial)
            {
                return render.defaultMaterial;
            }
            return AssetAsyncReferenceManager<Material>.LoadAsset(render.defaultMaterialAddress, unloadType).WaitForCompletion();
        }
    }

}