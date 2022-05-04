using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public BoxCollider boxBlockCollider;

    public float blockingPhysicalDamageAbsorption;

    private void Awake()
    {
        boxBlockCollider = GetComponent<BoxCollider>();
    }



    public void SetColliderDamageAbsorption(WeaponItem weapon)
    {
        if(weapon != null)
        {
            blockingPhysicalDamageAbsorption = weapon.physicalDamageAbsorption;
        }
    }

    
}
