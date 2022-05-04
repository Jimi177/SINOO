using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="A.I/Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyActions
{
    public bool canCombo;
    public bool isSpecialAttack;
    public EnemyAttackAction comboAction;

    public int attackScore = 3;
    public float recoveryTime = 2;

    public float maximumAttackAngle = 35;
    public float miniumAttackAngle = -35;

    public float minimumDistanceToAttack = 0;
    public float maximumDistanceToAttack = 2.5f;
}
