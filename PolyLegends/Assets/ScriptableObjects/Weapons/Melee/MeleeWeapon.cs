using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/MeleeWeapon", fileName = "New Melee Weapon")]
public class MeleeWeapon : Weapon
{
    public string weaponName;
    public string description;
    public float attackDamage;
    public float attackSpeed;

    public GameObject gameObject;

    public override GameObject getGameObject()
    {
        return this.gameObject;
    }
}
