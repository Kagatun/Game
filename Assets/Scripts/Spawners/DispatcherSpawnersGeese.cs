using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatcherSpawnersGeese : MonoBehaviour
{
    [SerializeField] private List<SpawnerGeese> _spawnerGeese;
    [SerializeField] private List<Goose> _geeseTypes;
    [SerializeField] private int _maxNumberGeese;
    [SerializeField] private List<int> _spawnCount;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Player _player;
    [SerializeField] private GeeseNavigator _geeseNavigator;

    private Dictionary<string, SpawnerGeese> _gooseDictionary;
    private float _minRandomTime = 0.3f;
    private float _maxRandomTime = 1.2f;
    private int _currentCountRemoveGeese;

    public event Action <Goose> Spawned;

    private void Start()
    {
        _gooseDictionary = new Dictionary<string, SpawnerGeese>();

        for (int i = 0; i < _spawnerGeese.Count; i++)
            _gooseDictionary.Add(_geeseTypes[i].GetType().Name, _spawnerGeese[i]);

        StartCoroutine(StartSpawnGeese());
    }

    private void OnEnable()
    {
        foreach (var spawner in _spawnerGeese)
            spawner.GooseRemoved += AddCount;
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawnerGeese)
            spawner.GooseRemoved -= AddCount;
    }

    private IEnumerator StartSpawnGeese()
    {
        int totalSpawned = 0;
        List<Goose> allGeeseToSpawn = new List<Goose>();

        for (int i = 0; i < _spawnCount.Count; i++)
        {
            string gooseTypeName = _geeseTypes[i].GetType().Name;
            int countToSpawn = _spawnCount[i];

            countToSpawn = Mathf.Min(countToSpawn, _maxNumberGeese);

            for (int j = 0; j < countToSpawn; j++)
                allGeeseToSpawn.Add(_geeseTypes[i]);
        }

        ShuffleList(allGeeseToSpawn);

        foreach (Goose gooseType in allGeeseToSpawn)
        {
            string gooseTypeName = gooseType.GetType().Name;

            if (_gooseDictionary.TryGetValue(gooseTypeName, out SpawnerGeese spawner))
            {
                int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Transform spawnPoint = _spawnPoints[randomIndex];

                Goose goose = spawner.SpawnGoose(spawnPoint);
                Spawned?.Invoke(goose);
                totalSpawned++;

                if (totalSpawned >= _maxNumberGeese)
                    yield break;

                float randomTimeGeneration = UnityEngine.Random.Range(_minRandomTime, _maxRandomTime);

                yield return new WaitForSecondsRealtime(randomTimeGeneration);
            }
        }

        int remainingToSpawn = _maxNumberGeese - totalSpawned;

        for (int i = 0; i < remainingToSpawn; i++)
        {
            if (_gooseDictionary.TryGetValue(_geeseTypes[0].GetType().Name, out SpawnerGeese spawner))
            {
                int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Transform spawnPoint = _spawnPoints[randomIndex];

                Goose goose = spawner.SpawnGoose(spawnPoint);
                Spawned?.Invoke(goose);

                allGeeseToSpawn.Add(goose);

                float randomTimeGeneration = UnityEngine.Random.Range(_minRandomTime, _maxRandomTime);

                yield return new WaitForSecondsRealtime(randomTimeGeneration);
            }
        }
    }

    private void ShuffleList(List<Goose> geese)
    {
        for (int i = 0; i < geese.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, geese.Count);
            Goose tempGoose = geese[i];
            geese[i] = geese[randomIndex];
            geese[randomIndex] = tempGoose;
        }
    }

    private void AddCount(int cost)
    {
        _currentCountRemoveGeese++;
        _player.AddMoney(cost);

        if (_currentCountRemoveGeese == _maxNumberGeese)
            Debug.Log("Победа");
    }
}
