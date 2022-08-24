using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadScene : MonoBehaviour
{
    [SerializeField]
    private Slider progressBar;

    private void Start()
    {
        loadLevel(1);
    }

    public void loadLevel(int sceneIndex)
    {
        StartCoroutine(loadAsynchrosely(sceneIndex));
    }

    IEnumerator loadAsynchrosely(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressBar.value = progress;

            yield return null;
        }
    }
}
