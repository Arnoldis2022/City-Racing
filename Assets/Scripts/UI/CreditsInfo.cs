using UnityEngine;
using UnityEngine.UI;

public class CreditsInfo : MonoBehaviour
{
    [SerializeField] private Text _creditsText;
    [SerializeField] private Player _player;

    private void Awake()
    {
        if(_player == null)
            _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _player.CreditsChanged += CreditsChange;
        CreditsChange(_player.Credits);
    }

    private void OnDisable()
    {
        _player.CreditsChanged -= CreditsChange;
    }

    public void CreditsChange(float credits)
    {
        _creditsText.text = credits.ToString("f0");
    }
}
