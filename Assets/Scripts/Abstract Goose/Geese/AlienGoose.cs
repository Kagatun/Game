using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGoose : Goose, IAbilityInvalidTarget, ISecretive
{
    protected override void Awake()
    {
        base.Awake();

        SetStartParameters();
    }

    protected override void SetTargetMovement(Transform transformTarget)
    {
        base.SetTargetMovement(transformTarget);
        Debug.Log("Стелс)");//Звук стелс
    }
}


