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

    public Vector3 positionOffset;
    public Vector3 angleOffset;

    public GameObject gameObject;

    public float soundDelay;

    public float accuracy;
    public float range;

    public string s_ShootArrow;
    public string s_PullBow;

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
        return "Ranged";
    }
}
