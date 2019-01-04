using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float smoothFactor = 0.5f;
    [SerializeField]
    private float minDistance = 1.5f;
    [SerializeField]
    private float maxDistance = 30f;
    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float mouseSensitivity = 0.5f;

    private Vector3 cameraOffset;

    private bool lookAtTarget = false;
    private bool rotateAround = false;

    private void Start()
    {
        cameraOffset = transform.position - target.position;
        lookAtTarget = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rotateAround = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rotateAround = false;
        }
    }

    private void LateUpdate()
    {
        if (rotateAround)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed;
            Quaternion camTurnAngle = Quaternion.Euler(y, x, 0);
            cameraOffset = camTurnAngle * cameraOffset;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * mouseSensitivity;
            float cameraDistance = cameraOffset.magnitude;
            scrollAmount *= cameraDistance;
            float newCameraDistance = cameraDistance + scrollAmount * -1f;
            newCameraDistance = Mathf.Clamp(newCameraDistance, minDistance, maxDistance);
            cameraOffset *= (newCameraDistance / cameraDistance);
        }
        Vector3 newPosition = target.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        if (lookAtTarget || rotateAround)
        {
            transform.LookAt(target);
        }
    }
}
