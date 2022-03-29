using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class JumperAgent : Agent
{
    public float jumpForce = 10f;
    
    Rigidbody rBody;
    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }
    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        rBody.AddForce(0, actionBuffers.DiscreteActions[0] * 100, 0);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsout = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.Space) == true)
            discreteActionsout[0] = 1;
    }
 
}
