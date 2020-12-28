using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeaponBrain : MonoBehaviour
{

    public float attackDamage = 10.0f;

    public CapsuleCollider swordCollider;
    // public LayerMask enemyLayers;
    public string enemyLayer;

    private bool idle = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (idle)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer(enemyLayer))
        {
            // get the enemy hit
            other.gameObject.GetComponent<HealthManager>().Damage(attackDamage);
            // deal damage to them (they must have death logic and a damageable/health manager script)
            Debug.Log("You hit an enemy lol");
        }
    }

    public void setIdle(bool boolean)
    {
        this.idle = boolean;
        if (!boolean)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
