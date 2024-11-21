using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CurrentWeather
{
    [Header("OpenWeather API Config")]
    [SerializeField] private string apiKey = "7866edcf5ed65880d83df9647134d145"; // Replace with your OpenWeather API key

    private const string BASE_URL = "https://api.openweathermap.org/data/2.5/weather";

    //public void FetchData(float latitude, float longitude)
    //{
    //    StartCoroutine(FetchCurrentWeatherData(latitude, longitude));
    //}

    public IEnumerator FetchCurrentWeatherData(float latitude, float longitude)
    {
        // Construct the API URL with parameters
        string url = $"{BASE_URL}?lat={latitude}&lon={longitude}&units=metric&appid={apiKey}";
        Debug.Log(url);

        // Use UnityWebRequest to make a GET request
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Send the request and wait for the response
            yield return webRequest.SendWebRequest();

            // Check for errors
            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error fetching weather data: {webRequest.error}");
            }
            else
            {
                // Parse and handle the response JSON
                string jsonResponse = webRequest.downloadHandler.text;
                Debug.Log($"Weather Data: {jsonResponse}");
                CurrentWeatherData currentWeatherData = JsonUtility.FromJson<CurrentWeatherData>(jsonResponse);
                WeatherController.instance.LoadWeatherScene(currentWeatherData.name, currentWeatherData.weather[0].main, currentWeatherData.weather[0].description, currentWeatherData.main.temp);
            }
        }
    }
}
