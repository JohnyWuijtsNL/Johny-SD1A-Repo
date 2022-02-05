using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerScript_DesignABaby : MonoBehaviour
{
    [SerializeField]
    GameObject[] bodyParts;
    GameObject currentPart;
    ObjectMover_DesignABaby currentScript;
    int partCount = 0;
    bool isBodyMoving = false;
    bool gameOver = false;
    float gameOverTimer = 2;
    AudioSource audioSource;
    [SerializeField]
    AudioClip splat;
    [SerializeField]
    AudioClip whoosh;    
    [SerializeField]
    AudioClip wow;
    [SerializeField]
    AudioClip lost;
    [SerializeField]
    GameObject particles;
    GameManager gameManager;
    [SerializeField]
    bool debug = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.StartGame(7, false, "Design-A-Baby!");
    }

    void Update()
    {
        if (!isBodyMoving)
        {
            if (partCount < bodyParts.Length)
            {
                currentPart = Instantiate(bodyParts[partCount]);
            }
            currentScript = currentPart.GetComponent<ObjectMover_DesignABaby>();
            partCount++;
            isBodyMoving = true;
        }
        if (!currentScript.falling)
        {
            isBodyMoving = false;
        }

        if (gameOver)
        {
            gameOverTimer -= Time.deltaTime;
        }

        if (gameOverTimer < 0 && debug)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void EndGame(bool hasWon)
    {
        if (!hasWon)
        {
            gameManager.SetWon(false);
            PlaySound(3);
        }
        else
        {
            PlaySound(2);
            particles.SetActive(true);
            gameManager.SetWon(true);
        }
        gameOver = true;
    }

    public void PlaySound(int sound)
    {
        switch (sound)
        {
            case 0:
                audioSource.PlayOneShot(splat);
                break;
            case 1:
                audioSource.Stop();
                audioSource.PlayOneShot(whoosh);
                break;
            case 2:
                audioSource.PlayOneShot(wow);
                break;
            case 3:
                audioSource.PlayOneShot(lost);
                break;
        }
    }
}
