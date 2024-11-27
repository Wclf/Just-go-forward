using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody2D rb;

    public float moveSpeed = 5f;

    private Vector2 moveValue;

    private void Start()
    {

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rb = GetComponent<Rigidbody2D>();
   
    }

    private void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveValue.x, 0);
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y );
    }



}
