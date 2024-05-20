using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicalAI : MonoBehaviour
{
    [Header("Misc Attributes")]
    public UnityEngine.AI.NavMeshAgent Enemy;
    public Transform Player;
    public Animator Anim;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
    public UniversalHealth Enemy_Health;
    [Header("Attack Attributes")]
    public bool IsHaunting;
    public float RotationSpeed;

    [Header("Distance From PlayerPos")]
    public float DistanceFromPlayer;
    [Header("Rooms")]
    public Transform Fav_Room;
    public float Range;
    public RoomsManager RoomManager;


    private void Start()
    {
        Enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();
        RoomManager = GameObject.FindGameObjectWithTag("World").GetComponent<RoomsManager>();
        Fav_Room = RoomManager.Favorite_Room;
        Invoke("GetPlayer", 3);

    }
    private void GetPlayer()
    {
        Player = GameManager.Instance.Player.transform;
    }
    public void Enemy_Patroling()
    {


        Vector3 point;
        if (RandomPoint(Fav_Room.position, Range, out point)) //pass in our centre point and radius of area
        {

            Enemy.SetDestination(point);
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void FixedUpdate()
    {
        if (Player != null)
        {
            DistanceFromPlayer = Vector3.Distance(Player.position, gameObject.transform.position);

            if (Enemy.remainingDistance <= Enemy.stoppingDistance) //done with path
            {
                if (!IsHaunting)
                {
                    Enemy_Patroling();
                }
                else
                {

                }
                Anim.SetBool("walk", false);
            }
            else
                Anim.SetBool("walk", true);

        }

    }
}
