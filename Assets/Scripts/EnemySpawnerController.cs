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

    public int spawnTimeBottom;
    public int spawnTimeTop;

    public Wave[] waves;

    public GameObject timeToNextWaveUI;
    public float timer = 20;
    private int waveCounter = 0;

    public float[] lastCountWave;

    public static float difficulty = 0.6f;

    public AudioSource waveStartSound;

    public void SetDifficulty(float value)
    {
        difficulty = value;
    }

    void Start()
    {
        SetDifficulty(PlayerPrefs.GetFloat("difficulty"));
        lastCountWave = waves[waves.Length - 1].countEnemies;
        SpawnWaves();
    }

    private void SpawnWaves()
    {
        StartCoroutine(SpawnWave(waves[waveCounter].countEnemies, waves[waveCounter].enemies));
    }

    IEnumerator SpawnWave(float[] countEnemy, GameObject[] enemies)
    {
        waveStartSound.Play();
        timeToNextWaveUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (waveCounter + 1).ToString() + "/" + waves.Length;
        
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
                // randomly choose enemy to spawn
                i = Random.Range(0, enemies.Length);
                // check if we can spawn enemy of this type in current wave
                if (countEnemy[i] > 0)
                {
                    // randomly select spawn point from available spawn points where enemy should be spawned
                    GameObject spawnedEnemy = Instantiate(enemies[i], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                    spawnedEnemy.transform.parent = enemiesOnMap.transform;
                    countEnemy[i] -= 1;
                    count -= 1;
                    if (waveCounter >= 6)
                    {
                        yield return new WaitForSeconds(Random.Range(0, 0.5f));
                    }
                    else
                    {
                        yield return new WaitForSeconds(Random.Range(spawnTimeBottom, spawnTimeTop));
                    }
                }
            }
            while (enemiesOnMap.transform.childCount > 0)
            {
                yield return null;    
            }

            for (float timeLeft = timer; timeLeft > 0; timeLeft -= Time.deltaTime)
            {
                if (waves.Length < waveCounter + 1)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 4)
                    {
                        SceneManager.LoadScene(0);
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
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
