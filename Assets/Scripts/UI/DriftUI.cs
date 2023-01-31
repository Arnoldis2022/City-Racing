using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DriftUI : MonoBehaviour
{
    [SerializeField] private DriftChecker _driftChecker;
    [SerializeField] private GameObject _container;
    [SerializeField] private Text _creditsText;
    [SerializeField] private Text _experienceText;
    [SerializeField] private Text _cancelDriftText;

    public void Init()
    {
        _driftChecker.Completed += CompleteDrift;
        _driftChecker.Cancelled += CancelDrift;
        _driftChecker.Updated += UpdateDriftPoint;
    }

    private void UpdateDriftPoint()
    {
        var credits = _driftChecker.CreditsFromDrifting;
        var experience = _driftChecker.ExperienceFromDrifting;
        _creditsText.text = credits.ToString();
        _experienceText.text = experience.ToString();
        _container.SetActive(true);
        _cancelDriftText.gameObject.SetActive(false);
    }

    private void CompleteDrift()
    {
        _container.SetActive(false);
    }

    private void CancelDrift()
    {
        _container.SetActive(false);
        StartCoroutine(DriftCancelTextShowing());
        _cancelDriftText.gameObject.SetActive(true);
    }

    private IEnumerator DriftCancelTextShowing()
    {
        yield return new WaitForSeconds(0.25f);
        _cancelDriftText.gameObject.SetActive(false);
    }
}
