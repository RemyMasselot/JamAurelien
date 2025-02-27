using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{
    // INPUT ACTIONS
    [Space(10)]
    [Header("INPUT ACTIONS")]
    private PlayerMap playerMap;
    public InputAction MoveH;
    public InputAction Jump;

    // PHYSICS
    [Space(10)]
    [Header("PHYSICS")]
    private Rigidbody rb;
    [SerializeField] private Vector3 moveForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float dragForce;

    // AUTO MODE
    [Space(10)]
    [Header("AUTO MODE")]
    [SerializeField] BorderDectection borderDectectionL;
    [SerializeField] BorderDectection borderDectectionR;
    public float torqueForce = 5f; // Intensité du torque
    public float maxTorque = 50f; // Limite la force maximale
    public float damping = 0.95f; // Amortissement
    public float angleThreshold = 2f; // Seuil d’angle sous lequel on arrête de tourner
    public float smoothFactor = 0.1f; // Lissage du torque pour éviter les oscillations

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set input actions
        playerMap = new PlayerMap();
        playerMap.Enable();
        MoveH = playerMap.PLAYER.MOVEH;
        Jump = playerMap.PLAYER.JUMP;
    }

    // Update is called once per frame
    void Update()
    {
        MOVEH();
        AutoMode();
        //Debug.Log("LEFT" + borderDectectionL.IsLayerDetected);
        //Debug.Log("RIGHT" + borderDectectionR.IsLayerDetected);
    }

    private void FixedUpdate()
    {
        SpeedBlock();
        FixOrientation();
    }

    private void MOVEH()
    {
        float moveValue = MoveH.ReadValue<float>();
        if (Mathf.Abs(moveValue) > 0)
        {
            rb.AddForce(moveForce * moveValue, ForceMode.Acceleration);
        }
        //Debug.Log(rb.velocity.magnitude);
    }

    private void FixOrientation()
    {
        Vector3 velocity = rb.velocity;

        if (velocity.sqrMagnitude > 0.01f) // Vérifie que l'objet bouge
        {
            // Prendre uniquement la direction horizontale (ignorer Y)
            Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
            if (horizontalVelocity.sqrMagnitude < 0.01f) return; // Évite les micro-ajustements

            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity.normalized);
            Quaternion currentRotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0); // Verrouille X et Z

            // Calculer l'angle signé entre la rotation actuelle et la cible
            float angleY = Vector3.SignedAngle(transform.forward, horizontalVelocity, Vector3.up);

            // Évite les oscillations en ignorant les petits angles
            if (Mathf.Abs(angleY) < angleThreshold)
            {
                rb.angularVelocity *= damping;
                return;
            }

            // Appliquer un torque lissé pour éviter les oscillations
            float appliedTorque = Mathf.Lerp(0, Mathf.Min(Mathf.Abs(angleY) * torqueForce, maxTorque), smoothFactor) * Mathf.Sign(angleY);
            rb.AddTorque(Vector3.up * appliedTorque * Time.fixedDeltaTime, ForceMode.VelocityChange);

            // Appliquer un amortissement pour stabiliser la rotation
            rb.angularVelocity *= damping;
        }
    }

    private void AutoMode()
    {
        if (borderDectectionL.IsLayerDetected == true)
        {
            rb.AddForce(moveForce, ForceMode.Acceleration);
        }
        if (borderDectectionR.IsLayerDetected == true)
        {
            rb.AddForce(-moveForce, ForceMode.Acceleration);
        }
    }

    private void SpeedBlock()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.AddForce(-rb.velocity.normalized * dragForce, ForceMode.Acceleration);
        }
    }

    private void JUMP()
    {

    }
}
