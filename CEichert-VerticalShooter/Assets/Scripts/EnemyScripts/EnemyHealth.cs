using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public SpawnManager spawnManager;
    private PowerupSpawner powerupSpawner;
    [SerializeField] private int health;

    private void Start()
    {
        powerupSpawner = GetComponent<PowerupSpawner>();
    }
    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            powerupSpawner.DeterminePowerUP();
            spawnManager.RemoveEnemy(gameObject);
            UIManager.addPoints?.Invoke(150);
            Destroy(gameObject);
        }
    }
}
