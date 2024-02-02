using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    public void TakeDamage()
    {
        health--;

        StartCoroutine(DamageDelay(5, 0.1f));

        if (health <= 0)
        {
            TimeControl.timeControl?.Invoke();
            UIManager.enable?.Invoke();
        }
    }

    IEnumerator DamageDelay(int invFrames, float flashInterval)
    {
        SpriteRenderer playerRender = GetComponent<SpriteRenderer>();
        gameObject.layer = 0;
        for (int i = 0; i <= invFrames; i++)
        {
            playerRender.enabled = !playerRender.enabled;
            yield return new WaitForSeconds(flashInterval);
        }
        gameObject.layer = 6;
    }
}
