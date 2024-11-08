public class GoslingGoose : Goose , ISecretive
{
    protected override void Awake()
    {
        base.Awake();
        IsSmall = true;

        SetStartParameters();
    }
}
