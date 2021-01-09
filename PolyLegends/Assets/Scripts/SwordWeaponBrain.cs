using UnityEngine;

public class SwordWeaponBrain : WeaponBrain
{

    public MeleeWeapon weapon;

    public CapsuleCollider swordCollider;
    // public LayerMask enemyLayers;
    public string[] stringEnemyLayers;
    private int[] enemyLayers;

    private bool idle = true;

    void Start()
    {
        idle = true;
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

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (idle)
            return;
        // other.gameObject.layer == LayerMask.NameToLayer(enemyLayer)
        DamageHitEnemy(enemyLayers, other, weapon.attackDamage);
    }

    public void setIdle(bool boolean)
    {
        this.idle = boolean;
    }
}
