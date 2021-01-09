using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public GameObject currentWeapon;
    public Weapon equippedWeaponSO;
    public string equippedWeaponType;

    public GameObject leftHand;

    public ScriptableObjectList weapons;

    // Start is called before the first frame update
    void Start()
    {
        equippedWeaponSO = (MeleeWeapon)((ScriptableObjectList)weapons.GetAtIndex(0)).GetAtIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            equippedWeaponSO = (MeleeWeapon)((ScriptableObjectList)weapons.GetAtIndex(0)).GetAtIndex(0);
            Debug.Log(equippedWeaponSO.name);
            equippedWeaponType = "MeleeWeapon";
            SwitchToWeapon(equippedWeaponSO);
        }
        if (Input.GetKeyDown("2"))
        {
            equippedWeaponSO = (MeleeWeapon)((ScriptableObjectList)weapons.GetAtIndex(0)).GetAtIndex(1);
            Debug.Log(equippedWeaponSO.name);
            equippedWeaponType = "MeleeWeapon";
            SwitchToWeapon(equippedWeaponSO);
        }
        if (Input.GetKeyDown("3"))
        {
            equippedWeaponSO = (RangedWeapon)((ScriptableObjectList)weapons.GetAtIndex(1)).GetAtIndex(0);
            Debug.Log(equippedWeaponSO.name);
            equippedWeaponType = "RangedWeapon";
            SwitchToWeapon(equippedWeaponSO);
        }
    }

    void SwitchToWeapon(Weapon weapon)
    {
        Destroy(currentWeapon);
        Vector3 aOffset = weapon.getAngleOffset();
        Quaternion angleOffset = Quaternion.Euler(aOffset.x,aOffset.y,aOffset.z);
        if (equippedWeaponType.Equals("RangedWeapon"))
        {
            Vector3 position = leftHand.transform.position;
            position += weapon.getPositionOffset();
            currentWeapon = Instantiate(weapon.getGameObject(), position, angleOffset, leftHand.transform);
        }
        else
        {
            Vector3 position = transform.position;
            position += weapon.getPositionOffset();
            currentWeapon = Instantiate(weapon.getGameObject(), position, angleOffset, transform);
        }
        currentWeapon.transform.localEulerAngles = aOffset;
        transform.localPosition = weapon.getPositionOffset();
    }

    public GameObject getCurrentWeapon()
    {
        return this.currentWeapon;
    }
}
