using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _invisible;
    [SerializeField] private Material _standardMaterial;

    private void OnEnable()
    {
        _skinnedMeshRenderer.material = _invisible;
    }

    private void OnDisable()
    {
        _skinnedMeshRenderer.material = _invisible;
    }

    private void OnMouseEnter()
    {
        _skinnedMeshRenderer.material = _standardMaterial;
    }

    private void OnMouseExit()
    {
        _skinnedMeshRenderer.material = _invisible;
    }
}
