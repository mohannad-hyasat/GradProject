using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject Light;
    public bool On = false;
    private void Start()
    {
        Light.SetActive(false);
    }
    private void Update()
    {  
        if(Input.GetKeyDown(KeyCode.F))
        {
            Light.SetActive(!On);
            On = !On;
        }
    }

}
