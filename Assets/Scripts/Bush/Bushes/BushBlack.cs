using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BushBlack : Bush
{
    protected override void Ability(Goose goose) =>//� ���� ����� �������� ����
        goose.transform.position = transform.position;
}
