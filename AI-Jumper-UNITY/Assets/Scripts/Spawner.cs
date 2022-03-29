using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawner : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject point;
    private float interval = 3.5f;
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
            GameObject.Instantiate(point);
        else
            GameObject.Instantiate(obstacle);




    }
}