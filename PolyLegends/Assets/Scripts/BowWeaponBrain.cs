using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeaponBrain : MonoBehaviour
{
    public RangedWeapon weapon;
    public string enemyLayer;

    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyLayer == null)
            enemyLayer = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject shotArrow = Instantiate(arrow);
    }
}
