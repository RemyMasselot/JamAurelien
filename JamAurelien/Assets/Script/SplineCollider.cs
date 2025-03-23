using System.Collections.Generic;
using UnityEngine;


public class SplineCollider : MonoBehaviour
{
    public float colliderWidth = 0.2f; // Largeur du collider
    private LineRenderer lineRenderer;
    private List<GameObject> colliders = new List<GameObject>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GenerateColliders();
    }

    void GenerateColliders()
    {
        ClearOldColliders();

        int pointCount = lineRenderer.positionCount;
        if (pointCount < 2) return; // Nécessite au moins deux points

        for (int i = 0; i < pointCount - 1; i++)
        {
            Vector3 start = lineRenderer.GetPosition(i);
            Vector3 end = lineRenderer.GetPosition(i + 1);
            CreateColliderBetweenPoints(start, end);
        }
    }

    void CreateColliderBetweenPoints(Vector3 start, Vector3 end)
    {
        GameObject colliderObj = new GameObject("SplineCollider");
        colliderObj.transform.parent = this.transform;

        BoxCollider collider = colliderObj.AddComponent<BoxCollider>();

        // Positionner au milieu des deux points
        Vector3 midPoint = (start + end) / 2f;
        colliderObj.transform.position = midPoint;

        // Orienter le collider
        Vector3 direction = (end - start).normalized;
        colliderObj.transform.rotation = Quaternion.LookRotation(direction);

        // Ajuster la taille
        float length = Vector3.Distance(start, end);
        collider.size = new Vector3(colliderWidth, colliderWidth, length);

        colliders.Add(colliderObj);
    }

    void ClearOldColliders()
    {
        foreach (GameObject obj in colliders)
        {
            Destroy(obj);
        }
        colliders.Clear();
    }
}
