using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGoose : Goose , ISecretive
{
    protected override void Awake()
    {
        base.Awake();

        GooseMover.SetMediumSpeed();
        Health.SetLittleHealth();
        MakeFly();

        SetStartParameters();
    }
}
