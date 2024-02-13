using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF : MonoBehaviour,IInteractable
{
    public GameObject emf;
    public PlayerMovement playerScript;
    private void Start()
    {
        emf = GameObject.FindGameObjectWithTag("emf");
        emf.SetActive(false);
    }
    public void Interact()
    {
        emf.SetActive(true);
        playerScript.itemsPickedUp[1] = 1;
        Destroy(gameObject);
    }
}
