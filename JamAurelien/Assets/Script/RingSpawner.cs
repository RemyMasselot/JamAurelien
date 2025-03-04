using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using System.Linq;

public class RingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ring;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private int knotDistance = 2;
    [SerializeField] private Vector3 ringOffset;

    public void SpawnRing()
    {
        ring.gameObject.transform.position = GetRingPosOnSpline() + ringOffset;
        Vector3 rot = new Vector3(GetRingRotOnSpline().x + 25, GetRingRotOnSpline().y + 90, GetRingRotOnSpline().z);
        Quaternion rotation = Quaternion.LookRotation(rot);
        ring.gameObject.transform.rotation = rotation;

        ring.gameObject.SetActive(true);
    }

    Vector3 GetRingPosOnSpline()
    {
        Spline spline = splineContainer.Spline; // Utilise la spline principale
        float closestT = GetClosestTOnSpline(spline, transform.position);
        BezierKnot ringKnotIndex = GetRingKnotIndex(spline, closestT);

        Vector3 ringPos = splineContainer.transform.TransformPoint(ringKnotIndex.Position);

        return ringPos;

        //Debug.Log("Knot actuel : " + closestKnotIndex);
    }
    Vector3 GetRingRotOnSpline()
    {
        Spline spline = splineContainer.Spline; // Utilise la spline principale
        float closestT = GetClosestTOnSpline(spline, transform.position);
        BezierKnot ringKnotIndex = GetRingKnotIndex(spline, closestT);

        Vector3 ringRot = splineContainer.transform.TransformPoint(ringKnotIndex.TangentOut);

        return ringRot;
    }

    float GetClosestTOnSpline(Spline spline, Vector3 position)
    {
        float closestT = 0f;
        float minDistance = Mathf.Infinity;

        for (float t = 0; t <= 1; t += 0.01f) // Échantillonnage de la spline
        {
            Vector3 splinePoint = spline.EvaluatePosition(t);
            float distance = Vector3.Distance(position, splinePoint);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestT = t;
            }
        }

        return closestT;
    }

    BezierKnot GetRingKnotIndex(Spline spline, float t)
    {
        int closestIndex = -1;
        float minDistance = Mathf.Infinity;
        Vector3 splinePosition = spline.EvaluatePosition(t); // Position sur la spline

        for (int i = 0; i < spline.Knots.Count(); i++)
        {
            // Correction : Convertir la position du knot dans le monde
            Vector3 knotPosition = splineContainer.transform.TransformPoint(spline[i].Position);

            float distance = Vector3.Distance(splinePosition, knotPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return spline[closestIndex + knotDistance];
    }
}
