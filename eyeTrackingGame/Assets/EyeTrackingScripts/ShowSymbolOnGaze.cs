using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class ShowSymbolOnGaze : MonoBehaviour
{
    private Material material;
    private GazeAware _gazeAware;
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            material.SetFloat("_ShowSymbol", 1);
        }
        else
        {
            material.SetFloat("_ShowSymbol", 0);
        }
    }
}
