using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private float objectSpeed = 0.1f;

    private float speedMultiplier = 0.1f;

    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z - (objectSpeed * speedMultiplier));

        if (transform.position.z <= -10)
        {
            Destroy(gameObject);
        }
    }
}
