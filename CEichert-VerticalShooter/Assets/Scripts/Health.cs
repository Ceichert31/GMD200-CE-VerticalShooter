using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private int health;
    private bool isPlayer;
    private void Awake()
    {
        if (GetComponent<InputManager>() != null)
        {
            isPlayer = true;
            inputManager = GetComponent<InputManager>();
        }
            
    }
    public void TakeDamage()
    {
        health--;

        if (isPlayer)
        {
            StartCoroutine(DamageDelay(5, 0.1f));
        }

        if (isPlayer && health <= 0)
        {
            inputManager.enabled = false;
            TimeControl.timeControl?.Invoke();
        }

        if (health <= 0 && !isPlayer)
            Destroy(gameObject);
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
