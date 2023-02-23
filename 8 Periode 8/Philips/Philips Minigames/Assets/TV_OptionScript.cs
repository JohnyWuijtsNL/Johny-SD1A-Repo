using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TV_OptionScript : MonoBehaviour
{
    [SerializeField] int buttonNumber;
    [SerializeField] TextMeshProUGUI buttonText;

    public void ChangeOption(string[] options)
    {
        switch (buttonNumber)
        {
            case 0: buttonText.text = options[0]; break;
            case 1: buttonText.text = options[1]; break;
            case 2: buttonText.text = options[2]; break;
        }
    }
}
