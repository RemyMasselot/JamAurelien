using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineRoad : MonoBehaviour
{
    private List<Vector3> m_vertsP1;
    private List<Vector3> m_vertsP2;
    private float resolution;
    private SplineSampler m_splineSampler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetVerts();
    }

    private void GetVerts()
    {
        m_vertsP1 = new List<Vector3>();
        m_vertsP2 = new List<Vector3>();

        float step = 1f / (float)resolution;
        for (int i = 0; i < resolution; i++)
        {
            float t = step * i;
           //m_splineSampler.SampleSplineWidth(t, out Vector3 p1, out Vector3 p2);
           //m_vertsP1.Add(p1);
           //m_vertsP2.Add(p2);
        }
    }
}
