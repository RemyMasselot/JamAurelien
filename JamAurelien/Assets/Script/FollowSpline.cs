using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    public SplineContainer splineContainer; // Référence à la spline
    public float speed = 1f; // Vitesse de déplacement
    public bool loop = false; // Boucle ou pas

    private float t = 0f; // Position sur la spline (0 = début, 1 = fin)

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
            t = 1f; // Arrête à la fin de la spline
        }

        // Déplacer l'objet sur la spline
        transform.position = splineContainer.Spline.EvaluatePosition(t);

        // Faire tourner l’objet dans la direction du mouvement
        transform.rotation = Quaternion.LookRotation(splineContainer.Spline.EvaluateTangent(t));
    }
}
