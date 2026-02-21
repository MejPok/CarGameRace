using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCreator : MonoBehaviour
{
    public float baseDamage;
    public float CalculatedDamage;
    public float baseRadius;

    public virtual void CreateDamageArea(float radius)
    {
        Debug.Log("Creating damage area");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << 6);
        ParticleCreator.instance.CreateParticle(transform.position, 0, 1f);
        Debug.Log("Colliders found: " + colliders.Length);
        if(colliders.Length > 0)
        {
            foreach(Collider2D coll in colliders)
            {
                ApplyDamage(coll);
            }
        }
    }
    public virtual void CalculateDamage(CarMove player)
    {
        CalculatedDamage = (player.GetComponent<Rigidbody2D>().velocity.magnitude / 5) * baseDamage;
    }
    public virtual void ApplyDamage(Collider2D enemyCollider)
    {
        Debug.Log("Enemy found, dealing damage...");
        EnemyHealth eh = enemyCollider.transform.GetComponent<EnemyHealth>();
        eh.DealDamage(CalculatedDamage); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, baseRadius);
    }
}
