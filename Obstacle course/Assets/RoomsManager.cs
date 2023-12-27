using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [Header("Rooms")]
    public Transform Favorite_Room;
    public Transform[] Rooms;
    private void Awake()
    {
        Favorite_Room = Rooms[Random.Range(0, Rooms.Length)];
    }

}
