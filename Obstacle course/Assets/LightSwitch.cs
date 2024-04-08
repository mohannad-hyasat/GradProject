using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] Lights;
    public bool OnOff = false;
    


    private void Awake()
    {
        Lights = GetComponentsInChildren<Light>();
        

    }
    private void Start()
    {
        foreach(Light light in Lights)
        {
            light.intensity = 0;
        }
    }


    private void Update()
    {
       
        if (OnOff)
        {
            foreach (Light light in Lights)
            {
                light.intensity = 0.5f;
                
            }

        }
        if (!OnOff)
        {
            foreach (Light light in Lights)
            {
                light.intensity = 0;
            }
           
        }
    }



}
