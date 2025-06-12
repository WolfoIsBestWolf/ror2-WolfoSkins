using RoR2;
using UnityEngine;

namespace WolfoSkinsMod
{
    public class SkinsNemMercenary
    {
        internal static void ModdedSkin(GameObject BodyObject)
        {
            Debug.Log("Nemesis Mercenary Skins");

            BodyIndex CharacterIndex = BodyObject.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = BodyObject.GetComponentInChildren<ModelSkinController>();
            SkinDef skinDefault = modelSkinController.skins[0];
        }



    }
}