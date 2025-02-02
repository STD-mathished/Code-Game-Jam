using System;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float laneWidth = 2f; // Largeur entre les voies
    public int currentLane = 1; // Voie actuelle (0 = gauche, 1 = centre, 2 = droite)
    public float transitionSpeed = 6.0f; // Vitesse de transition
    private Vector3 targetPosition; // Position cible

    public AudioSource laneChangeSound;

    private Quaternion targetRotation; // Rotation cible
    private float tiltAngle = 70f; // Angle d'inclinaison lors du changement de voie

    public GameObject gameOverUI;  // Le panneau de fin de jeu
    public bool gameOver = false;  // �tat du jeu
    public float lateralForce = 100000f;  // Force appliqu�e sur les c�t�s aux obstacles
    public float upwardForce = 300000f;   // Force verticale pour projeter les objets

    public TextMeshProUGUI timerText;  // R�f�rence au texte pour le timer
    public TextMeshProUGUI scoreText; // R�f�rence au texte pour le score

    [SerializeField] int score;
    [SerializeField] float timer;

    void Start()
    {
        // Initialise la position et la rotation cibles
        targetPosition = transform.position;
        targetRotation = transform.rotation;
        score = 0;
        if (laneChangeSound == null)
        {
            laneChangeSound = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (!gameOver) // Ne pas g�rer les entr�es si le jeu est termin�
        {
            HandleInput();
            MoveToTargetLane();
        }
    }

    void HandleInput()
    {
        // V�rifie les touches pour changer de voie
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--; // D�place � gauche
            targetRotation = Quaternion.Euler(0, 0, tiltAngle); // Incline � gauche
            TriggerLaneChangeSound();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++; // D�place � droite
            targetRotation = Quaternion.Euler(0, 0, -tiltAngle); // Incline � droite
            TriggerLaneChangeSound();
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0); // Retour � la position normale
        }

        // Met � jour la position cible
        targetPosition = new Vector3(currentLane * laneWidth - laneWidth, transform.position.y, transform.position.z);
    }

    void TriggerLaneChangeSound()
    {
        if (laneChangeSound.isPlaying)
        {
            laneChangeSound.Stop();
        }
        laneChangeSound.Play(); // Joue le son
    }

    void MoveToTargetLane()
    {
        // D�placement progressif vers la position cible
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            // Obtenez le point de contact principal
            ContactPoint contact = collision.contacts[0];

            // Calculez la position relative du contact par rapport au centre de la voiture
            Vector3 localPoint = transform.InverseTransformPoint(contact.point);

            if (localPoint.z > 0) // Collision frontale
            {
                HandleFrontCollision();
            }
            else // Collision lat�rale
            {
                HandleSideCollision(collision);
            }
        }
    }

    void HandleFrontCollision()
    {
        if (gameOver) return;

        Debug.Log("Game Over!");

        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        gameOver = true;
      
    }

    void HandleSideCollision(Collision collision)
    {
        Debug.Log("Collision lat�rale d�tect�e!");

        // V�rifiez si l'obstacle a un Rigidbody pour lui appliquer une force
        Rigidbody obstacleRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (obstacleRigidbody != null)
        {
            // Appliquer une force lat�rale et verticale pour projeter l'objet
            Vector3 forceDirection = (collision.contacts[0].point.x > transform.position.x)
                ? Vector3.right
                : Vector3.left;

            // Ajoutez une composante verticale pour que l'obstacle soit projet� en l'air
            forceDirection += Vector3.up * upwardForce;

            // Appliquer la force totale
            obstacleRigidbody.AddForce(forceDirection.normalized * lateralForce * 1000f);
        }
    }




}