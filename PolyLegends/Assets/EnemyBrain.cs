using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{

    public Enemy enemy;

    private bool canAttack = true;

    public float attackSpeed;
    public float attackDamage = 10;

    public Animator animator;
    private NavMeshAgent nav;

    public string[] stringEnemyLayers;
    private int[] enemyLayers = new int[1];

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        if (attackSpeed == 0f)
        {
            attackSpeed = 1.0f;
        }
        Debug.Log(enemy.enemyName);
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (stringEnemyLayers == null || stringEnemyLayers.Length == 0)
        {
            enemyLayers = WeaponBrain.GetEnemyDefaultLayers();
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

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("MovementSpeed", nav.velocity.magnitude);
/*        Debug.Log(nav.velocity.magnitude);*/
/*        Physics.OverlapBox();
*/    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        this.canAttack = false;
        StartCoroutine(AttackCooldown(attackSpeed));
    }

    public void HitDetection()
    {
        Vector3 center = new Vector3(0, 3.3499999f, 3.6f);
        center = gameObject.transform.TransformVector(center);
        Vector3 halfExtends = new Vector3(2f, 5.75f, 6.0f) / 2.5f;
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position + center, halfExtends, gameObject.transform.rotation);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.GetComponent<HealthManager>() != null)
            {
                WeaponBrain.DamageHitEnemy(enemyLayers, c, this.attackDamage);
            }
            /*            if (c.name != "Terrain"&& c.name != "SKELETON")
                        {
                            Debug.Log("Hit collider " + c);
                        }*/
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = new Vector3(0, 3.3499999f, 3.6f);
        center = gameObject.transform.TransformVector(center);
        Vector3 halfExtends = new Vector3(2f, 5.75f, 6.0f) / 2.5f;
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
/*            Gizmos.DrawWireCube(center, halfExtends * 2);
*/      Gizmos.DrawWireCube(gameObject.transform.position + center, halfExtends);
        Gizmos.DrawRay(new Ray(gameObject.transform.position, Vector3.forward * 3));
    }

    private IEnumerator AttackCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.canAttack = true;
    }

    public bool CanAttack()
    {
        return this.canAttack;
    }
}
