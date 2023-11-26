using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public float currentValue = 50f;
    public float minValue = 0f;
    public float maxValue = 100f;
    public float timer;

    public Slider scale;
    public Image scaleImage;
    public Image filter;

    private Color gradient;

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            increaseValue(1);
        }
    }

    void FixedUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - timer >= 1)
        {
            reduceValue(2);
            timer = currentTime;
        }

        scale.value = currentValue;
        gradient = Color.Lerp(Color.blue, Color.green, currentValue / maxValue);
        scaleImage.color = gradient;
        filter.color = gradient;
    }

    public void reduceValue(float air)
    {
        currentValue = currentValue - air;
        if (currentValue <= minValue)
        {
            currentValue = minValue;
        }
    }

    public void increaseValue(float nutrients)
    {
        currentValue = currentValue + nutrients;
            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
            }
        
    }
}
