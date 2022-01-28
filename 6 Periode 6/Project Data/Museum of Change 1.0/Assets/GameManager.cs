using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject VRArea;

    [SerializeField]
    Transform[] travelLocations;

    [SerializeField]
    MeshRenderer antenna, antennaTrigger;

    [SerializeField]
    Material[] materials;

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
        VRArea.transform.position = new Vector3(travelLocations[room].position.x, 0, travelLocations[room].position.z);
        currentRoom = room;

        antenna.material = materials[currentRoom];
        antennaTrigger.material = materials[currentRoom];
    }
}
