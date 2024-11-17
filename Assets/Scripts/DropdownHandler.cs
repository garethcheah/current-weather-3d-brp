using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownHandler : MonoBehaviour
{
    // Method to handle the value change
    public void OnDropdownValueChanged(int value)
    {
        // Example: React to the dropdown value
        switch (value)
        {
            case 0:
                //TODO: Trigger weather based on current location
                break;
            case 1:
                WeatherController.instance.LoadClearScene();
                break;
            case 2:
                WeatherController.instance.LoadCloudyScene();
                break;
            case 3:
                WeatherController.instance.LoadRainScene(WeatherIntensity.Light);
                break;
            case 4:
                WeatherController.instance.LoadRainScene(WeatherIntensity.Moderate);
                break;
            case 5:
                WeatherController.instance.LoadRainScene(WeatherIntensity.Heavy);
                break;
            case 6:
                WeatherController.instance.LoadSnowScene(WeatherIntensity.Light);
                break;
            case 7:
                WeatherController.instance.LoadSnowScene(WeatherIntensity.Moderate);
                break;
            case 8:
                WeatherController.instance.LoadSnowScene(WeatherIntensity.Heavy);
                break;
        }
    }
}
