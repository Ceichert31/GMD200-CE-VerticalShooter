using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum FireState
{
    single,
    burst,
    spread,
    split,
    triple,
}
public class BulletManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.MovementActions playerMovement;

    private InputManager manager;

    [SerializeField] private FireState fireState;

    [SerializeField] private GameObject bullet;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerMovement = playerInput.Movement;

        manager = GetComponent<InputManager>();
        
        
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
                    activeFireCoroutine = StartCoroutine(Shoot(0.1f, 3, 0.4f));
                break;
        }
    }

    Coroutine activeFireCoroutine = null;
    IEnumerator Shoot(float bulletDelay, int bulletNumber, float fireDelay)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            Vector2 spawnPosition = new(transform.position.x, transform.position.y + 0.6f);
            Bullet bulletInstance = Instantiate(bullet, spawnPosition, new(0, 0, 0, 0)).GetComponent<Bullet>();
            bulletInstance.direction = Vector2.up;
            bulletInstance.hitLayer = 7;
            yield return new WaitForSeconds(bulletDelay);
        }
        yield return new WaitForSeconds(fireDelay);
        activeFireCoroutine = null;
    }
}
