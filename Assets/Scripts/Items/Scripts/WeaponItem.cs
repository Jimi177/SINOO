using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item / Weapon item")]
public class WeaponItem : Item
{
    public bool isSword;
    public bool isTwoHandSword;
    public bool isShield;
    public bool isAxe;

    public GameObject modelPrefab;
    public bool isUnaremd;

    [Header("Damage")]
    public int baseDamage = 10;
    public int criticalDamageMultiplier = 3;
    public int lightAttackMultiplier = 1;
    public int heavyAttackMultiplier = 2;

    [Header("Absorption")]
    public float physicalDamageAbsorption;

    [Header("Movment Speed")]
    public float movementSpeed;

    [Header("Idle Animations)")]
    public int weponLocomotion;
    public string withdrawingWeapon;
    public string hidingWeapon;

    [Header("Light Attack Animations")]
    public string Light_Attack_01;
    public string Light_Attack_02;
    public string Light_Attack_03;

    [Header("Heavy Attack Animations")]
    public string Heavy_Attack_01;
    public string Heavy_Attack_02;

    [Header("Special Combat Animations")]
    public string Special_Attack_01;
    public string Special_Attack_02;

    [Header("Parry & Blocking")]
    public string Parry_01;
    public string Parry_02;
    public string Block_01;
    public string Block_02;

    [Header("Stamina Costs")]
    public int baseStamina;
    public float lightMultiplier;
    public float heavyMultiplier;



}
