using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 10f;
    void Start()
    {
        health = maxHealth;
    }
    public virtual void DealDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        Debug.Log($"Enemy has been dealt {damage} damage");
        if(health == 0f)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
