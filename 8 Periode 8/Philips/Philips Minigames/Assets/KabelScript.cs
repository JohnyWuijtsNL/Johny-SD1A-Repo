using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KabelScript : MonoBehaviour
{
    [SerializeField] GameObject[] startPoints;
    [SerializeField] GameObject[] endPoints;
    bool[] isConnected = {false, false, false, false};
    int _selectedStart = -1;
    Color white = new Color(1f, 1f, 1f);
    Color grey = new Color(0.5f, 0.5f, 0.5f);
    Color green = new Color(0, 1, 0);
    Color red = new Color(1, 0, 0);

    public void ButtonPressed(int buttonNumber)
    {
        if (buttonNumber < 4)
        {
            Debug.Log("test");
            SelectButton(buttonNumber);
        }
        else if (_selectedStart != -1)
        {
            ConnectLine(_selectedStart, buttonNumber);
        }
    }

    void SelectButton(int selectedStart)
    {
        for (int i = startPoints.Length - 1; i >= 0; i--)
        {
            if (i == selectedStart)
            {
                if (_selectedStart == i)
                {
                    _selectedStart = -1;
                    startPoints[i].GetComponent<Image>().color = white;
                }
                else
                {
                    startPoints[i].GetComponent<Image>().color = grey;
                    _selectedStart = i;
                }
            }
            else
            {
                if (isConnected[i])
                {
                    startPoints[i].GetComponent<Image>().color = grey;
                }
                else
                {
                    startPoints[i].GetComponent<Image>().color = white;
                }
                
            }
        }
    }
    void ConnectLine(int selectedStart, int selectedEnd)
    {
        isConnected[selectedStart] = true;
        if (selectedStart == selectedEnd - 4)
        {
            endPoints[selectedEnd - 4].GetComponent<Image>().color = green;
        }
        else
        {
            endPoints[selectedEnd - 4].GetComponent<Image>().color = red;
        }
    }
}
