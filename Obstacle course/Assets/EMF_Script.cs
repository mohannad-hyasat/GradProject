using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF_Script : MonoBehaviour
{
    public GameObject[] Lights;
    public bool isOn = false;
    private AudioManager AM;
    public RoomsManager RoomsManager;

    private void Start()
    {
        RoomsManager = GameObject.FindGameObjectWithTag("World").GetComponent<RoomsManager>();
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        foreach(GameObject light in Lights)
        {
            light.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AM.Play("SFX_click");
            turnOnOff();
        }
        if(isOn)
        {
            SetEmf();
        }
        
    }

    private void SetEmf()
    {
        for(int i =1; i < Lights.Length+1; i++)
        {
            if(i == RoomsManager.Emf_Level)
            {
                Lights[i-1].SetActive(true);
                
            }
            else
            {
                Lights[i-1].SetActive(false);
            }
            
        }
    }



    public void turnOnOff()
    {
        if (isOn)
        {
            foreach (GameObject light in Lights)
            {
                light.SetActive(false);
            }
        }
        else
        {
            Lights[0].SetActive(true);
        }
        isOn = !isOn;
    }
}
