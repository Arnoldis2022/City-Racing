using UnityEngine;

public class TextObject : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text _text;
    [SerializeField] private RectTransform _rectTransform;

    public RectTransform RectTransform => _rectTransform;

    public void SetText(string message)
    {
        _text.text = message;
    }
}
