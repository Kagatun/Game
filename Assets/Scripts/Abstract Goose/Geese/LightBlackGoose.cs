using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlackGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetSmallPause();
        GooseMover.SetSlowSpeed();
        Health.SetMediumHealth();

        SetStartParameters();
    }
}
