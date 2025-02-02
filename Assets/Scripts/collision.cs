using UnityEngine;

public class StopGameOnFrontCollision : MonoBehaviour
{
    // L'angle maximum autoris� pour consid�rer une collision comme "frontale" (en degr�s)
    [SerializeField] private float maxFrontAngle = 45f;

    private void OnCollisionEnter(Collision collision)
    {
        // V�rifiez si l'objet en collision a un tag sp�cifique (optionnel)
        if (collision.gameObject.CompareTag("obstacle"))
        {
            // Parcours des points de contact pour analyser la collision
            foreach (ContactPoint contact in collision.contacts)
            {
                // Calculer la direction de collision
                Vector3 collisionDirection = (contact.point - transform.position).normalized;

                // V�rifier si la collision est "frontale" (par rapport � l'avant de l'objet)
                float angle = Vector3.Angle(transform.forward, collisionDirection);

                if (angle <= maxFrontAngle)
                {
                    Debug.Log($"Collision frontale d�tect�e avec {collision.gameObject.name}. Arr�t du jeu.");

                    // Si vous �tes en mode �diteur
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
