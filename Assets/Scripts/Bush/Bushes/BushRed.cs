using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushRed : Bush
{
    private int _healing = 3;

    protected override void Ability(Goose goose) => //лечит гуся который зашел в куст
        goose.Heal(_healing);
}
