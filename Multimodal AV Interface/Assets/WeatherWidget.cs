using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class WeatherWidget : MonoBehaviour
{
    public string apiKey = "bd5e378503939ddaee76f12ad7a97608";
    public string city = "Athens";
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI weatherDescriptionText;
    public Image weatherIcon;
    public TextMeshProUGUI weatherCity;

    private const string apiUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

    void Start()
    {
        StartCoroutine(GetWeatherData());
    }

    IEnumerator GetWeatherData()
    {
        string url = string.Format(apiUrl, city, apiKey);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                ProcessWeatherData(www.downloadHandler.text);
            }
        }
    }

    void ProcessWeatherData(string json)
    {
        WeatherInfo weatherInfo = JsonUtility.FromJson<WeatherInfo>(json);

        temperatureText.text = ((int)weatherInfo.main.temp) + "°C";
        weatherCity.text = city;
        weatherDescriptionText.text = weatherInfo.weather[0].description;

        StartCoroutine(GetWeatherIcon(weatherInfo.weather[0].icon));
    }

    IEnumerator GetWeatherIcon(string iconCode)
    {
        string iconUrl = "http://openweathermap.org/img/wn/" + iconCode + "@2x.png";

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(iconUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                weatherIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
        }
    }
}

[System.Serializable]
public class WeatherInfo
{
    public Weather[] weather;
    public Main main;

    [System.Serializable]
    public class Weather
    {
        public string description;
        public string icon;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
    }
}
