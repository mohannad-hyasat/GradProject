using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.AI;
using System.Threading.Tasks;

public class MLAgent : Agent
{
    public float speed = 2f;
    public bool trainingMode;

    [Header("Misc Attributes")]
    public NavMeshAgent Enemy;
    public Transform PlayerPos;
    public Animator Anim;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
    public UniversalHealth Player_Health;

    [Header("Attack Attributes")]
    public bool IsHaunting;
    public float RotationSpeed;

    [Header("Distance From Player")]
    public float DistanceFromPlayer;
    [Header("Rooms")]
    public Transform Fav_Room;
    public float Range;
    public RoomsManager RoomManager;

    private bool duringHaunt = false;

    public const float hauntDuration = 2.5f;

    public override void Initialize()
    {



    }
    private void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        Enemy = GetComponent<NavMeshAgent>();
        RoomManager = GameObject.FindGameObjectWithTag("World").GetComponent<RoomsManager>();
        StartCoroutine(PlayerHandler());
        Get_Player();

    }
    public override void OnEpisodeBegin()
    {
        // Reset the agent and the player to their starting positions
        //transform.localPosition = new Vector3(Random.Range(-4, 4), 0.5f, Random.Range(-4, 4));
        //PlayerPos.localPosition = new Vector3(Random.Range(-4, 4), 0.5f, Random.Range(-4, 4));
        RoomManager.SetFavRoom();
        Fav_Room = RoomManager.Favorite_Room;
        //PlayerPos.localPosition = new Vector3(Fav_Room.position.x * Random.Range(-2,2), Fav_Room.position.y, Fav_Room.position.z * Random.Range(-2, 2));
        transform.localPosition = Fav_Room.localPosition;
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect the agent's and player's positions as observations
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(PlayerPos.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Convert actions to movement
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        Vector3 move = new Vector3(moveX, 0, moveZ);
        transform.Translate(move * speed * Time.deltaTime);

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.localPosition, PlayerPos.localPosition);

        // Reward the agent for getting closer to the player
        if (trainingMode)
        {
            if (distanceToPlayer < 1.5f)
            {
                SetReward(1.0f);
            }
            else
            {
                SetReward(-0.1f);
            }
        }
        

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Provide manual control for testing purposes
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
    private void Get_Player()
    {
        Player_Health = GameManager.Instance.Player;
        PlayerPos = GameManager.Instance.Player.transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (trainingMode && collision.collider.CompareTag("boundary"))
        {
            AddReward(-.5f);
        }
    }
    private async void Haunt()
    {
        duringHaunt = true;
        IsHaunting = true;
        gameObject.transform.position = Fav_Room.transform.position;
        float timeLapsed = 0f;
        while (timeLapsed < hauntDuration)
        { 
            timeLapsed += Time.deltaTime;
            await Task.Yield();
        }
        duringHaunt = false;
        IsHaunting = false;
    }
    private IEnumerator PlayerHandler()
    {
        yield return new WaitForSeconds(5);
    }
    private void FixedUpdate()
    {
        if(IsHaunting && !duringHaunt)
        {
            Haunt();
        }
        /*if(Player_Health.Sanity < 75)
        {
            Invoke("probability", 2);
            
        }*/
    }/*
    private void probability()
    {
        if (Random.Range(1, 100) <= 10)
            {
                StartHaunt();
            }
    }

    private void StartHaunt()
    {
        throw new System.NotImplementedException();
    }*/
}
