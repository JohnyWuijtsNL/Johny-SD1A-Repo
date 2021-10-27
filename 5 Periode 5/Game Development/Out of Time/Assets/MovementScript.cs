using UnityEngine;

public class MovementScript : MonoBehaviour
{

    Rigidbody2D playerRB;
    [SerializeField]
    float playerSpeed = 5;
    float jumpForce = 5;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
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

        playerRB.velocity = new Vector2((Input.GetAxis("Horizontal") * playerSpeed), playerRB.velocity.y);
        


    }
}
