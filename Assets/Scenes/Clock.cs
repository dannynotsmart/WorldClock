using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;

    [SerializeField]
    Transform hoursPivot, minutesPivot, secondsPivot;

    [SerializeField]
    TextMeshPro textMesh;

    [SerializeField]
    public string location;

    [SerializeField]
    [Range(-12f, 12f)]
    public int offset;

    void UpdateText(DateTime time)
    {
        textMesh.text = $"{location}\n{GetFormattedTime(time)}";
    }

    string GetFormattedTime(DateTime time)
    {
        int hours = time.Hour + offset;
        int minutes = time.Minute;
        int seconds = time.Second;

        if (hours < 0)
        {
            hours += 12;
        }
        else if (hours > 12)
        {
            hours -= 12;
        }

        return $"{hours}:{(minutes < 10 ? "0" : "")}{minutes}:{(seconds < 10 ? "0" : "")}{seconds} (GMT {offset})";
    }

    void Awake()
    {
        DateTime time = DateTime.UtcNow;
        hoursPivot.localRotation =
            Quaternion.Euler(0f, 0f, hoursToDegrees * (time.Hour + offset));
        minutesPivot.localRotation =
            Quaternion.Euler(0f, 0f, minutesToDegrees * time.Minute);
        secondsPivot.localRotation =
            Quaternion.Euler(0f, 0f, secondsToDegrees * time.Second);

        UpdateText(time);
    }

    void Update()
    {
        DateTime time = DateTime.UtcNow;

        TimeSpan ts = time.TimeOfDay;
        hoursPivot.localRotation =
            Quaternion.Euler(0f, 0f, hoursToDegrees * ((float)ts.TotalHours + offset));
        minutesPivot.localRotation =
            Quaternion.Euler(0f, 0f, minutesToDegrees * (float)ts.TotalMinutes);
        secondsPivot.localRotation =
            Quaternion.Euler(0f, 0f, secondsToDegrees * (float)ts.TotalSeconds);

        UpdateText(time);
    }
}
