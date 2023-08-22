using HarmonyLib;
using Reptile;
using UnityEngine;

namespace ACABmod
{
    [HarmonyPatch(typeof(BasicCop))]
    [HarmonyPatch(nameof(BasicCop.UpdateState))]
    public class BasicCopPatch
    {
        static bool Prefix(BasicCop __instance)
        {
            if (__instance.basicCopState == BasicCop.BasicCopState.HITRESPONSE && __instance.HP <= 0 && 
                (__instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.KNOCKBACK ||
                 __instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.KNOCKDOWN ||
                 __instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.NONE) &&
                __instance.hitBoxResponse.State == EnemyHitResponse.HitResponseState.LYINGDOWN)
            {
                Debug.Log("Cop dead");
                __instance.SetState(BasicCop.BasicCopState.DIE);
                return false;
            }
            
            return true;
        }
    }
}
