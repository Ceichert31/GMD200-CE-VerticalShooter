using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public SpawnManager spawnManager;
    private PowerupSpawner powerupSpawner;
    [SerializeField] private int health;

    private Animator animator;

    private void Start()
    {
        powerupSpawner = GetComponent<PowerupSpawner>();
        animator = GetComponent<Animator>();
    }
    public void TakeDamage()
    {
        health--;
        animator.SetTrigger("Damaged");

        if (health <= 0)
        {
            powerupSpawner.DeterminePowerUP();
            spawnManager.RemoveEnemy(gameObject);
            UIManager.addPoints?.Invoke(150);
            Destroy(gameObject);
        }
    }
}
