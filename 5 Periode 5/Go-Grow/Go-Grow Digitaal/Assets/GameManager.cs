using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //list of states the game moves between
    [SerializeField]
    GameObject[] states;

    //player names
    string player1 = "Speler 1";
    string player2 = "Speler 2";
    [SerializeField]
    TMP_InputField player1Field;
    [SerializeField]
    TMP_InputField player2Field;

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

    //array of dialogues
    string[] dialogues = { 
        //0 ask player 2 to pick positions
        ", kies je positie voor elk van de onderdelen:",
        //1 next button
        "Volgende",
        //2 tell player that not all positions have been chosen yet
        "Nog niet alle posities zijn gekozen!",
        //3 ask player 1 to pick positions
        ", kies je positie voor elk van de onderdelen:",
        //4 ask players to discuss their coices
        "Bespreek waarom jullie voor deze posities hebben gekozen.",
        //5 ask player to spin the wheel to choose the starting section
        "Draai aan het wiel om te kiezen met welk deel jullie beginnen!",
        //6 spin button
        "Draai",
        //7 know
        "Know!",
        //8 flow
        "Flow!",
        //9 glow
        "Glow!",
        //10 grow
        "Grow!",
        //11 ask player to grab a card
        "Pak een kaart van de stapel.",
        //12 explanation of joker card
        "Je hebt een joker kaart getrokken, dit betekent dat je een escape kaart mag trekken.",
        //13 explanation for mirror card
        "Je hebt een spiegelkaart getrokken, dit betekent dat de volgende vraag beantwoord wordt door de werkgever.",
        //14 ask player 2 to answer the question
        ", beantwoord de vraag.",
        //15 next section button
        "Volgend onderdeel",
        //16 ask player 1 to answer the question
        ", beantwoord de vraag.",
        //17 ask player to spin the wheel again to choose the next section
        "Draai aan het wiel om jullie volgende onderdeel te bepalen!",
        //18 finish game button
        "Sluit spel af",
        //19 tell players the game ended
        "Het spel is nu afgerond, bedankt voor het spelen!",
        //20 tell players the deck has ran out of cards
        "Er zijn helaas geen kaarten meer voor dit onderdeel.",
        //21 grab card button
        "Pak kaart",
    };

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
    TextMeshProUGUI dialogue;
    [SerializeField]
    GameObject nextButton;
    [SerializeField]
    GameObject nextButton2;
    [SerializeField]
    TextMeshProUGUI nextText;
    [SerializeField]
    TextMeshProUGUI nextText2;

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
    List<Sprite> know;
    [SerializeField]
    List<Sprite> flow;
    [SerializeField]
    List<Sprite> glow;
    [SerializeField]
    List<Sprite> grow;
    [SerializeField]
    List<Sprite> escape;

    //mirror and joker cards to check against
    [SerializeField]
    Sprite[] joker;
    [SerializeField]
    Sprite[] mirror;

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
    //is the card a joker card
    public bool isJoker = false;
    //is the card a mirror card
    public bool isMirror = false;
    //is the deck out of cards
    public bool outOfCards = false;
    //timer to time the card animation
    float cardTimer;

    //track which card decks are empty
    bool[] emptyDecks = { false, false, false, false };

    int chosenCard;


    //track which pawns are already on the board, to determine wether or not the user can go to the next state
    bool[] activePawns = new bool[4];
    bool isReady;


    void Start()
    {
        //choose a random section and set the starting state to state 0
        currentSection = Random.Range(0, 4);
        SetActiveState(7);
        nextButton2.SetActive(false);
        nextText.text = dialogues[1];
        nextText2.text = dialogues[15];
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //change the "Next" button text based on which state it is in
        if (currentState == 3)
        {
            nextText.text = dialogues[6];
        }
        else
        {
            if (!cardAppeared && currentState == 5)
            {
                nextText.text = dialogues[21];
            }
            else
            {
                nextText.text = dialogues[1];
            }
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
                    if (currentState == 0)
                    {
                        dialogue.text = player1 + dialogues[3];
                    }
                    else
                    {
                        dialogue.text = dialogues[4];
                    }
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
                    error.text = dialogues[2];
                }
                break;
            //for state 3, start the spinning sequence in state 4 and move to it
            case 3:
                //also sets the spin text to something else, for next time this state appears
                dialogue.text = dialogues[17];
                StartCoroutine(WaitForSpin());
                SetActiveState(4);
                spinner.GetComponent<Animator>().SetTrigger("startSpin");
                ChooseSection();
                break;
            //for case 5, either grab a card, remove the card on screen, or change the mirror or joker card into a different card
            case 5:
                //if there is no card on screen, start the card grab animation
                if (!emptyDecks[currentSection] || isJoker)
                {
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
                        //if the card on screen is a joker card, change it into an escape card
                        if (isJoker)
                        {
                            nextButton2.SetActive(true);
                            dialogue.text = player2 + dialogues[14];
                            grabInPlace = true;
                        }
                        //if it is a mirror card, change it into a regular card
                        else if (isMirror)
                        {
                            nextButton2.SetActive(true);
                            dialogue.text = player1+ dialogues[16];
                            grabInPlace = true;
                        }
                        //else, ask the user to grab another card, and stop them from being able to go to the next section
                        else
                        {
                            dialogue.text = dialogues[11];
                            realCard.SetActive(false);
                            cardAppeared = false;
                            nextButton2.SetActive(false);
                        }
                    }
                }
                else
                {
                    dialogue.text = dialogues[20];
                    nextButton.SetActive(false);
                    nextButton2.SetActive(true);
                }
                break;
            case 7:
                if (player1Field.text != "" && player2Field.text != "")
                {
                    SetActiveState(0);
                    player1 = player1Field.text;
                    player2 = player2Field.text;
                    dialogue.text = player2 + dialogues[0];
                }
                else
                {
                    error.text = "Er is nog geen naam ingevoerd voor elk van de spelers!";
                }
                break;
            //for all other states, simply move to the next state
            default:
                if (currentState == 2)
                {
                    dialogue.text = dialogues[5];
                }
                else if (currentState == 4)
                {
                    dialogue.text = dialogues[11];
                }
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
            nextText2.text = dialogues[18];
        }
        choose.transform.eulerAngles = new Vector3(0, 0, Random.Range(1, 90) - 90 * currentSection);
    }

    //wait until the spinning animation has finished, then tell the user which section is chosen, and change the target transform of the board
    IEnumerator WaitForSpin()
    {
        nextButton.SetActive(false);
        nextButton2.SetActive(false);
        dialogue.text = "";
        yield return new WaitForSeconds(4);
        nextButton.SetActive(true);
        switch (currentSection)
        {
            case 0:
                dialogue.text = dialogues[7];
                targetRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                dialogue.text = dialogues[8];
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                dialogue.text = dialogues[9];
                targetRotation = Quaternion.Euler(0, 0, 180);
                break;
            case 3:
                dialogue.text = dialogues[10];
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
                if (isJoker)
                {
                    dialogue.text = dialogues[12];
                    isEscape = true;
                }
                else if (isMirror)
                {
                    dialogue.text = dialogues[13];
                }
                else
                {
                    dialogue.text = player2 + dialogues[14];
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
            if (escape.Count > 0)
            {
                chosenCard = Random.Range(0, escape.Count);
                anything.sprite = escape[chosenCard];
                escape.RemoveAt(chosenCard);
                isEscape = false;
                isJoker = false;
                chosenCard = 100;
            }
            else
            {
                Debug.Log("no escape cards left");
            }
        }
        else
        {
            switch (currentSection)
            {
                case 0:
                    if (know.Count > 0)
                    {
                        chosenCard = Random.Range(0, know.Count);
                        anything.sprite = know[chosenCard];
                    }
                    else
                    {
                        //Debug.Log("no know cards left");
                        outOfCards = true;
                    }
                    break;
                case 1:
                    if (flow.Count > 0)
                    {
                        chosenCard = Random.Range(0, flow.Count);
                        anything.sprite = flow[chosenCard];
                    }
                    else
                    {
                        //Debug.Log("no flow cards left");
                        outOfCards = true;
                    }
                    break;
                case 2:
                    if (glow.Count > 0)
                    {
                        chosenCard = Random.Range(0, glow.Count);
                        anything.sprite = glow[chosenCard];
                    }
                    else
                    {
                        //Debug.Log("no glow cards left");
                        outOfCards = true;
                    }
                    break;
                case 3:
                    if (grow.Count > 0)
                    {
                        chosenCard = Random.Range(0, grow.Count);
                        anything.sprite = grow[chosenCard];
                    }
                    else
                    {
                        //Debug.Log("no grow cards left");
                        outOfCards = true;
                    }
                    break;
            }

            isJoker = false;
            isMirror = false;
            if (!outOfCards)
            {
                foreach (Sprite card in joker)
                {
                    if (card == anything.sprite)
                    {
                        isJoker = true;
                    }
                }
                foreach (Sprite card in mirror)
                {
                    if (card == anything.sprite)
                    {
                        isMirror = true;
                    }
                }

                if (isInPlace && (isJoker || isMirror))
                {
                    GenerateCard(true);
                }
                else
                {
                    switch (currentSection)
                    {
                        case 0:
                            know.RemoveAt(chosenCard);
                            if (know.Count == 0)
                            {
                                emptyDecks[0] = true;
                            }
                            break;
                        case 1:
                            flow.RemoveAt(chosenCard);
                            if (flow.Count == 0)
                            {
                                emptyDecks[1] = true;
                            }
                            break;
                        case 2:
                            glow.RemoveAt(chosenCard);
                            if (glow.Count == 0)
                            {
                                emptyDecks[2] = true;
                            }
                            break;
                        case 3:
                            grow.RemoveAt(chosenCard);
                            if (grow.Count == 0)
                            {
                                emptyDecks[3] = true;
                            }
                            break;
                    }
                }
            }
            else
            {

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
            dialogue.text = dialogues[19];
            SetActiveState(6);
            nextButton.SetActive(false);
            targetPosition = new Vector3(5.4f, 5.4f, 0);
            targetScale = new Vector3(0.3f, 0.3f, 0);
        }
        else
        {
            dialogue.text = dialogues[17];
            nextButton.SetActive(true);
            SetActiveState(3);
        }
        realCard.SetActive(false);
        nextButton2.SetActive(false);
        cardAppeared = false;
        chosenCard = 100;

    }

    public void ValueChanged()
    {
        error.text = "";
    }
}
