using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public BlockCollider blockCollider;
    PlayerInventory playerInventory;
    PlayerManager playerManager;

    private void Awake()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
        playerManager = GetComponentInParent<PlayerManager>();
    }

    private void Update()
    {
        EnableOrDisableBlockingCollider();
    }

    public void EnableOrDisableBlockingCollider()
    {
        if (playerManager.isBlocking)
        {
            blockCollider.SetColliderDamageAbsorption(playerInventory.leftWeapon);
            blockCollider.boxBlockCollider.enabled = true;
        }
        else
        {
            blockCollider.boxBlockCollider.enabled = false;
        }
    }

}
