using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform PlayerCam;
    public Transform DistChecker;
    public Transform Hinge;

    public float OpenSpeed;
    public Vector2 RotationConstraints;

    bool movingDoor;
    float rotation;
    Vector3 targetPosition;
    void Start()
    {
        DistChecker = GameObject.FindWithTag("DistanceChecker").transform;
        Hinge = GameObject.FindWithTag("Hinge").transform;
        PlayerCam = GameManager.Instance.PlayerCamera;
        targetPosition = DistChecker.position;
        OpenSpeed = 20;
        RotationConstraints = new Vector2(-90, 90);
        
    }
    /// <summary>
    /// Gets the Rotation of the door
    /// </summary>
    /// <returns>-firstDistance + secondDistance</returns>
    float GetRotation()
    {
        float firstDistance = (DistChecker.position - targetPosition).sqrMagnitude;
        Hinge.Rotate(Vector3.up);
        float secondDistance = (DistChecker.position - targetPosition).sqrMagnitude;
        Hinge.Rotate(-Vector3.up);
        return -firstDistance + secondDistance;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            if(Physics.Raycast(PlayerCam.position, PlayerCam.forward, out RaycastHit Hit, 3f))
            {
                if (Hit.collider.CompareTag("door"))
                {
                    movingDoor = true;
                }
            }
        }
        if (movingDoor)
        {
            if(Input.GetMouseButtonUp(0))
            {
                movingDoor= false;
            }
            targetPosition = PlayerCam.position + PlayerCam.forward * 2f;
        }
        rotation += Mathf.Clamp(-GetRotation() * 5000 * Time.deltaTime, -OpenSpeed, OpenSpeed);
        rotation = Mathf.Clamp(rotation, RotationConstraints.x, RotationConstraints.y);
        Hinge.rotation = Quaternion.Euler(0, rotation, 0);
    }

    
}
