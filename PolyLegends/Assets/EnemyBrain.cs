using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    public Enemy enemy;

    private bool canAttack = true;

    public float attackSpeed;

    public Animator animator;

    void Start()
    {
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
