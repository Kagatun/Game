using UnityEngine;

public class TreatmentArea : MonoBehaviour
{
    private int _healing = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose) && goose is IAbilityInvalidTarget == false)
            goose.Heal(_healing);
    }
}
