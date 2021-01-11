using UnityEngine;

public class WeaponBrain : MonoBehaviour
{

    public static bool DamageHitEnemy(int[] enemyLayers, Collider other, float attackDamage)
    {
        bool temp = false;
        foreach (int i in enemyLayers)
        {
            if (other.gameObject.layer == i)
            {
                // get the enemy hit
                other.gameObject.GetComponent<HealthManager>().Damage(attackDamage);
                temp = true;
                // deal damage to them (they must have death logic and a damageable/health manager script)
            }
        }
        return temp;
    }

    public static int[] GetDefaultLayers()
    {
        string s = "Enemy";
        string s2 = "Building";
        int[] layers = new int[2];
        layers[0] = LayerMask.NameToLayer(s);
        layers[1] = LayerMask.NameToLayer(s2);
        return layers;
    }

    public static int[] GetEnemyDefaultLayers()
    {
        string s = "Player";
        string s2 = "Building";
        int[] layers = new int[2];
        layers[0] = LayerMask.NameToLayer(s);
        layers[1] = LayerMask.NameToLayer(s2);
        return layers;
    }

}
