using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardIdleState : GuardState
{
    public float detectionRadius;
    public LayerMask detectionLayer;

    public GuardMoveState guardMoveState;

    public override GuardState Tick(GuardManager guardManager, GuardLocomotion guardLocomotion, GuardAnimator guardAnimator, NpcWeaponSlotManager npcWeaponSlotManager, NpcInventory npcInventory)
    {
        #region Handle Detection
        Collider[] collider = Physics.OverlapSphere(guardManager.transform.position, detectionRadius, detectionLayer);

        for (int i = 0; i < collider.Length; i++)
        {
            DollManager dollManager = collider[i].transform.GetComponent<DollManager>();

            if (dollManager != null)
            {
                if(dollManager.isFreeForInteraction)
                {
                    guardManager.currentDollTarget = dollManager;
                }
            }
        }
        #endregion

        #region Handle Switch State
        if (guardManager.currentDollTarget != null)
        {
            return guardMoveState;
        }
        else
        {
            return this;
        }
        #endregion
    }
}
