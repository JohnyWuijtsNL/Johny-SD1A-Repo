using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather
{
    string _cityName;
    public string CityName { get { return _cityName; } }
    float _sunPower;
    public float SunPower { get { return _sunPower; } }
    int _rain;
    public int Rain { get { return _rain; } }

    public Weather(string cityName, float sunPower, int rain)
    {
        _cityName = cityName;
        _sunPower = sunPower;
        _rain = rain;
    }
}
