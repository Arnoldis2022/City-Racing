using UnityEngine;
using UnityEngine.UI;

public class ImproveIndicator : MonoBehaviour
{
    [SerializeField] private Image _indicatorImage;
    [SerializeField] private Color _defaultIndicatorColor;
    [SerializeField] private Color _activeIndicatorColor;

    public void ResetIndicator()
    {
        _indicatorImage.color = _defaultIndicatorColor;
    }

    public void Activate()
    {
        _indicatorImage.color = _activeIndicatorColor;
    }
}
