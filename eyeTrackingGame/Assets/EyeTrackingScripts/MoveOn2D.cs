using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class MoveOn2D : MonoBehaviour
{
    public GameObject supportXNegative;
    public GameObject supportXPositive;
    public GameObject supportYPositive;
    public GameObject supportYNegative;
    public GameObject supportZPositive;
    public GameObject supportZNegative;

    public bool moveOnXAxis = true;
    public bool moveOnYAxis = true;
    public bool moveOnZAxis = true;

    private GazeAware _gazeAware;
    private bool _isActivated = false;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        HideSupportObjects();
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

        if (_isActivated)
        {
            MoveObject();
        }

        if (!_isActivated)
        {
            HideSupportObjects();
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
        if (moveOnXAxis)
        {
            if (supportXNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            if (supportXPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
        }

        if (moveOnYAxis)
        {
            if (supportYPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
            if (supportYNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
        }

        if (moveOnZAxis)
        {
            if (supportZPositive.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            if (supportZNegative.GetComponent<GazeAware>().HasGazeFocus)
            {
                transform.Translate(Vector3.back * Time.deltaTime);
            }
        }
    }
}
