using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DispatcherSpawnersBushes : MonoBehaviour
{
    [SerializeField] private List<SpawnerBushes> _spawnersBushes;
    [SerializeField] private List<int> _spawnCount;
    [SerializeField] private List<Transform> _spawnPoints;

    private List<Bush> _bushes = new List<Bush>();

    public event Action Spawned;

    public IReadOnlyList<Bush> Bushes => _bushes.AsReadOnly();

    private void Start()
    {
        CreateBushes();
        Spawned?.Invoke();
    }

    public void CreateBushes()
    {
        List<Transform> availableSpawnPoints = new List<Transform>(_spawnPoints);

        for (int i = 0; i < _spawnCount.Count; i++)
        {
            for (int j = 0; j < _spawnCount[i] && availableSpawnPoints.Count > 0; j++)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomIndex];

                Bush bush = _spawnersBushes[i].SpawnBush(spawnPoint);
                _bushes.Add(bush);

                availableSpawnPoints.RemoveAt(randomIndex);
            }
        }

        while (availableSpawnPoints.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomIndex];

            Bush bush = _spawnersBushes[5].SpawnBush(spawnPoint);
            _bushes.Add(bush);

            availableSpawnPoints.RemoveAt(randomIndex);
        }
    }
}


