using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Prefab; // Tableau des objets � instancier
    [SerializeField] float minSpawnInterval = 0.2f; // Intervalle minimum entre les spawns
    [SerializeField] float maxSpawnInterval = 1f; // Intervalle maximum entre les spawns

    [SerializeField] Transform[] spawnPoints; // Trois positions d�finissant les voies
    public float spawnAcceleration = 0.1f; // Rate at which the spawn speed increases

    private float currentSpawnInterval; // Current interval between spawns
    private float spawnTimer; // Timer to track spawn timing
    public bool gameOver;

    void Start()
    {
        if (!gameOver)
            StartCoroutine(FruitSpawn());
    }

    IEnumerator FruitSpawn()
    {
        while (true)
        {
 
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= currentSpawnInterval)
            {
                SpawnObject();
                spawnTimer = 0f;
            }

            
            // Attendre un intervalle al�atoire avant de recommencer
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);
        }
    }
    void SpawnObject()
    {
        // Choisir un spawner (voie) al�atoire parmi les spawnPoints
        int spawnerIndex = Random.Range(0, spawnPoints.Length);
        // Choisir un prefab al�atoire parmi les objets dans Prefab
        GameObject prefabToSpawn = Prefab[Random.Range(0, Prefab.Length)];

        // Instancier l'objet � la position du spawner choisi
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoints[spawnerIndex].position, Quaternion.identity);

        // D�truire l'objet apr�s 5 secondes
        Destroy(spawnedObject, 5f);
    }
}