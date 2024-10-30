using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BushBlack : Bush
{
    protected override void Ability(Goose goose) =>//в куст бытро пр€четс€ гусь
        goose.transform.position = transform.position + new Vector3(0, 1, 0);  //оффсет потом просто убрать
}
