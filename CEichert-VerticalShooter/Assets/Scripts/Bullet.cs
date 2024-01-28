using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletState
{
    normal,
    homing,
}
public class Bullet : MonoBehaviour
{


    [SerializeField] protected BulletState bulletState;

    private Rigidbody2D rb;

    public float
        bulletSpeed,
        lifeTime;

    public LayerMask hitLayer;

    [HideInInspector] public Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (bulletState)
        {
            case BulletState.normal:
                rb.velocity = bulletSpeed * direction;

                lifeTime -= Time.deltaTime;
                if (lifeTime <= 0)
                    Destroy(gameObject);
                break;

            case BulletState.homing:
                break;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == hitLayer)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
