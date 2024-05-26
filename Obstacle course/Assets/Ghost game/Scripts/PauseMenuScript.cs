using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject EndGame_Screen;
    public GameObject PauseMenu_Screen;
    public static bool isPaused;
    public Journal journal;
    public RoomsManager roomManager;
    private AudioManager audioManager;
    public TextMeshProUGUI EndGameText;
    public UniversalHealth PlayerStatus;
    
    
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        PauseMenu_Screen.SetActive(false);
        EndGame_Screen.SetActive(false);
        journal = gameObject.GetComponent<Journal>();
        roomManager = GameObject.FindGameObjectWithTag("World").GetComponent<RoomsManager>();
        PlayerStatus = FindObjectOfType<UniversalHealth>().GetComponent<UniversalHealth>();
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if(PlayerStatus.IsDead)
        {
            EndGame();
        }
    }

    public void PauseGame()
    {
        PauseMenu_Screen.SetActive(true);
        Time.timeScale =  0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu_Screen.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }
    public void EndGame()
    {
        EndGame_Screen.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        if (journal.GetGhostGuess() == roomManager.GhostType)
        {
            EndGameText.text = "CONGRATS, YOU WIN!!";
        }
        else
        {
            EndGameText.text = "BOOOOOO, YOU LOSE!!";
        }
        

    }
    public void BackToStart()
    {
        SceneManager.LoadScene(0);
    }
}
