using System;
using System.Collections;
using UnityEngine;

public class GooseMover : MonoBehaviour
{
    [SerializeField] private AnimationsGoose _animationsGoose;

    private Transform _targetTransform;
    private Coroutine _coroutine;

    public event Action TargetReached;

    public float FastSpeed { get; private set; } = 12f;
    public float MediumSpeed { get; private set; } = 8f;
    public float SlowSpeed { get; private set; } = 4f;
    public float CurrentSpeed { get; private set; }

    private void Update()
    {
        if (_targetTransform != null)
            GoToTarget(_targetTransform);
    }

    public void GoToTarget(Transform positionTarget)
    {
        _targetTransform = positionTarget;

        float minDistanceToTargetSqr = 0.01f;

        Vector3 direction = (_targetTransform.position - transform.position);
        direction.y = 0;

        if (direction.sqrMagnitude > minDistanceToTargetSqr)
        {
            direction.Normalize();
            transform.forward = direction;

            Vector3 newPosition = transform.position + transform.forward * CurrentSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
        else
        {
            print("вызвалось событие из мувера");
            TargetReached?.Invoke();
            _targetTransform = null;
        }
    }

    public void SetSlowSpeed() =>
        CurrentSpeed = SlowSpeed;

    public void SetMediumSpeed() =>
        CurrentSpeed = MediumSpeed;

    public void SetFastSpeed() =>
        CurrentSpeed = FastSpeed;

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
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator StartStun(float timeStun)
    {
        float startSpeed = CurrentSpeed;
        CurrentSpeed = 0;
        _animationsGoose.TriggerStun();

        yield return new WaitForSeconds(timeStun);

        CurrentSpeed = startSpeed;
        _animationsGoose.TriggerRun(CurrentSpeed, SlowSpeed, MediumSpeed, FastSpeed);
    }
}
