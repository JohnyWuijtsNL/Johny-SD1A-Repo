using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject VRArea;

    [SerializeField]
    Transform[] travelLocations;

    int currentRoom = 0;
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentRoom++;
            if (currentRoom >= travelLocations.Length)
            {
                currentRoom = 0;
            }
            Teleport(currentRoom);
        }
    }
    */
    public void Teleport(int room)
    {
        VRArea.transform.position = travelLocations[room].position;
        currentRoom = room;
    }
}
