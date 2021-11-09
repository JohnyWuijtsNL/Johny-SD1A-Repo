using UnityEngine;

public class PawnMoveScript : MonoBehaviour
{
    //start and end location
    [SerializeField]
    Transform[] locations;
    //variable that stores which pawn this script is attacked to
    [SerializeField]
    int pawnNumber;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //called whenever the position of the slider linked to this pawn is changed
    public void MovePawn(float moveTo)
    {
        //set the location of the pawn to a percentage of the difference between the start location and the end location
        transform.position = locations[0].position - (locations[0].position - locations[1].position) * moveTo;
        //set this pawn as active, to determine wether or not the user can continue
        gameManager.SetPawnActive(pawnNumber);
    }
}
