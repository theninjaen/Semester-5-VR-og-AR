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

    private Color cleanColor = new Color(0f, 0f, 1f, 0.1f);
    private Color dirtyColor = new Color(0f, 1f, 0f, 0.1f);
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
        scaleImage.color = Color.Lerp(Color.blue, Color.green, currentValue / maxValue);
        filter.color = Color.Lerp(cleanColor, dirtyColor, currentValue / maxValue - 0.2f);
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
