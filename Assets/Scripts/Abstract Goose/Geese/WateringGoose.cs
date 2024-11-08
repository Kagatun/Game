public class WateringGoose : Goose
{
    protected override void Awake()
    {
        base.Awake();

        GooseMover.SetMediumSpeed();
        Health.SetLittleHealth();

        SetStartParameters();
    }
}
