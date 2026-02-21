using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrifting : MonoBehaviour
{
    public float driftModifier;
    public bool drifting;
    void Update()
    {
        drifting = Input.GetButton("Jump");
        
        
    }
}
