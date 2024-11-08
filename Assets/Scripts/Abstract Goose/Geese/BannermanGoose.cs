using UnityEngine;

public class BannermanGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetAveragePause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetMediumSpeed();
        Health.SetLittleHealth();

        SetStartParameters();
    }
}
