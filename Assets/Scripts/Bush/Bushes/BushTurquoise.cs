using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTurquoise : Bush
{
    private float _speedBoost = 2f;

    protected override void Ability(Goose goose) => //ускоряет скорость гуся
        goose.IncreaseSpeedMovement(_speedBoost);
}
