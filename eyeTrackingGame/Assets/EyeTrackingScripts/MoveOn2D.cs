using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

[RequireComponent(typeof(GazeAware))]
public class MoveOn2D : MonoBehaviour
{
    public float maxMovementSpeed = 1.0f;
    public float dampingFactor = 0.95f;

    public GameObject supportXNegative;
    public GameObject supportXPositive;
    public GameObject supportYPositive;
    public GameObject supportYNegative;
    public GameObject supportZPositive;
    public GameObject supportZNegative;

    public bool moveOnXAxis = true;
    public bool moveOnYAxis = true;
    public bool moveOnZAxis = true;
    public bool mounting = false;
    public float forceMagnitude = 0.5f;

    private Rigidbody _rigidbody;
    public GameObject mountPosition;

    private GazeAware _gazeAware;
    private bool _isActivated = false;
    PlayerMovement playerMovement;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _rigidbody = GetComponent<Rigidbody>(); // Pobierz komponent Rigidbody
        HideSupportObjects();
        if (mounting)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus && Input.GetMouseButtonDown(0))
        {
            _isActivated = !_isActivated;
            ShowSupportObjects();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isActivated = false;
        }

        if (_isActivated && !mounting)
        {
            MoveObject();
        }

        if (!_isActivated)
        {
            HideSupportObjects();
            if(mounting){
                playerMovement.DeMount();
            }
        }

        if (_isActivated && mounting)
        {
            playerMovement.MountPlayer(mountPosition.transform);
            SpeedControl();
        }
    }

    void FixedUpdate()
    {
        if (_isActivated && mounting)
        {
            AddForceToMove();
            DampenVelocity();
        }
    }

    void HideSupportObjects()
    {
        if (moveOnXAxis)
        {
            if (supportXNegative) supportXNegative.SetActive(false);
            if (supportXPositive) supportXPositive.SetActive(false);
        }

        if (moveOnYAxis)
        {
            if (supportYPositive) supportYPositive.SetActive(false);
            if (supportYNegative) supportYNegative.SetActive(false);
        }

        if (moveOnZAxis)
        {
            if (supportZPositive) supportZPositive.SetActive(false);
            if (supportZNegative) supportZNegative.SetActive(false);
        }
    }


    void ShowSupportObjects()
    {
        if (moveOnXAxis)
        {
            supportXNegative.SetActive(true);
            supportXPositive.SetActive(true);
        }

        if (moveOnYAxis)
        {
            supportYPositive.SetActive(true);
            supportYNegative.SetActive(true);
        }

        if (moveOnZAxis)
        {
            supportZPositive.SetActive(true);
            supportZNegative.SetActive(true);
        }
    }

    void MoveObject()
    {
        float moveStep = forceMagnitude * Time.deltaTime;

        if (moveOnXAxis)
        {
            if (supportXNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.left * moveStep);
            }
            if (supportXPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.right * moveStep);
            }
        }

        if (moveOnYAxis)
        {
            if (supportYPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.up * moveStep);
            }
            if (supportYNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.down * moveStep);
            }
        }

        if (moveOnZAxis)
        {
            if (supportZPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.forward * moveStep);
            }
            if (supportZNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.back * moveStep);
            }
        }
    }

    void AddForceToMove()
    {
        if (moveOnXAxis)
        {
            if (supportXNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                _rigidbody.AddForce(Vector3.left * forceMagnitude);
            }
            if (supportXPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                _rigidbody.AddForce(Vector3.right * forceMagnitude);
            }
        }

        if (moveOnYAxis)
        {
            if (supportYPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                _rigidbody.AddForce(Vector3.up * forceMagnitude);
            }
            if (supportYNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                _rigidbody.AddForce(Vector3.down * forceMagnitude);
            }
        }

        if (moveOnZAxis)
        {
            if (supportZPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                _rigidbody.AddForce(Vector3.forward * forceMagnitude);
            }
            if (supportZNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                _rigidbody.AddForce(Vector3.back * forceMagnitude);
            }
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > maxMovementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxMovementSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }

    }
    void DampenVelocity()
    {
        // Mnożymy bieżącą prędkość przez dampingFactor, co stopniowo ją zmniejsza
        _rigidbody.velocity *= dampingFactor;
    }
}
