using UnityEngine;

public class BushRenderer : MonoBehaviour
{
    [SerializeField] private Bush _bush;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _materialFlowers;
    [SerializeField] private Material _transparentMaterial;
    [SerializeField] private Material[] _leafMaterials;

    private float _minSize = 0.2f;
    private float _maxSize = 1.2f;
    private int _maxLevel = 5;

    private void OnEnable()
    {
        _bush.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _bush.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(Bush bush)
    {
        float newSize = CalculateSize(bush.CurrentLevel);
        transform.localScale = new Vector3(newSize, newSize, newSize);

        Material[] materials = _meshRenderer.materials;
        materials[0] = _leafMaterials[bush.CurrentLevel];
        _meshRenderer.materials = materials;

        if (bush.CurrentLevel == _maxLevel && bush.CurrentAbilityPoints > 0)
        {
            materials[1] = _materialFlowers;
        }
        else
        {
            materials[1] = _transparentMaterial;
        }

        _meshRenderer.materials = materials;
    }

    private float CalculateSize(int level) =>
          _minSize + (level / (float)_maxLevel) * (_maxSize - _minSize);
}