using UnityEngine;

public class GreyGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetWithoutPause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetMediumSpeed();
        Health.SetBigHealth();

        SetStartParameters();
    }
}
