using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public InputSystem_Actions actions;

    public float speed;

    public float jumpforce;

    Animator animator;

    float move;

    bool grounded = true;

    bool is_attack = false;

    void Awake()
    {
        actions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += Movement;
        actions.Player.Jump.performed += Jumping;
        actions.Player.Attack.performed += Attack;

        actions.Player.Move.canceled += Movement;
        actions.Player.Jump.canceled += Jumping;
        actions.Player.Attack.canceled += Attack;
    }

    void OnDisable()
    {
        actions.Player.Disable();
        actions.Player.Move.performed -= Movement;
        actions.Player.Jump.performed -= Jumping;
        actions.Player.Attack.performed -= Attack;

        actions.Player.Move.canceled -= Movement;
        actions.Player.Jump.canceled -= Jumping;
        actions.Player.Attack.canceled -= Attack;
    }

    void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetBool("isAttack", true);
        }
        if (ctx.canceled)
        {
            animator.SetBool("isAttack", false);
        }
    }
    void Movement(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>().x;
    }

    void Jumping(InputAction.CallbackContext ctx)
    {
        if (grounded)
        {
            rb.linearVelocityY = jumpforce;
            grounded = false;
        }
    
    }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            grounded = true;
        }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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