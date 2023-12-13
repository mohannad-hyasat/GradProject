using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiManager : MonoBehaviour
{

    [Header("Misc Attributes")]
    public NavMeshAgent Enemy;
    public Transform Player;
    public Animator Anim;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
    public UniversalHealth Enemy_Health;
    [Header("Attack Attributes")]
    public bool IsHaunting;
    public float RotationSpeed;
    
    [Header("Distance From Player")]
    public float DistanceFromPlayer;
    [Header("Rooms")]
    public Transform Favorite_Room;
    public Transform[] Rooms;

    [Header("Point Randomizer")]
    public float Range; //radius of sphere
    public Transform CentrePoint;
    private void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        Player = GameManager.Instance.Player.transform;
    }
    public void Enemy_Patroling()
    {

        
            Vector3 point;
            if (RandomPoint(CentrePoint.position, Range, out point)) //pass in our centre point and radius of area
            {
                
                Enemy.SetDestination(point);
            }
            
        

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
   
    private void FixedUpdate()
    {
        DistanceFromPlayer = Vector3.Distance(Player.position, gameObject.transform.position);
        if (Enemy.remainingDistance <= Enemy.stoppingDistance) //done with path
        {
            Enemy_Patroling();
            Anim.SetBool("walk", false);
        }
        else
            Anim.SetBool("walk", true);

    }
}


