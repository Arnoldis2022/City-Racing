using UnityEngine;
using UnityEngine.UI;

public class RewardPreview : MonoBehaviour
{
    [SerializeField] private Text _positionText;
    [SerializeField] private Text _experienceText;
    [SerializeField] private Text _creditsText;

    public void Init(int place, int experience, float credits)
    {
        _positionText.text = place.ToString();
        _experienceText.text = experience.ToString();
        _creditsText.text = credits.ToString();
    }
}
