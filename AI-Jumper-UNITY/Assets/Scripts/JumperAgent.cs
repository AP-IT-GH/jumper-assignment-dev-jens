using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class JumperAgent : Agent
{
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public Transform groundCheck;

    private Rigidbody rb;

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
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    { 
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0;
        
        if (Input.GetKey(KeyCode.Space)) {
            discreteActionsOut[0] = 1;
        }
    }
}
