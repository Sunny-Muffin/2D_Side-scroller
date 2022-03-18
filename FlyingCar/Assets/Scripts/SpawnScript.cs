using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject enemyPrefab;
    public float spawnCD;

    
    // Start is called before the first frame update
    void Start()
    {
        spawnPos.position = new Vector3(transform.position.x, Random.Range(-2.5f, 2.5f), transform.position.z);
        Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
        StartCoroutine(Spawn());
    }

    void Repeat()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnCD);
        spawnPos.position = new Vector3(transform.position.x, Random.Range(-5f, 5f), transform.position.z);
        Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
            
        Repeat();
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
