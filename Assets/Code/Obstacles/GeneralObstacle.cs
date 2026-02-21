using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneralObstacle : DamageCreator
{
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("something found");
        CarMove player;
        other.gameObject.TryGetComponent<CarMove>(out player);
        if(player != null)
        {
            CalculateDamage(player);
            CreateDamageArea(baseRadius);
        }
    }
    
}
