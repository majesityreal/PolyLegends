using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public float updateSpeedSeconds = 0.5f;

    public virtual void SetHealth(float health)
    {
        StartCoroutine(HealthMoveDelay(health));
    }

    public virtual void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    private IEnumerator HealthMoveDelay(float value)
    {
        float preChangeValue = slider.value;
        float elapsedTime = 0f;
        while (elapsedTime < updateSpeedSeconds)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(preChangeValue, value, elapsedTime / updateSpeedSeconds);
            yield return null;
        }
        slider.value = value;
    }
    
}
