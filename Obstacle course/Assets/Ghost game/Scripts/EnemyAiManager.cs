using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Timers;

public class EnemyAiManager : MonoBehaviour
{

    [Header("Misc Attributes")]
    public NavMeshAgent Enemy;
    public Transform Player;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
    public UniversalHealth Enemy_Health;
    [Header("Attack Attributes")]
    public float AttackRange;
    public bool IsHaunting;
    public bool IsAttacking;

    public float RotationSpeed;
    

    [Header("Patrol Attributes")]
    public Transform[] Walkpoints;
    [HideInInspector] public int NextWalkpoint = 0;
    [HideInInspector] public Transform Current_walkpoint;
    [HideInInspector] public float DistanceFromWalkPoint;
    public bool Reached;
    public float TimeBetweenWalkpoints;

    [Header("Distance From Player")]
    public float DistanceFromPlayer;
    public Transform Favorite_Room;
    public Transform[] Rooms;


    private void Start()
    {
        Enemy = GetComponent<NavMeshAgent>();
        Player = GameManager.Instance.Player.transform;

        Current_walkpoint = Walkpoints[0];
    }
    public void Enemy_Patroling()
    {


        if (Reached)
        {
            StartCoroutine(Patrol());
            Enemy_Health.Anim.SetBool("Walking", false);
            Enemy_Health.Anim.SetBool("Running", false);

        }
        else
        {
            Enemy.SetDestination(Current_walkpoint.position);
            Enemy_Health.Anim.SetBool("Walking", true);
        }

    }
    IEnumerator Patrol()
    {
        if (Walkpoints.Length == 0)
        {
            yield return null;
        }


        yield return new WaitForSecondsRealtime(TimeBetweenWalkpoints);

        if (Reached)
        {
            NextWalkpoint = (NextWalkpoint + 1) % Walkpoints.Length;
            Current_walkpoint = Walkpoints[NextWalkpoint];

        }

        yield return new WaitForSecondsRealtime(TimeBetweenWalkpoints);
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, UnintrestedRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, FollowRange);
    }

    private void FixedUpdate()
    {
        DistanceFromPlayer = Vector3.Distance(Player.position, gameObject.transform.position);
       

    }
}


