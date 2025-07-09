using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerPrefad;

    void Start()
    {
       SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerPrefad, GenerateSpawnPosition(), powerPrefad.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn) //void는 값을 전달할 파라미터가 필요함
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    
    private Vector3 GenerateSpawnPosition() //파라미터가 필요x, 메서드 내에서 모든 계산 이루어짐
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
