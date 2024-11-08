public class KingGoose : Goose , IAbilityInvalidTarget
{
    protected override void Awake()
    {
        base.Awake();

        Health.SetBossHealth();

        SetStartParameters();
    }
}
