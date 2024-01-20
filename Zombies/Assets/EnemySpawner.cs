using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //1f means 1 frame
    public float spawnRate = 1f;

    public GameObject[] enemyPrefabs;

    //Contols if mobs can/cannot spawn
    private bool canSpawn = true;

    private void Start(){
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true) {
            yield return wait;

            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = enemyPrefabs[enemyIndex];

            Instantiate(enemy, transform.position, Quaternion.identity);

        }
    }
}
