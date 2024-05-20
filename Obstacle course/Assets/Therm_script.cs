using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Therm_script : MonoBehaviour
{
    public TextMeshProUGUI Temp;
    private bool isOn = false;
    private AudioManager AM;
    public RoomsManager RoomsManager;
    private void Start()
    {
        gameObject.SetActive(false);
        RoomsManager = GameObject.FindGameObjectWithTag("World").GetComponent<RoomsManager>();
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AM.Play("SFX_click");
            turnOnOff();
        }
        if (isOn)
        {
            setTemp();
        }
        else
        {
            Temp.text = string.Empty;
        }
    }
    public void turnOnOff()
    {
        if (isOn)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        isOn = !isOn;
    }
    public void setTemp()
    {
        Temp.text = ((int)RoomsManager.Temp_C).ToString() + " C";
    }
}
