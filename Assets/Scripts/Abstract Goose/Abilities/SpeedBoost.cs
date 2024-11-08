using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private Goose _gooseIgnore;

    private int _speedBoost = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose) && goose is IAbilityInvalidTarget == false)
        {
            if (goose != _gooseIgnore)
                goose.IncreaseSpeedMovement(_speedBoost);
        }
    }
}
