using System;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class TV_QuizScript : MonoBehaviour
{
    //start or pause game
    public bool startGame = false;
    bool gameStarted = false;

    //text for the different options. maps directly to objects[]
    [SerializeField] string[] options;
    //sprites for the different objects. maps directly to options[]
    [SerializeField] VideoClip[] videos;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite correctSprite;

    //sprite renderer for showing objects
    [SerializeField] VideoPlayer videoRenderer;
    [SerializeField] SpriteRenderer spriteRenderer;

    //the 3 buttons
    [SerializeField] GameObject[] optionButtons;

    //score and timer text
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;

    //to check if an object was chosen or not
    bool[] wasChosen;

    //for tracking the 3 options along with their indexes
    string[] chosenOptions = new string[3];
    int[] chosenOptionNumbers = new int[3];

    //tracking the correct option
    int correctOptionNumber;

    float timer = 10;
    float scorer;
    float timeLimit = 10;
    float pauseTimer = 0;
    float pauseTime = 1;
    bool pausing = false;

    //is true when all objects have appeared
    public bool gameOver = false;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource staticSource;
    [SerializeField] AudioClip[] correct;
    [SerializeField] AudioClip[] wrong;


    // Start is called before the first frame update
    void Start()
    {
        wasChosen = new bool[options.Length];
        staticSource.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && startGame)
        {
            timer -= Time.deltaTime;
            staticSource.volume = timer / 20;
            if (pauseTimer > 0)
            {
                pausing = true;
                pauseTimer -= Time.deltaTime;
                return;
            }
            else if (pausing)
            {
                spriteRenderer.sprite = null;
                timer = timeLimit;
                generateGame();
                foreach (var button in optionButtons)
                {
                    button.GetComponent<Image>().color = Color.white;
                }
                pausing = false;
            }
            if (!gameStarted)
            {
                generateGame();
                gameStarted = true;
            }
            if (timer < 0)
            {
                optionButtons[correctOptionNumber].GetComponent<Image>().color = Color.green;
                spriteRenderer.sprite = correctSprite;
                pauseTimer = pauseTime;
            }
            else
            {
                time.text = "Time: " + Math.Round(timer, 2);
            }
        }
        else if (gameOver)
        {
            time.text = "";
        }
    }

    public void buttonPressed(int buttonNumber)
    {
        if (!gameOver)
        {
            if (buttonNumber == correctOptionNumber)
            {
                scorer += timer;
                score.text = "Score: " + Mathf.Round(scorer * 100);
                audioSource.PlayOneShot(correct[UnityEngine.Random.Range(0, correct.Length)]);
            }
            else
            {
                optionButtons[buttonNumber].GetComponent<Image>().color = Color.red;
                audioSource.PlayOneShot(wrong[UnityEngine.Random.Range(0, wrong.Length)]);
            }
        }
        timer = 0;
    }

    void generateGame()
    {
        int optionsLeft = 0;
        for (int i = 0; i < wasChosen.Length; i++)
        {
            if (!wasChosen[i]) optionsLeft++;
        }
        if (optionsLeft == 0)
        {
            chosenOptions = new string[] { "nice", "nice", "nice" };
            chosenOptionNumbers = new int[] { -1, -1, -1 };
            gameOver = true;
            staticSource.volume = 0;
            videoRenderer.Stop();
            return;
        }
        else
        {
            bool objectAvailable = false;
            while (!objectAvailable)
            {
                chosenOptionNumbers[0] = UnityEngine.Random.Range(0, options.Length);
                chosenOptions[0] = options[chosenOptionNumbers[0]];
                chosenOptions[1] = chosenOptions[0];
                while (chosenOptions[1] == chosenOptions[0])
                {
                    chosenOptionNumbers[1] = UnityEngine.Random.Range(0, options.Length);
                    chosenOptions[1] = options[chosenOptionNumbers[1]];
                }
                chosenOptions[2] = chosenOptions[0];
                while (chosenOptions[2] == chosenOptions[0] || chosenOptions[2] == chosenOptions[1])
                {
                    chosenOptionNumbers[2] = UnityEngine.Random.Range(0, options.Length);
                    chosenOptions[2] = options[chosenOptionNumbers[2]];
                }
                for (int i = 0; i < chosenOptionNumbers.Length; i++)
                {
                    if (!wasChosen[chosenOptionNumbers[i]])
                    {
                        objectAvailable = true;
                    }
                }
            }

        }


        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponent<TV_OptionScript>().ChangeOption(chosenOptions);
        }

        if (chosenOptionNumbers[0] != -1)
        {
            correctOptionNumber = -1;
            while (correctOptionNumber == -1 || wasChosen[chosenOptionNumbers[correctOptionNumber]])
            {
                correctOptionNumber = UnityEngine.Random.Range(0, chosenOptions.Length);
            }
            wasChosen[chosenOptionNumbers[correctOptionNumber]] = true;
        }

        string correctOption = chosenOptions[correctOptionNumber];
        correctSprite = sprites[chosenOptionNumbers[correctOptionNumber]];
        ChangeObject(chosenOptionNumbers[correctOptionNumber]);


        
    }

    public void ChangeObject(int objectNumber)
    {
        if (objectNumber == -1)
        {
            Debug.Log("wow");
            videoRenderer.Play();
        }
        else
        {
            videoRenderer.clip = videos[objectNumber];
            videoRenderer.Play();
        }
    }
}
