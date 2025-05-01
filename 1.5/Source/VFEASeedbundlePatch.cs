using HarmonyLib;
using Verse;
using System.Collections.Generic;
using VFEAncients;
using generatedBundles = ProgressionAgriculture.DefGenerator_GenerateImpliedDefs_PreResolve_Patch;

namespace VFEASeedbundlePatch
{	
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("marlemmo.patches.vfeaferny");
            harmony.PatchAll();
        }
		
        [HarmonyPatch(typeof(CompSupplySlingshot), "AddForcedItems")]
        public static class Patch_CompSupplySlingshot_AddForcedItems
        {
            [HarmonyPostfix]
            public static void Postfix(ThingOwner container, ref List<ThingDefStuffCount> __result)
            {
                if (container == null || generatedBundles.generatedBundles == null)
                {
                    Log.Warning("[VFEASeedbundlePatch] Container or generatedBundles is null.");
                    return;
                }

                for (int i = container.Count - 1; i >= 0; i--)
                {
                    var item = container[i];
                    if (item?.def != null && generatedBundles.generatedBundles.Contains(item.def))
                    {
                        container.Remove(item);
                        Log.Message($"[VFEASeedbundlePatch] Yeeting {item.def.defName} into the ether.");
                    }
                }
			}
        }
    }
}