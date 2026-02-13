using UnityEngine;

public class TimeDeath : MonoBehaviour
{
    [SerializeField] private float _timeDeath;
    private float _timeToDestroy;

    private void Start()
    {
        _timeDeath = Time.time + _timeDeath; 
    }

    private void Update()
    {
        if (_timeToDestroy < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
