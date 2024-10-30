using System;
using UnityEngine;

public class GooseDetector : MonoBehaviour
{
    public event Action<Goose> Collided;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose))
            Collided?.Invoke(goose);
    }
}
