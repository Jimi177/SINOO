using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardState : MonoBehaviour
{
    public abstract GuardState Tick(GuardManager guardManager, GuardLocomotion guardLocomotion, GuardAnimator guardAnimator, NpcWeaponSlotManager npcWeaponSlotManager, NpcInventory npcInventory);
}
