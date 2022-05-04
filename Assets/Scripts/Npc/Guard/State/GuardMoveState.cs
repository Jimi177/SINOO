using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMoveState : GuardState
{
    public GuardTrainingState guardTrainingState;

    public override GuardState Tick(GuardManager guardManager, GuardLocomotion guardLocomotion, GuardAnimator guardAnimator, NpcWeaponSlotManager npcWeaponSlotManager, NpcInventory npcInventory)
    {
        Debug.Log("Move State");
        Vector3 targetDirection = guardManager.currentDollTarget.transform.position - guardManager.transform.position;
        float distanceFromTarget = Vector3.Distance(guardManager.currentDollTarget.transform.position, guardManager.transform.position);        

        if (distanceFromTarget > guardManager.stoppingDistance)
        {
            guardAnimator.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }

        HandleRotationTowardsTarget(guardManager);
         
        guardManager.guardNavmeshAnget.transform.localPosition = Vector3.zero;
        guardManager.guardNavmeshAnget.transform.localRotation = Quaternion.identity;

        if (distanceFromTarget <= guardManager.stoppingDistance)
        {
            return guardTrainingState;
        }
        else
        {
            return this;
        }
    }

    private void HandleRotationTowardsTarget(GuardManager guardManager)
    {
        if (guardManager.isPerformingTraining)
        {
            Vector3 direction = guardManager.currentDollTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, guardManager.rotationSpeed / Time.deltaTime);
        }
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(guardManager.guardNavmeshAnget.desiredVelocity);
            Vector3 targetVelocity = guardManager.guardRB.velocity;

            guardManager.guardNavmeshAnget.enabled = true;
            guardManager.guardNavmeshAnget.SetDestination(guardManager.currentDollTarget.transform.position);
            guardManager.guardRB.velocity = targetVelocity;
            guardManager.transform.rotation = Quaternion.Slerp(guardManager.transform.rotation, guardManager.guardNavmeshAnget.transform.rotation, guardManager.rotationSpeed / Time.deltaTime);
        }
    }
}
