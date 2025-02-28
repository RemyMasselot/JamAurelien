using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    // INPUT ACTIONS
    [Space(10)]
    [Header("INPUT ACTIONS")]
    private PlayerMap playerMap;
    public InputAction MoveH;
    public InputAction MoveV;
    public InputAction Jump;
    [Space(10)]
    public SplineContainer splineContainer; // Référence à la spline
    public float baseSpeed = 1f; // Vitesse de déplacement sur la spline
    public bool loop = false; // Boucle ou pas
    public float lateralMoveSpeed = 2f; // Vitesse de déplacement latéral
    public float maxLateralOffset = 2f; // Limite du déplacement latéral
    [Space(10)]
    private float t = 0f; // Position sur la spline (0 = début, 1 = fin)
    private float lateralOffset = 0f; // Décalage latéral du joueur
    private Vector3 previousPosition; // Stocke la position précédente pour calculer la direction réelle

    void Start()
    {
        // Set input actions
        playerMap = new PlayerMap();
        playerMap.Enable();
        MoveH = playerMap.PLAYER.MOVEH;
        MoveV = playerMap.PLAYER.MOVEV;
        Jump = playerMap.PLAYER.JUMP;

        previousPosition = transform.position;
    }

    void Update()
    {
        if (splineContainer == null) return;

        // Contrôle du joueur
        float moveValueH = MoveH.ReadValue<float>();
        float moveValueV = MoveV.ReadValue<float>();

        //float moveInput = Input.GetAxis("Vertical"); // Avancer/reculer (Z/S ou joystick)
        //float lateralInput = Input.GetAxis("Horizontal"); // Déplacement latéral (Q/D ou joystick)

        // Modifier la vitesse en fonction de l'input vertical
        float currentSpeed = baseSpeed + moveValueV * baseSpeed * 0.5f;

        // Avancer sur la spline
        t += currentSpeed * Time.deltaTime;

        // Gestion de la boucle
        if (loop)
        {
            t %= 1f;
        }
        else if (t > 1f)
        {
            t = 1f; // Arrête à la fin de la spline
        }

        // Position principale sur la spline
        Vector3 splinePosition = splineContainer.Spline.EvaluatePosition(t);
        Vector3 tangent = splineContainer.Spline.EvaluateTangent(t);
        Vector3 upVector = Vector3.up;

        // Axe latéral (perpendiculaire à la tangente)
        Vector3 right = Vector3.Cross(upVector, tangent).normalized;

        // Appliquer le déplacement latéral avec une limite
        lateralOffset += moveValueH * lateralMoveSpeed * Time.deltaTime;
        lateralOffset = Mathf.Clamp(lateralOffset, -maxLateralOffset, maxLateralOffset);

        // Calculer la position finale
        Vector3 lateralPosition = splinePosition + right * lateralOffset;
        transform.position = lateralPosition;

        // Faire tourner l’objet dans la direction du mouvement
        transform.rotation = Quaternion.LookRotation(splineContainer.Spline.EvaluateTangent(t));
        
        // Mettre à jour la position précédente pour le prochain frame
        previousPosition = transform.position;
    }
}
