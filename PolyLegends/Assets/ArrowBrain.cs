using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBrain : MonoBehaviour
{

    public string enemyLayer;
    public Rigidbody arrowRB;

    private Vector3 prevVelocity;
    private Vector3 currentVelocity;

    private float acceleration;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyLayer == null)
            enemyLayer = "Enemy";
        if (arrowRB == null)
            arrowRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position - arrowRB.velocity);
        transform.Rotate(0, -270, 0);
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
