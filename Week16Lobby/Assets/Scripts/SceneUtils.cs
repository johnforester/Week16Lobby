using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneUtils
{
    public static class Names
    {
        public static readonly string XRPersistency = "XRPersistency";
        public static readonly string ComplexInteractions = "ComplexInteractions";
        public static readonly string Maze = "Maze";
        public static readonly string JFLobby = "JFLobby";
    }

    public static void AlignXRRig(Scene persistentScene, Scene currentScene)
    {
        GameObject[] currentObjects = currentScene.GetRootGameObjects();
        GameObject[] persistentObjects = persistentScene.GetRootGameObjects();

        foreach (var rigOrigin in currentObjects)
        {
            if(rigOrigin.CompareTag("XR Rig Origin"))
            {
                foreach(var rig in persistentObjects)
                {
                    if (rig.CompareTag("XR Rig"))
                    {
                        rig.transform.rotation = rigOrigin.transform.rotation;
                        rig.transform.position = rigOrigin.transform.position;
                        return;
                    }
                }
            }
        }
    }
}
