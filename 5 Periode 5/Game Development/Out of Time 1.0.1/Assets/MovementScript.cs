using UnityEngine;

public class MovementScript : MonoBehaviour
{

    Rigidbody2D playerRB;
    AudioSource audioSource;
    [SerializeField]
    float playerSpeed = 5;
    [SerializeField]
    float jumpForce = 10f;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadiusX;
    public float checkGroundRadiusY;
    public LayerMask groundLayer;
    public AudioClip hit;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapBox(isGroundedChecker.position, new Vector2(checkGroundRadiusX, checkGroundRadiusY), 0, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }

        playerRB.velocity = new Vector2((Input.GetAxisRaw("Horizontal") * playerSpeed), playerRB.velocity.y);
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hit);
    }
}
