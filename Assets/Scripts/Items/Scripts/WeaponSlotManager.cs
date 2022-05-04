using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;
    PlayerInventory playerInventory;
    PlayerStats playerStats;

    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    public WeaponHolderSlot backSlot;
    public WeaponHolderSlot twoHandBackSlot;
    public WeaponHolderSlot legSlot;
    public WeaponHolderSlot legSlotRight;

    public DamageCollider leftWeaponDamageCollider;
    public DamageCollider rightWeaponDamageCollider;

    public BlockCollider blockCollider;

    public WeaponItem attackingWeapon;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponentInParent<InputManager>();
        playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        playerInventory = GetComponent<PlayerInventory>();
        playerStats = GetComponentInParent<PlayerStats>();


        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
            else if (weaponSlot.isBackSlot)
            {
                backSlot = weaponSlot;
            }
            else if (weaponSlot.isLegSlot)
            {
                legSlot = weaponSlot;
            }
            else if(weaponSlot.isLegSlotRight)
            {
                legSlotRight = weaponSlot;
            }
            else if (weaponSlot.isTwoHandBackSlot)
            {
                twoHandBackSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            if(playerInventory.leftWeapon != null)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.currentWeaponModel = weaponItem.modelPrefab;
                LoadLeftWeaponDamageCollider();

                if (inputManager.isLeftHandActive)
                {
                    leftHandSlot.LoadWeaponModel(weaponItem);
                }
                else
                {
                    #region Check Slot
                    if (weaponItem.modelPrefab.tag == "legSlot")
                    {
                        legSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    }
                    else if (weaponItem.modelPrefab.tag == "legSlotRight")
                    {
                        legSlotRight.LoadWeaponModel(leftHandSlot.currentWeapon);
                    }
                    else if (weaponItem.modelPrefab.tag == "backSlot")
                    {
                        backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    }
                    else if (weaponItem.modelPrefab.tag == "twoHandBackSlot")
                    {
                        twoHandBackSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    }
                    #endregion
                }
            }
        }
        else
        {
            if(playerInventory.rightWeapon != null)
            {
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.currentWeaponModel = weaponItem.modelPrefab;
                LoadRightWeaponDamageCollider();

                if (inputManager.isRightHandActive)
                {
                    rightHandSlot.LoadWeaponModel(weaponItem);
                    animator.SetInteger("weaponLocomotion", weaponItem.weponLocomotion);
                    playerLocomotion.isArmed = true;
                    playerLocomotion.weaponMovmentSpeed = weaponItem.movementSpeed;
                }
                else
                {
                    #region Check Slot
                    if (weaponItem.modelPrefab.tag == "legSlot")
                    {
                        legSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
                    }
                    if (weaponItem.modelPrefab.tag == "legSlotRight")
                    {
                        legSlotRight.LoadWeaponModel(rightHandSlot.currentWeapon);
                    }
                    else if (weaponItem.modelPrefab.tag == "backSlot")
                    {
                        backSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
                    }
                    else if (weaponItem.modelPrefab.tag == "twoHandBackSlot")
                    {
                        twoHandBackSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
                    }
                    #endregion
                }
            }
        }
    }

    public void HideOrWithdrawWeapon(WeaponItem weaponItem, bool isLeft, float movementSpeed)
    {
        if (isLeft)
        {
            if (inputManager.isLeftHandActive)
            {
                animator.CrossFade(weaponItem.withdrawingWeapon, 0.2f);
            }
            else
            {
                animator.CrossFade(weaponItem.hidingWeapon, 0.2f);
            }
        }
        else
        {
            if (inputManager.isRightHandActive)
            {

                animator.CrossFade(weaponItem.withdrawingWeapon, 0.2f);
                animator.SetInteger("weaponLocomotion", weaponItem.weponLocomotion);
               
                playerLocomotion.isArmed = true;
                playerLocomotion.weaponMovmentSpeed = weaponItem.movementSpeed;
            }
            else
            {
                animator.CrossFade(weaponItem.hidingWeapon, 0.2f);
                animator.SetInteger("weaponLocomotion", 0);
                playerLocomotion.isArmed = false;
            }
        }
    }

    #region Animations Withdraw/Hide weapon
    public void WithdrawRightWeapon(WeaponItem weaponItem)
    {
        weaponItem = rightHandSlot.currentWeapon;
        #region Check Slot
        if (weaponItem.modelPrefab.tag == "legSlot")
        {
            legSlot.UnloadWeaponAndDestroy();
        }
        else if(weaponItem.modelPrefab.tag =="legSlotRight")
        {
            legSlotRight.UnloadWeaponAndDestroy();
        }
        else if (weaponItem.modelPrefab.tag == "backSlot")
        {
            backSlot.UnloadWeaponAndDestroy();
        }
        else if (weaponItem.modelPrefab.tag == "twoHandBackSlot")
        {
            twoHandBackSlot.UnloadWeaponAndDestroy();
        }
        #endregion
        rightHandSlot.LoadWeaponModel(weaponItem);
    }

    public void HideRightWeapon(WeaponItem weaponItem)
    {
        weaponItem = rightHandSlot.currentWeapon;
        #region Check Slot
        if (weaponItem.modelPrefab.tag == "legSlot")
        {
            legSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
        }
        else if (weaponItem.modelPrefab.tag == "legSlotRight")
        {
            legSlotRight.LoadWeaponModel(rightHandSlot.currentWeapon);
        }
        else if (weaponItem.modelPrefab.tag == "backSlot")
        {
            backSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
        }
        else if (weaponItem.modelPrefab.tag == "twoHandBackSlot")
        {
            twoHandBackSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
        }
        #endregion
        rightHandSlot.UnloadWeapon();
    }

    public void WithdrawLeftWeapon(WeaponItem weaponItem)
    {
        weaponItem = leftHandSlot.currentWeapon;
        #region Check Slot
        if (weaponItem.modelPrefab.tag == "legSlot")
        {
            legSlot.UnloadWeaponAndDestroy();
        }
        else if (weaponItem.modelPrefab.tag == "legSlotRight")
        {
            legSlotRight.UnloadWeaponAndDestroy();
        }
        else if (weaponItem.modelPrefab.tag == "backSlot")
        {
            backSlot.UnloadWeaponAndDestroy();
        }
        else if (weaponItem.modelPrefab.tag == "twoHandBackSlot")
        {
            twoHandBackSlot.UnloadWeaponAndDestroy();
        }
        #endregion
        leftHandSlot.LoadWeaponModel(weaponItem);
    }

    public void HideLeftWeapon(WeaponItem weaponItem)
    {
        weaponItem = leftHandSlot.currentWeapon;
        #region Check Slot
        if (weaponItem.modelPrefab.tag == "legSlot")
        {
            legSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
        }
        else if (weaponItem.modelPrefab.tag == "legSlotRight")
        {
            legSlotRight.LoadWeaponModel(leftHandSlot.currentWeapon);
        }
        else if (weaponItem.modelPrefab.tag == "backSlot")
        {
            backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
        }
        else if (weaponItem.modelPrefab.tag == "twoHandBackSlot")
        {
            twoHandBackSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
        }
        #endregion
        leftHandSlot.UnloadWeapon();
        //backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
    }
    #endregion

    #region Enable/Disable weapon damage collider

    private void LoadLeftWeaponDamageCollider()
    {
        leftWeaponDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        leftWeaponDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        if(!playerInventory.leftWeapon.isShield)
        {
            leftWeaponDamageCollider.currentWeaponDamage = playerInventory.leftWeapon.baseDamage;
        } 
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightWeaponDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        rightWeaponDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        rightWeaponDamageCollider.currentWeaponDamage = playerInventory.rightWeapon.baseDamage;
    }
    #endregion

    public void DrainStaminaLightAttack()
    {
        playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightMultiplier));
    }

    public void DrainStaminaHeavytAttack()
    {
        playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyMultiplier));
    }
    //#region Animations Withdraw/Hide weapon
    //public void WithdrawRightWeapon(WeaponItem weaponItem)
    //{
    //    weaponItem = rightHandSlot.currentWeapon;
    //    legSlot.UnloadWeaponAndDestroy();
    //    rightHandSlot.LoadWeaponModel(weaponItem);
    //}

    //public void HideRightWeapon()
    //{
    //    rightHandSlot.UnloadWeapon();
    //    legSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
    //}

    //public void WithdrawLeftWeapon(WeaponItem weaponItem)
    //{
    //    weaponItem = leftHandSlot.currentWeapon;
    //    backSlot.UnloadWeaponAndDestroy();
    //    leftHandSlot.LoadWeaponModel(weaponItem);
    //}

    //public void HideLeftWeapon()
    //{
    //    leftHandSlot.UnloadWeapon();
    //    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
    //}
    //#endregion





    //#region Animations Withdraw/Hide weapon TEEEEEST!
    //public void WithdrawWeapon(WeaponItem weaponItem, bool isLeft)
    //{
    //    if (isLeft)
    //    {
    //        weaponItem = leftHandSlot.currentWeapon;
    //        backSlot.UnloadWeaponAndDestroy();
    //        leftHandSlot.LoadWeaponModel(weaponItem);
    //    }
    //    else
    //    {
    //        weaponItem = rightHandSlot.currentWeapon;
    //        legSlot.UnloadWeaponAndDestroy();
    //        rightHandSlot.LoadWeaponModel(weaponItem);
    //    }

    //}

    //public void HideWeapon(bool isLeft)
    //{
    //    if (isLeft)
    //    {
    //        leftHandSlot.UnloadWeapon();
    //        backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
    //    }
    //    else
    //    {
    //        rightHandSlot.UnloadWeapon();
    //        legSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
    //    }
    //}
    //#endregion
}
