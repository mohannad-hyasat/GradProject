using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour,IInteractable
{
    public GameObject F_Light;
    public PlayerCam cam;
    private void Start()
    {
        cam = FindObjectOfType<PlayerCam>().GetComponent<PlayerCam>();
        F_Light = cam.GetComponentInChildren<GameObject>();
    }
    public void Interact()
    {

    }
}
