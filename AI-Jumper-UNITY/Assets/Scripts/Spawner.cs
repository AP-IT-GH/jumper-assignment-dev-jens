using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float ObjectSpeed { get; set; }

    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject point;

    private float interval = 4f;
    private float timer = 0f;

    private void Update()
    {
        if (timer <= 0)
        {
            timer = interval;
            Spawn();
        }

        timer -= Time.deltaTime;
    }

    private void Spawn()
    {
        int ramdomNum = Random.Range(0, 3);
        GameObject obstaclePrefab = obstacle;

        if (ramdomNum == 0)
            obstaclePrefab = point;

        // Set obstacle speed to ObstacleSpeed defined by new episode
        ObjectMover newObstacleOM = Instantiate(obstaclePrefab, transform).GetComponent<ObjectMover>();
        newObstacleOM.ObjectSpeed = ObjectSpeed;
    }
}
