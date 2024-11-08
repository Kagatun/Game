using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperGoose : Goose, IArmorable, INonStunable , ISecretive
{
    protected override void Awake()
    {
        base.Awake();

        GooseMover.SetFastSpeed();
        Health.SetBigHealth();
        MakeFly();

        SetStartParameters();
    }
}
