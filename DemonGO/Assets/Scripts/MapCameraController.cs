using UnityEngine;
using System.Collections;

public class MapCameraController : MonoBehaviour
{
    public Transform target; // Player's transform
    public float rotationSpeed = 5.0f;
    public float pinchZoomSpeed = 0.01f;
    public float minZoom = 20f;
    public float maxZoom = 60f;

    private Vector3 offset;

    IEnumerator Start()
    {
        while (Input.location.status != LocationServiceStatus.Running)
        {
            yield return new WaitForSeconds(1);
        }
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Pinch-to-zoom
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float newFieldOfView = Camera.main.fieldOfView + deltaMagnitudeDiff * pinchZoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(newFieldOfView, minZoom, maxZoom);
        }

        // Rotation around player
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get the delta position of the touch
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Rotate the camera around the player based on touch movement
            float rotationX = touchDeltaPosition.x * rotationSpeed * Time.deltaTime;
            transform.RotateAround(target.position, Vector3.up, rotationX);
        }

        transform.LookAt(target.position);
    }
}
