using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtaclePhysix : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rb;
    void Start()
    {
        _rb = gameObject.transform.GetComponent<Rigidbody>();
        _rb.AddForce(new Vector3(0,0,-5f) * 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
