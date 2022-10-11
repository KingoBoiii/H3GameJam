using UnityEngine;
using UnityEngine.Rendering.Universal;

internal class PlayerLumenResource : MonoBehaviour
{
    [SerializeField] private float _startLumen;
    [SerializeField] private float _maxLumen = 100.0f;

    [SerializeField] private float _decreaseTime = 1.0f;
    [SerializeField] private float _lumenDecreaseAmount = 1.0f;

    [field: SerializeField] public float Lumen { get; set; }

    [SerializeField] private Light2D _light;

    private float _currentTime;

    private void Start()
    {
        Lumen = _startLumen;
        UpdateUI();
        UpdateLight();
    }

    private void Update()
    {
        DecreaseLuman();
    }

    private void DecreaseLuman()
    {
        if (_currentTime < Time.time && Lumen > 0.0f)
        {
            _currentTime = Time.time + _decreaseTime;
            Lumen -= _lumenDecreaseAmount;

            UpdateUI();
            UpdateLight();
        }
    }

    public bool AddLumen(float lumen)
    {
        if (Lumen + lumen > _maxLumen)
        {
            Lumen = _maxLumen;
            return false;
        }

        Lumen += lumen;
        UpdateUI();
        UpdateLight();
        return true;
    }

    public bool RemoveLumen(float lumen)
    {
        if (Lumen < lumen)
        {
            return false;
        }

        Lumen -= lumen;
        UpdateUI();
        UpdateLight();
        return true;
    }

    private void UpdateLight()
    {
        var lumenPercentage = Mathf.InverseLerp(0, _maxLumen, Lumen);
        _light.intensity = lumenPercentage;
    }

    private void UpdateUI()
    {
        var lumenPercentage = Mathf.InverseLerp(0, _maxLumen, Lumen);
        UIManager.Instance.UpdateLumen(lumenPercentage);
    }
}

