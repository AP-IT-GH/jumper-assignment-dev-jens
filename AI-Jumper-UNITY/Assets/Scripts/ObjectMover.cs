using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float objectSpeed = 0.1f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z - objectSpeed);

        if (transform.position.z <= -10)
        {
            Destroy(gameObject);
        }
    }
}
