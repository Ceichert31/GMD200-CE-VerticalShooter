using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnedEnemyList;
    [SerializeField] private List<EnemyScriptable> spawnPool = new();

    private void Update()
    {
        if (spawnedEnemyList.Count <= 0)
            SpawnEnemies(3);
    }

    void SpawnEnemies(int enemyNumber)
    {
        spawnedEnemyList = new List<GameObject>();
        for (int i = 0; i <= enemyNumber; i++)
        {
            DetermineEnemy();
        }
    }
    void DetermineEnemy()
    {
        int randomNumber = Random.Range(0, 100);
        List<EnemyScriptable> randomEnemyList = new();
        foreach (EnemyScriptable enemy in spawnPool)
        {
            if (randomNumber <= enemy.spawnRarity)
                randomEnemyList.Add(enemy);
        }
        if (randomEnemyList.Count > 0)
        {
            //Randomly select an enemy from the spawn pool
            EnemyScriptable selectedEnemy = randomEnemyList[Random.Range(0, randomEnemyList.Count)];
            GameObject spawnedEnemy = Instantiate(selectedEnemy.enemyPrefab);

            //Get the enemy health and assign this as the spawnmanager
            EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
            enemyHealth.spawnManager = this;

            spawnedEnemyList.Add(spawnedEnemy);
        }
    }
    public void RemoveEnemy(GameObject destroyedEnemy)
    {
        spawnedEnemyList.Remove(destroyedEnemy);
        spawnedEnemyList.TrimExcess();
    }
}
