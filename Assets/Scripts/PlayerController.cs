using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float runSpeed = 10f;
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private Transform icon;
    [SerializeField]
    private float iconRotationSpeed = 20f;

    private bool run = false;
    private bool iconRotate = false;
    private int groundMask;
    private int iconMask;

    private void Start()
    {
        groundMask = LayerMask.GetMask(Constants.LAYER_GROUND);
        iconMask = LayerMask.GetMask(Constants.LAYER_ICON);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, iconMask))
            {
                iconRotate = true;
            }
            else
            {
                run = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            run = false;
            iconRotate = false;
        }
        if (run)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, groundMask))
            {
                Vector3 hitPos = hit.point;
                Vector3 hitDir = hitPos - transform.position;
                float step = rotationSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, hitDir, step, 0.0f);
                newDir.y = 0.0f;
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position += transform.forward * runSpeed * Time.deltaTime;
            }
        }
        if (iconRotate)
        {
            float x = Input.GetAxis("Mouse X") * iconRotationSpeed;
            float y = Input.GetAxis("Mouse Y") * iconRotationSpeed;
            icon.Rotate(Vector3.down, x);
            icon.Rotate(Vector3.right, y);
        }
    }
}