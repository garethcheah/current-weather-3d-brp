using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton

    [Header("UI Objects")]
    public TMP_Text lblCurrentLocation;
    public TMP_Text lblCurrentWeather;
    public TMP_Text lblCurrentTemperature;
    public GameObject objWeatherIcon;

    [Header("Weather Icons")]
    public Sprite iconClear;
    public Sprite iconClouds;
    public Sprite iconRain;
    public Sprite iconSnow;


    public void SetLocationAndWeatherInfo(string location, WeatherType weatherType, string weatherDescription, float temperature)
    {
        lblCurrentLocation.text = $"Location: {location}";
        lblCurrentWeather.text = $"Weather: {weatherDescription}";
        lblCurrentTemperature.text = $"Temp: {temperature.ToString("F")} C";

        Image imgWeatherIcon = objWeatherIcon.GetComponent<Image>();

        switch (weatherType)
        {
            case WeatherType.Thunderstorm:
            case WeatherType.Drizzle:
            case WeatherType.Rain:
                SetWeatherIcon(iconRain);
                break;
            case WeatherType.Snow:
                SetWeatherIcon(iconSnow);
                break;
            case WeatherType.Clear:
                SetWeatherIcon(iconClear);
                break;
            case WeatherType.Clouds:
                SetWeatherIcon(iconClouds);
                break;
            default:
                objWeatherIcon.SetActive(false);
                break;
        }
    }

    private void SetWeatherIcon(Sprite weatherIcon)
    {
        Image imgWeatherIcon = objWeatherIcon.GetComponent<Image>();

        imgWeatherIcon.sprite = weatherIcon;
        objWeatherIcon.SetActive(true);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
