using UnityEngine;

public class PainterWindow : MoveableWindow
{
    [SerializeField] private PainterUI _painter;

    public PainterUI Painter => _painter;

    public virtual void OnEnable()
    {
        _painter.gameObject.SetActive(true);
    }

    public virtual void OnDisable()
    {
        _painter.gameObject.SetActive(false);
    }
}
