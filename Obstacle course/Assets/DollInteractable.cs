using UnityEngine;

public class DollInteractable : MonoBehaviour,IInteractable
{
    public RoomsManager Rm;
    public ParticleSystem DollDestroyVFX;
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
        Rm.DollDiscovered = true;
        DollDestroyVFX.Play();
        Player.Apply_Insanity(15f);
        Invoke(nameof(DestoryDoll), 1f);
    }

    void DestoryDoll()
    {
        Destroy(gameObject);
    }

}
