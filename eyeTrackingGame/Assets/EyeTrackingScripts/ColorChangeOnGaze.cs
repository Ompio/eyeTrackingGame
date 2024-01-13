using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class ColorChangeOnGazeChildren : MonoBehaviour
{
    private GazeAware _gazeAware;
    private Color originalColor;
    public Color colorOnGaze = Color.yellow;

    private Renderer[] _childRenderers;
    private Color[] _originalColors;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _childRenderers = GetComponentsInChildren<Renderer>();

        // Store the original colors
        _originalColors = new Color[_childRenderers.Length];
        for (int i = 0; i < _childRenderers.Length; i++)
        {
            _originalColors[i] = _childRenderers[i].material.color;
        }
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            foreach (var renderer in _childRenderers)
            {
                renderer.material.color = colorOnGaze;
            }
        }
        else
        {
            for (int i = 0; i < _childRenderers.Length; i++)
            {
                _childRenderers[i].material.color = _originalColors[i];
            }
        }
    }
}
