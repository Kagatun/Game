using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{

    [SerializeField] private List<DispatcherSpawnersGeese> _wave;
    [SerializeField] private int _index;

    private int _dispatcherCreated;
    private int _dispatcherFinished;

    public event Action<int> FinishedSpawn;
    public event Action<int> AllGeeseDead;

    private void Start()
    {
        _dispatcherCreated = _wave.Count;
        _dispatcherFinished = _wave.Count;
    }

    private void OnEnable()
    {
        foreach (DispatcherSpawnersGeese dispatcherSpawners in _wave)
        {
            dispatcherSpawners.Ñreated += StartTimerSpawnGeese;
            dispatcherSpawners.FinishedWave += StartSpawnGeese;
        }
    }

    private void OnDisable()
    {
        foreach (DispatcherSpawnersGeese dispatcherSpawners in _wave)
        {
            dispatcherSpawners.Ñreated -= StartTimerSpawnGeese;
            dispatcherSpawners.FinishedWave -= StartSpawnGeese;
        }
    }

    private void StartTimerSpawnGeese()
    {
        _dispatcherCreated--;

        if (_dispatcherCreated == 0)
            FinishedSpawn?.Invoke(_index);
    }

    private void StartSpawnGeese()
    {
        _dispatcherFinished--;

        if (_dispatcherFinished == 0)
            AllGeeseDead?.Invoke(_index);
    }

    public void StartWave()
    {
        foreach (DispatcherSpawnersGeese dispatcherSpawners in _wave)
            StartCoroutine(dispatcherSpawners.StartSpawnGeese());
    }
}
