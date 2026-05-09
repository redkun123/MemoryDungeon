using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    private void Start()
    {
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
        float minimumLoadingTime = 1f;
        float timer = 0f;

        AsyncOperation operation =
            SceneManager.LoadSceneAsync("MainScreen");

        operation.allowSceneActivation = false;

        float displayProgress = 0f;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // increase progress bar gradually
            if (operation.progress >= 0.9f)
            {
                targetProgress = 1f;
            }

            displayProgress = Mathf.MoveTowards(displayProgress,targetProgress,Time.deltaTime);

            progressBar.value = displayProgress;

            bool minimumTimePassed = timer >= minimumLoadingTime;

            bool progressBarFilled = displayProgress >= 0.99f;

            if (minimumTimePassed && progressBarFilled)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}