using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleCreator : MonoBehaviour
{
    public static ParticleCreator instance;
    public GameObject[] particleLibrary;
    void Start()
    {
        instance = this;
    }

    public void CreateParticle(Vector2 pos, int whichParticle, float destroyTime)
    {
        GameObject particle = Instantiate(particleLibrary[whichParticle], pos, Quaternion.identity);
        Destroy(particle, destroyTime);
    }
}
