using UnityEngine;

public class DarkGreyGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetSmallPause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetFastSpeed();
        Health.SetMediumHealth();

        SetStartParameters();
    }
}
