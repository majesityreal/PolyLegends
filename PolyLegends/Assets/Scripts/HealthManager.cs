using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{

    public float maxHealth;
    private float currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = maxHealth;
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
        Debug.Log(this.name + " health is: " + this.currentHealth);
    }

    void HandleHealthBar()
    {
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void Die()
    {
        Debug.Log(this.name + " has died :(");
        Destroy(this.gameObject);
    }

}
