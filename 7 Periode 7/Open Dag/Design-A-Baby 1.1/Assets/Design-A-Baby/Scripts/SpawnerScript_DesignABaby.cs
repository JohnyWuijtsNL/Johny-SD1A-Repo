using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    [SerializeField]
    TextMeshProUGUI attemptsText, scoreText;
    int attempts, score;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.StartGame(7, false, "Design-A-Baby!");

        if (PlayerPrefs.HasKey("Attempts"))
        {
            attempts = PlayerPrefs.GetInt("Attempts");
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            attempts = 0;
            score = 0;
        }

        UpdateScore();
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
        attempts++;
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
            score++;
        }
        gameOver = true;
        PlayerPrefs.SetInt("Attempts", attempts);
        PlayerPrefs.SetInt("Score", score);
        UpdateScore();
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

    void UpdateScore()
    {
        attemptsText.text = "Attempts: " + attempts;
        scoreText.text = "Babies created: " + score;
    }
}
