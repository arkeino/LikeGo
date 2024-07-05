using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Latitude and Longitude of the player
    public float latitude;
    public float longitude;
    public float oldLatitude;
    public float oldLongitude;

    private Vector2 oldPosition;
    private Vector2 newPosition;

    private Vector2 displacement;

    public GameObject GetLocation;
    public Rigidbody rb;

    // Zoom level of the map
    public int zoom = 17;

    // Size of the map
    public int size = 1000;

    // Distance in pixels per meter
    public double ppm;

    IEnumerator Start()
    {
        while (Input.location.status != LocationServiceStatus.Running)
        {
            yield return new WaitForSeconds(1);
        }
        transform.position = new Vector3(0, 10, 0);
        latitude = GetLocation.GetComponent<GetLocation>().latitude;
        longitude = GetLocation.GetComponent<GetLocation>().longitude;
        oldLatitude = latitude;
        oldLongitude = longitude;
        oldPosition = new Vector2(transform.position.x, transform.position.z);
        newPosition = oldPosition;
    }

    // Function to calculate distance in meters from pixels per meter
    public double GetDistanceInMeters(double pixelsPerMeter, double distanceInPixels)
    {
        return distanceInPixels / pixelsPerMeter;
    }

    // Function to calculate distance in meters between two geographical points
    public Vector2 CalculateDistanceInMeters(float oldLatitude, float oldLongitude, float latitude, float longitude)
    {
        // Convert latitude and longitude to radians
        double lat1 = Mathf.Deg2Rad * oldLatitude;
        double lon1 = Mathf.Deg2Rad * oldLongitude;
        double lat2 = Mathf.Deg2Rad * latitude;
        double lon2 = Mathf.Deg2Rad * longitude;

        // Earth's radius in meters
        double R = 6371000; // metres

        // Change in coordinates
        double x = (lon2 - lon1) * R * Mathf.Cos((float)((lat1 + lat2) / 2));
        double y = (lat2 - lat1) * R;

        return new Vector2((float)x, (float)y);
    }

    const double EARTH_RADIUS_METERS = 6378137;
    const double EARTH_CIRCUMFERENCE_METERS = 2 * Mathf.PI * EARTH_RADIUS_METERS;

    public double GetPixelsPerMeter(double latitude, double zoom, int size)
    {
        double pixelsPerTile = size;
        double numTiles = Mathf.Pow(2, (float)zoom);
        double metersPerTile = Mathf.Cos(Mathf.Deg2Rad * (float)latitude) * EARTH_CIRCUMFERENCE_METERS / numTiles;

        return pixelsPerTile / metersPerTile;
    }

    void LateUpdate()
    {
        while (Input.location.status != LocationServiceStatus.Running)
            return;
        // Calculate distance in meters between old position and new position
        ppm = GetPixelsPerMeter(latitude, zoom, size) / 10;
        if (oldLatitude != latitude || oldLongitude != longitude)
        {
            displacement = CalculateDistanceInMeters(oldLatitude, oldLongitude, latitude, longitude);
            displacement = new Vector2(displacement.x * (float)ppm, displacement.y * (float)ppm);
        }
        if (oldPosition.x == newPosition.x && oldPosition.y == newPosition.y)
        {
            oldLatitude = latitude;
            oldLongitude = longitude;
            oldPosition = newPosition;
            newPosition = new Vector2(transform.position.x + (float)displacement.x, transform.position.z + (float)displacement.y);
            displacement = new Vector2(0f, 0f);
            latitude = GetLocation.GetComponent<GetLocation>().latitude;
            longitude = GetLocation.GetComponent<GetLocation>().longitude;
        } else {
            rb.MovePosition(new Vector3(newPosition.x, 10, newPosition.y));
            oldPosition = new Vector2(transform.position.x, transform.position.z);
        }
    }
}
