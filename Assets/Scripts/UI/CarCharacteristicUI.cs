using UnityEngine;
using UnityEngine.UI;

public class CarCharacteristicUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void Init(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }
}
