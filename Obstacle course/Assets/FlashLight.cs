using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour,IInteractable
{
    public GameObject F_Light;
    private void Start()
    {
        F_Light = GameObject.FindGameObjectWithTag("FlashLight");
        F_Light.SetActive(false);
    }
    public void Interact()
    {
        F_Light.SetActive(true);
        Destroy(gameObject);
    }
}
