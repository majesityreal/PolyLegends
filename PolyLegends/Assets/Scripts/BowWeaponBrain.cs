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
        Vector3 aOffset = new Vector3(arrowRotation.x, arrowRotation.y - 90, arrowRotation.z);
        Quaternion angleOffset = Quaternion.Euler(aOffset.x, aOffset.y, aOffset.z);
        Vector3 position = transform.position;

        GameObject shotArrow = Instantiate(arrow, position, angleOffset);
        shotArrow.transform.localEulerAngles = aOffset;
/*        shotArrow.transform.localPosition = GET THE POSITION OFFSETT
*/      Debug.Log(arrowRotation);
        Vector3 addedForce = shotArrow.transform.forward;
        // Rotates the vector to face the forward direction
        addedForce.Set(addedForce.z, addedForce.y, -addedForce.x);
        addedForce *= 20;
        addedForce.y *= 2;
        shotArrow.GetComponent<Rigidbody>().AddForce(addedForce, ForceMode.Force);
        shotArrow.transform.Rotate(0, 0, 0);
    }

    public void setArrowRotation(Vector3 quat)
    {
        this.arrowRotation = quat;
    }
}
