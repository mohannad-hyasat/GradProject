using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public GameObject journal;
    public static bool isPaused;
    public static Ghost_Type Ghost_Guess;
    private void Start()
    {
        journal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) 
        {
            journal.SetActive(!isPaused);
            Time.timeScale = isPaused ? 1.0f : 0.0f;
            Cursor.visible = !isPaused;
            Cursor.lockState = isPaused ? CursorLockMode.Locked : CursorLockMode.None;
            isPaused = !isPaused;
        }
    }
    public Ghost_Type GetGhostGuess()
    {
        return Ghost_Guess;
    }
    public void SetGhostGuess(string GhostType)
    {
        if (GhostType == "Kikimora")
        {
            Ghost_Guess = Ghost_Type.Kikimora;
            
        }

        if (GhostType == "Ogbanje")
        {
            Ghost_Guess = Ghost_Type.Ogbanje;
            
        }

        if (GhostType == "Jinn")
        {
            Ghost_Guess = Ghost_Type.Jinn;
            
        }
        if (GhostType == "mylingar")
        {
            Ghost_Guess = Ghost_Type.mylingar;
            
        }
    }
}
