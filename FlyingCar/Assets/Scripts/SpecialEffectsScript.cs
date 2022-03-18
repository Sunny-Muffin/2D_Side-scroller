using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsScript : MonoBehaviour
{
    public static SpecialEffectsScript Instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Несколько экземпляров скриптов Эффектов!");
        }

        Instance = this;
    }

    public void Explosion (Vector3 position)
    {
        Boom(smokeEffect, position);
        Boom(fireEffect, position);
    }

    private ParticleSystem Boom (ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem explosion = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        Destroy(explosion.gameObject, explosion.startLifetime);
        return explosion;
    }
}
