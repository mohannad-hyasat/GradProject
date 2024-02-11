using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollInteractable : MonoBehaviour,IInteractable
{
    public RoomsManager Rm;
    public ParticleSystem DollDestroyVFX;
    private void Start()
    {
        Rm = FindObjectOfType<RoomsManager>().GetComponent<RoomsManager>();
    }

    public void Interact()
    {

        Rm.DollDiscovered = true;
       // DollDestroyVFX.Play();
        Invoke(nameof(DestoryDoll), 1f);


    }

    void DestoryDoll()
    {
        Destroy(gameObject);
    }

}
