using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : Window
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private GameObject _mainContainer;
    [SerializeField] private SoundSettings _soundSettings;
    [SerializeField] private Button _respawnButton;
    [SerializeField] private RaceManager _raceManager;

    private void OnEnable()
    {
        _soundSettings.Init();
        _respawnButton.interactable = !_raceManager.RaceIsActive;
    }

    private void OnDisable()
    {
        _mainContainer.gameObject.SetActive(true);
        _soundSettings.gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Game.Instance.UpdatePlayerData(_player);
        Game.Instance.SaveData();
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        _playerSpawner.SetPlayerPosition(_player.Car);
    }
}
