using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        GooseMover.SetFastSpeed();
        Health.SetMediumHealth();
        Navigator.SetStatusPersonRunningPastBushes(false);

        SetStartParameters();
    }
}
