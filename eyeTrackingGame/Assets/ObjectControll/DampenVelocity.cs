using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampenVelocity : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rigidbody;
    public float dampingFactor = 0.95f;
    // Update is called once per frame
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (_rigidbody != null)
        {
            Vector3.Lerp( _rigidbody.velocity, new Vector3(0,0,0) , dampingFactor * Time.deltaTime);
        }
    }
}
