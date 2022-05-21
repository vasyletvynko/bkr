using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [Range (0,1)]
    [SerializeField] private float timeOfDay;
    [SerializeField] private float lengthOfDay = 180f;

    [SerializeField] private AnimationCurve sunLightCurve;
    [SerializeField] private Light dayLight;

    [SerializeField] private AnimationCurve moonLightCurve;
    [SerializeField] private Light nightLight;

    [SerializeField] private AnimationCurve skyboxCurve;

    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    private float sunIntensity;
    private float moonIntensity;

    private void Start()
    {
        sunIntensity = dayLight.intensity;
        moonIntensity = nightLight.intensity;
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime / lengthOfDay;

        if (timeOfDay >= 1) timeOfDay -= 1;

        RenderSettings.skybox.Lerp(nightSkybox, daySkybox, skyboxCurve.Evaluate(timeOfDay));
        DynamicGI.UpdateEnvironment();

        dayLight.transform.localRotation = Quaternion.Euler(timeOfDay * 360f, 28.117f, 90.605f);
        nightLight.transform.localRotation = Quaternion.Euler(timeOfDay * 360f+180, 28.117f, 90.605f);

        dayLight.intensity = sunIntensity * sunLightCurve.Evaluate(timeOfDay);
        nightLight.intensity = moonIntensity * moonLightCurve.Evaluate(timeOfDay);
    }
}
