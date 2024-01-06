using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWaypointsCyclically : MonoBehaviour
{
    public GameObject waypointParent;
    private List<Transform> waypoints;
    public float speed = 2;
    public float moveInterval = 1f; // Time interval for object movement

    private float timer;
    private int index = 0;
    private bool objectInMotion = false;

    void Start()
    {
        waypoints = new List<Transform>();
        foreach (Transform child in waypointParent.transform)
        {
            waypoints.Add(child);
        }
        timer = moveInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !objectInMotion)
        {
            objectInMotion = true;
        }

        if (objectInMotion)
        {
            Vector3 destination = waypoints[index].transform.position;
            Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            transform.position = newPos;
            float distance = Vector3.Distance(transform.position, destination);
            if (distance <= 0.05)
            {
                if (index < waypoints.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                objectInMotion = false; // Stop moving if not looping
                timer = moveInterval;
            }
        }
    }
}
