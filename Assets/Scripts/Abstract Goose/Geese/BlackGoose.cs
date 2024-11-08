using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetSmallPause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetMediumSpeed();
        Health.SetMediumHealth();

        SetStartParameters();
    }
}
