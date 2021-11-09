using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //list of states the game moves between
    [SerializeField]
    GameObject[] states;

    [SerializeField]
    GameObject board;
    //target transform for the game board
    Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
    Vector3 targetPosition = new Vector3(5.4f, 5.4f, 0);
    Vector3 targetScale = new Vector3(0.3f, 0.3f, 0);

    //variable that tracks which state the game is currently in
    int currentState;
    int currentSection;
    bool gameEnded = false;
    //tracks which sections are already chosen
    bool[] chosenSections = new bool[4];

    //spinner itself
    [SerializeField]
    GameObject spinner;
    //the game object that surrounds the spinner
    //this object actually rotates, while the spinner plays an animation that always leaves it at the location it started in
    [SerializeField]
    GameObject choose;

    [SerializeField]
    TextMeshProUGUI error;
    [SerializeField]
    TextMeshProUGUI spinText;
    [SerializeField]
    TextMeshProUGUI startSpinText;
    [SerializeField]
    TextMeshProUGUI nextText;
    [SerializeField]
    TextMeshProUGUI nextText2;
    [SerializeField]
    TextMeshProUGUI cardText;
    [SerializeField]
    GameObject nextButton;
    [SerializeField]
    GameObject nextButton2;

    //empty card that moves
    [SerializeField]
    GameObject realCard;
    //sprite on the card, that changes throughout the game
    [SerializeField]
    SpriteRenderer anything;
    //lists of all the sprites for the card
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

    //list of bools to track the state of the card
    //is a card currently being grabbed from the deck
    bool cardGrabbed = false;
    //has the card turned to the point that its front side is showing
    bool cardTurned = false;
    //has the front side of the card generated
    bool cardGenerated = false;
    //has the card finished al its animations
    bool cardAppeared = false;
    //is the card already in the correct position when grabbing (skip grab animation and go straight to turning)
    bool grabInPlace = false;
    //is the card an escape card
    bool isEscape = false;
    //timer to time the card animation
    float cardTimer;

    int chosenCard;


    //track which pawns are already on the board, to determine wether or not the user can go to the next state
    bool[] activePawns = new bool[4];
    bool isReady;


    void Start()
    {
        //choose a random section and set the starting state to state 0
        currentSection = Random.Range(0, 4);
        SetActiveState(0);
    }

    void Update()
    {
        //move/rotate/scale the board smoothly to its target transform
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

    //called whenever a sliders position is changed
    public void SetPawnActive(int pawnNumber)
    {
        activePawns[pawnNumber] = true;
        error.text = "";
    }

    //called whenever the "Next" button is pressed
    public void Next()
    {
        //determine what the button should do, based on which state the player is in
        switch (currentState)
        {
            //for state 0 and 1, go to the next state if all pawns are on the board, otherwise, display an error message
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
            //for state 3, start the spinning sequence in state 4 and move to it
            case 3:
                //also sets the spin text to something else, for next time this state appears
                startSpinText.text = "Draai aan het wiel om jullie volgende onderdeel te bepalen!";
                StartCoroutine(WaitForSpin());
                SetActiveState(4);
                spinner.GetComponent<Animator>().SetTrigger("startSpin");
                ChooseSection();
                break;
            //for case 5, either grab a card, remove the card on screen, or change the mirror or joker card into a different card
            case 5:
                //if there is no card on screen, start the card grab animation
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
                //if there is a card on screen...
                else
                {
                    //...make the user able to go to the next section
                    nextButton2.SetActive(true);

                    //if the card on screen is a joker card, change it into an escape card
                    if (chosenCard < 2)
                    {
                        cardText.text = "Werknemer, beantwoord de vraag.";
                        grabInPlace = true;
                    }
                    //if it is a mirror card, change it into a regular card
                    else if (chosenCard < 4)
                    {
                        cardText.text = "Werkgever, beantwoord de vraag.";
                        grabInPlace = true;
                    }
                    //else, ask the user to grab another card, and stop them from being able to go to the next section
                    else
                    {
                        cardText.text = "Pak een kaart van de stapel.";
                        realCard.SetActive(false);
                        cardAppeared = false;
                        nextButton2.SetActive(false);
                    }
                }
                break;
            //for all other states, simply move to the next state
            default:
                SetActiveState(currentState + 1);
                break;
        }

    }

    //disable all states, enable the state that is given and set it as the active one
    public void SetActiveState(int activeState)
    {
        foreach (GameObject state in states)
        {
            state.SetActive(false);
        }
        states[activeState].SetActive(true);
        error.text = "";
        currentState = activeState;

        //change the "Next" button text based on which state it is in
        if (activeState == 3)
        {
            nextText.text = "Draai";
        }
        else
        {
            nextText.text = "Volgende";
        }
    }

    //choose a random section that is not yet chosen, and if there is only one section left, change the functionality of the second button so it will end the game
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

    //wait until the spinning animation has finished, then tell the user which section is chosen, and change the target transform of the board
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

    //grabbing animation, including generating the front side
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

    //same as GrabCard(), except the animation for grabbing the card is skipped and it goes into turning the card immediately
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

    //generate a random front side, corresponding to the correct section, including grabbing an escape card if the previous card was a joker card
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

    //reset the position of the card so it can be grabbed again
    void ResetCardPosition()
    {
        realCard.transform.position = new Vector3(3.96f, 2.58f, 0);
        realCard.transform.eulerAngles = new Vector3(0, 0, 33.5f);
    }

    //change the state back to the spinner so the user can go to a new section
    //if all sections are chosen already, ends the game instead
    //called whenever the second button is pressed
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
