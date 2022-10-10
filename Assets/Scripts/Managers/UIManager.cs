using UnityEngine;
using UnityEngine.UI;

internal class UIManager : SingletonManager<UIManager>
{
    [SerializeField] private Slider _lumenSlider;

    private void Awake()
    {
        s_Instance = this;
    }

    public void UpdateLumen(float lumenPercent)
    {
        _lumenSlider.value = lumenPercent;
    }
}
