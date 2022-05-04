using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInventory : MonoBehaviour
{
    private NpcWeaponSlotManager npcWeaponSlotManager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    private void Awake()
    {
        npcWeaponSlotManager = GetComponent<NpcWeaponSlotManager>();
    }

    private void Start()
    {
        npcWeaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        npcWeaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }
}
