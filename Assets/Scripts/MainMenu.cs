using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    //Called on a Start of Game
    void Start()
    {
        Time.timeScale = 1f;
    }

    //Next Scene starting
    public void PlayGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Timer to show the loading scene
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }

    //Quiting Game
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
