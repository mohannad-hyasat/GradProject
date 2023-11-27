using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{




    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("NULL");
            }
            return instance;
        }
    }

    [HideInInspector] public CharacterController Player;
    [HideInInspector] public UniversalHealth PlayerHealth;
    [HideInInspector] public PlayerMovement MovementScript;
    public Transform PlayerCamera;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else instance = this;

        PlayerHealth = Instantiate(Resources.Load<GameObject>("Player").GetComponent<UniversalHealth>());
        Player = PlayerHealth.GetComponent<CharacterController>();
        PlayerCamera = GameObject.FindWithTag("Camera").transform;
        


    }

}

