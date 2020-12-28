using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/MagicWeapon", fileName = "New Magic Weapon")]
public class MagicWeapon : Weapon
{
    public string weaponName;
    public string description;
    public float attackDamage;
    public float attackSpeed;

    public GameObject gameObject;

    public bool hasGravity;
    public float range;
    public float accuracy;

    public float manaCost;

    public override GameObject getGameObject()
    {
        return this.gameObject;
    }

}
