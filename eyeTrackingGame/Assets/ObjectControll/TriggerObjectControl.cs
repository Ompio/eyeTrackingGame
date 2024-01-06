using UnityEngine;
using System.Collections.Generic;

public class TriggerObjectControl : MonoBehaviour
{
    public List<GameObject> objectsToAffect; // Lista obiektów, na które ma wpływać trigger
    public bool activateOnTriggerEnter = true; // Ustaw, czy obiekty mają być aktywowane czy deaktywowane przy wejściu

    void Start()
    {
        if (activateOnTriggerEnter)
        {
            foreach (var obj in objectsToAffect)
            {
                obj?.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("object:"+activateOnTriggerEnter);
            // Aktywuj lub deaktywuj obiekty z listy
            foreach (var obj in objectsToAffect)
            {
                obj.SetActive(activateOnTriggerEnter);
            }
        }
    }
}
