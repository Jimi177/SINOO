using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : AnimatorManager
{
    EnemyManager enemyManager;
    EnemyStats enemyStats;
    EnemySpecialAttack enemySpecialAttack;

    InputManager inputManager;
    CameraManager cameraManager;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();

        enemyManager = GetComponentInParent<EnemyManager>();
        enemyStats = GetComponentInParent<EnemyStats>();
        enemySpecialAttack = GetComponentInParent<EnemySpecialAttack>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyManager.enemyRB.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRB.velocity = velocity;
    }

    public override void TakeCriticalDamageAnimationEvent()
    {
        enemyStats.TakeDamageNoAnimation(enemyManager.pendingCriticalDamage);
        enemyManager.pendingCriticalDamage = 0;
    }

    public void EnableRotate()
    {
        animator.SetBool("canRotate", true);
    }

    public void DisableRotate()
    {
        animator.SetBool("canRotate", false);
    }

    public void HandleDeath()
    {
        if (enemyManager.isDead)
        {
            enemyManager.enemyRB.useGravity = false;
            enemyManager.enemyCollider.enabled = false;
            enemyManager.lockOnTransform.gameObject.SetActive(false);
            inputManager.lockOnActive = false;
            cameraManager.ClearLockOn();
        }
    }

    public void EnableIsParrying()
    {
        enemyManager.isParrying = true;
    }

    public void DisableIsParrying()
    {
        enemyManager.isParrying = false;
    }

    public void EnableCanBeRiposted()
    {
        enemyManager.canBeRiposted = true;
    }

    public void DisableCanBeRiposted()
    {
        enemyManager.canBeRiposted = false;
    }

    public void EnableBersekerCritical()
    {
        enemyManager.canBeBerserkerCritical = true;
    }

    public void DisableBerserkerCritical()
    {
        enemyManager.canBeBerserkerCritical = false;
    }


    #region Golem Specials
    public void GolemSpecialAttack()
    {
        enemySpecialAttack.GolemSpecialAttack();
    }
    #endregion

    #region Goblin Specials

    public void GoblinThrow()
    {
        enemySpecialAttack.Thrower();
    }

    public void Berserker()
    {
        enemySpecialAttack.Berserker();
    }

    public void HealSpell()
    {
        enemySpecialAttack.HealSpell();
    }

    #endregion

}
