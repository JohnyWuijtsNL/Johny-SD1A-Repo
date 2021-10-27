using UnityEngine;

public class MovementScript : MonoBehaviour
{

    Rigidbody playerRB;
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
        playerRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collider = Physics.OverlapBox(isGroundedChecker.position, new Vector3(checkGroundRadiusX, checkGroundRadiusY, 1), Quaternion.identity, groundLayer);
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

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(hit);
    }
}
