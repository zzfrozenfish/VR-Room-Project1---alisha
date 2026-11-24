using System;
using UnityEngine;
using TMPro;

public class RealTimeClockAndSun : MonoBehaviour
{
    [Header("Digital Clock")]
    public TMP_Text digitalDisplay;
    public bool use24HourFormat = true;

    [Header("Analog Clock Hands")]
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    [Header("Sun / Directional Light")]
    public Light sunLight;
    public float maxSunIntensity = 1.2f;

    [Header("Testing")]
    public bool useRealTime = true;
    [Range(0f, 24f)] public float manualTimeHours = 12f;

    private void Update()
    {
        float totalHours;

        if (useRealTime)
        {
            DateTime now = DateTime.Now;
            totalHours = now.Hour + (now.Minute / 60f) + (now.Second / 3600f);

            if (digitalDisplay != null)
                digitalDisplay.text = now.ToString(use24HourFormat ? "HH:mm" : "hh:mm tt");
        }
        else
        {
            totalHours = manualTimeHours;
            int h = Mathf.FloorToInt(totalHours) % 24;
            int m = Mathf.FloorToInt((totalHours - Mathf.Floor(totalHours)) * 60f);

            if (digitalDisplay != null)
                digitalDisplay.text = $"{h:00}:{m:00}";
        }

        UpdateAnalogHands(totalHours);
        UpdateSun(totalHours);
    }

    private void UpdateAnalogHands(float totalHours)
    {
        float minutes = (totalHours % 1f) * 60f;
        float seconds = (minutes % 1f) * 60f;

        // Rotates around local Z; change to X or Y if your hands face a different axis
        if (hourHand != null)
            hourHand.localRotation = Quaternion.Euler(0f, 0f, -(totalHours % 12f) * 30f);
        if (minuteHand != null)
            minuteHand.localRotation = Quaternion.Euler(0f, 0f, -minutes * 6f);
        if (secondHand != null)
            secondHand.localRotation = Quaternion.Euler(0f, 0f, -seconds * 6f);
    }

    private void UpdateSun(float totalHours)
    {
        if (sunLight == null) return;

        // 6:00 = 0° (sunrise), 12:00 = 90° (noon), 18:00 = 180° (sunset)
        float sunAngle = (totalHours - 6f) * 15f;
        sunLight.transform.rotation = Quaternion.Euler(sunAngle, -30f, 0f);

        // Smoothly fade sun intensity to 0 at night
        if (totalHours >= 6f && totalHours <= 18f)
        {
            float dayProgress = (totalHours - 6f) / 12f;
            sunLight.intensity = Mathf.Sin(dayProgress * Mathf.PI) * maxSunIntensity;
        }
        else
        {
            sunLight.intensity = 0f;
        }
    }
}
