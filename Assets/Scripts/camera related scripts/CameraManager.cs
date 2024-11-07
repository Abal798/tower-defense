using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed = 50f; // Speed at which the camera moves
    public float speedFactor = 50f;
    public float edgeThreshold = 500f; // Distance from the edge of the screen to trigger movement
    
    public Vector2 minPosition; // Minimum bounds for the camera
    public Vector2 maxPosition; // Maximum bounds for the camera
    
    public float zoomSpeed = 1f; // Speed of zooming
    public float minZoom = 2f; // Minimum orthographic size
    public float maxZoom = 10f; // Maximum orthographic 
    
    private  new Camera camera;
    
    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        Vector3 targetPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;

        maxPosition = (new Vector2(-1.8f * camera.orthographicSize + 21.6f, (- 1.01f* camera.orthographicSize)  + 21.23f));
        minPosition = (new Vector2(1.8f * camera.orthographicSize - 21.6f ,   ( 1.01f* camera.orthographicSize)  - 21.23f));        
        
        moveSpeed = (camera.orthographicSize * 5) + speedFactor;

        if (mousePosition.x < edgeThreshold)
        {
            targetPosition += Vector3.left * moveSpeed * Time.deltaTime;
        }

        if (mousePosition.x > Screen.width - edgeThreshold)
        {
            targetPosition += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (mousePosition.y < edgeThreshold)
        {
            targetPosition += Vector3.down * moveSpeed * Time.deltaTime;
        }

        if (mousePosition.y > Screen.height - edgeThreshold)
        {
            targetPosition += Vector3.up * moveSpeed * Time.deltaTime;
        }
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
        
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize -= scrollInput * zoomSpeed;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoom, maxZoom);
        
        targetPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f); // Adjust the smoothing factor as needed
    }
}
