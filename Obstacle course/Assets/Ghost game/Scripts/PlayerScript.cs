using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public UniversalHealth player;
    public Vector3 PlayerPos
    {
        get
        {
            return player.transform.position;
        }
    }
    public float Sanity { get; private set; }
    public float health { get; private set; }


    public bool Alive {
        get
        {
            return health > 0;
        }
    }

    void Start()
    {
        Invoke("GetPlayer", 3);
    }

    private void GetPlayer()
    {
        player = GameManager.Instance.Player;
    }
}
