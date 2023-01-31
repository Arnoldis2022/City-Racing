using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public const string SceneOpeningTrigger = "sceneOpening";
    public const string SceneClosingTrigger = "sceneClosing";

    private Animator _animator;
    private AsyncOperation _loadSceneOperation;
    private static bool _shouldPlayOpeningOperation;
    private int _sceneIndex;

    public static void SwitchToScene(string sceneName)
    {
        Instance._animator.SetTrigger(SceneClosingTrigger);
        Instance._sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        //Instance._loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        //Instance._loadSceneOperation.allowSceneActivation = false;
    }

    public static void SwitchToScene(int sceneIndex)
    {
        Instance._animator.SetTrigger(SceneClosingTrigger);
        Instance._sceneIndex = sceneIndex;
        //Instance._loadSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
        //Instance._loadSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            _animator = GetComponent<Animator>();
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Debug.LogWarning("SceneLoader is already exists!");
        }

        if (_shouldPlayOpeningOperation)
            Instance._animator.SetTrigger(SceneOpeningTrigger);
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningOperation = true;
        //_loadSceneOperation.allowSceneActivation = true;
        SceneManager.LoadSceneAsync(Instance._sceneIndex);
    }

    public void CloseLoadscreen()
    {
        _animator.gameObject.SetActive(false);
    }
}
