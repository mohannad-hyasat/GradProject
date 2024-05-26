using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [HideInInspector] public UniversalHealth Player;
    [HideInInspector] public PlayerMovement MovementScript;
    public Transform PlayerCamera;
    public Transform Spawn;

    public Transform Fav_Room;
    public RoomsManager RoomManager;


 


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else instance = this;
        Spawn = GameObject.FindGameObjectWithTag("Spawn").transform;
        Player = GameObject.Instantiate(Resources.Load<GameObject>("Player").GetComponent<UniversalHealth>(),Spawn);
        PlayerCamera = GameObject.FindWithTag("Camera").transform;

    }
    private void SpawnPlayer() 
    {
        
    }


}

