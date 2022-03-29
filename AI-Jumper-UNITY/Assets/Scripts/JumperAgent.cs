using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.VFX;

public class JumperAgent : Agent
{
    public float jumpForce = 100f;
    
    private Rigidbody rb;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private bool isGrounded
    {
        get
        {
            return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        }
        
    }

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
        if (actions[0] == 1 && isGrounded)
        {
            rb.AddForce(0, jumpForce * 100, 0);
            print("jumpoing actiop");

        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    { 
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0;
        
        if (Input.GetKey(KeyCode.Space)) {
            discreteActionsOut[0] = 1;
            print("press jump");
        }
    }
}
