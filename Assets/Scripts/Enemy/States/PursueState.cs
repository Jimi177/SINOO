using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueState : State
{
    public float movementSpeed;
    public float berserkerSpeed = 2;
    public CombatState combatState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
    {
        if (enemyManager.isInteracting)
        {
            return this;
        }

        if (enemyManager.isPerformingAction)
        {
            enemyAnimator.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        if (distanceFromTarget > enemyManager.stoppingDistance && !enemyStats.isBerserkerActive)
        {
            enemyAnimator.animator.SetFloat("Vertical", movementSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            enemyAnimator.animator.SetFloat("Vertical", berserkerSpeed, 0.1f, Time.deltaTime);
        }

        if (enemyManager.canRotate)
        {
            HandleRotationTowardsTarget(enemyManager);
        }
        

       

        if(distanceFromTarget  <= enemyManager.stoppingDistance)
        {
            return combatState;
        }
        else
        {
            return this;
        }
    }

    private void HandleRotationTowardsTarget(EnemyManager enemyManager)
    {
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRB.velocity;

            enemyManager.navmeshAgent.enabled = true;
            enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRB.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }
}
