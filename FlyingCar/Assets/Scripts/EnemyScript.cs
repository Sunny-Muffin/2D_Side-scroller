using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    private Vector2 movement;
    public Rigidbody2D rb;

    private WeaponScript[] weapons;

    //private moveScript move;
    private bool isInCamera;

    private void Awake()
    {
        weapons = GetComponentsInChildren<WeaponScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInCamera = false;
        GetComponent<Collider2D>().enabled = false;
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        rb.velocity = movement;

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);

        if (isInCamera == false)
        {
            if(GetComponent<Renderer>().IsVisibleFrom(Camera.main))
            {
                InCamera();
            }
        }
        else
        {
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                    SoundEffectsScript.Instance.EnemyShot();
                }
            }

            if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    private void InCamera()
    {
        isInCamera = true;
        GetComponent<Collider2D>().enabled = true;
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }
}

