using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public FireState fireState;
    public BulletState bulletState;

    public string powerUpText;

    [SerializeField] private float fallSpeed = 3;
    private void Update()
    {
        Vector2 currentPos = transform.position;
        currentPos.y -= Time.deltaTime * fallSpeed;
        transform.position = new Vector2(currentPos.x, currentPos.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            UIManager.addPoints?.Invoke(50);
            Destroy(gameObject);
        }
            
    }
}
