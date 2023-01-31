using System.Collections;
using UnityEngine;

public class PopupInfo : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextObject _experienceTextPrefab;
    [SerializeField] private TextObject _creditsTextPrefab;
    [SerializeField] private GameObject _levelUpPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private float _popupTimePerSeconds = 2f;

    public void Init()
    {
        _player.ExperienceAdded += StartExperienceText;
        _player.CreditsAdded += StartCreditsText;
        _player.PlayerLevel.LevelUP += StartLevelUPText;
    }

    public void StartLevelUPText()
    {
        var levelUpText = Instantiate(_levelUpPrefab, _container);
        StartCoroutine(Popup(levelUpText));
    }

    public void StartCreditsText(float value)
    {
        var creditsText = Instantiate(_creditsTextPrefab, _container);
        creditsText.SetText(value.ToString());
        StartCoroutine(Popup(creditsText.gameObject));
    }

    public void StartExperienceText(int value)
    {
        var experienceText = Instantiate(_experienceTextPrefab, _container);
        experienceText.SetText(value.ToString());
        StartCoroutine(Popup(experienceText.gameObject));
    }

    public IEnumerator Popup(GameObject text)
    {
        yield return new WaitForSeconds(_popupTimePerSeconds);
        Destroy(text);
    }
}
