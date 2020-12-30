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

    public Vector3 positionOffset;
    public Vector3 angleOffset;

    public GameObject gameObject;

    public float soundDelay;

    public bool hasGravity;
    public float range;
    public float accuracy;

    public float manaCost;

    public override GameObject getGameObject()
    {
        return this.gameObject;
    }

    public override Vector3 getAngleOffset()
    {
        return this.angleOffset;
    }
    public override Vector3 getPositionOffset()
    {
        return this.positionOffset;
    }

    public override string GetWeaponType()
    {
        return "Magic";
    }
}
