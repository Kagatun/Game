using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    public void DisableDisplay()
    {
        _skinnedMeshRenderer.enabled = false;
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    public void EnableDisplay()
    {
        _skinnedMeshRenderer.enabled = true;
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
    }
}
