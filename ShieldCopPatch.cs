using HarmonyLib;
using Reptile;
using UnityEngine;

namespace ACABmod
{
    [HarmonyPatch(typeof(ShieldCop))]
    [HarmonyPatch(nameof(ShieldCop.UpdateState))]
    public class ShieldCopPatch
    {
        static bool Prefix(ShieldCop __instance)
        {
            if (__instance.shieldCopState == ShieldCop.ShieldCopState.HITRESPONSE && __instance.HP <= 0 && 
                (__instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.KNOCKBACK ||
                 __instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.KNOCKDOWN ||
                 __instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.NONE) &&
                __instance.hitBoxResponse.State == EnemyHitResponse.HitResponseState.LYINGDOWN)
            {
                Debug.Log("Shield cop dead");
                __instance.SetState(ShieldCop.ShieldCopState.DIE);
                return false;
            }
            
            return true;
        }
    }
}
