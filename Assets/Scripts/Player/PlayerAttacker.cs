using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerInventory playerInventory;
    PlayerStats playerStats;
    WeaponSlotManager weaponSlotManager;
    DamageCollider damageCollider;
    InputManager inputManager;
    public string lastAttack;

    public float buffTimer;

    LayerMask backStabLayer = 1 << 12;
    LayerMask riposteLayer = 1 << 13;

    [Header("Body Transforms")]
    public Transform onCharacter;
    public Transform underCharacter;
    public Transform rightWeapon;
    public GameObject leftEye;
    public GameObject rightEye;

    [Header("Berserker")]
    public GameObject berserkerFX;

    [Header("Templar")]
    public bool isFireBonusDamageActive;
    public GameObject fireWeaponFX;
    public int bonusFireDamage = 15;

    [Header("Knight")]
    public GameObject holyAttackFX;
    public float holyDamageRadius;
    public int holyDamage;
    public LayerMask enemyLayer;
    

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerInventory = GetComponentInChildren<PlayerInventory>();
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        playerStats = GetComponent<PlayerStats>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        damageCollider = GetComponentInChildren<DamageCollider>();

        HandleFireSwordDamage();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (inputManager.canDoComboActive)
        {
            playerAnimatorManager.animator.SetBool("canDoCombo", false);

            if (lastAttack == weapon.Light_Attack_01)
            {
                if (weapon.baseStamina * weapon.lightMultiplier > playerStats.currentStamina)
                {
                    return;
                }
                playerAnimatorManager.PlayTargetAnimation(weapon.Light_Attack_02, true);
                lastAttack = weapon.Light_Attack_02;
            }
            else if (lastAttack == weapon.Light_Attack_02)
            {
                if (weapon.baseStamina * weapon.lightMultiplier > playerStats.currentStamina)
                {
                    return;
                }
                playerAnimatorManager.PlayTargetAnimation(weapon.Light_Attack_03, true);
                lastAttack = weapon.Light_Attack_03;
            }
            else if (lastAttack == weapon.Heavy_Attack_01)
            {
                if (weapon.baseStamina * weapon.heavyMultiplier > playerStats.currentStamina)
                {
                    return;
                }
                playerAnimatorManager.PlayTargetAnimation(weapon.Heavy_Attack_02, true);
                lastAttack = weapon.Heavy_Attack_02;
            }
        }
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        if (weapon.baseStamina * weapon.lightMultiplier > playerStats.currentStamina)
        {
            return;
        }
        damageCollider.currentWeaponDamage = weapon.baseDamage;
        damageCollider.currentWeaponDamage = weapon.lightAttackMultiplier * weapon.baseDamage;
        weaponSlotManager.attackingWeapon = weapon;
        playerAnimatorManager.PlayTargetAnimation(weapon.Light_Attack_01, true);
        lastAttack = weapon.Light_Attack_01;
    }

    public void HandleHeavytAttack(WeaponItem weapon)
    {
        if (weapon.baseStamina * weapon.heavyMultiplier > playerStats.currentStamina)
        {
            return;
        }

        damageCollider.currentWeaponDamage = weapon.baseDamage;
        damageCollider.currentWeaponDamage = weapon.heavyAttackMultiplier * weapon.baseDamage;
        weaponSlotManager.attackingWeapon = weapon;
        playerAnimatorManager.PlayTargetAnimation(weapon.Heavy_Attack_01, true);
        lastAttack = weapon.Heavy_Attack_01;
    }

    public void HandleSpecial()
    {
        if (playerInventory.leftWeapon.isShield)
        {
            HandleWeaponParrySystem();
        }

        if (playerInventory.leftWeapon.isAxe)
        {
            HandleKick();
        }

        //Potencjalnie miejsce na inne bronie (brak animacji)
    }

    public void HandleBlock()
    {
        HandleWeaponBlockSystem();
    }

    public void AttemptBackStabOrRipost()
    {
        RaycastHit hit;

        if (Physics.Raycast(inputManager.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = weaponSlotManager.rightWeaponDamageCollider;

            if (enemyCharacterManager != null)
            {
                if (enemyCharacterManager.isPossibleToBackstab)
                {
                    playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalStandingPoint.position;
                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    playerAnimatorManager.PlayTargetAnimation("Back Stab", true);
                    enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
                }
            }
        }
        else if (Physics.Raycast(inputManager.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = weaponSlotManager.rightWeaponDamageCollider;

            if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
            {
                playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalStandingPoint.position;
                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayTargetAnimation("Riposte_Stab", true);
                enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Riposte_Stabbed", true);
            }
        }
        else if (Physics.Raycast(inputManager.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 2f, riposteLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = weaponSlotManager.rightWeaponDamageCollider;

            if (enemyCharacterManager != null && enemyCharacterManager.canBeBerserkerCritical)
            {
                playerManager.transform.position = enemyCharacterManager.riposteCollider.berserkerCriticalStandingPoint.position;
                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayTargetAnimation("Berserker Critical", true);
                enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Kickd_Death", true);
                enemyCharacterManager.GetComponentInChildren<EnemyAnimator>().animator.SetBool("isDead", true);
            }
        }
    }

    private void HandleWeaponParrySystem()
    {
            if (playerManager.isInteracting)
            {
                return;
            }

            playerAnimatorManager.PlayTargetAnimation(playerInventory.leftWeapon.Parry_01, true);
    }

    private void HandleKick()
        {
            if (playerManager.isInteracting)
            {
                return;
            }

            playerAnimatorManager.PlayTargetAnimation(playerInventory.leftWeapon.Special_Attack_01, true);
        }

    private void HandleWeaponBlockSystem()
        {
            if (playerManager.isInteracting)
            {
                return;
            }

            if (playerManager.isBlocking)
            {
                return;
            }

            playerAnimatorManager.PlayTargetAnimation(playerInventory.leftWeapon.Block_01, true);
            playerManager.isBlocking = true;
        }

    public void HandleBerserker(float timer)
    {
        playerAnimatorManager.PlayTargetAnimation("Berserker Player", true);
        //leftEye.SetActive(true);
        //rightEye.SetActive(true);

        buffTimer = timer;
        Instantiate(berserkerFX, onCharacter);
    }

    public void HandleFireSword(float timer)
    {
        playerAnimatorManager.PlayTargetAnimation("Templar Power Up", true);
        leftEye.SetActive(true);
        rightEye.SetActive(true);

        buffTimer = timer;
        Instantiate(fireWeaponFX, damageCollider.specialEffect);
        damageCollider.fireDamage = bonusFireDamage;
    }

    public void HandleHolyAttack()
    {
        playerAnimatorManager.PlayTargetAnimation("Knight Holy Attack", true);
        Instantiate(holyAttackFX, onCharacter);

        Collider[] collider = Physics.OverlapSphere(transform.position, holyDamageRadius, enemyLayer);

        for (int i = 0; i < collider.Length; i++)
        {
            EnemyStats enemyStats = collider[i].GetComponent<EnemyStats>();
            EnemyManager enemyManager = collider[i].GetComponent<EnemyManager>();

            if (enemyStats != null)
            {
                if (!enemyManager.isDead)
                {
                    enemyStats.TakeDamage(holyDamage);
                }
            }
        }
    }

    public void HandleFireSwordDamage()
    {
        if(isFireBonusDamageActive)
        {
            damageCollider.isFireBonusActive = true;
        }
        else
        {
            damageCollider.isFireBonusActive = false;
        }
    }


   
}

