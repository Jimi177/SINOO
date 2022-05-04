using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public CharacterManager characterManager;
    public Collider throwCollider;
    public Collider specialCollider;
    public Collider damageCollider;
    public Transform specialEffect;

    public Animator animator;
    public int currentWeaponDamage;
    public bool isSpecialAttack;
    public bool isThrowAttack;
    public bool isPerformingAttack;
    public bool isPerformingSpecialAttack;

    [Header("Special Buffs")]
    public bool isFireBonusActive = false;
    public int fireDamage;



    private void Awake()
    {
        LoadColliders();
    }

    private void Update()
    {
        if(!isThrowAttack)
        {
            UpdateAnimatorBool();
            UpdateColliders();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("jestem");

            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            CharacterManager playerCharacterManager = collision.GetComponent<CharacterManager>();
            BlockCollider shield = collision.transform.GetComponentInChildren<BlockCollider>();

            if (playerCharacterManager != null)
            {
                if (playerCharacterManager.isParrying)
                {
                    Debug.Log("Parry!");
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                    return;
                }
                else if (playerCharacterManager.isKicking)
                {
                    Debug.Log("Kick!");
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Kicked", true);
                    return;
                }
                else if (shield != null && playerCharacterManager.isBlocking)
                {

                    float physicalDamageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                    if (playerStats != null)
                    {
                        playerStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block_Damage");
                        return;
                    }
                }
            }



            if (playerStats != null)
            {
                playerStats.TakeDamage(currentWeaponDamage);
            }
        }

        if (collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();

            if (enemyCharacterManager != null)
            {
                if (enemyCharacterManager.isParrying)
                {
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                    return;
                }
            }

            if (enemyStats != null)
            {
                if(isFireBonusActive)
                {
                    enemyStats.TakeDamage(currentWeaponDamage + fireDamage);
                }
                else
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
            }
        }
    }

    private void LoadColliders()
    {
        if (isSpecialAttack)
        {
            specialCollider = GetComponent<BoxCollider>();
        }
        else if (isThrowAttack)
        {
            throwCollider = GetComponent<MeshCollider>();
        }
        else
        {
            damageCollider = GetComponent<BoxCollider>();
        }
    }

    private void UpdateAnimatorBool()
    {
        if(isSpecialAttack)
        {
            //animator = GetComponentInChildren<Animator>();
            isPerformingSpecialAttack = animator.GetBool("isPerformingSpecialAttack");
        }
        else
        {
            animator = GetComponentInParent<Animator>();
            isPerformingAttack = animator.GetBool("isPerformingAttack");
        }
        
    }

    public void UpdateColliders()
    {
        if(isSpecialAttack)
        {
            EnableOrDisableSpecialDamageCollider();
        }
        else
        {
            EnableOrDisableDamageCollider();
        }
    }

    public void EnableOrDisableDamageCollider()
    {
        if (damageCollider != null)
        {
            if (isPerformingAttack)
            {
                damageCollider.enabled = true;
            }
            else
            {
                damageCollider.enabled = false;
            }
        }
        
    }

    public void EnableOrDisableSpecialDamageCollider()
    {
        if (specialCollider != null)
        {
            if (isPerformingSpecialAttack)
            {
                specialCollider.enabled = true;
            }
            else
            {
                specialCollider.enabled = false;
            }
        }
    }
}
