using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class ColorChangeOnGaze : MonoBehaviour
{   
    private Renderer _renderer;
    private GazeAware _gazeAware;
    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            _renderer.material.color = Color.yellow;
        }
        else{
            _renderer.material.color = Color.black;
        }
    }
}
