using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBrain : MonoBehaviour
{

    public string enemyLayer;

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

        if (other.gameObject.layer == LayerMask.NameToLayer(enemyLayer))
        {
            // get the enemy hit
            other.gameObject.GetComponent<HealthManager>().Damage(10);
            // deal damage to them (they must have death logic and a damageable/health manager script)
        }
    }
}
