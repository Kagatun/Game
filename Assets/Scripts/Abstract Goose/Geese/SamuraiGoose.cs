using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiGoose : Goose , IRunPast
{
    protected override void Awake()
    {
        base.Awake();

        GooseMover.SetFastSpeed();
        Health.SetMediumHealth();

        SetStartParameters();
    }
}
