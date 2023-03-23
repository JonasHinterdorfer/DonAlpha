using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinData : MonoBehaviour
{
    public void ShowCanvas(Canvas can)
    {
        can.gameObject.SetActive(true);
    }

    public void HideCanvas(Canvas can)
    {
        can.gameObject.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void Stop()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
