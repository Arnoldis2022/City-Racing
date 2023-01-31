using UnityEngine;
using UnityEngine.UI;

public class CarCheckerUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _driftContainer;
    [SerializeField] private Text _driftExperienceText;
    [SerializeField] private Text _driftCreditsText;
    private Car _car;
    private int _credits;
    private int _experience;

    private void Start()
    {
        _car = _player.Car;
    }

    private void LateUpdate()
    {
        if (_car.IsDrifting)
        {
            _driftContainer.gameObject.SetActive(true);
            _credits++;
            _experience++;
            _driftCreditsText.text = _credits.ToString();
            _driftExperienceText.text = _experience.ToString();
        }
        else
        {
            _credits = 0;
            _experience = 0;
            _driftContainer.gameObject.SetActive(false);
        }
    }
}
