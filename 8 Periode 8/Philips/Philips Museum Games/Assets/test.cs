using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class test : MonoBehaviour
{
    public List<InputDevice> controllers = new List<InputDevice>();
    public InputDevice controller;
    public string output;
    public Vector3 buuton; 

    private void Update()
    {
        if (!controller.isValid)
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, controllers);
            foreach (var con in controllers)
            {
                Debug.Log(con.name);
                Debug.Log(con.characteristics);
                controller = con;
                Debug.Log(controller.name);
            }
        }

        if (controller.TryGetFeatureValue(CommonUsages.devicePosition, out buuton))
        {
            Debug.Log("Feest");
            output += "Trigger Pressed: " + buuton + "\n";

            Debug.Log(output);
        }

    }

    //private void CommonInput()
    //{
    //    string output = string.Empty;

    //    // Trigger press
    //    if (controller[0].TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger))
    //        output += "Trigger Pressed: " + trigger + "\n";

    //    Debug.Log(output);
    //}
}
