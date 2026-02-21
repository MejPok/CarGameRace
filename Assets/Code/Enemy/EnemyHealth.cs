using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    void Start()
    {
        health = maxHealth;
    }
    public virtual void DealDamage(float damage)
    {
        health = Mathf.Clamp(health - (int)damage, 0, maxHealth);
        Debug.Log($"Enemy has been dealt {damage} damage");
        if(damage != 0)
        {
            DamageNotificator.instance.CreateDamageNotification(transform.position, (int)damage);
        }
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
