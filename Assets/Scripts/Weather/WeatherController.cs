using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WeatherController : MonoBehaviour
{
    public static WeatherController instance; // Singleton

    public UnityEvent<WeatherType> OnWeatherChanged = new UnityEvent<WeatherType>();

    [Header("Game Objects")]
    public Camera mainCamera;
    public GameObject directionalLight;
    public GameObject clouds;
    public GameObject ground;
    public GameObject water;
    public GameObject fire;
    public GameObject plants;
    public GameObject rain;
    public GameObject snowLayer1;
    public GameObject snowLayer2;
    public GameObject[] mountainRanges;

    [Header("Materials")]
    public Material rockMaterial;
    public Material shadowMaterial;
    public Material grassMaterial;
    public Material snowMaterial;
    public Material waterMaterial;
    public Material iceMateterial;

    private string _location;
    private string _weatherDescription;
    private float _temperature;

    public void FetchAndLoadCurrentWeather()
    {
        UIManager.instance.ClearUserMessage();
        CurrentWeather currentWeather = new CurrentWeather();
        StartCoroutine(currentWeather.FetchCurrentWeatherData(UIManager.instance.GetLatitude(), UIManager.instance.GetLongitude()));
    }

    public void LoadWeatherScene(string location, string weather, string weatherDescription, float temperature)
    {
        WeatherIntensity weatherIntensity = WeatherIntensity.Moderate;

        if (weatherDescription.ToLower().Contains("light"))
        {
            weatherIntensity = WeatherIntensity.Light;
        }
        else if (weatherDescription.ToLower().Contains("heavy"))
        {
            weatherIntensity = WeatherIntensity.Heavy;
        }

        _location = location;
        _weatherDescription = weatherDescription;
        _temperature = temperature;

        switch (weather)
        {
            case "Thunderstorm":
            case "Drizzle":
            case "Rain":
                LoadRainScene(weatherIntensity);
                break;
            case "Snow":
                LoadSnowScene(weatherIntensity);
                break;
            case "Clear":
                LoadClearScene();
                break;
            case "Clouds":
                LoadCloudyScene();
                break;
        }
    }

    public void LoadClearScene()
    {
        mainCamera.backgroundColor = new Color(0.5254902f, 0.9333333f, 1.0f); // 86EEFF
        directionalLight.SetActive(true);
        clouds.SetActive(true);
        fire.SetActive(true);
        plants.SetActive(true);
        rain.SetActive(false);
        snowLayer1.SetActive(false);
        snowLayer2.SetActive(false);
        SetMountainRangeMaterial(rockMaterial);
        SetGroundMaterial(grassMaterial);
        SetWaterMaterial(waterMaterial);
        UIManager.instance.SetLocationAndWeatherInfo(_location, WeatherType.Clear, _weatherDescription, _temperature);
        OnWeatherChanged?.Invoke(WeatherType.Clear);
    }

    public void LoadCloudyScene()
    {
        mainCamera.backgroundColor = new Color(0.5456123f, 0.5712766f, 0.5754717f);
        directionalLight.SetActive(true);
        clouds.SetActive(false);
        fire.SetActive(true);
        plants.SetActive(true);
        rain.SetActive(false);
        snowLayer1.SetActive(false);
        snowLayer2.SetActive(false);
        SetMountainRangeMaterial(rockMaterial);
        SetGroundMaterial(grassMaterial);
        SetWaterMaterial(waterMaterial);
        UIManager.instance.SetLocationAndWeatherInfo(_location, WeatherType.Clouds, _weatherDescription, _temperature);
        OnWeatherChanged?.Invoke(WeatherType.Clouds);
    }

    public void LoadRainScene(WeatherIntensity intensity)
    {
        mainCamera.backgroundColor = new Color(0.5456123f, 0.5712766f, 0.5754717f);
        directionalLight.SetActive(true);
        clouds.SetActive(false);
        fire.SetActive(false);
        plants.SetActive(true);
        snowLayer1.SetActive(false);
        snowLayer2.SetActive(false);
        SetMountainRangeMaterial(rockMaterial);
        SetGroundMaterial(grassMaterial);
        SetWaterMaterial(waterMaterial);

        ParticleSystem rainSystem = rain.GetComponent<ParticleSystem>();
        var emission = rainSystem.emission;

        switch (intensity)
        {
            case WeatherIntensity.Light:
                emission.rateOverTime = 25.0f;
                break;
            case WeatherIntensity.Moderate:
                emission.rateOverTime = 100.0f;
                break;
            case WeatherIntensity.Heavy:
                emission.rateOverTime = 500.0f;
                break;
        }

        rain.SetActive(true);
        UIManager.instance.SetLocationAndWeatherInfo(_location, WeatherType.Rain, _weatherDescription, _temperature);
        OnWeatherChanged?.Invoke(WeatherType.Rain);
    }

    public void LoadSnowScene(WeatherIntensity intensity)
    {
        mainCamera.backgroundColor = new Color(0.1886792f, 0.1886792f, 0.1886792f);
        directionalLight.SetActive(true);
        clouds.SetActive(false);
        fire.SetActive(true);
        plants.SetActive(false);
        rain.SetActive(false);
        SetMountainRangeMaterial(shadowMaterial);
        SetGroundMaterial(snowMaterial);
        SetWaterMaterial(iceMateterial);

        ParticleSystem snowSystem1 = snowLayer1.GetComponent<ParticleSystem>();
        ParticleSystem snowSystem2 = snowLayer2.GetComponent<ParticleSystem>();
        var emission1 = snowSystem1.emission;
        var emission2 = snowSystem2.emission;

        switch (intensity)
        {
            case WeatherIntensity.Light:
                emission1.rateOverTime = 50.0f;
                emission2.rateOverTime = 50.0f;
                break;
            case WeatherIntensity.Moderate:
                emission1.rateOverTime = 200.0f;
                emission2.rateOverTime = 200.0f;
                break;
            case WeatherIntensity.Heavy:
                emission1.rateOverTime = 600.0f;
                emission2.rateOverTime = 600.0f;
                break;
        }

        snowLayer1.SetActive(true);
        snowLayer2.SetActive(true);
        UIManager.instance.SetLocationAndWeatherInfo(_location, WeatherType.Snow, _weatherDescription, _temperature);
        OnWeatherChanged?.Invoke(WeatherType.Snow);
    }

    private void SetMountainRangeMaterial(Material mountainMaterial)
    {
        foreach (GameObject mountain in mountainRanges)
        {
            MeshRenderer mrMountain = mountain.GetComponent<MeshRenderer>();
            mrMountain.material = mountainMaterial;
        }
    }

    private void SetGroundMaterial(Material groundMaterial)
    {
        MeshRenderer mrGround = ground.GetComponent<MeshRenderer>();
        mrGround.material = groundMaterial;
    }

    private void SetWaterMaterial(Material waterMaterial)
    {
        MeshRenderer mrWater = water.GetComponent<MeshRenderer>();
        mrWater.material = waterMaterial;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
