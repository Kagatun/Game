public class GreyGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetWithoutPause();
        GooseMover.SetFastSpeed();
        Health.SetBigHealth();

        SetStartParameters();
    }
}
