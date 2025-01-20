
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    AudioManager audioManager;
    Rigidbody2D rb;
    Animator animator;

    public float moveSpeed = 5f;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;

    //cho platform dieu kien va rigidbody
    public bool isOnPlatform;
    public Rigidbody2D platformRb;


    Vector2 RelativeTranform;

    private float speedMultiplier;
    private bool btnPressed;
    private bool isWallTouch;
    private bool facingRight = true;

    

    

    [Range(1f, 10f)]
    [SerializeField] float accleration; //them gia toc

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateRelativeTranform();
    }


    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();
        float targetSpeed = moveSpeed * speedMultiplier * RelativeTranform.x;

        if(isOnPlatform)
        {
            //neu dang tren platform thi cong them voi velocity truc x cua platform
            rb.linearVelocity = new Vector2(targetSpeed+ platformRb.linearVelocity.x, rb.linearVelocity.y);

        }
        else
        {
            rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);

        }

        //kiem tra co va cham voi layer wall khong
        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position , new Vector2(0.12f, 0.9655325f),0, wallLayer);

        if(isWallTouch )
        {
            audioManager.PlaySFX(audioManager.wallTouch);
            Flip();
        }
    }


    private void OnDrawGizmos()
    {
        //ve cho de nhin colider wallcheck
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(wallCheckPoint.position, new Vector3(0.12f, 0.96f, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle") && facingRight == false)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        UpdateRelativeTranform();
    }

    void UpdateRelativeTranform()
    {
        RelativeTranform = transform.InverseTransformVector(Vector2.one); 
    }

    public void move(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            btnPressed = true;
        }
        else if(value.canceled)
        {
            btnPressed= false;
        }
        animator.SetBool("IsRunning",btnPressed);
    }

    public void UpdateSpeedMultiplier()
    {
        if(btnPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime * accleration;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {   
            speedMultiplier -= Time.deltaTime * accleration;
            if(speedMultiplier < 0)
            {
                speedMultiplier = 0;
            }
        }
    }






}
