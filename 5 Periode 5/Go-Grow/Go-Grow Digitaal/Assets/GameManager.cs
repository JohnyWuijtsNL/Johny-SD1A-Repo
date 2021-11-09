using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] states;
    int currentState;
    TextMeshProUGUI error;

    GameObject spinner;
    GameObject choose;
    TextMeshProUGUI spinText;
    TextMeshProUGUI startSpinText;
    TextMeshProUGUI nextText;
    TextMeshProUGUI nextText2;
    GameObject nextButton;
    GameObject nextButton2;
    [SerializeField]
    GameObject board;
    Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
    Vector3 targetPosition = new Vector3(5.4f, 5.4f, 0);
    Vector3 targetScale = new Vector3(0.3f, 0.3f, 0);

    [SerializeField]
    GameObject realCard;
    [SerializeField]
    SpriteRenderer anything;
    [SerializeField]
    Sprite[] backsides;
    [SerializeField]
    Sprite[] know;
    [SerializeField]
    Sprite[] flow;
    [SerializeField]
    Sprite[] glow;
    [SerializeField]
    Sprite[] grow;
    [SerializeField]
    Sprite[] escape;
    bool cardGrabbed = false;
    bool cardTurned = false;
    bool cardGenerated = false;
    bool cardAppeared = false;
    bool grabInPlace;
    bool isEscape = false;
    float cardTimer;
    int chosenCard;
    TextMeshProUGUI cardText;
    bool gameEnded = false;

    bool[] activePawns = new bool[4];
    bool isReady;

    int currentSection;
    bool[] chosenSections = new bool[4];

    void Start()
    {
        error = GameObject.Find("Error").GetComponent<TextMeshProUGUI>();
        spinner = GameObject.Find("spinner");
        choose = GameObject.Find("choose");
        spinText = GameObject.Find("spin text").GetComponent<TextMeshProUGUI>();
        startSpinText = GameObject.Find("startSpinText").GetComponent<TextMeshProUGUI>();
        nextText = GameObject.Find("NextText").GetComponent<TextMeshProUGUI>();
        nextText2 = GameObject.Find("NextText2").GetComponent<TextMeshProUGUI>();
        nextButton = GameObject.Find("nextButton");
        nextButton2 = GameObject.Find("nextButton2");
        nextButton2.SetActive(false);
        cardText = GameObject.Find("cardText").GetComponent<TextMeshProUGUI>();
        currentSection = Random.Range(0, 4);
        SetActiveState(0);
    }

    void Update()
    {
        board.transform.rotation = Quaternion.RotateTowards(board.transform.rotation, targetRotation, 360 * Time.deltaTime);
        board.transform.position = Vector3.Lerp(board.transform.position, targetPosition, 5 * Time.deltaTime);
        board.transform.localScale = Vector3.Lerp(board.transform.localScale, targetScale, 5 * Time.deltaTime);

        if (cardGrabbed)
        {
            GrabCard();
        }
        else if (grabInPlace)
        {
            GrabCardInPlace();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetPawnActive(int pawnNumber)
    {
        activePawns[pawnNumber] = true;
        error.text = "";
    }

    public void Next()
    {
        switch (currentState)
        {
            case 0: case 1:
                isReady = true;
                foreach (bool activePawn in activePawns)
                {
                    if (!activePawn)
                    {
                        isReady = false;
                    }
                }

                if (isReady)
                {
                    SetActiveState(currentState + 1);
                    int i = 0;
                    while (i < activePawns.Length)
                    {
                        activePawns[i] = false;
                        i++;
                    }
                }
                else
                {
                    error.text = "Nog niet alle posities zijn gekozen!";
                }
                break;
            case 3:
                startSpinText.text = "Draai aan het wiel om jullie volgende onderdeel te bepalen!";
                StartCoroutine(WaitForSpin());
                SetActiveState(4);
                spinner.GetComponent<Animator>().SetTrigger("startSpin");
                ChooseSection();
                break;
            case 5:
                if (!cardAppeared)
                {
                    nextButton.SetActive(false);
                    nextButton2.SetActive(false);
                    realCard.SetActive(true);
                    ResetCardPosition();
                    anything.sprite = backsides[currentSection];
                    cardGrabbed = true;
                    cardTimer = 1;
                }
                else
                {
                    nextButton2.SetActive(true);
                    if (chosenCard < 2)
                    {
                        cardText.text = "Werknemer, beantwoord de vraag.";
                        grabInPlace = true;
                    }
                    else if (chosenCard < 4)
                    {
                        cardText.text = "Werkgever, beantwoord de vraag.";
                        grabInPlace = true;
                    }
                    else
                    {
                        cardText.text = "Pak een kaart van de stapel.";
                        realCard.SetActive(false);
                        cardAppeared = false;
                        nextButton2.SetActive(false);
                    }
                }

                break;
            default:
                SetActiveState(currentState + 1);
                break;
        }

    }

    public void SetActiveState(int activeState)
    {
        foreach (GameObject state in states)
        {
            state.SetActive(false);
        }
        states[activeState].SetActive(true);
        error.text = "";
        currentState = activeState;
        if (activeState == 3)
        {
            nextText.text = "Draai";
        }
        else
        {
            nextText.text = "Volgende";
        }
    }

    public void ChooseSection()
    {
        while (chosenSections[currentSection] && !gameEnded)
        {
            currentSection = Random.Range(0, 4);
        }
        chosenSections[currentSection] = true;
        gameEnded = true;
        foreach (bool chosenSection in chosenSections)
        {
            if (!chosenSection)
            {
                gameEnded = false;
            }
        }
        if (gameEnded)
        {
            nextText2.text = "Sluit spel af";
        }
        choose.transform.eulerAngles = new Vector3(0, 0, Random.Range(1, 90) - 90 * currentSection);
    }

    IEnumerator WaitForSpin()
    {
        nextButton.SetActive(false);
        nextButton2.SetActive(false);
        spinText.text = "";
        yield return new WaitForSeconds(4);
        nextButton.SetActive(true);
        switch (currentSection)
        {
            case 0:
                spinText.text = "Know!";
                targetRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                spinText.text = "Flow!";
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                spinText.text = "Glow!";
                targetRotation = Quaternion.Euler(0, 0, 180);
                break;
            case 3:
                spinText.text = "Grow!";
                targetRotation = Quaternion.Euler(0, 0, -90);
                break;

        }
        targetPosition = new Vector3(10.8f, 0, 0);
        targetScale = new Vector3(0.6f, 0.6f, 0);
    }

    void GrabCard()
    {
        if (cardAppeared)
        {
            cardGrabbed = false;
            cardGenerated = false;
            cardTurned = false;
            return;
        }
        if (realCard.transform.position.x < 15)
        {
            realCard.transform.Translate(20 * Time.deltaTime, 0, 0);
        }
        else
        {
            realCard.transform.position = new Vector3(15, realCard.transform.position.y, 0);
        }
        if (realCard.transform.position.y < 5.5f)
        {
            realCard.transform.Translate(0, 10 * Time.deltaTime, 0);
        }
        else
        {
            realCard.transform.position = new Vector3(realCard.transform.position.x, 5.5f, 0);
        }
        realCard.transform.rotation = Quaternion.RotateTowards(realCard.transform.rotation, Quaternion.Euler(0, 0, 0), 90 * Time.deltaTime);
        cardTimer -= Time.deltaTime;
        if (cardTimer < 0)
        {
            if (!cardTurned)
            {
                realCard.transform.localScale -= new Vector3(5 * Time.deltaTime, 0, 0);
                if (realCard.transform.localScale.x < 0)
                {
                    cardTurned = true;
                }
            }
            else if (realCard.transform.localScale.x < 0.6)
            {
                realCard.transform.localScale += new Vector3(5 * Time.deltaTime, 0, 0);
                GenerateCard(false);
            }
            else
            {
                realCard.transform.localScale = new Vector3(0.6f, 0.6f, 0);
                if (chosenCard < 2)
                {
                    cardText.text = "Je hebt een joker kaart getrokken, dit betekend dat je een escape kaart mag trekken.";
                    isEscape = true;
                }
                else if (chosenCard < 4)
                {
                    cardText.text = "Je hebt een spiegelkaart getrokken, dit betekend dat de volgende vraag beantwoord wordt door de werkgever.";
                }
                else
                {
                    cardText.text = "Werknemer, beantwoord de vraag.";
                    nextButton2.SetActive(true);
                }
                cardAppeared = true;
                nextButton.SetActive(true);
            }
        }
    }

    void GrabCardInPlace()
    {
        if (!cardTurned)
        {
            realCard.transform.localScale -= new Vector3(5 * Time.deltaTime, 0, 0);
            if (realCard.transform.localScale.x < 0)
            {
                cardTurned = true;
            }
        }
        else if (realCard.transform.localScale.x < 0.6)
        {
            realCard.transform.localScale += new Vector3(5 * Time.deltaTime, 0, 0);
            GenerateCard(true);
        }
        else
        {
            realCard.transform.localScale = new Vector3(0.6f, 0.6f, 0);
            cardAppeared = true;
            cardTurned = false;
            cardGenerated = false;
            grabInPlace = false;
        }
    }

    void GenerateCard(bool isInPlace)
    {
        if (cardGenerated)
        {
            return;
        }
        if (isEscape)
        {
            anything.sprite = escape[Random.Range(0, escape.Length)];
            isEscape = false;
            chosenCard = 100;
        }
        else
        {
            int index = 0;
            if (isInPlace)
            {
                index = 4;
            }
            switch (currentSection)
            {
                case 0:
                    chosenCard = Random.Range(index, know.Length);
                    anything.sprite = know[chosenCard];
                    break;
                case 1:
                    chosenCard = Random.Range(index, flow.Length);
                    anything.sprite = flow[chosenCard];
                    break;
                case 2:
                    chosenCard = Random.Range(index, glow.Length);
                    anything.sprite = glow[chosenCard];
                    break;
                case 3:
                    chosenCard = Random.Range(index, grow.Length);
                    anything.sprite = grow[chosenCard];
                    break;
            }
        }
        cardGenerated = true;
    }

    void ResetCardPosition()
    {
        realCard.transform.position = new Vector3(3.96f, 2.58f, 0);
        realCard.transform.eulerAngles = new Vector3(0, 0, 33.5f);
    }

    public void NextPart()
    {
        if (gameEnded)
        {
            SetActiveState(6);
            nextButton.SetActive(false);
            targetPosition = new Vector3(5.4f, 5.4f, 0);
            targetScale = new Vector3(0.3f, 0.3f, 0);
        }
        else
        {
            SetActiveState(3);
        }
        realCard.SetActive(false);
        nextButton2.SetActive(false);
        cardAppeared = false;
        chosenCard = 100;
        cardText.text = "Pak een kaart van de stapel.";

    }
}
