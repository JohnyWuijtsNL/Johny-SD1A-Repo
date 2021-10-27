using UnityEngine;
using UnityEngine.SceneManagement;

public class CroissantScript_FlappyCroissant : MonoBehaviour
{
    float yVel = 0;
    [SerializeField]
    float jumpHeight;
    float gravity = 75;
    [SerializeField]
    GameObject croissant;
    bool lost = false;
    AudioSource audioSource;
    [SerializeField]
    AudioClip flap;
    [SerializeField]
    AudioClip bounce;
    [SerializeField]
    AudioClip splat;
    GameManager gameManager;
    [SerializeField]
    bool debug = false;
    [SerializeField]
    GameObject camera;
    Vector3 cameraRotation;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.StartGame(7, true, "Flappy Croissant");
        audioSource = GetComponent<AudioSource>();
        cameraRotation = camera.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lost)
        {
            if (camera != null)
            {
                camera.transform.LookAt(transform.position * 0.2f);
                camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, cameraRotation.y, cameraRotation.z);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVel = jumpHeight;
                audioSource.PlayOneShot(flap);
            }

            croissant.transform.eulerAngles = new Vector3(0, 0, yVel * 2);
            transform.Translate(0, yVel * Time.deltaTime, 0);
            yVel -= gravity * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            GetComponent<Rigidbody>().useGravity = true;
            lost = true;
            GetComponent<Rigidbody>().AddForce(-25, 0, 0, ForceMode.Impulse);
            audioSource.PlayOneShot(bounce);
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (debug)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                gameManager.SetWon(false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(splat);
        }
    }
}
