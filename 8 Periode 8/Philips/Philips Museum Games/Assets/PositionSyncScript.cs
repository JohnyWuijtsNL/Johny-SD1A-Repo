using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSyncScript : MonoBehaviour
{
    [SerializeField]
    GameObject vrCamera, vrRig, vrController, vrFakeController;
    Vector3 locationDifference;

    void Update()
    {
        //calculate location difference between vr camera (headset) and itself (attach script to empty object to be the camera position)
        locationDifference = vrCamera.transform.position - transform.position;
        vrRig.transform.position -= locationDifference;

        //set the rotation of the fake controller to be the same as the real controller
        vrFakeController.transform.rotation = vrController.transform.rotation;
    }
}
