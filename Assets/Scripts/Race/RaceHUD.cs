using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RaceHUD : MonoBehaviour
{
    [SerializeField] private GameObject _parentContainer;
    [SerializeField] private GameObject _statsContainer;
    [SerializeField] private Text _circleText;
    [SerializeField] private Text _positionText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _countDownText;

    public void Init()
    {
        _parentContainer.SetActive(true);
        _statsContainer.SetActive(false);
        _countDownText.gameObject.SetActive(false);
    }

    public void Countdown(int time)
    {
        var seconds = time + 1;
        _countDownText.gameObject.SetActive(true);
        _countDownText.text = seconds.ToString();
    }

    public void StartCountdown()
    {
        StartCoroutine(StartTextOnScreen());
    }

    private IEnumerator StartTextOnScreen()
    {
        var timer = 0f;
        _countDownText.text = "Start!";
        while (timer % 60 < 2)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _countDownText.gameObject.SetActive(false);
        _statsContainer.SetActive(true);
    }

    public void UpdatePlayerPosition(int playerPosition, int totalPositions)
    {
        _positionText.text = playerPosition + " / " + totalPositions;
    }

    public void UpdateCircle(int currentCircle, int totalCircles)
    {
        _circleText.text = currentCircle + " / " + totalCircles;
    }

    public void UpdateTime(string time)
    {
        _timeText.text = time.ToString();
    }

    public void HideHUD()
    {
        _statsContainer.SetActive(false);
        _countDownText.gameObject.SetActive(false);
        _parentContainer.SetActive(false);
    }
}
