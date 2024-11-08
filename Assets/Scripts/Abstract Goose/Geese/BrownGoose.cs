using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetAveragePause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetSlowSpeed();
        Health.SetBigHealth();

        SetStartParameters();
    }
}

