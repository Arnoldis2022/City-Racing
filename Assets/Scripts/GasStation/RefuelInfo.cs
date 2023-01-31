using UnityEngine;
using UnityEngine.UI;

public class RefuelInfo : MonoBehaviour
{
    [SerializeField] private Text _playerCreditsText;
    [SerializeField] private Text _requiredCreditsText;

    public void OpenRefuelInfo(float playerCredits, float requiredCredits)
    {
        gameObject.SetActive(true);
        _playerCreditsText.text = playerCredits.ToString();
        _requiredCreditsText.text = requiredCredits.ToString();
    }

    public void CloseRefuelInfo()
    {
        gameObject.SetActive(false);
    }
}
