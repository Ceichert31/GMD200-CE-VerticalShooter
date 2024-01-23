using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.MovementActions playerMovement;

    private Rigidbody2D rb;

    private Animator anim;

    private Vector2 moveDirection;

    [SerializeField] private float 
        dampForce = 5f,
        speed,
        dashForce;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        playerMovement = playerInput.Movement;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        playerMovement.Enable();
        playerMovement.Extra.performed += Dash;
    }
    private void OnDisable()
    {
        playerMovement.Disable();
        playerMovement.Extra.performed -= Dash;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Slow down the player by dampening movement
        if (!playerMovement.Move.IsInProgress() && rb.velocity.magnitude > 0)
            rb.velocity -= dampForce * Time.deltaTime * rb.velocity;

        rb.AddForce(speed * MoveDirection(), ForceMode2D.Force);
    }
    Vector2 MoveDirection()
    {
        moveDirection = playerMovement.Move.ReadValue<Vector2>();
        return moveDirection;
    }

    void Dash(InputAction.CallbackContext ctx)
    {
        rb.AddForce(dashForce * MoveDirection(), ForceMode2D.Impulse);
        anim.SetTrigger("Dash");
    }
   
}
