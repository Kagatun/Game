using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatcherWavesGeese : MonoBehaviour
{
    [SerializeField] private DispatcherSpawnersBushes _dispatchersSpawnersBushes;
    [SerializeField] private List<Wave> _waves;

    private Coroutine _coroutineFinish;
    private Coroutine _coroutineDead;
    private WaitForSeconds _delayBeforWave;
    private WaitForSeconds _wait;
    private WaitForSeconds _waitStart;
    private int _timeToSpawnWave = 15;
    private int _timeWaitWave = 3;
    private float _timeWaitStart = 3.5f;
    private int _currentWaveIndex;
    private int _AllWaveCount;

    //public event Action Ended;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeToSpawnWave);
        _delayBeforWave = new WaitForSeconds(_timeWaitWave);
        _waitStart = new WaitForSeconds(_timeWaitStart);
        _AllWaveCount = _waves.Count;
    }

    private void OnEnable()
    {
        foreach (var wave in _waves)
        {
            wave.FinishedSpawn += StartFinishTimerWave;
            wave.AllGeeseDead += StartDeadTimerWave;
        }
    }

    private void OnDisable()
    {
        foreach (var wave in _waves)
        {
            wave.FinishedSpawn -= StartFinishTimerWave;
            wave.AllGeeseDead -= StartDeadTimerWave;
        }
    }

    public void StartGame()
    {
        _dispatchersSpawnersBushes.CreateBushes();

        StartCoroutine(StartGameWave());
    }

    private IEnumerator StartGameWave()
    {
        int startingFirst = 0;
        OpenFenceWave(startingFirst);

        yield return _waitStart;

        StartNextWave(startingFirst);
    }

    private void StartDeadTimerWave(int waveIndex)
    {
        _coroutineDead = StartCoroutine(StartNextWaveJob(waveIndex, _delayBeforWave));
        _AllWaveCount--;

        if (_AllWaveCount == 0)
        {
            Debug.Log("Победа");
            //Ended?.Invoke();
            //вызвать событие о завершении и высветится подсчет очков
        }
    }

    private void OpenFenceWave(int index)
    {
        if (index < _waves.Count)
        {
            if (_waves[index] is WaveIntermediary waveIntermediary)
            {
                // Вызываем методы, специфичные для WaveIntermediary
                waveIntermediary.ActiveVisualPoint();
                waveIntermediary.MakeStatusStartTrue();
            }
        }
    }

    private void StartFinishTimerWave(int waveIndex) =>
        _coroutineFinish = StartCoroutine(StartNextWaveJob(waveIndex, _wait));

    private IEnumerator StartNextWaveJob(int waveIndex, WaitForSeconds time)
    {
        OpenFenceWave(waveIndex);

        yield return time;

        StartNextWave(waveIndex);
    }

    private void StartNextWave(int waveIndex)
    {
        int correctionIndex = 1;

        if (_currentWaveIndex <= _waves.Count - correctionIndex && waveIndex == _currentWaveIndex)
        {
            StopTimer();
            _waves[_currentWaveIndex].StartWave();
            _currentWaveIndex++;
        }
    }

    private void StopTimer()
    {
        if (_coroutineFinish != null)
            StopCoroutine(_coroutineFinish);

        if (_coroutineDead != null)
            StopCoroutine(_coroutineDead);
    }
}
