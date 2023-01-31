using UnityEngine;
using UnityEngine.UI;

public class LockableButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _lockImage;

    public void LockButton()
    {
        _lockImage.enabled = true;
        _button.interactable = false;
    }

    public void UnlockButton()
    {
        _lockImage.enabled = false;
        _button.interactable = true;
    }
}
