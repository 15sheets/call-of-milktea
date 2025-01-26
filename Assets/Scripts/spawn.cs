using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class Spawn : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    
    //public float waitDuration = 10;
    public int groupSize = 5;
    //public int maxGroupNum;

    public float sparsity = 4f;

    public float enemy1prob = 0.34f;
    public float enemy2prob = 0.33f;
    public float enemy3prob = 0.33f;

    private Vector3 spawnOnePos = Vector3.zero;

    private SpawnManager spm;

    IEnumerator WaitAndSpawn()
    {
        // suspend execution for waitDuration seconds
        yield return new WaitForSeconds(spm.spawnPeriod);

        spawnEnemies();

        StartCoroutine ("WaitAndSpawn");
    }

    void spawnEnemies()
    {
        if (this.transform.childCount < groupSize * (spm.groupLimit - 1))
        {
            for (int i = 0; i < groupSize; i++)
            {
                spawnOnePos.x = this.transform.position.x + Random.Range(-sparsity, sparsity);
                spawnOnePos.z = this.transform.position.z + Random.Range(-sparsity, sparsity);

                // Which type of enemy to spawn
                var enemyType = Random.Range(0f, 1f);
                if (enemyType < enemy1prob)
                {
                    SpawnEnemy(enemy1Prefab, spawnOnePos);
                }
                else if (enemyType < (enemy1prob + enemy2prob))
                {
                    SpawnEnemy(enemy2Prefab, spawnOnePos);
                }
                else
                {
                    SpawnEnemy(enemy3Prefab, spawnOnePos);
                }
            }
        }
    }

    void SpawnEnemy(GameObject prefab, Vector3 spawnPos) {
        // may need to change spawn location logic later
        var bounds = prefab.GetComponent<Renderer>().bounds;
        //spawnPos.y = this.transform.position.y + bounds.size.y / 2;

        Instantiate(prefab, spawnPos, Quaternion.identity, this.transform);
    }

    IEnumerator Start()
    {
        //Assert.AreApproximatelyEqual(enemy1prob + enemy2prob + enemy3prob, 1f);
        spm = GetComponentInParent<SpawnManager>();

        spawnEnemies();

        // Start function WaitAndSpawn as a coroutine
        yield return StartCoroutine("WaitAndSpawn");
    }
}
