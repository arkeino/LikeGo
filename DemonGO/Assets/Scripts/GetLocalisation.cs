using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLocation : MonoBehaviour
{
    public float latitude;
    public float longitude;

    private IEnumerator Start()
    {
        // Wait 10 seconds for user to accept location services
        yield return StartCoroutine(WaitForTenSeconds());

        // Check if location services are enabled on the device
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled.");
            yield break; // Exit the coroutine if location services are not enabled
        }

        // Start the location service with desired accuracy and update distance
        Input.location.Start(10f, 10f);
    }

    private void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Get the latitude and longitude
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;

            // Use the latitude and longitude as needed
            //Debug.Log("Latitude: " + latitude + ", Longitude: " + longitude);
        }
    }

    private void OnDestroy()
    {
        // Stop the location service when the script is destroyed or no longer needed
        Input.location.Stop();
    }

    private IEnumerator WaitForTenSeconds()
    {
        // Wait for 10 seconds
        yield return new WaitForSeconds(5f);
    }
}