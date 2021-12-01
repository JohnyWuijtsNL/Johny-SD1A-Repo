public class Weather
{
    string _cityName;
    public string CityName { get { return _cityName; } }
    float _sunPower;
    public float SunPower { get { return _sunPower; } }
    int _rain;
    public int Rain { get { return _rain; } }
    float _temperature;
    public float Temperature { get { return _temperature; } }

    public Weather(string cityName, float sunPower, int rain, float temperature)
    {
        _cityName = cityName;
        _sunPower = sunPower;
        _rain = rain;
        _temperature = temperature;
    }
}
