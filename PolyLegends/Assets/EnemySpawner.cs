using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public ScriptableObjectList enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(GameObject enemy, Transform position)
    {
        Instantiate(enemy, position.position, position.rotation);
    }

    public void SpawnEnemy(string enemyName, Transform position)
    {
        ScriptableObject fakeEnemy = enemies.GetByName(enemyName);
        Enemy enemy = (Enemy) fakeEnemy;
        Instantiate(enemy.gameObject, position.position, position.rotation);
    }

}
