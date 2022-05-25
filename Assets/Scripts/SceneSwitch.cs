using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void MoveToScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}