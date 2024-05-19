using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject Light;
    public bool On = false;
    private AudioManager AM;
    private void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Light.SetActive(false);
    }
    private void Update()
    {  
        if(Input.GetKeyDown(KeyCode.F))
        {
            AM.Play("SFX_click");
            Light.SetActive(!On);
            On = !On;
        }
    }

}
