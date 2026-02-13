using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _lifeTime = 5f;

    private Rigidbody _rb;
    private SphereCollider _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
    }

    private void Start()
    {
        _rb.linearVelocity = transform.forward * _speed; // Changed from AddForce for more reliable velocity
        Destroy(gameObject, _lifeTime); // Proper way to destroy after lifetime
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet hit: {other.gameObject.name}"); // Debug to see what's being hit

        // Check if it's not the player ship and has health
        if (other.GetComponent<ShipMovement>() == null)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log($"Damaging {other.gameObject.name} for {_damage} damage");
                health.Damage(_damage);

                if (health == null || health.HealthPercentage <= 0)
                {
                    GameManager gameManager = FindAnyObjectByType<GameManager>();
                    gameManager.AddScore(20);
                }

                Destroy(gameObject); // Destroy bullet on hit
            }
        }
    }
}
