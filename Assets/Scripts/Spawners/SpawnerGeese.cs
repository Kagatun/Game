using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGeese : SpawnerObjects<Goose>
{
    private IReadOnlyList<Bush> _bushes;
    private Transform _targetHouse;

    public event Action <int> GooseRemoved;

    public void SetListBushes(IReadOnlyList<Bush> bushes)
    {
        _bushes = bushes;
    }

    public void SetTargetHouse(Transform transform)
    {
        _targetHouse = transform;
    }

    protected override Goose CreateObject()
    {
        Goose goose = base.CreateObject();
        goose.Navigator.SetBushes(_bushes);
        goose.Navigator.SetTargetHouse(_targetHouse);

        return goose;
    }

    protected override void OnGet(Goose goose)
    {
        base.OnGet(goose);
    }

    protected override void OnRelease(Goose goose)
    {
        base.OnRelease(goose);
        goose.ResetParameters();
        GooseRemoved?.Invoke(goose.Cost);
    }

    public Goose SpawnGoose(Transform spawnPoint)
    {
        Goose goose = Get();
        goose.Init(this);
        goose.transform.position = spawnPoint.position;

        return goose;
    }
}
