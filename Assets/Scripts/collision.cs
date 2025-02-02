using UnityEngine;

public class StopGameOnFrontCollision : MonoBehaviour
{
    // L'angle maximum autorisé pour considérer une collision comme "frontale" (en degrés)
    [SerializeField] private float maxFrontAngle = 45f;

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifiez si l'objet en collision a un tag spécifique (optionnel)
        if (collision.gameObject.CompareTag("obstacle"))
        {
            // Parcours des points de contact pour analyser la collision
            foreach (ContactPoint contact in collision.contacts)
            {
                // Calculer la direction de collision
                Vector3 collisionDirection = (contact.point - transform.position).normalized;

                // Vérifier si la collision est "frontale" (par rapport à l'avant de l'objet)
                float angle = Vector3.Angle(transform.forward, collisionDirection);

                if (angle <= maxFrontAngle)
                {
                    Debug.Log($"Collision frontale détectée avec {collision.gameObject.name}. Arrêt du jeu.");

                    // Si vous êtes en mode éditeur
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    // Quittez le jeu (pour une build)
                    Application.Quit();
#endif

                    break; // Une seule collision frontale suffit
                }
            }
        }
    }
}
