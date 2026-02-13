using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Health _health;

    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void Update()
    {
        _image.fillAmount = _health.HealthPercentage;
    }
}