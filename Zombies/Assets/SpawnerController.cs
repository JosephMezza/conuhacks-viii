using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{
    //1f means 1 frame
    public float spawnRate = 1f;

    public int round = 1;

    private int numberOfZombiesToSpawn = 15;

    private int zombiesSpawnedThisRound = 0;

    public List<EnemySpawner> zombieSpawners;

    public Text roundNumber;

    // Start is called before the first frame update
    void Start()
    {
        roundNumber.text = "Round " + round.ToString();
        foreach (Transform childTransform in this.transform)
        {
            EnemySpawner w = childTransform.GetComponent<EnemySpawner>();
            zombieSpawners.Add(w);
        }
        print(zombieSpawners[0]);
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true)
        {
            yield return wait;

            EnemySpawner chosenSpawner = zombieSpawners[Random.Range(0, zombieSpawners.Count)];

            chosenSpawner.Spawn();

            zombiesSpawnedThisRound++;

            if (zombiesSpawnedThisRound == numberOfZombiesToSpawn)
            {
                while (true)
                {
                    yield return wait;
                    if (GameObject.FindObjectsOfType<Enemy>().Length == 0)
                    {
                        round++;
                        roundNumber.text = "Round " + round.ToString();
                        numberOfZombiesToSpawn = numberOfZombiesToSpawn + 2 * (round - 1);
                        zombiesSpawnedThisRound = 0;
                        print(numberOfZombiesToSpawn);
                        break;
                    }
                }
            }

        }
    }
}
