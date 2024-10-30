using UnityEngine;

public class TreatmentArea : MonoBehaviour
{
    private int _healing = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose))
            goose.Heal(_healing);
    }
}
