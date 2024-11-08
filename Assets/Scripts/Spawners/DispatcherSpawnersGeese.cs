using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatcherSpawnersGeese : MonoBehaviour
{
    [SerializeField] private DispatcherSpawnersBushes _dispatcherSpawnersBushes;
    [SerializeField] private SpawnerEffects _spawnerEffects;
    [SerializeField] private Transform _targetHouse;
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<SpawnerGeese> _spawnerGeese;
    [SerializeField] private float _minRandomTime = 0.3f;
    [SerializeField] private float _maxRandomTime = 1.2f;
    [SerializeField] private List<Goose> _geeseTypes;
    [SerializeField] private List<int> _spawnCount;

    [field: SerializeField] public int MaxNumberGeese { get; private set; }

    private Dictionary<string, SpawnerGeese> _gooseDictionary;
    private int _currentCountRemoveGeese;
    private int _currentCountSpawnGeese;

    public event Action FinishedWave;
    public event Action Ñreated;

    private void Start()
    {
        _gooseDictionary = new Dictionary<string, SpawnerGeese>();

        for (int i = 0; i < _spawnerGeese.Count; i++)
            _gooseDictionary.Add(_geeseTypes[i].GetType().Name, _spawnerGeese[i]);
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

    public IEnumerator StartSpawnGeese()
    {
        int totalSpawned = 0;
        List<Goose> allGeeseToSpawn = new List<Goose>();

        for (int i = 0; i < _spawnCount.Count; i++)
        {
            string gooseTypeName = _geeseTypes[i].GetType().Name;
            int countToSpawn = _spawnCount[i];

            countToSpawn = Mathf.Min(countToSpawn, MaxNumberGeese);

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

                spawner.SetListBushes(_dispatcherSpawnersBushes.Bushes);
                spawner.SetTargetHouse(_targetHouse);
                Goose goose = spawner.SpawnGoose(spawnPoint);
                goose.Navigator.OnAssignTarget();
                _spawnerEffects.AddGooseList(goose);

                _currentCountSpawnGeese++;
                totalSpawned++;

                if (totalSpawned >= MaxNumberGeese)
                    yield break;

                float randomTimeGeneration = UnityEngine.Random.Range(_minRandomTime, _maxRandomTime);

                yield return new WaitForSeconds(randomTimeGeneration);
            }
        }

        int remainingToSpawn = MaxNumberGeese - totalSpawned;

        for (int i = 0; i < remainingToSpawn; i++)
        {
            if (_gooseDictionary.TryGetValue(_geeseTypes[0].GetType().Name, out SpawnerGeese spawner))
            {
                int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Transform spawnPoint = _spawnPoints[randomIndex];

                spawner.SetListBushes(_dispatcherSpawnersBushes.Bushes);
                spawner.SetTargetHouse(_targetHouse);
                Goose goose = spawner.SpawnGoose(spawnPoint);
                goose.Navigator.OnAssignTarget();
                _spawnerEffects.AddGooseList(goose);

                _currentCountSpawnGeese++;
                allGeeseToSpawn.Add(goose);

                float randomTimeGeneration = UnityEngine.Random.Range(_minRandomTime, _maxRandomTime);

                yield return new WaitForSeconds(randomTimeGeneration);
            }
        }

        if(_currentCountSpawnGeese == MaxNumberGeese)
            Ñreated?.Invoke();
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

    private void AddCount(int costGoose)
    {
        _currentCountRemoveGeese++;
        _player.AddMoney(costGoose);

        if (_currentCountRemoveGeese == MaxNumberGeese)
            FinishedWave?.Invoke();
    }
}
