using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform ori;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ori = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime*mouseSensitivity;
        float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        yRotation += mouseX;
        xRotation -= mousey;


        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        ori.Rotate(Vector3.up, mouseX);
    }
}
