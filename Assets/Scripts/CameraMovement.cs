using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Setting Min and Max Angles for the Y axis movement
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 30.0f;

    public Joystick cameraJoystick;
    public Transform lookAt;
    public float smoothSpeed = 0.250f;
    public Transform camTransform;

    private Camera cam;

    Vector2 input;

    //Calling the Axises and Sensitvity
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 3.0f;
    private float sensivityY = 2.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }


    private void Update()
    {
        //Zoom In & Out for Mouse use only
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cam.fieldOfView > 33)
            {
                cam.fieldOfView--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.fieldOfView < 43)
            {
                cam.fieldOfView++;
            }
        }

        //Camera Movement Joystick
        currentX += cameraJoystick.Horizontal * sensivityX;
        currentY += cameraJoystick.Vertical * sensivityY;

        Vector3 camF = camTransform.forward;
        Vector3 camR = camTransform.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;

        transform.position += (camF * input.y + camR * input.x) * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    //Rest of Camera Movement Joystick
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 1, -4);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, dir, smoothSpeed);
        transform.position = smoothPosition;
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }

}
