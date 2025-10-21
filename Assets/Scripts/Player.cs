using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Vectores")]
    public Vector2 playerVelocity;
    public Vector2 jumpVelocity;    
    public Vector2 input=Vector2.zero;

    [Header("Movimiento")]
    public float speed = 5;
    private Rigidbody2D rb2d;
    private float moveInput;
    public float Jump = 4;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask graundLayer;
    private Animator anim;

    [Header("Inventario")]
    public int collectibles = 0;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    [System.Obsolete]
    public void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb2d.linearVelocity = new Vector2(moveInput * speed, rb2d.velocity.y);

        if (moveInput != 0)
        {
            float baseScaleX = Mathf.Abs(transform.localScale.x);
            float currentScaleY = transform.localScale.y;
            float currentScaleZ = transform.localScale.z;
            float newScaleX = baseScaleX * Mathf.Sign(moveInput);
            transform.localScale = new Vector3(newScaleX, currentScaleY, currentScaleZ);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, Jump);
        }

        anim.SetFloat("walk", Mathf.Abs(moveInput));
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, graundLayer);
    }

    public void AddCollectible(int amount)
    {
        collectibles += amount;
    }
}
