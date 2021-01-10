using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{

    public Enemy enemy;

    private bool canAttack = true;

    public float attackSpeed;

    public Animator animator;
    private NavMeshAgent nav;

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
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("MovementSpeed", nav.velocity.magnitude);
        Debug.Log(nav.velocity.magnitude);
        Physics.OverlapBox();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        this.canAttack = false;
        StartCoroutine(AttackCooldown(attackSpeed));
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
