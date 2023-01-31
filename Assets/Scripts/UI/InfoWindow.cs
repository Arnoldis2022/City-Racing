using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private Text _text;
    [SerializeField] private Animator _animator;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private AudioClip _openClip;
    [SerializeField] private float _durationPerSeconds;
    private IEnumerator _timerCoroutine;
    private Queue _messages;

    public Text Text => _text;

    private void Awake()
    {
        _messages = new Queue();
        _timerCoroutine = InfoWindowLife();
    }

    public void OpenInfoWindow(string message)
    {
        _timerCoroutine = InfoWindowLife();
        _parentObject.SetActive(true);
        _animator.SetTrigger("open");
        _text.text = message;
        StartCoroutine(_timerCoroutine);
        _soundManager?.PlayOneShot(_openClip);
    }

    public void CloseInfoWindow()
    {
        StopCoroutine(_timerCoroutine);
        _animator.SetTrigger("close");
    }

    private IEnumerator InfoWindowLife()
    {
        float timer = 0f;
        while(true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            timer += Time.deltaTime;
            if(timer >= _durationPerSeconds)
                break;
        }
        _animator.SetTrigger("close");
    }

    public void DisableInfoWindow()
    {
        _parentObject.SetActive(false);
    }
}
