using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMoveScript : MonoBehaviour
{
    [SerializeField]
    Transform[] locations;
    [SerializeField]
    int pawnNumber;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePawn(float moveTo)
    {
        transform.position = locations[0].position - (locations[0].position - locations[1].position) * moveTo;
        gameManager.SetPawnActive(pawnNumber);
    }
}
