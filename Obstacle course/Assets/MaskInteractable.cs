using UnityEngine;

public class MaskInteractable : MonoBehaviour,IInteractable
{
    public RoomsManager Rm;
    public ParticleSystem MaskDestroyVFX;
    public UniversalHealth Player;
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Rm = FindObjectOfType<RoomsManager>().GetComponent<RoomsManager>();
        Player = FindObjectOfType<UniversalHealth>().GetComponent<UniversalHealth>();
    }
    public void Interact()
    {
        audioManager.Play("SFX_whisper");
        Rm.MaskDiscovered = true;
        MaskDestroyVFX.Play();
        Player.Apply_Insanity(15f);
        Invoke(nameof(DestoryMask), 1f);
    }
    void DestoryMask()
    {
        Destroy(gameObject);
    }
}
