using UnityEngine;

public class ChemistGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();
        PauseBeforeTransition.SetSmallPause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);

        SetStartParameters();
    }
}
