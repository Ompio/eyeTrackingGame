using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class MoveWhenNotLookedAt : MonoBehaviour
{
    public Transform playerTransform;
    private GazeAware _gazeAware;
    public float speed = 2;
    private float stopTime = 0;
    private float countdownTimer = 0;

    private bool isMoving = false;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    void Update()
    {
        // Sprawdź, czy spojrzenie jest skierowane na obiekt
        if (_gazeAware.HasGazeFocus)
        {
            isMoving = false;
            stopTime = Random.Range(2f, 5f); // Ustaw losowy czas zatrzymania od 2 do 5 sekund
            countdownTimer = stopTime;
        }

        // Odliczanie czasu, po którym obiekt znów się poruszy
        if (!isMoving && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                isMoving = true;
            }
        }

        // Poruszanie się obiektu
        if (isMoving)
        {
            Vector3 destination = playerTransform.position;
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }
}
