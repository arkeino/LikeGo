using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static int sceneNb = 0;
    
    public void LoadScene(int sceneNb)
    {
        SceneManager.LoadScene(sceneNb);
    }

    public void LoadNextScene()
    {
        sceneNb++;
        SceneManager.LoadScene(sceneNb);
    }

    public void LoadPreviousScene()
    {
        sceneNb--;
        SceneManager.LoadScene(sceneNb);
    }

    public void LoadCaptureScene()
    {
        SceneManager.LoadScene("SamCapture");
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("SamMap");
    }

    public void LoadDexScene()
    {
        SceneManager.LoadScene("SamDex");
    }
}
