using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Transform transformActor;

    private void Update()
    {
        GetComponent<RectTransform>().localScale = transformActor.localScale/Mathf.Abs(transformActor.localScale.x);
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
    }

    public void SetCurrentHealth(float curHealth)
    {
        healthBar.value = curHealth;
    }

    public float GetMaxHealth()
    {
        return healthBar.maxValue;
    }

    public float GetCurHealth()
    {
        return healthBar.value;
    }
}
