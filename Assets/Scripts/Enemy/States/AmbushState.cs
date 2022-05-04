using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushState : State
{

    public bool isInAmbush;
    public float ambushDetectionRadius;
    public string stateAnimation;
    public string changeStateAnimation;
    public float minimumDetectionAngle = -35;
    public float maximumDetectionAngle = 35;
    public LayerMask detectionLayer;

    public PursueState pursueState;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
    {
        if(isInAmbush && enemyManager.isPerformingAction == false)
        {
            enemyAnimator.PlayTargetAnimation(stateAnimation, true);
        }

        #region Handle Detection

        Collider[] collider = Physics.OverlapSphere(transform.position, ambushDetectionRadius, detectionLayer);

        for (int i = 0; i < collider.Length; i++)
        {
            CharacterStats characterStats = collider[i].transform.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                //Check for team ID

                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

                if (characterStats.transform.root != transform.root)
                {
                    if (viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                        isInAmbush = false;
                        enemyAnimator.PlayTargetAnimation(changeStateAnimation, true);
                    }
                }
            }
        }
        #endregion

        #region Handle Switch State
        if(enemyManager.currentTarget != null)
        {
            return pursueState;
        }
        else
        {
            return this;
        }
        #endregion
    }
}
