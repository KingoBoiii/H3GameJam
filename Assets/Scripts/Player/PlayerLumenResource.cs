using UnityEngine;

internal class PlayerLumenResource : MonoBehaviour
{
    [SerializeField] private float _startLumen;
    [SerializeField] private float _maxLumen = 100.0f;

    [field: SerializeField] public float Lumen { get; set; }

    private void Start()
    {
        Lumen = _startLumen;
        UpdateUI();
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
        if(Lumen < lumen)
        {
            return false;
        }

        Lumen -= lumen;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        var lumenPercentage = (float)Lumen / _maxLumen;
        UIManager.Instance.UpdateLumen(lumenPercentage);
    }
}

