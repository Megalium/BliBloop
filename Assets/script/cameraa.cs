using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 300f; // sensibilité de la souris

    private float xRotation = 0f; // Rotation accumulée sur l'axe X (vertical) / variable qui stock la valeur de l'angle de la caméra / le 0 = angle de horizon 
    public Transform playerBody; // Référence au joueur pour le faire tourner horizontalement

    void Start()
    {
        // pour bloquer le curseur dans l'écran 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Récupere les axe pour tourner 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculer la rotation verticale (axe X)
        xRotation -= mouseY; // Inverser pour un comportement intuitif
        xRotation = Mathf.Clamp(xRotation, -45f, 45f); // Limiter la rotation verticale /empeche la caméra de faire un 360 pour que ca soit naturel

        // Appliquer la rotation verticale à la caméra
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Tourner le joueur sur l'axe Y en fonction de la souris
        playerBody.Rotate(Vector3.up * mouseX);
    }
}