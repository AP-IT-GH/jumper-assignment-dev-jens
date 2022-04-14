using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class JumperAgent : Agent
{
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Spawner objectSpawner;

    private Rigidbody rb;

    private bool isGrounded
    {
        get
        {
            return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SetReward(-10f);
            EndEpisode();
        }

        if (other.CompareTag("Point"))
        {
            AddReward(1f);
            Destroy(other.gameObject);
        }
    }

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public override void OnEpisodeBegin()
    {
        // Destroy all obstacles still in the game
        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
            Destroy(obstacle);
        foreach (var point in GameObject.FindGameObjectsWithTag("Point"))
            Destroy(point);

        // Generate a new speed for the obstacles
        objectSpawner.ObjectSpeed = Random.Range(3f, 6f);
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
