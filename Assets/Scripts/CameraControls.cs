using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float sens = 100f;
    public Transform playerBody;
    public float xr = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        xr -= mouseY;
        xr = Mathf.Clamp(xr, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xr, 90f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
