using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<Transform> spawnPoints;
    public int spawnCount;

    void Start()
    {
        SpawnRandomItems();
    }

    void SpawnRandomItems()
    {
        // Copiamos la lista para no modificar la original
        List<Transform> points = new List<Transform>(spawnPoints);

        // Mezclar (shuffle) los puntos
        for (int i = 0; i < points.Count; i++)
        {
            Transform temp = points[i];
            int randomIndex = Random.Range(i, points.Count);
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }

        // Spawnear sin pasarse del número de puntos
        for (int i = 0; i < spawnCount && i < points.Count; i++)
        {
            Instantiate(itemPrefab, points[i].position + Vector3.up * 2f, points[i].rotation);
        }
    }
}