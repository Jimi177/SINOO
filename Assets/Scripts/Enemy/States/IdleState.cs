using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public int randomValue;
    public int maxScore;
    public int temporaryScore;

    [Header("Idle Animations")]
    public string idleAnimation;
    public string stopIdleAnimation;
    public EnemyIdleAnimtions[] enemyIdleAnimations;
    public EnemyIdleAnimtions currentIdleAnimation;

    [Header("Idle Options")]
    public bool isInIdleState;
    public bool isStaticIdle;
    public bool isGrounded;

    [Header("Detection")]
    public float detectionRadius;
    public float minimumDetectionAngle = -35;
    public float maximumDetectionAngle = 35;
    public PursueState pursueState;
    public LayerMask detectionLayer;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
    {
        if (isInIdleState)
        {
            if (isStaticIdle && enemyManager.isInteracting == false)
            {
                enemyAnimator.PlayTargetAnimation(idleAnimation, true);
            }
            else
            {
                if (currentIdleAnimation != null)
                {
                    if(enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                        enemyAnimator.PlayTargetAnimation(currentIdleAnimation.actionAnimation, true);
                        enemyManager.isPerformingAction = true;
                        enemyManager.currentRecoveryTime = currentIdleAnimation.recoveryTime;
                        currentIdleAnimation = null;
                        return this;
                    }
                }
                else
                {
                    GetNewAnimation();
                }
            }
        }

        #region Handle Detecion
        if (isGrounded)
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < collider.Length; i++)
            {
                CharacterStats characterStats = collider[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    if (characterStats.transform.root != transform.root)
                    {
                        if (characterStats.gameObject.tag != "Enemy")
                        {
                            enemyManager.currentTarget = characterStats;
                            isInIdleState = false;
                            enemyAnimator.animator.SetBool("hasActiveTarget", true);
                            enemyAnimator.PlayTargetAnimation(stopIdleAnimation, true);
                        }
                    }
                }
            }
        }
        else if(enemyManager.fightModeActive)
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, 50, detectionLayer);

            for (int i = 0; i < collider.Length; i++)
            {
                CharacterStats characterStats = collider[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    if (characterStats.transform.root != transform.root)
                    {
                        if (characterStats.gameObject.tag != "Enemy")
                        {
                            enemyManager.currentTarget = characterStats;
                            isInIdleState = false;
                            enemyAnimator.animator.SetBool("hasActiveTarget", true);
                            enemyAnimator.PlayTargetAnimation(stopIdleAnimation, true);
                        }
                    }
                }
            }
        }
        else
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < collider.Length; i++)
            {
                CharacterStats characterStats = collider[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (characterStats.transform.root != transform.root)
                    {
                        if (viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                        {
                            if (characterStats.gameObject.tag != "Enemy")
                            {
                                enemyManager.currentTarget = characterStats;
                                isInIdleState = false;
                                enemyAnimator.animator.SetBool("hasActiveTarget", true);
                                enemyAnimator.PlayTargetAnimation(stopIdleAnimation, true);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Handle Switch State
        if (enemyManager.currentTarget != null)
        {
            return pursueState;
        }
        else
        {
            return this;
        }
        #endregion
    }

    private void GetNewAnimation()
    {
        Debug.Log("nowa animacja");

        maxScore = 0;

        for (int i = 0; i < enemyIdleAnimations.Length; i++)
        {
            EnemyIdleAnimtions idleAnimation = enemyIdleAnimations[i];
            maxScore += idleAnimation.animationScore;
        }

        randomValue = Random.Range(0, maxScore);
        temporaryScore = 0;

        for (int i = 0; i < enemyIdleAnimations.Length; i++)
        {
            EnemyIdleAnimtions idleAnimation = enemyIdleAnimations[i];

            if (currentIdleAnimation != null)
            {
                return;
            }

            temporaryScore += idleAnimation.animationScore;
            if(temporaryScore >= randomValue)
            {
                currentIdleAnimation = idleAnimation;
            }
        }
    }
}
