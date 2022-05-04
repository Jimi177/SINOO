using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public PlayerAnimatorManager playerAnimatorManager;
    public PlayerManager playerManager;
    public PlayerAttacker playerAttacker;
    public PlayerLocomotion playerLocomotion;

    [Header("Berserker")]
    public bool isBerserker;
    public bool isBerserkerActive;
    public float berserkerTimer;
    public float invokeBerserker;

    [Header("Templar")]
    public bool isTemplar;
    public bool isFireSwordActive;
    public float fireSwordTimer;
    public float invokeFireSword;

    [Header("Knight")]
    public bool isKnight;
    public bool isHolyAttackReady = true;
    public float invokeHolyAttack;


    public int staminaLevel;
    public float maxStamina;
    public float currentStamina;

    public float staminaRegenerationAmount = 2;
    public float staminaBerserkerRegenerationAmount = 6;
    public float staminaRegenerationTimer = 0;

    private void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthBasedOnLevel();
        currentHealth = maxHealth;

        SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(maxHealth);
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetCurrentStamina(currentStamina);
    }

    private void Update()
    {
        HandlePlayerSpecialEffectTimers();
    }

    private void LateUpdate()
    {

    }

    private int SetMaxHealthBasedOnLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private float SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public void TakeDamage(int damage, string damageAnimation = "TakeDamage")
    {
        if (playerManager.isInvurnelable)
        {
            return;
        }

        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);
        playerAnimatorManager.PlayTargetAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            playerAnimatorManager.PlayTargetAnimation("Death", true);
        }
    }

    public void TakeStaminaDamage(int damage)
    {
        currentStamina -= damage;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        if (playerManager.isInteracting)
        {
            staminaRegenerationTimer = 0;
        }
        else
        {
            staminaRegenerationTimer += Time.deltaTime;

            if (currentStamina < maxStamina && staminaRegenerationTimer > 1f)
            {
                if (isBerserkerActive)
                {
                    currentStamina += staminaBerserkerRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
                else
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }

            }
        }
    }

    public void PlayerBerserker()
    {
        if (!isBerserkerActive && invokeBerserker <= 0)
        {
            if (currentHealth <= maxHealth / 4)
            {
                isBerserkerActive = true;
                playerLocomotion.isBerserkerActive = true;
                currentHealth += 45;
                healthBar.SetCurrentHealth(currentHealth);
                berserkerTimer = 45f;
                invokeBerserker = 45;
                playerAttacker.HandleBerserker(berserkerTimer);
            }
            else if (currentHealth <= maxHealth / 2)
            {
                isBerserkerActive = true;
                playerLocomotion.isBerserkerActive = true;
                currentHealth += 15;
                healthBar.SetCurrentHealth(currentHealth);
                berserkerTimer = 15f;
                invokeBerserker = 45;
                playerAttacker.HandleBerserker(berserkerTimer);
            }
        }
    }

    public void FireSword()
    {
        if (!isFireSwordActive && invokeFireSword <= 0)
        {
            isFireSwordActive = true;
            fireSwordTimer = 45f;
            invokeFireSword = 45;
            playerAttacker.HandleFireSword(fireSwordTimer);
            playerAttacker.isFireBonusDamageActive = true;
        }
    }

    public void HolyAttack()
    {
        if(isHolyAttackReady)
        {
            isHolyAttackReady = false;
            invokeHolyAttack = 60;
            playerAttacker.HandleHolyAttack();
        }
    }

    public void HandlePlayerSpecialEffectTimers()
    {
        if(isBerserkerActive)
        {
            berserkerTimer -= Time.deltaTime;

            if(berserkerTimer <= 0)
            {
                isBerserkerActive = false;
                playerLocomotion.isBerserkerActive = false;
            }
        }
        else
        {
            invokeBerserker -= Time.deltaTime;
        }

        if(isFireSwordActive)
        {
            fireSwordTimer -= Time.deltaTime;

            if(fireSwordTimer <= 0)
            {
                isFireSwordActive = false;
                playerAttacker.isFireBonusDamageActive = false;
            }
        }
        else
        {
            invokeFireSword -= Time.deltaTime;
        }

        if(!isHolyAttackReady)
        {
            invokeHolyAttack -= Time.deltaTime;

            if(invokeHolyAttack <= 0)
            {
                isHolyAttackReady = true;
            }
        }
    }
}
