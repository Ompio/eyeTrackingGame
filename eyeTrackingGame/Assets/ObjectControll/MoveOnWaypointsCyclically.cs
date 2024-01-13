using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnWaypointsCyclically : MonoBehaviour
{
    public GameObject waypointParent;
    private List<Transform> waypoints;
    public float forceMagnitude = 10;
    public float maxSpeed = 5f; // Maksymalna prędkość
    public float moveInterval = 1f;
    public float stoppingDistance = 0.05f;

    private Rigidbody rb;
    private float timer;
    private int index = 0;
    private bool objectInMotion = false;
    private float previousDistance = Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waypoints = new List<Transform>();
        foreach (Transform child in waypointParent.transform)
        {
            waypoints.Add(child);
        }
        timer = moveInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !objectInMotion)
        {
            objectInMotion = true;
            previousDistance = Mathf.Infinity;
        }

        if (objectInMotion)
        {
            Vector3 direction = (waypoints[index].transform.position - transform.position).normalized;
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(direction * forceMagnitude);
            }

            float currentDistance = Vector3.Distance(transform.position, waypoints[index].transform.position);

            if (currentDistance > previousDistance)
            {
                rb.velocity = Vector3.zero; 
                rb.angularVelocity = Vector3.zero;
            }
            else if (currentDistance <= stoppingDistance)
            {
                rb.velocity = Vector3.zero; 
                rb.angularVelocity = Vector3.zero;

                if (index < waypoints.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                objectInMotion = false; 
                timer = moveInterval;
            }

            previousDistance = currentDistance;
        }
    }
}
