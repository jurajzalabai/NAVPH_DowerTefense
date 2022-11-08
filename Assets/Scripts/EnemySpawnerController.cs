using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemiesOnMap;

    public GameObject[] enemies;
    public GameObject[] spawnPoints;

    public GameObject timeToNextWaveUI;
    public float timer = 20;
    private int waveCounter = 1;

    public float countEnemyTypeOne;
    public float countEnemyTypeTwo;
    public float countEnemyTypeThree;
    public float countEnemyTypeFour;
    // Start is called before the first frame update
    void Start()
    {
        SpawnWaves();
        //SpawnWave(spawning);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWaves()
    {
        float[] spawning = { countEnemyTypeOne, countEnemyTypeTwo, countEnemyTypeThree, countEnemyTypeFour };
        StartCoroutine(SpawnWave(spawning));
    }

    IEnumerator SpawnWave(float[] countEnemy)
    {
        timeToNextWaveUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (waveCounter).ToString();
        waveCounter += 1;
        int i = 0;
        float count = 0;
        foreach (var item in countEnemy)
        {
            count += item;
        }
        if (enemies.Length == countEnemy.Length)
        {
            while(count > 0)
            {
                i = Random.Range(0, 4);
                if (countEnemy[i] > 0)
                {
                    GameObject spawnedEnemy = Instantiate(enemies[i], spawnPoints[Random.Range(0, 4)].transform.position, Quaternion.identity);
                    spawnedEnemy.transform.parent = enemiesOnMap.transform;
                    countEnemy[i] -= 1;
                    count -= 1;
                    yield return new WaitForSeconds(Random.Range(0, 3));
                }
            }
            while (enemiesOnMap.transform.childCount > 0)
            {
                yield return null;    
            }

            for (float timeLeft = timer; timeLeft > 0; timeLeft -= Time.deltaTime)
            {
                timeToNextWaveUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)timeLeft).ToString();
                yield return null;
            }
            SpawnWaves();

        }
        else
        {
            Debug.Log("Zly pocet vo waves");
        }
        
    }
}
