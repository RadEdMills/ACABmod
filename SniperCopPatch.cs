using HarmonyLib;
using Reptile;
using UnityEngine;

namespace ACABmod
{
    [HarmonyPatch(typeof(SniperCop))]
    [HarmonyPatch(nameof(SniperCop.UpdateState))]
    public class SniperCopPatch
    {
        static bool Prefix(SniperCop __instance)
        {
            if (__instance.sniperCopState == SniperCop.SniperCopState.HITRESPONSE && __instance.HP <= 0 && 
                (__instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.KNOCKBACK ||
                 __instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.KNOCKDOWN ||
                 __instance.hitBoxResponse.State != EnemyHitResponse.HitResponseState.NONE) &&
                __instance.hitBoxResponse.State == EnemyHitResponse.HitResponseState.LYINGDOWN)
            {
                Debug.Log("Sniper cop dead");
                __instance.SetState(SniperCop.SniperCopState.DIE);
                return false;
            }
            
            return true;
        }
    }
}
