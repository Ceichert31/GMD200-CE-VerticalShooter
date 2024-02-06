using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletState
{
    normal,
    homing,
}
public class EnemyManager : MonoBehaviour
{

    [SerializeField] private FireState fireState;
    [SerializeField] private BulletState bulletState;

    [SerializeField]
    private float fireRate;

    private float waitTime;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject bullet;

    private void Start()
    {
        waitTime = fireRate;
    }
    void Update()
    {
        waitTime -= Time.deltaTime;

        if (waitTime <= 0)
        {
            waitTime = fireRate;

            Fire();
        }
    }

    private void Fire()
    {
        switch (fireState)
        {
            case FireState.single:
                if (activeFireCoroutine == null)
                    activeFireCoroutine = StartCoroutine(Shoot(0.2f, 1, 0.1f));
                break;

            case FireState.burst:
                if (activeFireCoroutine == null)
                    activeFireCoroutine = StartCoroutine(Shoot(0.1f, 3, 0.4f));
                break;
            case FireState.split:
                if (activeFireCoroutine == null)
                {
                    activeFireCoroutine = StartCoroutine(Shoot(0.1f, 1, 0.4f));
                    activeFireCoroutine = StartCoroutine(Shoot(0.1f, 1, 0.4f));
                }
                break;
        }
    }

    Coroutine activeFireCoroutine = null;
    IEnumerator Shoot(float bulletDelay, int bulletNumber, float fireDelay)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            //Vector2 spawnPosition = new(transform.position.x, transform.position.y + 0.6f);
            Quaternion spawnRotation = new Quaternion(transform.rotation.x, transform.rotation.y, 180, transform.rotation.w);
            EnemyBullet bulletInstance = Instantiate(bullet, spawnPoint.position, spawnRotation).GetComponent<EnemyBullet>();
            bulletInstance.direction = (GameManager.Instance.player.position - transform.position).normalized;
            bulletInstance.hitLayer = 6;
        
            if (bulletState == BulletState.homing)
            {
                bulletInstance.bulletState = BulletState.homing;
            }
            yield return new WaitForSeconds(bulletDelay);
        }
        yield return new WaitForSeconds(fireDelay);
        activeFireCoroutine = null;
    }
}
