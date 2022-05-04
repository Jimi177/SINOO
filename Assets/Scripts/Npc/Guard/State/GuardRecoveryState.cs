using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRecoveryState : GuardState
{
    public override GuardState Tick(GuardManager guardManager, GuardLocomotion guardLocomotion, GuardAnimator guardAnimator, NpcWeaponSlotManager npcWeaponSlotManager, NpcInventory npcInventory)
    {
        guardAnimator.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
        return this;
    }
}
