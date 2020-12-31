using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeaponBrain : MonoBehaviour
{
    public RangedWeapon weapon;
    public string enemyLayer;

    public GameObject arrow;

    private Vector3 arrowRotation;

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
        GameObject shotArrow = Instantiate(arrow, GetComponent<Transform>().position, Quaternion.Euler(90, 90, 0));
        shotArrow.transform.localEulerAngles = arrowRotation;
        shotArrow.GetComponent<Rigidbody>().AddForce(shotArrow.transform.forward * 10.0f, ForceMode.Force);
    }

    public void setArrowRotation(Vector3 quat)
    {
        this.arrowRotation = quat;
    }
}
