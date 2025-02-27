using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    public SplineContainer splineContainer; // R�f�rence � la spline
    public float speed = 1f; // Vitesse de d�placement
    public bool loop = false; // Boucle ou pas

    private float t = 0f; // Position sur la spline (0 = d�but, 1 = fin)

    void Update()
    {
        if (splineContainer == null) return;

        // Avance sur la spline
        t += speed * Time.deltaTime;

        // Gestion de la boucle
        if (loop)
        {
            t %= 1f;
        }
        else if (t > 1f)
        {
            t = 1f; // Arr�te � la fin de la spline
        }

        // D�placer l'objet sur la spline
        transform.position = splineContainer.Spline.EvaluatePosition(t);

        // Faire tourner l�objet dans la direction du mouvement
        transform.rotation = Quaternion.LookRotation(splineContainer.Spline.EvaluateTangent(t));
    }
}
