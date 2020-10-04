using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemHelper : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();        
    }

    void Update()
    {
        if (!particleSystem.isPlaying)
        {
            Destroy(gameObject);
        }        
    }
}
