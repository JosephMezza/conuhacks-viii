using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;


    public void Spawn() {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = enemyPrefabs[enemyIndex];

        Instantiate(enemy, transform.position, Quaternion.identity);
    }


}
