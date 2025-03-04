using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehavior : MonoBehaviour
{
    [SerializeField] private Transform player; // Référence au joueur
    [SerializeField] private SetInput setInput;

    public float sensitivity = 100f;
    public float distanceFromPlayer = 3f; // Distance entre la caméra et le joueur
    public Vector2 clampVertical = new Vector2(-30f, 80f); // Limites verticales
    public Vector2 clampHorizontal = new Vector2(-30f, 80f); // Limites horizontales

    private float xRotation = 0f; // Rotation verticale
    private float yRotation = 0f; // Rotation horizontale

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector2 lookInput = setInput.Look.ReadValue<Vector2>();
        float rotX = lookInput.x * sensitivity * Time.deltaTime;
        float rotY = lookInput.y * sensitivity * Time.deltaTime;

        // Rotation horizontale 
        yRotation += rotX;
        //yRotation = Mathf.Clamp(yRotation, clampHorizontal.x, clampHorizontal.y);

        // Rotation verticale 
        xRotation -= rotY;
        xRotation = Mathf.Clamp(xRotation, clampVertical.x, clampVertical.y);

        // Calcul de la nouvelle position orbitale
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Vector3 offset = rotation * new Vector3(0, 0, -distanceFromPlayer); // Place la caméra derrière le joueur

        // Appliquer la position et l'orientation
        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}
