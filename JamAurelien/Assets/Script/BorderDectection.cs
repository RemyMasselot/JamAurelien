using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDectection : MonoBehaviour
{
    public Vector3 capsuleCenterOffset = Vector3.zero; // Décalage du centre de la capsule par rapport à l'objet
    public Vector3 capsuleDirection = Vector3.up; // Direction de l'orientation (Up, Right, Forward...)
    public float capsuleHeight = 2f;
    public float capsuleRadius = 0.5f;
    public LayerMask layerToCheck; // Couches à détecter

    public bool IsLayerDetected { get; private set; }

    void Update()
    {
        // Calcule les points de la capsule selon l'orientation choisie
        Vector3 center = transform.position + transform.TransformDirection(capsuleCenterOffset);
        Vector3 halfDirection = transform.TransformDirection(capsuleDirection.normalized) * (capsuleHeight * 0.5f - capsuleRadius);

        Vector3 point1 = center + halfDirection; // Extrémité haute
        Vector3 point2 = center - halfDirection; // Extrémité basse

        // Détection avec Physics.OverlapCapsule
        IsLayerDetected = Physics.OverlapCapsule(point1, point2, capsuleRadius, layerToCheck).Length > 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = IsLayerDetected ? Color.green : Color.red;

        Vector3 center = transform.position + transform.TransformDirection(capsuleCenterOffset);
        Vector3 halfDirection = transform.TransformDirection(capsuleDirection.normalized) * (capsuleHeight * 0.5f - capsuleRadius);

        Vector3 point1 = center + halfDirection; // Extrémité haute
        Vector3 point2 = center - halfDirection; // Extrémité basse

        // Dessiner la capsule
        Gizmos.DrawWireSphere(point1, capsuleRadius);
        Gizmos.DrawWireSphere(point2, capsuleRadius);
        Gizmos.DrawLine(point1 + Vector3.right * capsuleRadius, point2 + Vector3.right * capsuleRadius);
        Gizmos.DrawLine(point1 - Vector3.right * capsuleRadius, point2 - Vector3.right * capsuleRadius);
        Gizmos.DrawLine(point1 + Vector3.forward * capsuleRadius, point2 + Vector3.forward * capsuleRadius);
        Gizmos.DrawLine(point1 - Vector3.forward * capsuleRadius, point2 - Vector3.forward * capsuleRadius);
    }
}
