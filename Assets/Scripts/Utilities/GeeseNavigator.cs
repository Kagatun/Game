using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeeseNavigator : MonoBehaviour
{
    [SerializeField] private DispatcherSpawnersBushes _dispatcherSpawnersBushes;
    [SerializeField] private Transform _target;

    private List<Bush> _bushes = new List<Bush>();
    private List<Goose> _geese = new List<Goose>();
    private float _minDistance = 15;

    private void OnEnable()
    {
        _dispatcherSpawnersBushes.Spawned += AddListBushes;
    }

    private void OnDisable()
    {
        _dispatcherSpawnersBushes.Spawned -= AddListBushes;

        foreach (var bush in _bushes)
            bush.LevelChangedDown -= SetBushReassignment;
    }

    public void AddGoose(Goose goose) =>
        _geese.Add(goose);

    public void RemoveGoose(Goose goose) =>
        _geese.Remove(goose);

    public void OnAssignTarget(Goose goose)
    {
        //print("вызвалась из метода");
        goose.StopWait();

        if (goose is IRunPast)
        {
            goose.SetTargetMovement(_target.transform);
            return;
        }

        float distanceToTargetSqr = 0.01f;

        Vector3 direction = (_target.position - goose.transform.position);

        if (direction.sqrMagnitude < _minDistance * _minDistance)
        {
            goose.SetBush(null);
            goose.SetTargetMovement(_target);
            return;
        }

        if (direction.sqrMagnitude > distanceToTargetSqr)
        {
            Bush closestBush = FindClosestBush(goose);

            if (closestBush != null)
            {
                goose.SetBush(closestBush);
                goose.SetTargetMovement(closestBush.transform);
            }
            else
            {
                goose.SetBush(null);
                goose.SetTargetMovement(_target);
            }
        }
    }

    private void SetBushReassignment(Bush bush)
    {
        if (_geese.Count > 0)
        {
            foreach (Goose goose in _geese)
            {
                if (goose.TargetBush == bush && goose.MinimumBushLevel > bush.CurrentLevel || goose.TargetBush == bush && goose.TargetMovement == null)
                {
                    OnAssignTarget(goose);
                }
            }
        }

    }

    private Bush FindClosestBush(Goose goose)
    {
        List<Bush> bushes = _bushes;

        if (goose.TargetBush != null)
            bushes.Remove(goose.TargetBush);

        var suitableBushesByLevel = bushes
        .Where(bush => bush.CurrentLevel >= goose.MinimumBushLevel).ToList();

        float sqrDistanceGooseToTarget = (goose.transform.position - _target.position).sqrMagnitude;

        var closestBushes = suitableBushesByLevel
            .Where(bush => (bush.transform.position - _target.position).sqrMagnitude < sqrDistanceGooseToTarget)
            .OrderBy(bush => (goose.transform.position - bush.transform.position).sqrMagnitude)
            .Take(2).ToList();

        if (closestBushes.Count == 0)
            return null;

        if (closestBushes.Count == 1)
            return closestBushes[0];

        return closestBushes[Random.Range(0, closestBushes.Count)];
    }

    private void AddListBushes()
    {
        _bushes = new List<Bush>(_dispatcherSpawnersBushes.Bushes);

        foreach (var bush in _bushes)
            bush.LevelChangedDown += SetBushReassignment;
    }
}
