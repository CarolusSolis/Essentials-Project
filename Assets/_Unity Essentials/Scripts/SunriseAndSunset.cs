using UnityEngine;

public class SunriseAndSunset : MonoBehaviour
{
    [Header("Day/Night Settings")]
    [SerializeField] private float dayDuration = 30f; // Duration of a full day in seconds
    [SerializeField] private float startTime = 0.25f; // Start at sunrise (0.25 = 6:00 AM)
    
    [Header("Light Settings")]
    [SerializeField] private float maxLightIntensity = 1f;
    [SerializeField] private float minLightIntensity = 0f;
    
    private Light directionalLight;
    private float currentTime;

    void Start()
    {
        directionalLight = GetComponent<Light>();
        currentTime = startTime;
    }

    void Update()
    {
        // Update the current time
        currentTime += Time.deltaTime / dayDuration;
        if (currentTime >= 1f)
            currentTime -= 1f;

        // Calculate sun rotation and intensity
        float sunRotation = currentTime * 360f;
        transform.rotation = Quaternion.Euler(sunRotation, -30f, 0f);

        // Adjust light intensity based on sun position
        float intensity = Mathf.Clamp01(Mathf.Sin(currentTime * 2f * Mathf.PI));
        directionalLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, intensity);
    }

    // Helper method to get the current time of day (0-24)
    public float GetTimeOfDay()
    {
        return currentTime * 24f;
    }
}
