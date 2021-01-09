using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBrain : WeaponBrain
{
    public string[] stringEnemyLayers;
    private int[] enemyLayers;

    public Rigidbody arrowRB;

    private Vector3 prevVelocity;
    private Vector3 currentVelocity;

    private float acceleration;

    // Start is called before the first frame update
    void Start()
    {
        if (stringEnemyLayers == null || stringEnemyLayers.Length == 0)
        {
            enemyLayers = GetDefaultLayers();
        }
        else
        {
            enemyLayers = new int[stringEnemyLayers.Length];
            int i = 0;
            foreach (string s in stringEnemyLayers)
            {
                enemyLayers[i] = LayerMask.NameToLayer(stringEnemyLayers[i]);
                i++;
            }
        }
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
        if (DamageHitEnemy(enemyLayers, other, 10f))
        {
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
