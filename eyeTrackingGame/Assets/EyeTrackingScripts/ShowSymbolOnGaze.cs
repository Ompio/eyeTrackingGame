using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class ShowSymbolOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;
    private List<Material> materialsWithSymbol;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        materialsWithSymbol = new List<Material>();

        AddMaterialIfHasSymbol(GetComponent<MeshRenderer>());

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            AddMaterialIfHasSymbol(renderer);
        }
    }

    void Update()
    {
        float visibility = _gazeAware.HasGazeFocus ? 1.0f : 0.0f;
        SetSymbolVisibility(visibility);
    }

    private void AddMaterialIfHasSymbol(MeshRenderer renderer)
    {
        if (renderer != null && renderer.material.HasProperty("_ShowSymbol"))
        {
            materialsWithSymbol.Add(renderer.material);
        }
    }

    private void SetSymbolVisibility(float visibility)
    {
        foreach (Material material in materialsWithSymbol)
        {
            material.SetFloat("_ShowSymbol", visibility);
        }
    }
}
