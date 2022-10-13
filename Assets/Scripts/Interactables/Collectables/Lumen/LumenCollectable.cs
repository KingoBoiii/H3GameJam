using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

internal class LumenCollectable : Interactable, ILumenCollectable
{
    [SerializeField] private int _startLumen = 100;
    [field: SerializeField] public float Lumen { get; set; }

    [SerializeField] private Light2D _light;
    [SerializeField] private ParticleSystem _particleSystem;

    [Header("Fade settings")]
    [SerializeField] private float _timeBeforeRegen = 2.0f;
    [SerializeField] private float _regenSpeed = 0.1f;

    private void Start()
    {
        Lumen = _startLumen;
        _particleSystem.Stop();
    }

    public override void Interact()
    {
        Collect();
    }

    public float Collect()
    {
        if (Lumen <= 0)
        {
            Lumen = 0;

            Regen();
        }
        else if (Lumen > _startLumen)
        {
            Lumen = _startLumen;
        }

        if(!_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }

        UpdateLightIntensity();

        return Lumen;
    }

    public void Regen()
    {
        _particleSystem.Stop();
        
        StartCoroutine(RegenLumen());
    }

    private void UpdateLightIntensity()
    {
        var lumenPercent = (float)Lumen / _startLumen;
        _light.intensity = lumenPercent;
    }

    private IEnumerator RegenLumen()
    {
        yield return new WaitForSeconds(_timeBeforeRegen);

        while (Lumen < _startLumen)
        {
            Lumen++;
            UpdateLightIntensity();
            yield return new WaitForSeconds(_regenSpeed);
        }
    }
}


