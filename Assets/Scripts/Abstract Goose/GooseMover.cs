using System;
using System.Collections;
using UnityEngine;

public class GooseMover : MonoBehaviour
{
    private Transform _targetTransform;
    private Coroutine _coroutine;
    private float _startSpeed;
    private float _heightMovement;

    public event Action TargetReached;
    public event Action <bool> Stuned;

    public float FastSpeed { get; private set; } = 12f;
    public float MediumSpeed { get; private set; } = 8f;
    public float SlowSpeed { get; private set; } = 4f;
    public float CurrentSpeed { get; private set; }

    private void Update()
    {
        if (_targetTransform != null)
            GoToTarget(_targetTransform, _heightMovement);
    }

    public void GoToTarget(Transform positionTarget, float height)
    {
        _heightMovement = height;
        _targetTransform = positionTarget;

        float minDistanceToTargetSqr = 0.01f;

        Vector3 direction = (_targetTransform.position - transform.position);

        if (direction.sqrMagnitude > minDistanceToTargetSqr)
        {
            direction.Normalize();
            transform.forward = direction;

            Vector3 newPosition = transform.position + transform.forward * CurrentSpeed * Time.deltaTime;
            newPosition.y = height;

            transform.position = newPosition;
        }
        else
        {
            TargetReached?.Invoke();
            _targetTransform = null;
        }
    }

    public void ResetTargetMovement() =>
        _targetTransform = null;

    public void SetSlowSpeed()
    {
        CurrentSpeed = SlowSpeed;
        _startSpeed = CurrentSpeed;
    }

    public void SetMediumSpeed()
    {
        CurrentSpeed = MediumSpeed;
        _startSpeed = CurrentSpeed;
    }

    public void SetFastSpeed()
    {
        CurrentSpeed = FastSpeed;
        _startSpeed = CurrentSpeed;
    }

    public float ResetSpeed(float startSpeed) =>
        CurrentSpeed = startSpeed;

    public void IncreaseSpeedMovement(float speedUp)
    {
        CurrentSpeed += speedUp;
        CurrentSpeed = Mathf.Clamp(CurrentSpeed, SlowSpeed, FastSpeed);
    }

    public void TakeStun(float timeStun)
    {
        StopStunCoroutine();
        _coroutine = StartCoroutine(StartStun(timeStun));
    }

    private void StopStunCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator StartStun(float timeStun)
    {
        CurrentSpeed = 0;
        Stuned?.Invoke(true);

        yield return new WaitForSeconds(timeStun);

        CurrentSpeed = _startSpeed;
        Stuned?.Invoke(false);
    }
}
