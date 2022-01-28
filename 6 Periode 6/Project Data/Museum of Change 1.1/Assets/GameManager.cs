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

    public int currentRoom = -1;
    
    public void Teleport(int room)
    {
        VRArea.transform.position = new Vector3(travelLocations[room].position.x, 0, travelLocations[room].position.z);
        currentRoom = room;

        antenna.material = materials[currentRoom];
        antennaTrigger.material = materials[currentRoom];
    }
}
