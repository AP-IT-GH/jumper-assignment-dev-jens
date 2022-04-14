using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float ObjectSpeed { get; set; }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z - (ObjectSpeed * Time.deltaTime));

        if (transform.position.z <= -10)
        {
            Destroy(gameObject);
        }
    }
}
