using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform ShotPrefab;

    public float shootingRate = 0.25f;
    public float shootCoolDown;
    
    // Start is called before the first frame update
    void Start()
    {
        shootCoolDown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCoolDown > 0)
        {
            shootCoolDown = shootCoolDown - Time.deltaTime;
        }
    }

    public void Attack (bool isEnemy)
    {
        if (CanAttack == true)
        {
            shootCoolDown = shootingRate;

            Transform shotTransform = Instantiate(ShotPrefab) as Transform;
            shotTransform.position = transform.position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();

            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;

            }

            moveScript move = shotTransform.gameObject.GetComponent<moveScript>();
            if (move != null)
            {
                move.direction = this.transform.right;
            }
        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCoolDown <= 0f;
        }
    }
}
