using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField] private float moveTime = 3;

    private bool isCharging;

    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging)
            return;
        transform.position = new Vector2(GameManager.Instance.player.position.x, 9);
    }
    /// <summary>
    /// Sends ant enemy at player, Called by Animator
    /// </summary>
    private void Charge()
    {
        isCharging = true;
        Vector2 endPos = new Vector2(transform.position.x, -15);
        StartCoroutine(MoveDown(endPos, moveTime));
    }
    IEnumerator MoveDown(Vector2 endPos, float duration)
    {
        float time = 0;
        Vector2 startPos = transform.position;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        enemyHealth.TakeDamage();
        UIManager.addPoints?.Invoke(50);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            collision.gameObject.GetComponent<Health>().TakeDamage();
    }
}
