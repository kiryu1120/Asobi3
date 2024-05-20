using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoushoku : MonoBehaviour
{
    public GameObject  kabe; // Enemyのプレハブ
    public float spawnInterval = 4.0f;
    private float spawnTimer = 0.0f;

    public GameObject kabe2;
    public float spawnInterval2 = 5.0f;
    private float spawnTimer2 = 0.0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0.0f;

            // 一定時間ごとに生産スピードを上げる（例：現在の生産間隔を0.9倍にする）
            spawnInterval *= 1.0f;
        }

        spawnTimer2 += Time.deltaTime;
        if (spawnTimer2 >= spawnInterval2)
        {
            SpawnEnemy();
            spawnTimer2 = 0.0f;

            // 一定時間ごとに生産スピードを上げる（例：現在の生産間隔を1.0倍にする）
            spawnInterval2 *= 1.0f;
        }
    }

    void SpawnEnemy()
    {
        // ランダムなY座標を生成
        float randomY = Random.Range(4f, 7f);
        float newrandomY = Random.Range(-4f, -7f);

        // Enemyを生成（X座標は8のまま）
        Vector3 spawnPosition = new Vector3(9.0f, randomY, 0.0f);
        Instantiate(kabe, spawnPosition, Quaternion.identity);

        Vector3 spawnPosition2 = new Vector3(9.0f, newrandomY, 0.0f);
        Instantiate(kabe2, spawnPosition2, Quaternion.identity);
    }
}
