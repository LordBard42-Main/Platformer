using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Target and Speed")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float cameraSpeed = 5;

    [Header("Camera Travel Borders")]
    [SerializeField] private float currentXMax = 0;
    [SerializeField] private float currentXMin = 0;
    [SerializeField] private float currentYMax = 0;
    [SerializeField] private float currentYMin = 10;

    private Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        //SetMaxMinBordersForCamera(0, 10, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
        
    }

    /// <summary>
    /// Sets the world boundries the the camera cannot cross
    /// </summary>
    /// <param name="minX"></param>
    /// <param name="maxX"></param>
    /// <param name="minY"></param>
    /// <param name="maxY"></param>
    public void SetMaxMinBordersForCamera(float minX, float maxX, float minY, float maxY)
    {
        currentXMin = minX;
        currentXMax = maxX;
        currentYMin = minY;
        currentYMax = maxY;
    }

    public void UpdateCameraPosition()
    {
        //Doesnt let Camera Go past edge of the world
        temp.x = Mathf.Clamp(playerTransform.position.x, currentXMin, currentXMax);
        temp.y = Mathf.Clamp(playerTransform.position.y, currentYMin, currentYMax);
        temp.z = -10;

        transform.position = Vector3.Lerp(transform.position, temp, Time.deltaTime * cameraSpeed);

    }

}
