using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetAveragePause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetMediumSpeed();
        Health.SetMediumHealth();

        SetStartParameters();
    }
}
