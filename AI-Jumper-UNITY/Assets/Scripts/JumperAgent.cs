using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class JumperAgent : Agent
{
    public float jumpForce = 10f;
    
    Rigidbody rb = null;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }
    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var actions = actionBuffers.DiscreteActions;
        rb.AddForce(0, actions[0] * jumpForce, 0);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    { 
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0;
        
        if (Input.GetKey(KeyCode.Space))
            discreteActionsOut[0] = 1;
    }
 
}