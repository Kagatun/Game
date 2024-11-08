using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] private int MinimumBushLevel;

    private Transform _targetHouse;
    private IReadOnlyList<Bush> _bushes;
    private Transform _currentTargetMovement;
    private float _minDistance = 15;

    public event Action<Transform> ReadedTarget;

    public bool IsBushNeeded { get; private set; } = true;
    public Bush CurrentBush { get; private set; }

    private void OnDisable()
    {
        if (CurrentBush != null)
            CurrentBush.LevelChangedDown -= SetBushReassignment;
    }

    public void SetStatusPersonRunningPastBushes(bool isBushNeeded) =>
        IsBushNeeded = isBushNeeded;

    public void SetTargetHouse(Transform transform) =>
        _targetHouse = transform;

    public void SetBushes(IReadOnlyList<Bush> bushes) =>
        _bushes = bushes;

    public void ResetTargetMovement() =>
        _currentTargetMovement = null;

    public void OnAssignTarget()
    {
        if (CurrentBush != null)
            CurrentBush.LevelChangedDown -= SetBushReassignment;

        _currentTargetMovement = null;

        if (IsBushNeeded == false)
        {
            SetNewTarget(null, _targetHouse);
            return;
        }

        float distanceToTargetSqr = 0.01f;

        Vector3 direction = (_targetHouse.position - transform.position);

        if (direction.sqrMagnitude < _minDistance * _minDistance)
        {
            SetNewTarget(null, _targetHouse);

            return;
        }
        else if (direction.sqrMagnitude > distanceToTargetSqr)
        {
            Bush closestBush = FindClosestBush();

            if (closestBush != null)
            {
                SetNewTarget(closestBush, closestBush.transform);
            }
            else
            {
                SetNewTarget(null, _targetHouse);
            }
        }
    }

    private void SetNewTarget(Bush bush, Transform targetMovement)
    {
        CurrentBush = bush;
        _currentTargetMovement = targetMovement;
        ReadedTarget?.Invoke(_currentTargetMovement);

        if (bush != null)
            bush.LevelChangedDown += SetBushReassignment;
    }

    private void SetBushReassignment(Bush bush)
    {
        if (MinimumBushLevel > bush.CurrentLevel || _currentTargetMovement == null)
            OnAssignTarget();
    }

    private Bush FindClosestBush()
    {
        List<Bush> bushes = _bushes.ToList();

        int minCount = 2;
        int maxPercent = 100;
        int minPercent = 0;
        int percentSecondBush = 20;

        if (CurrentBush != null)
            bushes.Remove(CurrentBush);

        var suitableBushesByLevel = bushes
        .Where(bush => bush.CurrentLevel >= MinimumBushLevel).ToList();

        float sqrDistanceGooseToTarget = (transform.position - _targetHouse.position).sqrMagnitude;

        var closestBushes = suitableBushesByLevel
            .Where(bush => (bush.transform.position - _targetHouse.position).sqrMagnitude < sqrDistanceGooseToTarget)
            .OrderBy(bush => (transform.position - bush.transform.position).sqrMagnitude)
            .Take(minCount).ToList();

        if (closestBushes.Count == 0)
            return null;

        if (closestBushes.Count == 1)
            return closestBushes[0];

        int randomValue = UnityEngine.Random.Range(minPercent, maxPercent);

        if (randomValue > percentSecondBush)
        {
            return closestBushes[0];
        }
        else
        {
            return closestBushes[1];
        }
    }
}
