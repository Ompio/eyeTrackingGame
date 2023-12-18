using System;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class ResizeOnGaze : MonoBehaviour
{
    [Flags]
    public enum ScaleDimension
    {
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2
    }

    [Serializable]
    public class DimensionSettings
    {
        public ScaleDimension dimension;
        public float speed;
        public float maxScale;
        public float minScale;
    }

    public List<DimensionSettings> dimensionSettings = new List<DimensionSettings>()
    {
        new DimensionSettings { dimension = ScaleDimension.X, speed = 1.0f, maxScale = 2.0f, minScale = 0.5f },
    };

    public float speedOfReturn = 1.0f;
    public float returnDelay = 2.0f;

    private ScaleDimension scaleDimension = ScaleDimension.X | ScaleDimension.Y | ScaleDimension.Z;
    private GazeAware _gazeAware;
    private List<GazeAware> _gazeAwareList;
    private Vector3 initialScale;
    private float timeSinceLossOfFocus = 0.0f;
    // Dodać transformacje z jednego kształtu do drugiego zamiast tego co aktualnie się dzieje
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _gazeAwareList = new List<GazeAware>(GetComponentsInChildren<GazeAware>());
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (IsGazeFocused())
        {
            timeSinceLossOfFocus = 0.0f;
            ScaleObject();
        }
        else
        {
            UpdateTimeSinceLossOfFocus();
        }
    }

    bool IsGazeFocused()
    {
        return _gazeAware.HasGazeFocus || HasGazeFocusInChildren();
    }

    bool HasGazeFocusInChildren()
    {
        foreach (GazeAware g in _gazeAwareList)
        {
            if (g.HasGazeFocus)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateTimeSinceLossOfFocus()
    {
        if (timeSinceLossOfFocus >= returnDelay)
        {
            ReturnToOriginalScale();
        }
        else
        {
            timeSinceLossOfFocus += Time.deltaTime;
        }
    }

    void ScaleObject()
    {
        Vector3 newScale = transform.localScale;

        foreach (var settings in dimensionSettings)
        {
            if ((scaleDimension & settings.dimension) != 0)
            {
                ApplyScaling(ref newScale, settings);
                ApplyScaleLimits(settings, ref newScale);
            }
        }

        transform.localScale = newScale;
    }

    void ApplyScaling(ref Vector3 scale, DimensionSettings settings)
    {
        float scaleFactor = 1.0f + settings.speed * Time.deltaTime;

        if ((settings.dimension & ScaleDimension.X) != 0)
        {
            scale.x *= scaleFactor;
        }
        if ((settings.dimension & ScaleDimension.Y) != 0)
        {
            scale.y *= scaleFactor;
        }
        if ((settings.dimension & ScaleDimension.Z) != 0)
        {
            scale.z *= scaleFactor;
        }
    }

    void ApplyScaleLimits(DimensionSettings settings, ref Vector3 scale)
    {
        ApplyLimit(ScaleDimension.X, ref scale.x, settings, initialScale.x);
        ApplyLimit(ScaleDimension.Y, ref scale.y, settings, initialScale.y);
        ApplyLimit(ScaleDimension.Z, ref scale.z, settings, initialScale.z);
    }

    void ApplyLimit(ScaleDimension dimension, ref float scaleComponent, DimensionSettings settings, float initialScaleComponent)
    {
        if ((settings.dimension & dimension) != 0)
        {
            scaleComponent = Mathf.Clamp(scaleComponent, settings.minScale * initialScaleComponent, settings.maxScale * initialScaleComponent);
        }
    }

    void ReturnToOriginalScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, initialScale, speedOfReturn * Time.deltaTime);
    }
}
