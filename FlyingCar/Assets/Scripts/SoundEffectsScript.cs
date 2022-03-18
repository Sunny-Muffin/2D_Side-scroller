using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    // Синглтон
    public static SoundEffectsScript Instance;

    public AudioClip explosion;
    public AudioClip playerShot;
    public AudioClip enemyShot;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Синглтон звуков уже есть!");
        }
        Instance = this;
    }

    public void Explosion() // воспроизводит звук взрыва
    {
        MakeSound(explosion);
    }

    public void PlayerShot() // воспроизводит звук выстрела игрока
    {
        MakeSound(playerShot);
    }

    public void EnemyShot() // воспроизводит звук выстрела врага
    {
        MakeSound(enemyShot);
    }

    private void MakeSound(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
