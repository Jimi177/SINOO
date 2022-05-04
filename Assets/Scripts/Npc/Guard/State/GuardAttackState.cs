using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttackState : GuardState
{
    public GuardTrainingState guardTrainingState;
    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;
    public float trainingTime;



    public override GuardState Tick(GuardManager guardManager, GuardLocomotion guardLocomotion, GuardAnimator guardAnimator, NpcWeaponSlotManager npcWeaponSlotManager, NpcInventory npcInventory)
    {

        if (guardManager.isPerformingTraining)
        {
            if (currentAttack != null)
            {
                if(guardManager.isPerformingAction == false)
                {
                    guardAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
                    guardAnimator.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                    guardAnimator.animator.SetBool("isPerformingAction", true);
                    currentAttack = null;
                }
            }
            else
            {
                GetNewAttack();
            }

            return this;
        }
        else if (guardManager.isTrainingFinish)
        {
            return guardTrainingState;
        }


        RandomTrainingTime(guardManager);
        return this;
    }

    public void RandomTrainingTime(GuardManager guardManager)
    {
        trainingTime = Random.Range(15, 20);

        guardManager.currentTrainingTime = trainingTime;
        guardManager.isPerformingTraining = true;
    }

    private void GetNewAttack()
    {
        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];
            maxScore += enemyAttackAction.attackScore;
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (currentAttack != null)
            {
                return;
            }

            temporaryScore += enemyAttackAction.attackScore;

            if (temporaryScore >= randomValue)
            {
                currentAttack = enemyAttackAction;
            }
        }
    }
}
