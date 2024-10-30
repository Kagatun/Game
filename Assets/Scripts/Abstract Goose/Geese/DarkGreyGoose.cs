public class DarkGreyGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetSmallPause();
        GooseMover.SetFastSpeed();
        Health.SetMediumHealth();

        SetStartParameters();
    }
}
