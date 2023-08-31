using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float mouseSensitivity;




    [Header("Player transfrom")]
    public Transform PlayerOriantation;
    [HideInInspector]
    public float X_Rotation = 0f;
    [HideInInspector]
    public float Y_Rotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerOriantation = transform.parent;
    }
    /// <summary>
    /// Takes in mouse inputs and turns the inputs into A eular rotation
    /// </summary>
    public void RotatePlayerAndCamera()
    {
        float mouse_Input_X = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouse_Input_Y = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        Y_Rotation += mouse_Input_X;
        X_Rotation -= mouse_Input_Y;
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(X_Rotation, -90f, 90f), Y_Rotation, 0f);
        PlayerOriantation.Rotate(Vector3.up, mouse_Input_X);
    }
    void Update()
    {
        RotatePlayerAndCamera();
    }
}
