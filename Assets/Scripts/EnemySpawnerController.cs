using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemiesOnMap;

    //public GameObject[] enemies;
    public GameObject[] spawnPoints;

    public Wave[] waves;

    public GameObject timeToNextWaveUI;
    public float timer = 20;
    private int waveCounter = 0;

    public float[] lastCountWave;

    public static float difficulty = 0.6f;

    public void SetDifficulty(float value)
    {
        difficulty = value;
    }

    //public float countEnemyTypeOne;
    //public float countEnemyTypeTwo;
    //public float countEnemyTypeThree;
    //public float countEnemyTypeFour;
    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty(PlayerPrefs.GetFloat("difficulty"));
        // A[] array2 = array1.Select (a =>(A)a.Clone()).ToArray();
        lastCountWave = waves[waves.Length - 1].countEnemies;
        SpawnWaves();
        //SpawnWave(spawning);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnWaves()
    {
        
            StartCoroutine(SpawnWave(waves[waveCounter].countEnemies, waves[waveCounter].enemies));
        
    }

    IEnumerator SpawnWave(float[] countEnemy, GameObject[] enemies)
    {
        timeToNextWaveUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (waveCounter + 1).ToString();
        if (waves.Length - 1 != waveCounter)
        {
            waveCounter += 1;
   
        }
        else
        {
            countEnemy = lastCountWave;
        }
        //Debug.Log(lastCountWave);
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
                i = Random.Range(0, enemies.Length);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
