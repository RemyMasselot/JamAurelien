using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float RotSpeed;
    
    void Update()
    {
        transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);
    }
}
