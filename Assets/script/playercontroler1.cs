using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Assure-toi d'importer cet espace de noms pour le changement de scène

public class PlayerController : MonoBehaviour
{
    // Attributs de vitesse, force de saut et vérification si le joueur est au sol
    [Header("Movement Settings")]
    [SerializeField] private float speed = 8f; // Vitesse de déplacement du joueur
    [SerializeField] private float jumpForce = 5f; // Force de saut du joueur
    [SerializeField] private bool isGrounded = true; // Booléen pour vérifier si le joueur est au sol

    // Le Rigidbody pour la physique et le Transform pour téléporter le joueur quand il échoue
    private Rigidbody rb;
    public Transform initialPosition; // La position initiale du joueur

    private int pieceCount = 0; // Compteur de pièces
    public int piecesRequiredToChangeScene = 10; // Nombre de pièces requis pour changer de scène
    public string nextSceneName = "Victory"; // Nom de la prochaine scène

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialise la référence au Rigidbody attaché au joueur
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Empêche la rotation du joueur sur les axes X et Z

        // Si la position initiale n'est pas définie, utilise la position actuelle du joueur
        if (initialPosition == null)
        {
            initialPosition = this.transform;
        }

        UpdatePieceCountDisplay(); // Mise à jour de l'affichage du compteur de pièces au démarrage
    }

    void Update()
    {
        // Déplacement horizontal (Q et D) et vertical (Z et S)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Direction "avant" et "droite" du joueur (lié à la rotation)
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Calcul de la direction du mouvement (ignore la hauteur)
        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * speed;
        movement.y = rb.velocity.y; // Conserve la vitesse verticale pour éviter les conflits

        // Applique la vélocité au joueur
        rb.velocity = movement;

        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Applique une force vers le haut pour le saut
            isGrounded = false; // Empêche le saut tant qu'on n'est pas retombé
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si le joueur touche le sol (tag "Grounded")
        if (collision.gameObject.CompareTag("Grounded"))
        {
            isGrounded = true;
        }

        // Détecte la collision avec la boule
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Collision avec une boule détectée, téléportation à la position initiale...");
            TeleportToInitialPosition(); // Téléporte le joueur à sa position initiale
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant a le tag "Pic"
        if (other.CompareTag("Pic"))
        {
            Debug.Log("Collision avec un pic détectée, téléportation à la position initiale...");
            TeleportToInitialPosition(); // Téléporte le joueur à sa position initiale
        }

        // Vérifie si l'objet entrant a le tag "Coin"
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Pièce collectée !");
            Destroy(other.gameObject); // Détruit l'objet pièce
            pieceCount++; // Incrémente le compteur de pièces
            UpdatePieceCountDisplay(); // Mise à jour de l'affichage du compteur de pièces

            // Vérifie si le nombre requis de pièces est atteint
            if (pieceCount >= piecesRequiredToChangeScene)
            {
                ChangeScene(); // Change de scène
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet sortant a le tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur est sorti de la zone de trigger !");
        }
    }

    void TeleportToInitialPosition()
    {
        // Téléporte le joueur à sa position initiale
        transform.position = initialPosition.position;
        transform.rotation = initialPosition.rotation;
    }

    // Mise à jour de l'affichage du compteur de pièces
    void UpdatePieceCountDisplay()
    {
        Debug.Log("Nombre de pièces collectées : " + pieceCount);
        // Si tu as une interface utilisateur (UI), tu peux mettre à jour le texte ici
    }

    // Fonction pour changer de scène
    void ChangeScene()
    {
        Debug.Log("Nombre requis de pièces atteint. Changement de scène vers : " + nextSceneName);
        SceneManager.LoadScene(nextSceneName); // Charge la scène spécifiée pour passer a celle de Victoryyy !
        
    }
}

        
    

