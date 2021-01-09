using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeaponBrain : MonoBehaviour
{
    public RangedWeapon weapon;
    public string enemyLayer;

    public GameObject arrow;

    private Vector3 arrowRotation;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyLayer == null)
            enemyLayer = "Enemy";
        this.canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (!GetCanShoot())
        {
            return;
        }
        Vector3 aOffset = new Vector3(arrowRotation.x, arrowRotation.y - 90, arrowRotation.z);
        Quaternion angleOffset = Quaternion.Euler(aOffset.x, aOffset.y, aOffset.z);
        Vector3 shootPosition = transform.root.position;
        shootPosition += new Vector3(0, 0.7f, 0);
        shootPosition += transform.root.forward;
        GameObject shotArrow = Instantiate(arrow, shootPosition, angleOffset);
        shotArrow.transform.localEulerAngles = aOffset;
/*        shotArrow.transform.localPosition = GET THE POSITION OFFSETT
*/      Debug.Log(arrowRotation);
        Vector3 addedForce = shotArrow.transform.forward;
        // Rotates the vector to face the forward direction
        addedForce.Set(addedForce.z, addedForce.y, -addedForce.x);
        // 20, y 1.5
        addedForce *= 10 * weapon.range;
        addedForce.y *= 1.5f;
        shotArrow.GetComponent<Rigidbody>().AddForce(addedForce, ForceMode.Force);
        shotArrow.transform.Rotate(0, 0, 0);
        this.canShoot = false;
        StartCoroutine(ShootCooldown(weapon.attackSpeed));
    }

    public void setArrowRotation(Vector3 quat)
    {
        this.arrowRotation = quat;
    }

    // TODO, make further can shoot implementations, such as a paused game and such
    bool GetCanShoot()
    {
        if (canShoot)
        {
            return true;
        }
        return false;
    }

    private IEnumerator ShootCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        this.canShoot = true;
    }
}
