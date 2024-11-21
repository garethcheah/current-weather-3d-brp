using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton

    [Header("Inputs")]
    public TMP_InputField inputLatitude;
    public TMP_InputField inputLongitude;

    [Header("UI Objects")]
    public TMP_Text lblCurrentLocation;
    public TMP_Text lblCurrentWeather;
    public TMP_Text lblCurrentTemperature;
    public TMP_Text lblUserMessage;
    public GameObject objWeatherIcon;

    [Header("Weather Icons")]
    public Sprite iconClear;
    public Sprite iconClouds;
    public Sprite iconRain;
    public Sprite iconSnow;

    public float GetLatitude()
    {
        float latitude;

        if (float.TryParse(inputLatitude.text, out latitude))
        {
            // Clamp latitude to valid range (-90 to 90)
            if (latitude < -90.0f || latitude > 90.0f)
            {
                lblUserMessage.text += "Latitude must be between -90 and 90. ";
                return 0.0f;
            }

            return latitude;
        }
        else
        {
            lblUserMessage.text += "Latitude must be a numeric value. ";
            return 0.0f;
        }
    }

    public float GetLongitude()
    {
        float longitude;

        if (float.TryParse(inputLongitude.text, out longitude))
        {
            // Clamp longitude to valid range (-180 to 180)
            if (longitude < -180.0f || longitude > 180.0f)
            {
                lblUserMessage.text += "Longitude must be between -180 and 180. ";
                return 0.0f;
            }

            return longitude;
        }
        else
        {
            lblUserMessage.text += "Longitude must be a numeric value. ";
            return 0.0f;
        }
    }

    public void ClearUserMessage()
    {
        lblUserMessage.text = "";
    }


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
