using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour,IInteractable
{
    public GameObject F_Light;
    public PlayerMovement playerScript;
    private void Start()
    {
        F_Light = GameObject.FindGameObjectWithTag("FlashLight");
        F_Light.SetActive(false);
    }
    public void Interact()
    {
        F_Light.SetActive(true);
        playerScript.itemsPickedUp[0] = 1;
        Destroy(gameObject);
    }
}
