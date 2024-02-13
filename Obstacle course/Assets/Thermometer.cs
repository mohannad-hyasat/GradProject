using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermometer : MonoBehaviour,IInteractable
{
    public GameObject thermometer;
    public PlayerMovement playerScript;
    private void Start()
    {
        thermometer = GameObject.FindGameObjectWithTag("thermometer");
        thermometer.SetActive(false);
    }
    public void Interact()
    {
        thermometer.SetActive(true);
        playerScript.itemsPickedUp[2] = 1;
        Destroy(gameObject);
    }
}
