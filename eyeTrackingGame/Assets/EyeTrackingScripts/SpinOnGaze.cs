using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class SpinOnGaze : MonoBehaviour
{   
    private GazeAware _gazeAware;
    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            transform.Rotate(0,1,0);
        }
    }
}
