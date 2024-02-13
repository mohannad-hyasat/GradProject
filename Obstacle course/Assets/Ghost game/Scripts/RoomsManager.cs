using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [Header("Player Settings")]
    public float DistanceBetweenPlayerAndRoom;

    [Header("Rooms")]
    public Transform Favorite_Room;
    public Transform[] Rooms;
  

    [Header("Tempreture Controls")]
    public float Temp_C = 15;
    public const float Max_Temp = 45;
    public const float Min_Temp = -15;
    public int DistanceToTrigger;

    [Header("Ghost Choice Settings")]
    public Ghost_Type GhostType;
    [HideInInspector] public GameObject Moths;

    [Header("EMF Contorols")]
    public int Emf_Level;
    private const int Max_Emf = 5;
    private const int Min_Emf = 1;
    public int EmfRange;

    [Header("Discoverables settings")]
    public GameObject Mask;
    public GameObject Doll;
    public bool DollDiscovered = false;
    public bool MaskDiscovered = false;
    public Transform[] AllSpawnablesLocations;
    public Transform DollSpawnLocation;
    public Transform MaskSpawnLocation;
    private void Awake()
    {
        Favorite_Room = Rooms[Random.Range(0, Rooms.Length)]; // randomize favorite room
        GhostType = (Ghost_Type)Random.Range(0, 4);    // randomize ghost type
        DollSpawnLocation = AllSpawnablesLocations[Random.Range(0, AllSpawnablesLocations.Length)];
        MaskSpawnLocation = AllSpawnablesLocations[Random.Range(0, AllSpawnablesLocations.Length)];
        if(MaskSpawnLocation == DollSpawnLocation)
        {
            RandmoizeSpawnableLocation();
        }
        
    }
    private void Start()
    {
        Mask = GameObject.Instantiate(Resources.Load<GameObject>("Mask"),MaskSpawnLocation);
        Doll = GameObject.Instantiate(Resources.Load<GameObject>("Doll"), DollSpawnLocation);

        Emf_Level = Min_Emf;
    }
    private void OnDrawGizmos()
    {
        if (Favorite_Room != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Favorite_Room.position, DistanceToTrigger);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Favorite_Room.position, EmfRange);
        }
    }
    public void Kikimora()
    {
        if(DollDiscovered)
        {
            Moths = GameObject.Instantiate(Resources.Load<GameObject>("Moths area"), Favorite_Room);
          DollDiscovered = false;
        }
        if (MaskDiscovered)
        {
            if (DistanceBetweenPlayerAndRoom <= DistanceToTrigger)
            {

                if (Temp_C >= Min_Temp)
                {
                    Temp_C -= 0.01f;
                }

            }
            else
            {
                Temp_C = Random.Range(1f, 30f);
            }
        }
        else
        {
            Temp_C = Random.Range(10f, 30f);
        }

    }
    public void Ogbanje()
    {
        if (DollDiscovered)
        {
            Moths = GameObject.Instantiate(Resources.Load<GameObject>("Moths area"), Favorite_Room);
            DollDiscovered = false;
        }
        if (MaskDiscovered)
        {
            if (DistanceBetweenPlayerAndRoom <= DistanceToTrigger)
            {

                if (Temp_C <= Max_Temp)
                {
                    Temp_C += 0.01f;
                }
                else
                {
                    Temp_C = Random.Range(10f, 30f);
                }

            }
            
        }
        else
        {
            Temp_C = Random.Range(10f, 30f);
        }
    }

    public void Jinn()
    {
       

        if (DistanceBetweenPlayerAndRoom <= DistanceToTrigger  )
        {
            if (MaskDiscovered)
            {
                if (Temp_C <= Max_Temp)
                {
                    Temp_C += 0.01f;
                }
                else
                {
                    Temp_C = Random.Range(10f, 30f);
                }
            }

            if (DollDiscovered)
            {
                if (DistanceBetweenPlayerAndRoom <= EmfRange)
                {

                    if (Emf_Level <= Max_Emf)
                    {
                        Emf_Level = Max_Emf;
                    }

                }
            }
            else
            {
                Emf_Level = Random.Range(Min_Emf,4);
            }

        }
        else
        {
            Temp_C = Random.Range(10f,30f);
            Emf_Level = Random.Range(Min_Emf, 4);
        }
       
    }
    public void Mylinger()
    {
        if (DollDiscovered)
        {
            Moths = GameObject.Instantiate(Resources.Load<GameObject>("Moths area"), Favorite_Room);
            DollDiscovered = false;
        }
        if (MaskDiscovered)
        {
            if (DistanceBetweenPlayerAndRoom <= EmfRange)
            {

                if (Emf_Level <= Max_Emf)
                {
                    Emf_Level = Max_Emf;
                }

            }
        }
        else
        {
            Emf_Level = Random.Range(Min_Emf, 4);
        }
    }
    public void GhostIdentifier()
    {
        if(GhostType == Ghost_Type.Kikimora)
        {
            Kikimora();
        }

        if (GhostType == Ghost_Type.Ogbanje)
        {
            Ogbanje();
        }

        if(GhostType == Ghost_Type.Jinn)
        {
            Jinn();
        }
        if(GhostType == Ghost_Type.mylingar)
        {
            Mylinger();
        }
    }

    void RandmoizeSpawnableLocation()
    {
        while(MaskSpawnLocation == DollSpawnLocation)
        {
            MaskSpawnLocation = AllSpawnablesLocations[Random.Range(0, AllSpawnablesLocations.Length)];
        }
    }

    private void Update()
    {
       GhostIdentifier();
    }
}
public enum Ghost_Type
{
    Kikimora,  //freezing / moths
    Jinn,      //Burning / emf
    mylingar,  // moths / emf
    Ogbanje,   //burning/ moths
}