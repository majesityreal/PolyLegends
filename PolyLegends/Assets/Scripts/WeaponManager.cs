using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public GameObject currentWeapon;
    public Weapon equippedWeapon;
    public string equippedWeaponType;

    public ScriptableObjectList meleeWeapons;

    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = (MeleeWeapon)meleeWeapons.GetAtIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            equippedWeapon = ((MeleeWeapon)meleeWeapons.GetAtIndex(0));
            Debug.Log(equippedWeapon.name);
            SwitchToWeapon(equippedWeapon);
        }
        if (Input.GetKeyDown("2"))
        {
            equippedWeapon = (MeleeWeapon)meleeWeapons.GetAtIndex(1);
            Debug.Log(equippedWeapon.name);
            SwitchToWeapon(equippedWeapon);
        }
    }

    void SwitchToWeapon(Weapon weapon)
    {
        Destroy(currentWeapon);
        currentWeapon = Instantiate(weapon.getGameObject());
    }
}
