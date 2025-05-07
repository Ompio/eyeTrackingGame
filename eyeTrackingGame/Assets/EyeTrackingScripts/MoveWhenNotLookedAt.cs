using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware), typeof(Rigidbody))]
public class MoveWhenNotLookedAt : MonoBehaviour
{
    public Transform playerTransform;
    private GazeAware _gazeAware;
    private Rigidbody _rigidbody;
    public float forceAmount = 10;
    public float maxVelocity = 5;  // Maksymalna prędkość, którą może osiągnąć obiekt
    private float stopTime = 0;
    private float countdownTimer = 0;

    private bool isMoving = false;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _rigidbody = GetComponent<Rigidbody>();
        stopTime = Random.Range(2f, 5f); // Ustaw losowy czas zatrzymania od 2 do 5 sekund
        countdownTimer = stopTime;
    }

    void FixedUpdate()
    {
        // Sprawdź, czy spojrzenie jest skierowane na obiekt
        if (_gazeAware.HasGazeFocus)
        {
            isMoving = false;
            _rigidbody.velocity = Vector3.zero; // Zatrzymuje ruch obiektu
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
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            _rigidbody.AddForce(direction * forceAmount, ForceMode.Force);

            // Ograniczenie prędkości
            if (_rigidbody.velocity.magnitude > maxVelocity)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * maxVelocity;
            }
        }
    }
}
