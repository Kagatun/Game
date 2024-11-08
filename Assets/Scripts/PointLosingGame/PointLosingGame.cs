using UnityEngine;

public class PointLosingGame : MonoBehaviour
{
    [SerializeField] private Dog _dog;

    private Health _health;
    private int _healthStart = 5;
    //public event Action Ended;

    private void Awake()
    {
        _health = new Health(_healthStart);
    }

    private void OnEnable()
    {
        _health.Deceased += Die;
    }

    private void OnDisable()
    {
        _health.Deceased -= Die;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose))
        {
            if (_health.Value != 0)
            {
                _dog.StartBarking();
                TakeDamage(goose.Damage);
                goose.Die();
            }
        }
    }

    private void TakeDamage(int damage) =>
        _health.TakeDamage(damage);

    private void Die()
    {
        //Ended?.Invoke(); //подпишется на это событие Game который управляет стартом сцены.
        Debug.Log("Игра завершена, вы проиграли");
    }
}
