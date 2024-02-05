using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject currentEnemy = null;
    private bool playerIsInside = false;
    public float minSpawnTime = 5f; 
    public float maxSpawnTime = 10f;
    private bool firstSpawn = true;

    void Start()
    {
        if (firstSpawn == true)
        {
            firstSpawn = false;
            currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {

        while (true)
        {
            yield return new WaitUntil(() => currentEnemy == null && !playerIsInside);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            if (!playerIsInside)
            {
                currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }
}
