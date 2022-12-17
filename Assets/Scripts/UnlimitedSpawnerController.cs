using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UnlimitedSpawnerController : MonoBehaviour
{
    public GameObject enemiesOnMap;

    public GameObject[] spawnPoints;

    public int spawnTimeBottom;
    public int spawnTimeTop;

    public GameObject timeToNextWaveUI;
    public float timer = 20;
    private int waveCounter = 0;

    public float[] lastCountWave;

    public static float difficulty = 0.6f;

    public AudioSource waveStartSound;

    public Wave wave;

    private float countEnemyOne = 2;
    private float countEnemyTwo = 2;
    private float countEnemyThree = 1;
    private float countEnemyFour = 1;
    private float countEnemyFive = 1;
    private float countEnemySix = 2;

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
        SpawnWaves();
        //SpawnWave(spawning);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnWaves()
    {

        StartCoroutine(SpawnWave());



    }

    IEnumerator SpawnWave()
    {
        Wave point = new Wave();
        waveStartSound.Play();
        timeToNextWaveUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (waveCounter + 1).ToString();
        //Debug.Log(lastCountWave);
        waveCounter += 1;
        int i = 0;
        float count = 0;
        foreach (var item in wave.countEnemies)
        {
            count += item;
        }
        if (wave.enemies.Length == wave.countEnemies.Length)
        {
            Debug.Log(enemiesOnMap.transform.childCount);
            Debug.Log(wave.countEnemies[i]);
            while (count > 0)
            {
                i = Random.Range(0, wave.enemies.Length);
                if (wave.countEnemies[i] > 0)
                {
                    GameObject spawnedEnemy = Instantiate(wave.enemies[i], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                    spawnedEnemy.transform.parent = enemiesOnMap.transform;
                    wave.countEnemies[i] -= 1;
                    count -= 1;
                    yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
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
            wave.countEnemies[0] = Mathf.Round((countEnemyOne * 1.2f) + waveCounter);
            wave.countEnemies[1] = Mathf.Round((countEnemyTwo * 1.2f) + waveCounter);
            wave.countEnemies[2] = Mathf.Round((countEnemyThree * 1.1f));
            wave.countEnemies[3] = Mathf.Round((countEnemyFour * 1.1f));
            wave.countEnemies[4] = Mathf.Round((countEnemyFive * 1.1f));
            if (waveCounter >= 7)
            {
                wave.countEnemies[5] = Mathf.Round((countEnemySix * 1.2f));
            }
            Debug.Log("1: " + Mathf.Round((countEnemyOne * 1.2f) + waveCounter));
            Debug.Log("2: " + Mathf.Round((countEnemyTwo * 1.2f) + waveCounter));
            Debug.Log("3: " + Mathf.Round((countEnemyThree * 1.1f)));
            Debug.Log("4: " + Mathf.Round((countEnemyFour * 1.1f)));
            Debug.Log("5: " + Mathf.Round((countEnemyFive * 1.1f)));
            if (waveCounter >= 7)
            {
                Debug.Log("5: " + Mathf.Round((countEnemySix * 1.2f)));
            }
            countEnemyOne = (countEnemyOne * 1.2f);
            countEnemyTwo = countEnemyTwo * 1.2f;
            countEnemyThree = countEnemyThree * 1.2f;
            countEnemyFour = countEnemyFour * 1.1f;
            countEnemyFive = countEnemyFive * 1.1f;
            if (waveCounter >= 7)
            {
                countEnemySix = countEnemySix * 1.2f;
            }

            if (waveCounter + 1 % 10 == 0)
            {
                difficulty = difficulty + 0.3f;
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
