using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private BulletManager bulletManager;
    private Animator animator;

    [SerializeField] private float powerUpDuration = 15f;

    private void Awake()
    {
        bulletManager = GetComponent<BulletManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Powerup instance = collision.GetComponent<Powerup>();
            bulletManager.fireState = instance.fireState;
            bulletManager.bulletState = instance.bulletState;
            StartCoroutine(PowerUpDuration(powerUpDuration));
        }
    }
    IEnumerator PowerUpDuration(float waitTime)
    {
        while (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
            yield return null;
        }
        bulletManager.fireState = FireState.single;
        bulletManager.bulletState = BulletState.normal;
    }
}