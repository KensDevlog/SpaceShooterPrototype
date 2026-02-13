using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _damage = 25f;
    [SerializeField] private float _lifetime = 10f;
    private float _timeToDestroy;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }
    private void Start()
    {
        _timeToDestroy = Time.time + _lifetime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Asteroid>() != null) return;
        // Try to get Health component from the object we hit
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.Damage(_damage);
            _health.Damage(999);
        }
    }

    private void Update()
    {
        if (_timeToDestroy < Time.time)
        {
            Destroy(gameObject);
        }
    }
}