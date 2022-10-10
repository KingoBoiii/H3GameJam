﻿using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

internal class LumenCollectable : MonoBehaviour, ILumenCollectable
{
    [SerializeField] private int _startLumen = 100;

    [Header("Light settings")]
    [SerializeField] private Light2D _light;
    [SerializeField] private float _minIntensity = 0.2f;
    [SerializeField] private float _maxIntensity = 1f;

    [Header("Fade settings")]
    [SerializeField] private float _timeBeforeRegen = 2.0f;
    [SerializeField] private float _regenSpeed = 0.1f;
    [SerializeField] private float _fadeSpeed = 0.7f;

    public float Lumen { get; set; }

    private void Start()
    {
        Lumen = _startLumen;
    }

    public float Collect()
    {
        if (Lumen <= 0)
        {
            Lumen = 0;

            StartCoroutine(RegenLumen());

        }
        else if (Lumen > _startLumen)
        {
            Lumen = _startLumen;
        }

        UpdateLightIntensity();

        return Lumen;
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

