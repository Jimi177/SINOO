using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private Collider enemyCollider;

    public UIEnemyHealthBar enemyHealthBar;


    public Animator animator;
    public EnemyManager enemyManager;
    public EnemySpecialAttack enemySpecialAttack;
    public CharacterStats whoAttacked;
    public bool isBerserker;
    public bool isBerserkerActive;
    public int berserkerLevel = 2;
    public int berserkerHealthBuff = 50;

    public bool isHealer;
    public int maxHeals;
    public int numbersOfHeals;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemySpecialAttack = GetComponent<EnemySpecialAttack>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthBasedOnLevel();
        currentHealth = maxHealth;
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    private void LateUpdate()
    {
        BerserkerCheck();
        HealerCheck();
    }

    private int SetMaxHealthBasedOnLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamageNoAnimation(int damage)
    {
        if (enemyManager.isInvurnelable)
        {
            return;
        }

        enemyManager.fightModeActive = true;

        currentHealth -= damage;

        enemyHealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetBool("isDead", true);
        }
    }

    public void TakeDamage(int damage)
    {
        if(enemyManager.isInvurnelable)
        {
            return;
        }

        enemyManager.fightModeActive = true;

        currentHealth -= damage;

        enemyHealthBar.SetHealth(currentHealth);

        animator.Play("TakeDamage");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Death");

            if(isBerserker)
            {
                enemySpecialAttack.berserkerFX.gameObject.SetActive(false);
            }
        }
    }

    public void AddHealth(int heal)
    {
        currentHealth += heal;
    }

    public void BerserkerCheck()
    {
        if(isBerserker && !isBerserkerActive)
        {
            if(currentHealth <= maxHealth / berserkerLevel)
            {
                isBerserkerActive = true;
                enemySpecialAttack.Berserker();
                AddHealth(berserkerHealthBuff);
            }
        }
    }

    public void HealerCheck()
    {
        if(isHealer && numbersOfHeals <= maxHeals)
        {
            if(currentHealth <= maxHealth / 2)
            {
                numbersOfHeals += 1;
                enemySpecialAttack.HealSpell();
            }
        }
    }
}
