using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public SpawnManager spawnManager;
    [SerializeField] private int health;
    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            spawnManager.RemoveEnemy(gameObject);
            UIManager.addPoints?.Invoke(150);
            Destroy(gameObject);
        }
    }
}
