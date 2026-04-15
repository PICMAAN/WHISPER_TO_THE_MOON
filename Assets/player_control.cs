using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public InputSystem_Actions actions;

    public float speed;

    public float jumpforce;

    float move;

    void Awake()
    {
        actions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += Movement;
        actions.Player.Jump.performed += Jumping;

        actions.Player.Move.canceled += Movement;
        actions.Player.Jump.canceled += Jumping;
    }

    void OnDisable()
    {
        actions.Player.Disable();
        actions.Player.Move.performed -= Movement;
        actions.Player.Jump.performed -= Jumping;

        actions.Player.Move.canceled -= Movement;
        actions.Player.Jump.canceled -= Jumping;
    }

    void Movement(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>().x;
    }

    void Jumping(InputAction.CallbackContext ctx)
    {
        rb.linearVelocityY = jumpforce;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocityX = move * speed;
        flip();
    }

    void flip ()
    {
        if (rb.linearVelocityX > 0.1f)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (rb.linearVelocityX < -0.1f)
            GetComponent<SpriteRenderer>().flipX = true;
    }
}
