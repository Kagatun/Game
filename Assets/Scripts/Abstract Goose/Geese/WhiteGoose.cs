using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhiteGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        SetStartParameters();
    }
}
