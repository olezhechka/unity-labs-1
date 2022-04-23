using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 200f;
    [SerializeField]
    private float moveBorderPadding = 30f;

    [SerializeField]
    private float minX = 500f;
    [SerializeField]
    private float maxX = 800f;
    [SerializeField]
    private float minY = 300f;
    [SerializeField]
    private float maxY = 800f;
    [SerializeField]
    private float minZ = 600f;
    [SerializeField]
    private float maxZ = 1200f;

    [SerializeField]
    private float zoomSpeed = 150f;

    private const float ZOOM_SPEED_AMPLIFIER = 100f;

    void Update()
    {
        Vector3 newCameraPosition = this.transform.position;

        bool shouldMoveUp = Input.mousePosition.y <= this.moveBorderPadding;
        bool shouldMoveDown = Input.mousePosition.y >= (Screen.height - this.moveBorderPadding);
        bool shouldMoveLeft = Input.mousePosition.x <= this.moveBorderPadding;
        bool shouldMoveRight = Input.mousePosition.x >= (Screen.width - this.moveBorderPadding);

        if (shouldMoveUp)
        {
            newCameraPosition.z += this.moveSpeed * Time.deltaTime;
        }
        if(shouldMoveDown)
        {
            newCameraPosition.z -= this.moveSpeed * Time.deltaTime;
        }
        if(shouldMoveLeft)
        {
            newCameraPosition.x += this.moveSpeed * Time.deltaTime;
        }
        if(shouldMoveRight)
        {
            newCameraPosition.x -= this.moveSpeed * Time.deltaTime;
        }

        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        newCameraPosition.y -= zoomDelta * zoomSpeed * ZOOM_SPEED_AMPLIFIER * Time.deltaTime;

        // Limit camera movement by boundaries
        newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, this.minX, this.maxX);
        newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, this.minY, this.maxY);
        newCameraPosition.z = Mathf.Clamp(newCameraPosition.z, this.minZ, this.maxZ);

        this.transform.position = newCameraPosition;
    }
}
