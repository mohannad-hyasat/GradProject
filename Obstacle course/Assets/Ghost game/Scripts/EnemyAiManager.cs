using Google.Protobuf.WellKnownTypes;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiManager : MonoBehaviour
{
    public MLAgent MLtype;
    public ClassicalAI classicalAI;

    private void Update()
    {
        if(AItype.Classic)
    }

}
public enum AItype{
    Classic,
    ML,
}

