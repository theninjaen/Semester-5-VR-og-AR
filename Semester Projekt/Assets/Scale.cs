using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public int currentValue = 50;
    public int minValue;
    public int maxValue;
    public float timer;

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
    }

    void reduceValue(int air)
    {
        if (currentValue < minValue)
        {
            currentValue = minValue;
        } else {
            currentValue = currentValue - air;
        }
    }

    void increaseValue(int nutrients)
    {
        if(currentValue > maxValue)
        {
            currentValue = maxValue;
        } else {
            currentValue = currentValue + nutrients;
        }
        
    }
}
