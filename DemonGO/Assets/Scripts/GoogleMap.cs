using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class GoogleMap : MonoBehaviour
{
    public Material material;
    public GameObject MapCube;
    public GameObject GetLocation;
    public string apiKey = "sk.eyJ1Ijoib25seXNhbSIsImEiOiJjbHRtaTgyc24xZmQwMmpzNGZ4cXc1enloIn0.c-dNiwKWQxlHwtgB57LzCg"; // Replace with your Google Maps API key
    public double latitude = 0;
    public double longitude = 0;
    private int zoom = 17;
    private string size = "1000x1000";
    private string maptype = "roadmap";
    private string style = "onlysam/cltnpveb5019b01o81t477pw6";
    private string markers = "color:blue|label:S|";
    private float time_delay = 30f;
    private float elapsedTime = 0f;
    private bool isStart = true;

    private IEnumerator Start()
    {
        while (Input.location.status != LocationServiceStatus.Running)
        {
            yield return new WaitForSeconds(1);
        }
        longitude = GetLocation.GetComponent<GetLocation>().longitude;
        latitude = GetLocation.GetComponent<GetLocation>().latitude;
        yield return StartCoroutine(create_map((float)latitude, (float)longitude, zoom, size, maptype, style, markers));
    }

    public IEnumerator create_map(float latitude, float longitude, int zoom, string size, string maptype, string style, string markers)
    {
        markers += latitude + "," + longitude;
        string url = String.Format ("https://api.mapbox.com/styles/v1/{5}/static/{0},{1},{2},0,0/{3}@2x?attribution=false&logo=false&access_token={4}", 
        longitude, latitude, zoom, size, apiKey, style);
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            material.mainTexture = texture;
            MapCube.GetComponent<Renderer>().material = material;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            if (isStart)
            {
                isStart = false;
                elapsedTime = time_delay;
                return;
            }
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= time_delay)
            {
                Debug.Log("Updating Map");
                elapsedTime = 0f;
                longitude = GetLocation.GetComponent<GetLocation>().longitude;
                latitude = GetLocation.GetComponent<GetLocation>().latitude;
                StartCoroutine(create_map((float)latitude, (float)longitude, zoom, size, maptype, style, markers));
            }
        }
    }
}
