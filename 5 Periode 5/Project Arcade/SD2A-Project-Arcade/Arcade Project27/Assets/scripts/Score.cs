using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour
{

    public static int scoreAmount;
    public TMP_Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        scoreAmount = 0;
    }


        void Update ()
        {
            ScoreText.text = $"Points: {scoreAmount}";
        }
    
}
