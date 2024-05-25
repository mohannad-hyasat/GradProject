using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("PlayerPos transfrom")]
    public Transform PlayerOriantation;
    public UniversalHealth Playerhealth;
    [HideInInspector]
    public float X_Rotation = 0f;
    [HideInInspector]
    public float Y_Rotation = 0f;
    [Header("Mouse sensitivity")]
    public float mouseSensitivity;
    public Camera cam;
    public bool haunting;
    public EnemyAiManager ghostScript;

    public Transform Head;
    void Start()
    {
        ghostScript = FindObjectOfType<EnemyAiManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerOriantation = transform.parent;
        Playerhealth = GetComponentInParent<UniversalHealth>();
        Head = GameObject.Find("Pov").transform;
    }
    /// <summary>
    /// Takes in mouse inputs and turns the inputs into A eular rotation
    /// </summary>
    public void RotatePlayerAndCamera()
    {
        float mouse_Input_X = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouse_Input_Y = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        Y_Rotation += mouse_Input_X;
        X_Rotation -= mouse_Input_Y;
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(X_Rotation, -90f, 90f), Y_Rotation, 0f);
        PlayerOriantation.Rotate(Vector3.up, mouse_Input_X * mouseSensitivity);
        transform.position = Head.position;
    }
    void Update()
    {
        if (!Playerhealth.IsDead )
        {
            RotatePlayerAndCamera(); 
        }

        if (ghostScript.Ishaunting)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    public void Hide()
    {
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("ghost"));
    }
    public void Show()
    {
        cam.cullingMask |= 1 << LayerMask.NameToLayer("ghost");
    }
}
