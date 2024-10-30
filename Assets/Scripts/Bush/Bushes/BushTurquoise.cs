using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTurquoise : Bush
{
    private float _speedBoost = 2f;

    protected override void Ability(Goose goose) => //�������� �������� ����
        goose.IncreaseSpeedMovement(_speedBoost);
}
