using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public Transform[] placablePoints;
    void Start()
    {
        for(int i = 0; i < placablePoints.Length; i++)
        {
            if(Random.Range(0.001f, 1f) > 0.6f)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], placablePoints[i].position, placablePoints[i].rotation);
            }
        }        
    }

}
