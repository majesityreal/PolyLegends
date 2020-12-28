using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/RangedWeapon", fileName = "New Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public string weaponName;
    public string description;
    public float attackDamage;
    public float attackSpeed;

    public GameObject gameObject;

    public float accuracy;
    public float range;

    public override GameObject getGameObject()
    {
        return this.gameObject;
    }

}
