using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [SerializeField] private SpawnerBall _spawnerBall;
    [SerializeField] private Transform _tower;
    [SerializeField] private ZoneAttack _zoneAttack;
    [SerializeField] private Transform _spawnPointBall;

    private WaitForSeconds _waitRemove;
    private WaitForSeconds _waitRecharging;
    private Coroutine _coroutineTimerRemove;
    private Coroutine _coroutineRecharging;
    private int _timeRemove = 80;
    private float _timeRecharging = 0.8f;
    private int _countBall = 55;

    private void Awake()
    {
        _waitRemove = new WaitForSeconds(_timeRemove);
        _waitRecharging = new WaitForSeconds(_timeRecharging);
    }

    private void OnEnable()
    {
        _zoneAttack.SuitableGoose += StartAttack;
    }

    private void OnDisable()
    {
        _zoneAttack.SuitableGoose -= StartAttack;
        _countBall = 5;
    }

    public void SetSpawnerBall(SpawnerBall spawnerBall) =>
    _spawnerBall = spawnerBall;

    public void StartRemove()
    {
        _coroutineTimerRemove = StartCoroutine(StartTimerRemove());
    }

    private void StartAttack(Goose goose)
    {
        _coroutineRecharging = StartCoroutine(StartShootBalls(goose));
    }

    private void StopCoroutineShoot()
    {
        if (_coroutineRecharging != null)
            StopCoroutine(_coroutineRecharging);
    }

    private IEnumerator StartShootBalls(Goose goose)
    {
        while (true)
        {
            Shoot(goose);

            yield return _waitRecharging;

            if (goose = null)
                StopCoroutineShoot();

            if (_countBall == 0)
            {
                StopCoroutineTimer();
                Destroy(gameObject);
            }
        }
    }

    private void Shoot(Goose goose)
    {
        Vector3 direction = goose.transform.position - _tower.position;
        direction.y = 0;

        float offsetDirection = 1.3f;
        Vector3 offset = goose.transform.forward * offsetDirection;
        Vector3 targetPosition = goose.transform.position + offset;

        Vector3 targetDirection = targetPosition - _tower.position;
        targetDirection.y = 0;

        if (targetDirection != Vector3.zero)
        {
            _tower.forward = targetDirection.normalized;
        }

        _spawnerBall.SpawnBall(_spawnPointBall, targetDirection.normalized);
        _countBall--;
    }

    private void StopCoroutineTimer()
    {
        if (_coroutineTimerRemove != null)
            StopCoroutine(_coroutineTimerRemove);
    }

    private IEnumerator StartTimerRemove()
    {
        yield return _waitRemove;
        Destroy(gameObject);
    }
}