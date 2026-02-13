using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth = 1f;
    [SerializeField] private UnityEvent _onDeathEvents;

    public float HealthPercentage => Mathf.Clamp((_health / _maxHealth), 0, 1);

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void Damage(float damage)
    {
        Debug.Log($"{gameObject.name} taking {damage} damage. Current health: {_health}");
        _health -= damage;

        if (_health <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log($"{gameObject.name} died!");
        _onDeathEvents?.Invoke(); // Null check to prevent errors
        Destroy(gameObject); // Destroy the asteroid
    }
}
