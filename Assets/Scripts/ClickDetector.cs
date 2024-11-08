using System;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layerMask;
    //у клик детектора должны быть свои эффекты, которые будут срабатывать при нажатии и запускатьс€. “огда когда будет нажатие, 
    //будет подписка на кликкера, будет перезар€дка и он будет недоступен false, корутина пройдет и станет доступен.
    private int _damage = 1;
    private float _radiusClickCursor = 0.15f;
    private float _radiusClickCursorUltimate = 3f;

    private void OnEnable()
    {
        _inputDetector.LeftClicked += OnHandleLeftClick;
        _inputDetector.RightClicked += OnHandleRightClick;
    }

    private void OnDisable()
    {
        _inputDetector.LeftClicked -= OnHandleLeftClick;
        _inputDetector.RightClicked -= OnHandleRightClick;
    }

    private void OnHandleRightClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _radiusClickCursorUltimate, out hit, Mathf.Infinity, _layerMask))
        {
            if (hit.transform.TryGetComponent(out Goose goose))
            {
                goose.TakeStun(_damage);
                goose.TakeCleanDamage(_damage);
            }

            if (hit.transform.TryGetComponent(out Bush bush))
            {
                bush.MakeOrdinary(_damage);
            }
        }
    }

    private void OnHandleLeftClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _radiusClickCursor, out hit, Mathf.Infinity, _layerMask))
        {
            if (hit.transform.TryGetComponent(out Goose goose))
            {
                goose.TakeCleanDamage(_damage);
            }

            if (hit.transform.TryGetComponent(out Bush bush))
            {
               bush.Shrink(_damage);
            }
        }
    }
}

