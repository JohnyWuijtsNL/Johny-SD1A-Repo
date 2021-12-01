using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class WeatherManager : MonoBehaviour
{
    [SerializeField]
    GameObject sunSprite;
    [SerializeField]
    GameObject[] rainSprites;
    [SerializeField]
    TextMeshProUGUI cityText;

    List<Weather> weathers = new List<Weather>();

    StreamReader streamReader = new StreamReader("weatherData.csv");

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(streamReader);
        weathers.Add(new Weather("Amsterdam", 1.3f, 1));
        weathers.Add(new Weather("Eindhoven", 0.4f, 2));
        weathers.Add(new Weather("Rotterdam", 1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw(int weather)
    {
        cityText.text = weathers[weather].CityName;
        sunSprite.transform.localScale = new Vector3(weathers[weather].SunPower * 0.3f, weathers[weather].SunPower * 0.3f, 1);
        foreach (GameObject rainSprite in rainSprites)
        {
            rainSprite.SetActive(false);
        }
        for (int i = 0; i < weathers[weather].Rain; i++)
        {
            rainSprites[i].SetActive(true);
        }
    }
}
