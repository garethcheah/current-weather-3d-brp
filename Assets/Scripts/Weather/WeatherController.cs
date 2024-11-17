using System.Collections;
using System.Collections.Generic;
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
    public GameObject trees;
    public GameObject rain;
    public GameObject snow;
    public GameObject[] mountainRanges;

    [Header("Materials")]
    public Material rockMaterial;
    public Material shadowMaterial;
    public Material grassMaterial;
    public Material snowMaterial;
    public Material waterMaterial;
    public Material iceMateterial;

    public void LoadClearScene()
    {
        mainCamera.backgroundColor = new Color(0.5254902f, 0.9333333f, 1.0f);
        directionalLight.SetActive(true);
        clouds.SetActive(true);
        fire.SetActive(true);
        plants.SetActive(true);
        rain.SetActive(false);
        snow.SetActive(false);
        SetMountainRangeMaterial(rockMaterial);
        SetGroundMaterial(grassMaterial);
        SetWaterMaterial(waterMaterial);
        UIManager.instance.SetLocationAndWeatherInfo("Somewhere", WeatherType.Clear, "clear sky", 22.256f);
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
        snow.SetActive(false);
        SetMountainRangeMaterial(rockMaterial);
        SetGroundMaterial(grassMaterial);
        SetWaterMaterial(waterMaterial);
        UIManager.instance.SetLocationAndWeatherInfo("Somewhere", WeatherType.Clouds, "scattered clouds", 18.256f);
        OnWeatherChanged?.Invoke(WeatherType.Clouds);
    }

    public void LoadRainScene(WeatherIntensity intensity)
    {
        mainCamera.backgroundColor = new Color(0.5456123f, 0.5712766f, 0.5754717f);
        directionalLight.SetActive(true);
        clouds.SetActive(false);
        fire.SetActive(false);
        plants.SetActive(true);
        snow.SetActive(false);
        SetMountainRangeMaterial(rockMaterial);
        SetGroundMaterial(grassMaterial);
        SetWaterMaterial(waterMaterial);

        ParticleSystem rainSystem = rain.GetComponent<ParticleSystem>();
        var emission = rainSystem.emission;
        string weatherDescription = "rain";

        switch (intensity)
        {
            case WeatherIntensity.Light:
                emission.rateOverTime = 25.0f;
                weatherDescription = "light rain";
                break;
            case WeatherIntensity.Moderate:
                emission.rateOverTime = 100.0f;
                weatherDescription = "moderate rain";
                break;
            case WeatherIntensity.Heavy:
                emission.rateOverTime = 500.0f;
                weatherDescription = "heavy intensity rain";
                break;
        }

        rain.SetActive(true);
        UIManager.instance.SetLocationAndWeatherInfo("Somewhere", WeatherType.Rain, weatherDescription, 9.256f);
        OnWeatherChanged?.Invoke(WeatherType.Rain);
    }

    public void LoadSnowScene(WeatherIntensity intensity)
    {
        mainCamera.backgroundColor = new Color(0.1886792f, 0.1886792f, 0.1886792f);
        directionalLight.SetActive(false);
        clouds.SetActive(false);
        fire.SetActive(true);
        plants.SetActive(false);
        rain.SetActive(false);
        SetMountainRangeMaterial(shadowMaterial);
        SetGroundMaterial(snowMaterial);
        SetWaterMaterial(iceMateterial);

        ParticleSystem snowSystem = snow.GetComponent<ParticleSystem>();
        var emission = snowSystem.emission;
        string weatherDescription = "snow";

        switch (intensity)
        {
            case WeatherIntensity.Light:
                emission.rateOverTime = 100.0f;
                weatherDescription = "light snow";
                break;
            case WeatherIntensity.Moderate:
                emission.rateOverTime = 300.0f;
                weatherDescription = "snow";
                break;
            case WeatherIntensity.Heavy:
                emission.rateOverTime = 600.0f;
                weatherDescription = "heavy snow";
                break;
        }

        snow.SetActive(true);
        UIManager.instance.SetLocationAndWeatherInfo("Somewhere", WeatherType.Snow, weatherDescription, -5.256f);
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
