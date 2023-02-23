using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampGame : MonoBehaviour
{
    public Transform pointPosition01;
    public Transform pointPosition02;

    [SerializeField] int counter;

    public LineRenderer[] lines = new LineRenderer[4];

    public void Click(GameObject button)
    {
        //if(pointPosition == null)
        //{
        //    pointPosition = button.transform;
        //}
        //if(pointPosition != null)
        //{
        //    lines[counter].SetPosition(0, new Vector3(pointPosition.position.x, pointPosition.position.y, 0));
        //    lines[counter].SetPosition(1, new Vector3(button.transform.position.x, button.transform.position.y, 0));
        //    counter++;
        //    if (counter > 4)
        //    {
        //        counter = 0;
        //        print("reset 4 lines");
        //    }
        //    pointPosition = null;

        //}
        if (pointPosition01 == null)
        {
            pointPosition01 = button.transform;
        }
        if(pointPosition01 != null)
        {
            pointPosition02 = button.transform;
        }

    }





}
