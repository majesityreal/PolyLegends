using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public GameObject currentWeapon;
    public Weapon equippedWeapon;
    public string equippedWeaponType;

    public ScriptableObjectList weapons;

    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = (MeleeWeapon)((ScriptableObjectList)weapons.GetAtIndex(0)).GetAtIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            equippedWeapon = (MeleeWeapon)((ScriptableObjectList)weapons.GetAtIndex(0)).GetAtIndex(0);
            Debug.Log(equippedWeapon.name);
            SwitchToWeapon(equippedWeapon);
        }
        if (Input.GetKeyDown("2"))
        {
            equippedWeapon = (MeleeWeapon)((ScriptableObjectList)weapons.GetAtIndex(0)).GetAtIndex(1);
            Debug.Log(equippedWeapon.name);
            SwitchToWeapon(equippedWeapon);
        }
        if (Input.GetKeyDown("3"))
        {
            equippedWeapon = (RangedWeapon)((ScriptableObjectList)weapons.GetAtIndex(1)).GetAtIndex(0);
            Debug.Log(equippedWeapon.name);
            SwitchToWeapon(equippedWeapon);
        }
    }

    void SwitchToWeapon(Weapon weapon)
    {
        Destroy(currentWeapon);
        Vector3 aOffset = weapon.getAngleOffset();
        Quaternion angleOffset = Quaternion.Euler(aOffset.x,aOffset.y,aOffset.z);
        Vector3 position = transform.position;
        currentWeapon = Instantiate(weapon.getGameObject(), position, angleOffset, transform);
        currentWeapon.transform.localEulerAngles = aOffset;
        Debug.Log(transform.position);
        transform.localPosition = weapon.getPositionOffset();
        Debug.Log(transform.position);
    }

    public GameObject getCurrentWeapon()
    {
        return this.currentWeapon;
    }
}
