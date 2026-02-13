using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private float _minSpawnInterval = 1f;
    [SerializeField] private float _maxSpawnInterval = 3f;

    [Header("Size Settings")]
    [SerializeField] private float _smallSize = 0.5f;
    [SerializeField] private float _mediumSize = 1f;
    [SerializeField] private float _largeSize = 2f;

    [Header("Speed Settings")]
    [SerializeField] private float _minSpeed = 5f;
    [SerializeField] private float _maxSpeed = 15f;

    [Header("Direction Settings")]
    [SerializeField] private Vector3 _generalDirection = Vector3.forward;
    [SerializeField] private float _lateralVariance = 2f; // How much left-right deviation

    private BoxCollider _spawnBounds;
    private float _spawnTimer;
    private float _currentSpawnInterval;

    private void Awake()
    {
        _spawnBounds = GetComponent<BoxCollider>();
        if (_spawnBounds == null)
        {
            Debug.LogError("AsteroidSpawner requires a BoxCollider component to define spawn bounds!");
        }

        // Make sure the collider is a trigger so it doesn't interfere with physics
        _spawnBounds.isTrigger = true;

        // Set initial random spawn interval
        _currentSpawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _currentSpawnInterval)
        {
            SpawnAsteroid();
            _spawnTimer = 0f;
            // Set new random interval for next spawn
            _currentSpawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
    }

    private void SpawnAsteroid()
    {
        if (_asteroidPrefab == null || _spawnBounds == null) return;

        // Get random position within bounds
        Vector3 randomPosition = GetRandomPositionInBounds();

        // Instantiate asteroid
        GameObject asteroid = Instantiate(_asteroidPrefab, randomPosition, Random.rotation);

        // Set random size
        float size = GetRandomSize();
        asteroid.transform.localScale = Vector3.one * size;

        // Set random velocity
        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 velocity = GetRandomVelocity();
            rb.linearVelocity = velocity;  // Changed from linearVelocity
        }
        else
        {
            Debug.LogWarning("Asteroid prefab doesn't have a Rigidbody component!");
        }
    }

    private Vector3 GetRandomPositionInBounds()
    {
        Vector3 center = _spawnBounds.center;
        Vector3 size = _spawnBounds.size;

        float randomX = Random.Range(-size.x / 2f, size.x / 2f);
        float randomY = Random.Range(-size.y / 2f, size.y / 2f);
        float randomZ = Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 localPosition = center + new Vector3(randomX, randomY, randomZ);
        return transform.TransformPoint(localPosition);
    }

    private float GetRandomSize()
    {
        int randomSize = Random.Range(0, 3);

        switch (randomSize)
        {
            case 0:
                return _smallSize;
            case 1:
                return _mediumSize;
            case 2:
                return _largeSize;
            default:
                return _mediumSize;
        }
    }

    private Vector3 GetRandomVelocity()
    {
        // Normalize the general direction
        Vector3 direction = _generalDirection.normalized;

        // Add lateral (perpendicular) variance
        Vector3 lateralOffset = Vector3.Cross(direction, Vector3.up);
        if (lateralOffset == Vector3.zero)
        {
            // If direction is up/down, use a different perpendicular
            lateralOffset = Vector3.Cross(direction, Vector3.forward);
        }

        lateralOffset = lateralOffset.normalized * Random.Range(-_lateralVariance, _lateralVariance);

        // Combine direction with lateral variance
        Vector3 finalDirection = (direction + lateralOffset).normalized;

        // Random speed
        float speed = Random.Range(_minSpeed, _maxSpeed);

        return finalDirection * speed;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize spawn bounds in editor
        if (_spawnBounds == null)
            _spawnBounds = GetComponent<BoxCollider>();

        if (_spawnBounds != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(_spawnBounds.center, _spawnBounds.size);
        }

        // Visualize spawn direction
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _generalDirection.normalized * 5f);
    }
}