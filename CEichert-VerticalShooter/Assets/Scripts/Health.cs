using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
}
