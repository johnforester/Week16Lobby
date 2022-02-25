using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] Material SceneFade = null;

    [Range(0.0f, 5.0f)]
    [SerializeField] float AddedWaitTime = 1.0f;

    [SerializeField] UnityEvent onSceneLoadStart = new UnityEvent();
    [SerializeField] UnityEvent onSceneLoadFinish = new UnityEvent();

    [Min(0.0f)]
    [SerializeField] float m_fadeSpeed = 1.0f;

    bool m_isLoading = false;
    float m_fadeAmount = 0.0f;

    Coroutine m_fadeCoroutine = null;
    static readonly int m_fadeAmountPropID = Shader.PropertyToID("_FadeAmount");

    Scene m_persistentScene;

    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;
        m_persistentScene = SceneManager.GetActiveScene();

        if (!Application.isEditor)
        {
            SceneManager.LoadSceneAsync(SceneUtils.Names.JFLobby, LoadSceneMode.Additive);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;
    }

    void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        SceneUtils.AlignXRRig(m_persistentScene, scene);
    }

    public void LoadScene(string sceneName)
    {
        if (m_isLoading) { return; }

        StartCoroutine(Load(sceneName));
    }

    IEnumerator Load(string sceneName)
    {
        m_isLoading = true;
        onSceneLoadStart?.Invoke();

        yield return FadeOut();
        yield return StartCoroutine(UnloadCurrentScene());

        yield return new WaitForSeconds(AddedWaitTime);

        yield return LoadNewScene(sceneName);
        yield return FadeIn();
        onSceneLoadFinish?.Invoke();
        m_isLoading = false;

    }

    
    IEnumerator UnloadCurrentScene()
    {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        while(!unload.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!load.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        if(m_fadeCoroutine != null)
        {
            StopCoroutine(m_fadeCoroutine);
        }

        m_fadeCoroutine = StartCoroutine(Fade(0.0f));
        yield return m_fadeCoroutine;
    }

    IEnumerator FadeOut()
    {
        if (m_fadeCoroutine != null)
        {
            StopCoroutine(m_fadeCoroutine);
        }

        m_fadeCoroutine = StartCoroutine(Fade(1.0f));
        yield return m_fadeCoroutine;
    }

    IEnumerator Fade(float target)
    {
        while(!Mathf.Approximately(m_fadeAmount, target))
        {
            m_fadeAmount = Mathf.MoveTowards(m_fadeAmount, target, m_fadeSpeed * Time.deltaTime);
            SceneFade.SetFloat(m_fadeAmountPropID, m_fadeAmount);
            yield return null;
        }

        SceneFade.SetFloat(m_fadeAmountPropID, target);
    }
}
