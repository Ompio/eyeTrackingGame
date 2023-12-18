using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]

public class MoveOnWayPointsOnGaze : MonoBehaviour
{
    public GameObject waypointParent;
    private List<Transform> waypoints;
    private GazeAware _gazeAware;
    private int waypointsJumpCount = 0;
    public float speed = 2;
    public bool isLoop = false;
    public bool goRandom = false;
    public bool waypointsJump;

    int index = 0;
    bool objectInMotion = false;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        waypoints = new List<Transform>();
        foreach (Transform child in waypointParent.transform)
        {
            waypoints.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus && !objectInMotion)
        {
            objectInMotion = true;
            if (waypointsJump)
                waypointsJumpCount = Random.Range(1, 3);
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

                    if (goRandom)
                        index = Random.Range(0, waypoints.Count);
                    else
                        index++;

                else
                {
                    if (isLoop)
                    {
                        index = 0;
                    }
                }
                if (waypointsJumpCount == 0)
                    objectInMotion = false;
                if (waypointsJump)
                    waypointsJumpCount--;

            }

        }
    }
}
