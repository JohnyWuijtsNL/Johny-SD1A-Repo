using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSyncScript : MonoBehaviour
{
    [SerializeField]
    GameObject vrCamera, vrRig, vrController, vrCursor;
    Vector3 locationDifference;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        locationDifference = vrCamera.transform.position - transform.position;
        vrRig.transform.position -= locationDifference;
        vrCursor.transform.rotation = vrController.transform.rotation;
    }
}
