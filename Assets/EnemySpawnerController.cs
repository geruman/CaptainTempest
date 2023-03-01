using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] GameObject enemy1prefab,enemy2prefab;
    private bool spawnEnemy1, spawnEnemy2, spawnEnemy3, spawnEnemy4 = false;
    private float enemyY = 0;
    private float enemyY2 = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spawnEnemy1 = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            spawnEnemy2 = true;
        }
    }
    private void FixedUpdate()
    {
        if (spawnEnemy1)
        {
            SpawnEnemy1();
        }
        if (spawnEnemy2)
        {
            SpawnEnemy2();
        }
    }

    private void SpawnEnemy2()
    {
        
        GameObject enemy = Instantiate(enemy2prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x, transform.position.y + enemyY2);
        enemy = Instantiate(enemy2prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x + 1, transform.position.y + enemyY2);
        enemy = Instantiate(enemy2prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x + 2, transform.position.y + enemyY2);
        enemy = Instantiate(enemy2prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x + 3, transform.position.y + enemyY2);
        spawnEnemy2 = false;
        if (enemyY2 == 0)
        {
            enemyY2 = 3;
        }
        else if (enemyY2 == 3)
        {
            enemyY2 = -3;
        }
        else
        {
            enemyY2 = 0;
        }
    }

    private void SpawnEnemy1()
    {
        GameObject enemy = Instantiate(enemy1prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x, transform.position.y + enemyY);
        enemy = Instantiate(enemy1prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x + 1, transform.position.y + enemyY);
        enemy = Instantiate(enemy1prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x + 2, transform.position.y + enemyY);
        enemy = Instantiate(enemy1prefab, transform.position, Quaternion.identity);
        enemy.transform.position = new Vector2(transform.position.x + 3, transform.position.y + enemyY);
        spawnEnemy1 = false;
        if (enemyY == 0)
        {
            enemyY = 3;
        }
        else if (enemyY == 3)
        {
            enemyY = -3;
        }
        else
        {
            enemyY = 0;
        }
    }
}
