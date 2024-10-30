using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetSmallPause();
        GooseMover.SetMediumSpeed();
        Health.SetMediumHealth();

        SetStartParameters();
    }
}
