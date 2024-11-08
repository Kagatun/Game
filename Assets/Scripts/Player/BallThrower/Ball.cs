using UnityEngine;

public class Ball : MonoBehaviour
{
    private int _damage = 1;
    private float _speed = 50f;
    private IPoolAdder<Ball> _poolAdder;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Goose goose))
        {
            goose.TakeDamage(_damage);
            Remove();
        }
    }

    public void SetPowerDamage(int damage) =>
        _damage = damage;

    public void Init(IPoolAdder<Ball> poolAdder) =>
        _poolAdder = poolAdder;

    public void Remove() =>
        _poolAdder.AddToPool(this);
}
