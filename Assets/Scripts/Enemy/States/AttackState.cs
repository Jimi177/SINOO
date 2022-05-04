using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public CombatState combatState;
    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;
    public EnemyAttackAction lastAttack;

    public bool willDoComboOnNextAttack = false;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
    {
        if (enemyManager.isInteracting && enemyManager.canDoCombo == false)
        {
            return this;
        }
        else if(enemyManager.isInteracting && enemyManager.canDoCombo)
        {
            if(willDoComboOnNextAttack && !enemyManager.canBeRiposted)
            {
                enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
                willDoComboOnNextAttack = false;
            }
        }

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.transform.position, enemyManager.currentTarget.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        if (enemyManager.isPerformingAction)
        {
            return combatState;
        }
      

        if(currentAttack != null)
        {
            //if we are too close to the enemy to preform curren attack, get new attack
            if(distanceFromTarget < currentAttack.minimumDistanceToAttack)
            {
                return this;
            }
            //if we are close enough to attack, then let us proceed
            else if(distanceFromTarget < currentAttack.maximumDistanceToAttack)
            {
                //if our enemy is within out attacks viewable angle, we can attack
                if(viewableAngle <= currentAttack.maximumAttackAngle && enemyManager.viewableAngle >= currentAttack.miniumAttackAngle)
                {
                    if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                        enemyAnimator.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimator.animator.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                        enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
                        enemyManager.isPerformingAction = true;

                        RollForComboChance(enemyManager);

                        if(currentAttack.canCombo && willDoComboOnNextAttack)
                        {
                            currentAttack = currentAttack.comboAction;
                            return this;
                        }
                        else
                        {
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatState;
                        }
                    }
                }
            }
        }
        else
        {
            GetNewAttack(enemyManager);
        }

        return combatState;
    }

    private void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.miniumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.miniumAttackAngle)
                {
                    
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
    }

    private void RollForComboChance(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);
        if(enemyManager.allowAIToPerformCombos && comboChance <= enemyManager.comboLikelyHood)
        {
            willDoComboOnNextAttack = true;
        }
    }
}
