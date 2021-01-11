using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{

    public Image foregroundImage;
    public float updateSeconds = 0.5f;

    private void Awake()
    {
/*        GetComponentInParent<HealthManager>().OnHealthPctChanged += HandleHealthChanged;
*/    }

    public override void SetMaxHealth(float health)
    {
        foregroundImage.fillAmount = 1.0f;
    }

    public override void SetHealth(float health)
    {
        Debug.Log("health amount setting to " + health);
        StartCoroutine(ChangeToPct(health));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsedTime = 0f;
        while (elapsedTime < updateSeconds)
        {
            elapsedTime += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsedTime / updateSeconds);
            yield return null;
        }
        foregroundImage.fillAmount = pct;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}
