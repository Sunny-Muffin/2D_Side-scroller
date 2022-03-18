using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health = 1;
    public bool isEnemy = true;

    public GameObject gameOver;
    private GameObject scripts;

    private void Start()
    {
        scripts = GameObject.FindGameObjectWithTag("Scripts");
    }

    public void Damage (int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            SpecialEffectsScript.Instance.Explosion(transform.position);
            SoundEffectsScript.Instance.Explosion();
            if(isEnemy == false)
            {
                gameOver.SetActive(true);
                Time.timeScale = 0f;
            }

            if(isEnemy == true)
            {
                scripts.GetComponent<ScoreScript>().AddScore();
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if(isEnemy != shot.isEnemyShot)
            {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
}
