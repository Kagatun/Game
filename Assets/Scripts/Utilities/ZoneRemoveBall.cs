using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRemoveBall : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ball ball))
            ball.Remove();
    }
}
