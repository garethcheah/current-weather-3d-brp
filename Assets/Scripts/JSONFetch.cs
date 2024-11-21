using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JSONFetch : MonoBehaviour
{
    private const string BASE_URL = "https://jsonplaceholder.typicode.com/users";

    private void Start()
    {
        StartCoroutine(GetUsers());
    }

    private IEnumerator GetUsers()
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(BASE_URL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;

                // Deserialize the JSON into list of users
                List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonResponse);

                foreach (var user in users)
                {
                    Debug.Log($"ID: {user.id}, Name: {user.name}, Username: {user.username}, Email: {user.email}");
                }
            }
            else
            {
                Debug.LogError($"Error fetching users: {webRequest.error}");
            }
        }
    }
}

[System.Serializable]
public class User
{
    public int id;
    public string name;
    public string username;
    public string email;
}