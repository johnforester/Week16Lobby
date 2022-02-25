using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;

public static class SceneMenu
{
    [MenuItem("Scenes/JFLobby")]
    static void OpenLobby()
    {
        OpenScene(SceneUtils.Names.JFLobby);
    }

    [MenuItem("Scenes/Maze")]
    static void OpenMaze()
    {
        OpenScene(SceneUtils.Names.Maze);
    }

    [MenuItem("Scenes/ComplexInteractions")]
    static void OpenComplexInteractions()
    {
        OpenScene(SceneUtils.Names.ComplexInteractions);
    }

    static void OpenScene(string name)
    {
        Scene persistentScene = EditorSceneManager.OpenScene("Assets/Scenes/" + SceneUtils.Names.XRPersistency + ".unity", OpenSceneMode.Single);

        Scene currentScene = EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity", OpenSceneMode.Additive);

        SceneUtils.AlignXRRig(persistentScene, currentScene);
    }
}
