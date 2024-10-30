public class WateringGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetLongPause();
        GooseMover.SetMediumSpeed();
        Health.SetLittleHealth();

        SetStartParameters();
    }
}
