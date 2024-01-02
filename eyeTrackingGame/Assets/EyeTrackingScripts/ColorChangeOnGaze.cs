using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class ColorChangeOnGaze : MonoBehaviour
{   
    private Renderer _renderer;
    private GazeAware _gazeAware;
    private Color originalColor; // Variable to store the original color
    public Color colorOnGaze = Color.yellow; // Public variable for the color when gazed at

    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _renderer = GetComponent<Renderer>();

        // Capture the original color from the material
        originalColor = _renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {   
            _renderer.material.color = colorOnGaze;
        }
        else
        {
            // Revert to the original color
            _renderer.material.color = originalColor;
        }
    }
}
