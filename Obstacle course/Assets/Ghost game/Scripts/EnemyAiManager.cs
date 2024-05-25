using Google.Protobuf.WellKnownTypes;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiManager : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Player;
    public bool Ishaunting;
    public UniversalHealth PlayerSanity;
    public Transform FavRoom;
    public int hauntmultiplier;


    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().transform;
        PlayerSanity = FindObjectOfType<UniversalHealth>().GetComponent<UniversalHealth>();
        FavRoom = FindAnyObjectByType<RoomsManager>().GetComponent<RoomsManager>().Favorite_Room;
        Ishaunting = false;
        InvokeRepeating(nameof(hauntRandom), 1, 1);
        InvokeRepeating(nameof(PlayerFinder), 1, 0.3f);
    }

    public void Haunt()
    {
        gameObject.transform.position = FavRoom.position;
    }

    public void hauntRandom()
    {
        hauntmultiplier = Random.Range(1, 100);
    }
    public void PlayerFinder()
    {
        Player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().transform;
    }
    private void Update()
    {
        Agent.SetDestination(Player.position);

        if(PlayerSanity.Sanity <= 75 && !Ishaunting)
        {
           if (hauntmultiplier <= 15)
            {
                Haunt();
                Ishaunting = true;
                Agent.SetDestination(Player.position);
            }
          

        }
        if (hauntmultiplier >= 85)
        {
            Ishaunting = false;
            Agent.SetDestination(Player.position);
        }
        
    }


}


