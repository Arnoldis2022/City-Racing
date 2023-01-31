using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadScreen : MonoBehaviour
{
    public Action LoadScreenOpened;
    public Action LoadScreenClosed;
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private Image _blackScreenLoad;

    public void Open()
    {
        _parentObject.gameObject.SetActive(true);
        StartCoroutine(OpenLoadScreen());
    }

    private IEnumerator OpenLoadScreen()
    {
        _blackScreenLoad.gameObject.SetActive(true);
        var color = _blackScreenLoad.color;
        color.a = 0f;
        _blackScreenLoad.color = color;

        while (color.a < 1f)
        {
            color = _blackScreenLoad.color;
            color.a += 0.1f;
            _blackScreenLoad.color = color;
            yield return null;
        }
        LoadScreenOpened?.Invoke();
    }

    public void Close()
    {
        StartCoroutine(CloseLoadScreen());
    }

    private IEnumerator CloseLoadScreen()
    {
        var color = _blackScreenLoad.color;
        color.a = 1f;
        _blackScreenLoad.color = color;

        while (color.a > 0f)
        {
            color.a -= 0.1f;
            _blackScreenLoad.color = color;
            yield return null;
        }
        color.a = 0f;
        _blackScreenLoad.color = color;
        _blackScreenLoad.gameObject.SetActive(false);
        LoadScreenClosed?.Invoke();
    }
}
