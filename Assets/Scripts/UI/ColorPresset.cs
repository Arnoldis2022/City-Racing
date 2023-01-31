using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ColorPresset : MonoBehaviour
{
    [SerializeField] private PainterUI _painter;
    private Image _image;
    private Button _colorButton;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _colorButton = GetComponent<Button>();
        _colorButton.onClick.AddListener(SetColor);
    }

    public void SetColor()
    {
        _painter.SetColor(_image.color);
    }
}
