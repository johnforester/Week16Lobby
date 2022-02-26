using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOverSceneChange : MonoBehaviour
{
    [SerializeField] bool applyToChildren = true;

    int m_persistentLayer = 0;
    int m_currentLayer = 0;

    private void Awake()
    {
        m_persistentLayer = LayerMask.NameToLayer("Persistent");
        m_currentLayer = gameObject.layer;
    }

    private void OnEnable()
    {
        SceneLoader.Instance.onSceneLoadStart.AddListener(StartPersist);
        SceneLoader.Instance.onSceneLoadFinish.AddListener(EndPersist);
    }

    private void OnDisable()
    {
        if (SceneLoader.Instance == null) { return; }

        SceneLoader.Instance.onSceneLoadStart.RemoveListener(StartPersist);
        SceneLoader.Instance.onSceneLoadFinish.RemoveListener(EndPersist);
    }

    private void StartPersist()
    {
        m_currentLayer = gameObject.layer;
        SetLayer(gameObject, m_persistentLayer);
    }

    private void EndPersist()
    {
        SetLayer(gameObject, m_currentLayer);
    }
    private void SetLayer(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        if (applyToChildren)
        {
            foreach(Transform child in obj.transform)
            {
                SetLayer(child.gameObject, newLayer);
            }
        }
    }
}
