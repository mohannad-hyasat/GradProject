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

    private void Awake()
    {
        Favorite_Room = Rooms[Random.Range(0, Rooms.Length)];
    }
  
    private void OnDrawGizmos()
    {
        if (Favorite_Room != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Favorite_Room.position, DistanceToTrigger);
        }
    }

    private void Update()
    {
        if (DistanceBetweenPlayerAndRoom <= DistanceToTrigger)
        {
            if (Temp_C <= Max_Temp)
            {
                Temp_C += 0.1f;
            }
        }

    }
}
public enum Ghost_Type
{

}