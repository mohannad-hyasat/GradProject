using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class Agentml : Agent
{
    public Transform playerTransform;
    public float speed = 2f;
    public EnemyAiManager Ghost;

    public override void Initialize()
    {



    }
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;

    }
    public override void OnEpisodeBegin()
    {
        // Reset the agent and the player to their starting positions
        transform.localPosition = new Vector3(Random.Range(-4, 4), 0.5f, Random.Range(-4, 4));
        playerTransform.localPosition = new Vector3(Random.Range(-4, 4), 0.5f, Random.Range(-4, 4));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect the agent's and player's positions as observations
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(playerTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Convert actions to movement
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        Vector3 move = new Vector3(moveX, 0, moveZ);
        transform.Translate(move * speed * Time.deltaTime);

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.localPosition, playerTransform.localPosition);

        // Reward the agent for getting closer to the player
        if (distanceToPlayer < 1.5f)
        {
            SetReward(1.0f);
        }
        else
        {
            SetReward(-0.1f);
        }

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Provide manual control for testing purposes
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
