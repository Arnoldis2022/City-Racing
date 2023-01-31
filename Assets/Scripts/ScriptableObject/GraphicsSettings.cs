using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Mega Racing/Graphics settings")]
public class GraphicsSettings : ScriptableObject
{
    [SerializeField] private int _graphicsSettingsIndex;
    [SerializeField] private float _cameraFarClipPlane;
    [SerializeField] private float _fogEnd;
    [SerializeField] private int _trafficDensity;
    [SerializeField] private LightShadows _lightShadows;
    [SerializeField] private PostProcessProfile _postProcessProfile;

    public PostProcessProfile PostProcessProfile => _postProcessProfile;
    public int TrafficDensity => _trafficDensity;
    public LightShadows LightShadows => _lightShadows;

    public void SetGraphicsSettings(Camera camera, Light light, PostProcessVolume postProcessVolume = null)
    {
        SetFogSettings();
        SetCameraSettings(camera);
        light.shadows = _lightShadows;
        if(postProcessVolume != null)
            postProcessVolume.profile = _postProcessProfile;
    }

    public void SetFogSettings()
    {
        RenderSettings.fogStartDistance = _fogEnd * 0.86f;
        RenderSettings.fogEndDistance = _fogEnd;
    }

    public void SetCameraSettings(Camera camera)
    {
        camera.farClipPlane = _cameraFarClipPlane;
    }
}
