using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    public Enemy enemy;

    void Start()
    {
        Debug.Log(enemy.enemyName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
