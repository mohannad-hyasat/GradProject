using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform playerCam;
    public Transform distChecker;
    public Transform hinge;

    public float openSpeed;
    public Vector2 rotationConstraints;

    bool movingDoor;
    float rotation;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = distChecker.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            if(Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit Hit, 3f))
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
            targetPosition = playerCam.position + playerCam.forward * 2f;
        }
        rotation += Mathf.Clamp(-GetRotation() * 5000 * Time.deltaTime, -openSpeed, openSpeed);
        rotation = Mathf.Clamp(rotation, rotationConstraints.x, rotationConstraints.y);
        hinge.rotation = Quaternion.Euler(0, rotation, 0);
    }

    float GetRotation()
    {
        float firstDistance = (distChecker.position - targetPosition).sqrMagnitude;
        hinge.Rotate(Vector3.up);
        float secondDistance = (distChecker.position - targetPosition).sqrMagnitude;
        hinge.Rotate(-Vector3.up);
        return -firstDistance + secondDistance;
    }
}
