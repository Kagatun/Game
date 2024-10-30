using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeeseNavigator : MonoBehaviour, IMovable
{
    [SerializeField] private DispatcherSpawnersBushes _dispatcherSpawnersBushes;
    [SerializeField] private DispatcherSpawnersGeese _dispatcherSpawnersGeese;
    [SerializeField] private Transform _target;

    private List<Bush> _bushes = new List<Bush>();
    private List<Goose> _geese = new List<Goose>();
    private float _minDistance = 15;

    private void OnEnable()
    {
        _dispatcherSpawnersBushes.Spawned += AddListBushes;
        _dispatcherSpawnersGeese.Spawned += AddListGeese;
    }

    private void OnDisable()
    {
        _dispatcherSpawnersBushes.Spawned -= AddListBushes;

        foreach (var bush in _bushes)
            bush.LevelChangedDown -= SetBushReassignment;

        _dispatcherSpawnersGeese.Spawned -= AddListGeese;

        //foreach (var goose in _geese)
        //    goose.ReadedToMove -= OnAssignTarget;
    }

    public void OnAssignTarget(Goose goose)
    {
        //print("вызвалась из метода");
        goose.StopWait();

        if (goose is IRunPast)
        {
            goose.SetTargetMovement(_target.transform);

            print("Бегу сразу к цели");
            return;
        }

        float distanceToTargetSqr = 0.01f;

        Vector3 direction = (_target.position - goose.transform.position);

        if (direction.sqrMagnitude < _minDistance * _minDistance)
        {
            goose.SetBush(null);
            goose.SetTargetMovement(_target);
            print("Не нужен ближайший куст иду к цели");

            return;
        }

        if (direction.sqrMagnitude > distanceToTargetSqr)
        {
            Bush closestBush = FindClosestBush(goose);

            if (closestBush != null)
            {
                goose.SetBush(closestBush);
                goose.SetTargetMovement(closestBush.transform);

                print("нашел ближайший куст");
                return;
            }
            else
            {
                goose.SetBush(null);
                goose.SetTargetMovement(_target);
                print("Не нашел ближайший куст");
                return;
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
        int minCount = 2;

        if (goose.TargetBush != null)
            bushes.Remove(goose.TargetBush);

        var suitableBushesByLevel = bushes
        .Where(bush => bush.CurrentLevel >= goose.MinimumBushLevel).ToList();

        float sqrDistanceGooseToTarget = (goose.transform.position - _target.position).sqrMagnitude;

        var closestBushes = suitableBushesByLevel
            .Where(bush => (bush.transform.position - _target.position).sqrMagnitude < sqrDistanceGooseToTarget)
            .OrderBy(bush => (goose.transform.position - bush.transform.position).sqrMagnitude)
            .Take(minCount).ToList();

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

    private void AddListGeese(Goose goose)
    {
        _geese.Add(goose);
        OnAssignTarget(goose);
        goose.SetNewTarget(this);
    }

    public void AssignTargetMovement(Goose goose)
    {
        OnAssignTarget(goose);
    }
}
