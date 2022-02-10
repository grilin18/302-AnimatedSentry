using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset = new Vector3(0, 1, 0);

    private Camera cam;

    private float pitch = 0;
    private float yaw = 0;

    private float dollyDis = 10;
    public float scrollSensitivity = 3;

    public float mouseSensitivityX = 5;
    public float mouseSensitivityY = -5;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.position = AnimMath.Ease(transform.position, target.position + targetOffset, .01f, Time.deltaTime);
        }

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        yaw += mx * mouseSensitivityX;
        pitch += my * mouseSensitivityY;

        pitch = Mathf.Clamp(pitch, -10, 89);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        dollyDis += Input.mouseScrollDelta.y * scrollSensitivity;
        dollyDis = Mathf.Clamp(dollyDis, 3, 10);

        cam.transform.localPosition = AnimMath.Ease(cam.transform.localPosition, new Vector3(0, 0, -dollyDis), .02f, Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        if (!cam) cam = GetComponentInChildren<Camera>();
        if (!cam) return;

        Gizmos.DrawWireCube(transform.position, Vector3.one);
        Gizmos.DrawLine(transform.position, cam.transform.position);

    }



}
