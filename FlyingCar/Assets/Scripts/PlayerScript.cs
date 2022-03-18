using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50);
    private Vector2 movement;
    public Rigidbody2D rb;

    //public Joystick joystick;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //float inputX = joystick.Horizontal;
        //float inputY = joystick.Vertical;

        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        bool shoot = Input.GetButtonDown("Fire1");
        if (shoot == true)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null && weapon.shootCoolDown <= 0f)
            {
                weapon.Attack(false);
                SoundEffectsScript.Instance.PlayerShot();
            }
        }

        // код ниже не дает игроку выйти за границы камеры
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();

        if (enemy != null)
        {
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(enemyHealth.health);
            }
            damagePlayer = true;
        }

        if (damagePlayer == true)
        {
            HealthScript playerHealth = GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.Damage(playerHealth.health);
            }
        }
    }
}
