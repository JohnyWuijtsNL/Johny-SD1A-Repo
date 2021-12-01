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
    [SerializeField]
    TextMeshProUGUI temperatureText;
    List<Weather> weathers;

    // Start is called before the first frame update
    void Start()
    {
        weathers = ReadCSVFile();
    }

    public void Draw(int weather)
    {
        if (weathers == null)
        {
            return;
        }
        cityText.text = weathers[weather].CityName;
        temperatureText.text = weathers[weather].Temperature + "°C";
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

    List<Weather> ReadCSVFile()
    {
        StreamReader streamReader = new StreamReader(Application.streamingAssetsPath + "/weatherData.csv");
        List<Weather> newWeathers = new List<Weather>();
        if (streamReader == null)
        {
            return null;
        }

        while(true)
        {
            string dataString = streamReader.ReadLine();
            if (dataString == null)
            {
                break;
            }

            string[] dataValues = dataString.Split(',');
            newWeathers.Add(new Weather(dataValues[0], float.Parse(dataValues[1]), int.Parse(dataValues[2]), float.Parse(dataValues[3])));
        }

        return newWeathers;
    }
}
