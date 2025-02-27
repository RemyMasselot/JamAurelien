using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode()]
public class SplineSampler : MonoBehaviour
{
    [SerializeField] private SplineContainer m_splineContainer;

    [SerializeField] private int m_splineIndex;

    [SerializeField][Range(0f, 1f)] private float m_time;

    [SerializeField] private float m_width;

    private Vector3 p1;
    private Vector3 p2;

    float3 position;
    float3 tangent;
    float3 forward;
    float3 upVector;

    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.SphereHandleCap(0, position, Quaternion.identity, 1f, EventType.Repaint);
    }

    public void SampleSplineWidth(float t, Vector3 p1, Vector3 p2)
    {
        m_splineContainer.Evaluate(m_splineIndex, m_time, out position, out forward, out upVector);
        float3 right = Vector3.Cross(forward, upVector).normalized;
        p1 = position + (right * m_width);
        p2 = position + (-right * m_width);
    }
}
