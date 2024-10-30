using UnityEngine;

public class MoverCamera : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;

    private float _cameraSpeed = 100f;
    private float _cameraSpeedMouse = 500f;
    private float _zoomSpeed = 10f;
    private float _minZoom = 10f;
    private float _maxZoom = 40f;
    private float _minX = -35f;
    private float _maxX = 35f;
    private float _minZ = -120f;
    private float _maxZ = -57f;

    private void OnEnable()
    {
        _inputDetector.Moved += OnMoveCamera;
        _inputDetector.Dragged += OnDragCamera;
        _inputDetector.Zoomed += OnZoomCamera;
    }

    private void OnDisable()
    {
        _inputDetector.Moved -= OnMoveCamera;
        _inputDetector.Dragged -= OnDragCamera;
        _inputDetector.Zoomed -= OnZoomCamera;
    }

    private void OnMoveCamera(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * _cameraSpeed * Time.deltaTime;
        ApplyBounds(ref newPosition);
        transform.position = newPosition;
    }

    private void OnDragCamera(Vector3 dragDirection)
    {
        Vector3 newPosition = transform.position + dragDirection * _cameraSpeedMouse * Time.deltaTime;
        ApplyBounds(ref newPosition);
        transform.position = newPosition;
    }

    private void OnZoomCamera(float zoomAmount)
    {
        Camera.main.fieldOfView -= zoomAmount * _zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, _minZoom, _maxZoom);
    }

    private void ApplyBounds(ref Vector3 position)
    {
        float zoomFactor = (Camera.main.fieldOfView - _minZoom) / (_maxZoom - _minZoom);

        float dynamicMinX = Mathf.Lerp(_minX, _minX + 15f, zoomFactor);
        float dynamicMaxX = Mathf.Lerp(_maxX, _maxX - 15f, zoomFactor);
        float dynamicMinZ = Mathf.Lerp(_minZ, _minZ + 15f, zoomFactor);
        float dynamicMaxZ = Mathf.Lerp(_maxZ, _maxZ - 15f, zoomFactor);

        position.x = Mathf.Clamp(position.x, dynamicMinX, dynamicMaxX);
        position.z = Mathf.Clamp(position.z, dynamicMinZ, dynamicMaxZ);
    }
}