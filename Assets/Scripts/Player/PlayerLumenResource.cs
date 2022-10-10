using UnityEngine;

internal class PlayerLumenResource : MonoBehaviour
{
    [SerializeField] private float _startLumen;
    [SerializeField] private float _maxLumen = 100.0f;

    [SerializeField] private float _decreaseTime = 1.0f;
    [SerializeField] private float _lumenDecreaseAmount = 1.0f;

    [field: SerializeField] public float Lumen { get; set; }

    private float _currentTime;

    private void Start()
    {
        Lumen = _startLumen;
        UpdateUI();
    }

    private void Update()
    {
        DecreaseLuman();
    }

    private void DecreaseLuman()
    {
        if (_currentTime < Time.time)
        {
            _currentTime = Time.time + _decreaseTime;
            Lumen -= _lumenDecreaseAmount;

            UpdateUI();
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
        return true;
    }

    private void UpdateUI()
    {
        var lumanPercentage = Mathf.InverseLerp(0, _maxLumen, Lumen);

        UIManager.Instance.UpdateLumen(lumanPercentage);
    }
}

