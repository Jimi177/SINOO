using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public float timeUntileBarIsHidden = 0;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        timeUntileBarIsHidden = 3;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    private void Update()
    {
        SetBarTimer();

        if(slider.value <= 0)
        {
            Destroy(slider.gameObject);
        }
    }

    public void SetBarTimer()
    {
        timeUntileBarIsHidden -= Time.deltaTime;

        if(slider!=null)
        {
            if (timeUntileBarIsHidden <= 0)
            {
                timeUntileBarIsHidden = 0;
                slider.gameObject.SetActive(false);
            }
            else
            {
                if (!slider.gameObject.activeInHierarchy)
                {
                    slider.gameObject.SetActive(true);
                }
            }
        }
    }
}
