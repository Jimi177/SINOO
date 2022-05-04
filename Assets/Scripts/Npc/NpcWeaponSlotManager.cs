using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWeaponSlotManager : MonoBehaviour
{
    private NpcInventory npcInventory;
    private GuardManager guardManager;

    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    public WeaponHolderSlot legSlot;
    public WeaponHolderSlot twoHandBackSlot;
    public WeaponHolderSlot backSlot;

    private Animator animator;

    private void Awake()
    {
        npcInventory = GetComponent<NpcInventory>();
        guardManager = GetComponentInParent<GuardManager>();
        animator = GetComponent<Animator>();

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
            else if (weaponSlot.isLegSlot)
            {
                legSlot = weaponSlot;
            }
            else if (weaponSlot.isBackSlot)
            {
                backSlot = weaponSlot;
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
            if (npcInventory.leftWeapon != null)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.currentWeaponModel = weaponItem.modelPrefab;
                if (guardManager.isArmed)
                {
                    leftHandSlot.LoadWeaponModel(weaponItem);
                }
                else
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                }
            }
        }
        else
        {
            
            if (npcInventory.rightWeapon != null)
            {
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.currentWeaponModel = weaponItem.modelPrefab;
                if (guardManager.isArmed)
                {
                    rightHandSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
                    animator.SetInteger("weaponLocomotion", weaponItem.weponLocomotion);
                    //playerLocomotion.isArmed = true;
                    //playerLocomotion.weaponMovmentSpeed = weaponItem.movementSpeed;
                }
                else
                {
                    #region Check Slot
                    if (weaponItem.modelPrefab.tag == "legSlot")
                    {
                        legSlot.LoadWeaponModel(rightHandSlot.currentWeapon);
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

    public void HideOrWithdrawWeapon(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            if (guardManager.isArmed == false)
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
            if (guardManager.isArmed == false)
            {

                animator.CrossFade(weaponItem.withdrawingWeapon, 0.2f);
                animator.SetInteger("weaponLocomotion", weaponItem.weponLocomotion);

                //playerLocomotion.isArmed = true;
                //playerLocomotion.weaponMovmentSpeed = weaponItem.movementSpeed;
            }
            else
            {
                animator.CrossFade(weaponItem.hidingWeapon, 0.2f);
                animator.SetInteger("weaponLocomotion", 0);
                //playerLocomotion.isArmed = false;
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
    #endregion

    #region isArmed Check
    public void IsArmed()
    {
        animator.SetBool("isArmed", true);
    }

    public void IsNotArme()
    {
        animator.SetBool("isArmed", false);
    }
    #endregion
}