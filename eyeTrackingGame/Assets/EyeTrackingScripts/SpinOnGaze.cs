using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class SpinOnGaze : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Zmienna predkość rotacji

    private GazeAware _gazeAware;
    private List<GazeAware> _gazeAwareList; // Używamy List zamiast tablicy dla zoptymalizowanego dostępu

    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _gazeAwareList = new List<GazeAware>(GetComponentsInChildren<GazeAware>());
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            RotateObject();
        }

        foreach (GazeAware g in _gazeAwareList)
        {
            if (g.HasGazeFocus)
            {
                RotateObject();
            }
        }
    }

    // Metoda do obracania obiektu
    void RotateObject()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
