using System;
using UnityEngine;
using Tobii.Gaming;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(GazeAware))]
public class ResizeOnGaze : MonoBehaviour
{
    [Serializable]
    public class DimensionSettings
    {
        public float speed;
        public float targetScale;  // Właściwość do przechowywania docelowej skali
    }

    public DimensionSettings xSettings = new DimensionSettings { speed = 0.0f, targetScale = 2.0f };
    public DimensionSettings ySettings = new DimensionSettings { speed = 0.0f, targetScale = 2.0f };
    public DimensionSettings zSettings = new DimensionSettings { speed = 0.0f, targetScale = 2.0f };

    public float speedOfReturn = 1.0f;
    public float returnDelay = 2.0f;

    private GazeAware _gazeAware;
    private List<GazeAware> _gazeAwareList;
    private Vector3 initialScale;
    private float timeSinceLossOfFocus = 0.0f;
    private bool isScaling = false;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _gazeAwareList = new List<GazeAware>(GetComponentsInChildren<GazeAware>());
        initialScale = transform.localScale;
        if (xSettings.speed == 0.0f)
        {
            xSettings.targetScale = initialScale.x;
        }

        if (ySettings.speed == 0.0f)
        {
            ySettings.targetScale = initialScale.y;
        }

        if (zSettings.speed == 0.0f)
        {
            zSettings.targetScale = initialScale.z;
        }
    }

    void Update()
    {
        if (IsGazeFocused())
        {
            isScaling = true;
            timeSinceLossOfFocus = 0.0f;
        }
        else
        {
            UpdateTimeSinceLossOfFocus();
        }

        if (isScaling)
        {
            ScaleObjectTowardsTarget();
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
            isScaling = false;
            ReturnToOriginalScale();
        }
        else
        {
            timeSinceLossOfFocus += Time.deltaTime;
        }
    }

    void ScaleObjectTowardsTarget()
    {
        Vector3 newScale = transform.localScale;

        newScale.x = Mathf.Lerp(newScale.x, xSettings.targetScale, xSettings.speed * Time.deltaTime);
        newScale.y = Mathf.Lerp(newScale.y, ySettings.targetScale, ySettings.speed * Time.deltaTime);
        newScale.z = Mathf.Lerp(newScale.z, zSettings.targetScale, zSettings.speed * Time.deltaTime);
        Debug.Log($"newScale: {newScale}, TargetScale: (X: {xSettings.targetScale}, Y: {ySettings.targetScale}, Z: {zSettings.targetScale})");

        if (newScale == new Vector3(xSettings.targetScale, ySettings.targetScale, zSettings.targetScale))
        {
            Debug.Log("Osiągnięto docelową skalę.");
            isScaling = false;
        }

        transform.localScale = newScale;
    }

    void ReturnToOriginalScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, initialScale, speedOfReturn * Time.deltaTime);
    }
}
