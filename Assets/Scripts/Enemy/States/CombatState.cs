using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : State
{
    public AttackState attackState;
    public PursueState pursueState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
    {
        if (enemyManager.isInteracting)
        {
            return this;
        }

        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        if(enemyManager.currentRecoveryTime <= 0 && distanceFromTarget <= enemyManager.stoppingDistance)
        {
            return attackState;
        }
        else if(distanceFromTarget > enemyManager.stoppingDistance)
        {
            return pursueState;
        }
        else
        {
            return this;
        }
    }
}
