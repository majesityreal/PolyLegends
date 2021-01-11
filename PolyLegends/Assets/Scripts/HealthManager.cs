using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{

    public float maxHealth;
    private float currentHealth;

    public GameObject healthBarObject;
    private HealthBar healthBar;
    private bool isEnemyHealthBar;
    private EnemyHealthBar enemyHealthBar;

    /*    public event Action<float> OnHealthPctChanged = delegate { };
    */
    // Start is called before the first frame update

    private void Awake()
    {
        if (healthBarObject.GetComponent<EnemyHealthBar>() != null)
        {
            enemyHealthBar = healthBarObject.GetComponent<EnemyHealthBar>();
            isEnemyHealthBar = true;
        }
        else if (healthBarObject.GetComponent<HealthBar>() != null)
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();
            isEnemyHealthBar = false;
        }
        else
        {
            Debug.LogWarning("GameObject " + gameObject.name + " does not have a health bar attached, but it has a health manager");
        }
    }
    void Start()
    {
        this.currentHealth = maxHealth;
        if (isEnemyHealthBar)
        {
            enemyHealthBar.SetMaxHealth(1.0f);
        }
        else
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    } 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float amount)
    {
        if (this.currentHealth - amount <= 0.0f)
        {
            Die();
            return;
        }
        this.currentHealth -= amount;
        HandleHealthBar();
    }

    void HandleHealthBar()
    {
        Debug.Log("Enemy health bar? " + isEnemyHealthBar);
        if (isEnemyHealthBar)
        {
            float currentHealthPct = (float)currentHealth / (float)maxHealth;
            Debug.Log("Current health pct: " + currentHealthPct);
            enemyHealthBar.SetHealth(currentHealthPct);

        }
        else
        {
            healthBar.SetHealth(this.currentHealth);
        }
    }

/*    void HandleHealthBar()
    {
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }*/

    private void Die()
    {
        // TODO - make this a layer mask with more than just player, maybe the layer player?
        if (gameObject.name == "Player")
        {
            // sets the respawn screen, maybe add an end game screen too possibly mechanics  
            FindObjectOfType<GameManager>().EndGame();
            return;
        }
        {

        }
        Debug.Log(this.name + " has died :(");
        Destroy(this.gameObject);
    }

}
