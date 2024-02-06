using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum FireState
{
    single,
    burst,
    split,
    triple,
}
public class BulletManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.MovementActions playerMovement;

    private InputManager manager;

    private AudioSource audioSource;

    [SerializeField] private AudioClip bulletClip;

    public FireState fireState;

    public BulletState bulletState;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float assistAngle = 30f;

    [SerializeField] private Transform spawnPoint;

    private Vector2 nearEnemy;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerMovement = playerInput.Movement;

        manager = GetComponent<InputManager>();
        audioSource = GetComponent<AudioSource>();        
    }
    private void OnEnable()
    {
        playerMovement.Enable();
        playerMovement.Fire.performed += Shoot;
    }
    private void OnDisable()
    {
        playerMovement.Disable();
        playerMovement.Fire.performed -= Shoot;
    }
    void Shoot(InputAction.CallbackContext ctx)
    {
        if (!manager._canDash)
            return;

        switch (fireState)
        {
            case FireState.single:
                if (activeFireCoroutine == null)
                    activeFireCoroutine = StartCoroutine(Shoot(0.2f, 1, 0.1f));
                break;

            case FireState.burst:
                if (activeFireCoroutine == null)
                    activeFireCoroutine = StartCoroutine(HomingShot(0.1f, 3, 0.4f));
                break;
        }

        

    }

    Coroutine activeFireCoroutine = null;
    IEnumerator Shoot(float bulletDelay, int bulletNumber, float fireDelay)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            Bullet bulletInstance = Instantiate(bullet, spawnPoint.position, new(0, 0, 0, 0)).GetComponent<Bullet>();
            audioSource.PlayOneShot(bulletClip, 0.4f);
            bulletInstance.hitLayer = 7;

            bulletInstance.direction = Vector2.up;

            yield return new WaitForSeconds(bulletDelay);
        }
        yield return new WaitForSeconds(fireDelay);
        activeFireCoroutine = null;
    }

    IEnumerator HomingShot(float bulletDelay, int bulletNumber, float fireDelay)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            Bullet bulletInstance = Instantiate(bullet, spawnPoint.position, new(0, 0, 0, 0)).GetComponent<Bullet>();
            audioSource.PlayOneShot(bulletClip, 0.4f);
            bulletInstance.hitLayer = 7;

            //Aim Assist
            //Find nearest enemy
            if (FindObjectOfType<EnemyBullet>() != null)
                nearEnemy = FindObjectOfType<EnemyBullet>().transform.position;
                //Get players position
                //playerPos = new(transform.position.x, transform.position.y);
                Vector2 playerPos = spawnPoint.position.normalized;
                //Find the direction of the enemy and normalize it
                Vector2 enemyDirection = (nearEnemy - playerPos).normalized;
                //Get the angle between the two vectors
                float enemyDot = Vector2.Dot(playerPos, enemyDirection);

                if (enemyDot > -0.8f && enemyDot < 0.8f)
                    bulletInstance.direction = enemyDirection;
                else
                    bulletInstance.direction = Vector2.up;

            yield return new WaitForSeconds(bulletDelay);
        }
        yield return new WaitForSeconds(fireDelay);
        activeFireCoroutine = null;
    }
}
