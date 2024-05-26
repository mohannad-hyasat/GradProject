using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiManager : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Player;
    public bool Ishaunting;
    public UniversalHealth PlayerSanity;
    public Transform FavRoom;
    private AudioManager AM;
    public int hauntmultiplier;
    private const float hauntDuration = 20f;
    private bool duringHaunt = false;
    private bool duringCooldown = false;
    private const float cooldownDuration = 60f;


    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().transform;
        PlayerSanity = FindObjectOfType<UniversalHealth>().GetComponent<UniversalHealth>();
        FavRoom = FindAnyObjectByType<RoomsManager>().GetComponent<RoomsManager>().Favorite_Room;
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Ishaunting = false;
        InvokeRepeating(nameof(hauntRandom), 1, 1);
        InvokeRepeating(nameof(PlayerFinder), 1, 0.3f);
    }

    private async void Haunt()
    {
        duringHaunt = true;
        Ishaunting = true;
        gameObject.transform.position = FavRoom.transform.position;
        AM.Play("SFX_Haunt");
        float timeLapsed = 0f;
        while (timeLapsed < hauntDuration)
        {
            Agent.SetDestination(Player.position);
            timeLapsed += Time.deltaTime;
            await Task.Yield();
        }
        duringHaunt = false;
        Ishaunting = false;
    }
    private async void HauntCooldown()
    {
        duringCooldown = true;
        float timeLapsed = 0f;
        while (timeLapsed < cooldownDuration)
        {
            Agent.SetDestination(Player.position);
            timeLapsed += Time.deltaTime;
            await Task.Yield();
        }
        duringCooldown = false;
    }

    public void hauntRandom()
    {
        hauntmultiplier = Random.Range(1, 100);
    }
    public void PlayerFinder()
    {
        Player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(Ishaunting)
        {
            if(other.CompareTag("Player"))
            {
                PlayerSanity.Apply_Damage(100);
            }



        }



    }



    private void Update()
    {
        Agent.SetDestination(Player.position);

        if(PlayerSanity.Sanity <= 75 && !Ishaunting)
        {
           if (hauntmultiplier <= 15 && !duringHaunt && !duringCooldown)
            {
                Haunt();
                HauntCooldown();
            }
          

        }
        
    }


}


