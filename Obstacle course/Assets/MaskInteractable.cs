using UnityEngine;

public class MaskInteractable : MonoBehaviour,IInteractable
{
    public RoomsManager Rm;
    public ParticleSystem MaskDestroyVFX;
    private void Start()
    {
        Rm = FindObjectOfType<RoomsManager>().GetComponent<RoomsManager>();
    }
    public void Interact()
    {
        Rm.MaskDiscovered = true;
        MaskDestroyVFX.Play();
        Invoke(nameof(DestoryMask), 1f);
    }
    void DestoryMask()
    {
        Destroy(gameObject);
    }
}
