using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    UIControl uIControl;
    void Start()
    {
        uIControl = FindObjectOfType<UIControl>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIControl.EndScreen();
        }
    }
}
