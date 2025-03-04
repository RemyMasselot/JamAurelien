using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    [SerializeField] private SetInput setInput;

    public bool CanFollowSpline = true;
    public SplineContainer splineContainer; // R�f�rence � la spline
    public float baseSpeed = 1f; // Vitesse de d�placement sur la spline
    public bool loop = false; // Boucle ou pas
    public float lateralMoveSpeed = 2f; // Vitesse de d�placement lat�ral
    public float maxLateralOffset = 2f; // Limite du d�placement lat�ral

    private float t = 0f; // Position sur la spline (0 = d�but, 1 = fin)
    
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] private float accelerationTime = 1f; // Temps pour atteindre la vitesse max
    private float accelerationProgress = 0f; // Avancement de l'acc�l�ration

    private float lateralOffset = 0f; // D�calage lat�ral du joueur
    private Vector3 previousPosition; // Stocke la position pr�c�dente pour calculer la direction r�elle

    [SerializeField] private Animator animator;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        if (CanFollowSpline == true)
        {
            FollowTheSpline();
        }
    }

    public void UpdateAnim(string animName)
    {
        foreach (var param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(param.name, false);
            }
        }
        animator.SetBool(animName, true);
    }

    private void FollowTheSpline()
    {
        if (splineContainer == null) return;

        // Contr�le du joueur
        float moveValueH = setInput.MoveH.ReadValue<float>();
        float moveValueV = setInput.MoveV.ReadValue<float>();

        if (moveValueH > 0)
        {
            UpdateAnim("Right");
        }
        if (moveValueH == 0)
        {
            UpdateAnim("Middle");
        }
        if (moveValueH < 0)
        {
            UpdateAnim("Left");
        }

        // Modifier la vitesse en fonction de l'input vertical
        float currentSpeed = baseSpeed /*+ moveValueV*/ * baseSpeed * 0.5f;

        // Avancer sur la spline
        t += currentSpeed * Time.deltaTime;

        // Gestion de la boucle
        if (loop)
        {
            t %= 1f;
        }
        else if (t > 1f)
        {
            t = 1f; // Arr�te � la fin de la spline
        }

        // Position principale sur la spline
        Vector3 splinePosition = splineContainer.Spline.EvaluatePosition(t);
        Vector3 tangent = splineContainer.Spline.EvaluateTangent(t);
        Vector3 upVector = Vector3.up;

        // Axe lat�ral (perpendiculaire � la tangente)
        Vector3 right = Vector3.Cross(upVector, tangent).normalized;

        // Appliquer le d�placement lat�ral avec une limite
        accelerationProgress += Time.deltaTime / accelerationTime;
        accelerationProgress = Mathf.Clamp01(accelerationProgress); // Clamp entre 0 et 1

        float accelerationFactor = accelerationCurve.Evaluate(accelerationProgress);
        lateralOffset += moveValueH * lateralMoveSpeed * accelerationFactor * Time.deltaTime;

        //lateralOffset += moveValueH * lateralMoveSpeed * Time.deltaTime;
        lateralOffset = Mathf.Clamp(lateralOffset, -maxLateralOffset, maxLateralOffset);

        // Calculer la position finale
        Vector3 lateralPosition = splinePosition + right * lateralOffset;
        transform.position = lateralPosition;

        // Faire tourner l�objet dans la direction du mouvement
        transform.rotation = Quaternion.LookRotation(splineContainer.Spline.EvaluateTangent(t));
        
        // Mettre � jour la position pr�c�dente pour le prochain frame
        previousPosition = transform.position;
    }
}
