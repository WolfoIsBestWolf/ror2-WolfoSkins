using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace WolfoSkinsMod
{
    public class SkinsNemCommando
    {
        internal static void ModdedSkin(GameObject BodyObject)
        {
            Debug.Log("Nemesis Commando Skins");

            //unlockableDef.hidden = false;
            BodyIndex CharacterIndex = BodyObject.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = BodyObject.GetComponentInChildren<ModelSkinController>();
            SkinDef skinDefault = modelSkinController.skins[0];
        }



    }
}