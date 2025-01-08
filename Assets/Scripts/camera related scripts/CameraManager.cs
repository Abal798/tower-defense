using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed = 50f; // Speed at which the camera moves
    public float speedFactor = 50f;
    public float edgeThreshold = 500f; // Distance from the edge of the screen to trigger movement
    public float dragSpeedReductor = 0.1f;
    
    public Vector2 minPosition; // Minimum bounds for the camera
    public Vector2 maxPosition; // Maximum bounds for the camera
    
    public float zoomSpeed = 3f; // Speed of zooming
    public float minZoom = 2f; // Minimum orthographic size
    public float maxZoom = 10f; // Maximum orthographic 
    
    private  new Camera camera;

    private float basicZoom;
    private Vector3 basicCameraPosition;
    
    
    
    private Vector2 previousMousePosition; // Stocke la position de la souris a la frame précédent
    private Vector2 mouseDelta;           // Stocke le déplacement de la souris

    private bool isDragging = false; 
    
    
    
    private void Start()
    {
        camera = GetComponent<Camera>();
        basicZoom = camera.orthographicSize;
        basicCameraPosition = transform.position;
    }

    private void Update()
    {
        Vector3 targetPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;

        maxPosition =
            (new Vector2(-1.8f * camera.orthographicSize + 21.6f, (-1.01f * camera.orthographicSize) + 21.23f));
        minPosition = (new Vector2(1.8f * camera.orthographicSize - 21.6f, (1.01f * camera.orthographicSize) - 21.23f));

        moveSpeed = (camera.orthographicSize * 5) + speedFactor;


        if (isDragging == false && TutorialBehaviour.cameraLocked == false)
        {
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
        }
        
        if (Input.GetMouseButtonDown(2) && MenuManager.activePanel.name == "IngamePanel")
        {
             isDragging = true;
             previousMousePosition = Input.mousePosition; 
        }

         
        if (isDragging && Input.GetMouseButton(2))
        {
             Vector2 currentMousePosition = Input.mousePosition;
             mouseDelta = currentMousePosition - previousMousePosition; 
             previousMousePosition = currentMousePosition;

             
             Debug.Log($"Mouse Delta: {mouseDelta}");
        }

        
        if (Input.GetMouseButtonUp(2))
        {
             isDragging = false;
             mouseDelta = Vector2.zero;
        }

        if (isDragging && TutorialBehaviour.cameraLocked == false)
        {
            targetPosition.x -= GetMouseDelta().x * (dragSpeedReductor * camera.orthographicSize);
            targetPosition.y -= GetMouseDelta().y * (dragSpeedReductor * camera.orthographicSize);
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        float scrollInput = 0f;
        if (TutorialBehaviour.cameraLocked == false) scrollInput = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize -= scrollInput * zoomSpeed;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoom, maxZoom);

        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);


        if (TutorialBehaviour.cameraLocked == true)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, basicZoom, Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, basicCameraPosition, Time.deltaTime);
        }
    }
    
    public Vector2 GetMouseDelta()
    {
        return mouseDelta;
    }

}

