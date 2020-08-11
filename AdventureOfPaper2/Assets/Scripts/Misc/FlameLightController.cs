using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

public class FlameLightController : MonoBehaviour
{
    public float maxIntensity = 1;
    //[Range(0,1)]public float maxFallOffIntensity = 0.5f;

    public float minValue = 0.1f;
    private float originalMaxIntensity;
    //private float originalMaxFallIntensity;
    private bool flickering = true, flick = false, dimming = false;
    public float flickerSpeed = 1;
    float minIntensity = 0.25f;


    float minRange;
    float maxRange;
    Light2D _lightSource;

    private void Start()
    {
        _lightSource = GetComponent<Light2D>();
        originalMaxIntensity = maxIntensity;

        maxRange = _lightSource.pointLightOuterRadius;
        minRange = maxRange * 0.25f;

    }

    private void Update()
    {
        
        if (flickering)
        {
            Flick();
        }
    }

    //Flickers light
    public void Flick()
    {
        if (!dimming)
        {
            _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, maxIntensity + 0.2f, Time.deltaTime * flickerSpeed);
            _lightSource.pointLightOuterRadius = Mathf.Lerp(_lightSource.pointLightOuterRadius, maxRange, Time.deltaTime * flickerSpeed);
            _lightSource.pointLightInnerRadius = Mathf.Lerp(_lightSource.pointLightInnerRadius, maxRange* 0.5f, Time.deltaTime * flickerSpeed);
            //_lightSource.m_FalloffIntensity = Mathf.Lerp(_lightSource.m_FalloffIntensity, maxFallOffIntensity, Time.deltaTime * flickerSpeed);
            if (_lightSource.intensity >= maxIntensity)
            {
                dimming = true;
            }
        }

        if (dimming)
        {
            _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, 0, Time.deltaTime * flickerSpeed);
            _lightSource.pointLightOuterRadius = Mathf.Lerp(_lightSource.pointLightOuterRadius, minRange, Time.deltaTime * flickerSpeed);
            _lightSource.pointLightInnerRadius = Mathf.Lerp(_lightSource.pointLightInnerRadius, minRange * 0.5f, Time.deltaTime * flickerSpeed);
            //_lightSource.m_FalloffIntensity = Mathf.Lerp(_lightSource.m_FalloffIntensity, 0, Time.deltaTime * flickerSpeed );
            if (_lightSource.intensity <= minValue)
            {
                dimming = false;
            }
        }
    }

    public void turnOn()
    {
        _lightSource.intensity = maxIntensity;
    }

    public void turnOff()
    {
        _lightSource.intensity = 0;
    }
    #region Flicker setup
    public void ToggleFlickering(bool b)
    {
        flickering = b;
    }

    public bool IsFlickering()
    {
        return flickering;
    }

    public void ChangeFlickeringSpeed(float f)
    {
        flickerSpeed = f;
    }

    public float GetFlickerSpeed()
    {
        return flickerSpeed;
    }
    #endregion

    #region Change lightsource Intensity
    public void IncreaseLightIntensity(float f)
    {
        if (maxIntensity + f < originalMaxIntensity * 2)
        {
            maxIntensity += f;
        }

    }

    public void DecreaseLigthIntensity(float f)
    {
        if (maxIntensity + f > originalMaxIntensity / 2)
        {
            maxIntensity -= f;
        }

    }

    public void TurnMaxIntencityBackToOriginal()
    {
        maxIntensity = originalMaxIntensity;
    }
    public float GetOriginalMaxIntensity()
    {
        return originalMaxIntensity;
    }
    #endregion

    public void ChangeLightsourceOuterRadius(float f)
    {
        maxRange = f;
        minRange = maxRange * 0.25f;
    }
}
