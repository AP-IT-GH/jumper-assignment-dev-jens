using UnityEngine;
using Random = UnityEngine.Random;


public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject point;

    private float interval = 4f;
    private float timer = 0f;
    
    void Update()
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

        if (ramdomNum == 0)
            Instantiate(point, transform);
        else
            Instantiate(obstacle, transform);
    }
}
