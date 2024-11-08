public class DemonGoose : Goose, IAbilityInvalidTarget, INonStunable
{
    protected override void Awake()
    {
        base.Awake();

        GooseMover.SetSlowSpeed();
        Health.SetBossHealth();

        SetStartParameters();
    }
}
