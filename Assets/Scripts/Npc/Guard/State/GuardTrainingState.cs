using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTrainingState : GuardState
{
    public GuardAttackState guardAttackState;
    public GuardRecoveryState guardRecoveryState;

    public override GuardState Tick(GuardManager guardManager, GuardLocomotion guardLocomotion, GuardAnimator guardAnimator, NpcWeaponSlotManager npcWeaponSlotManager, NpcInventory npcInventory)
    {
        Debug.Log("Training State Active");
        if (guardManager.isArmed == false)
        {
            npcWeaponSlotManager.HideOrWithdrawWeapon(npcInventory.rightWeapon, false);
            return guardAttackState;
        }
        else if (guardManager.isArmed && guardManager.isPerformingTraining == false)
        {
            guardManager.isTrainingFinish = false;
            npcWeaponSlotManager.HideOrWithdrawWeapon(npcInventory.rightWeapon, false);
            return guardRecoveryState;
        }
        else
        {
            return this;
        }
    }
}

