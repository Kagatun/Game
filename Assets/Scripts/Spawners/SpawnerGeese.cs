using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGeese : SpawnerObjects<Goose>
{
    public event Action <int> GooseRemoved;

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
