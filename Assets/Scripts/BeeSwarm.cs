using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSwarm : MonoBehaviour
{
    [SerializeField] GameObject beePrefab;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] int beesPerWave = 5;
    [SerializeField] float swarmSpeed = 1f;
    [SerializeField] float spawnRadius = 1.5f;
    [SerializeField] Transform[] spawnAreas;
    [SerializeField] Transform player;

    void Start()
    {
        StartCoroutine(SpawnBeeWaves());
    }

    IEnumerator SpawnBeeWaves()
    {
        while (true)
        {
            SpawnBeeGroup();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBeeGroup()
    {
        Vector2 spawnCenter = GetRandomSpawnPosition();

        for (int i = 0; i < beesPerWave; i++)
        {
            Vector2 spawnPosition = spawnCenter + Random.insideUnitCircle * spawnRadius;
            GameObject bee = Instantiate(beePrefab, spawnPosition, Quaternion.identity);
            BeeMovement beeMovement = bee.GetComponent<BeeMovement>();
            if (beeMovement != null)
            {
                beeMovement.Initialize(player, swarmSpeed);
            }
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        int index = Random.Range(0, spawnAreas.Length);
        Transform selectedArea = spawnAreas[index];
        return selectedArea.position;
    }
}