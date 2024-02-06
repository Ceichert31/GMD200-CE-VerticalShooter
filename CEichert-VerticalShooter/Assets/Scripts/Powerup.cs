using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public FireState fireState;
    public BulletState bulletState;

    public string powerUpText;

    private AudioSource source;
    [SerializeField] private AudioClip pickupSound;

    [SerializeField] private float lifeTime = 8;

    [SerializeField] private float fallSpeed = 3;

    private void Start()
    {
        source = GameManager.Instance.player.GetComponent<AudioSource>();
    }
    private void Update()
    {
        //Destroy object if lifetime is exceeded
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            Destroy(gameObject);

        //Subtract y position over time
        Vector2 currentPos = transform.position;
        currentPos.y -= Time.deltaTime * fallSpeed;
        transform.position = new Vector2(currentPos.x, currentPos.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if there is collision with player
        if (collision.gameObject.layer == 6)
        {
            //Add points to the total score
            UIManager.addPoints?.Invoke(50);

            source.PlayOneShot(pickupSound, 0.7f);
            Destroy(gameObject);
        }
            
    }
}
