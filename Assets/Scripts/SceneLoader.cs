using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [Header("Название сцены")]
    public string sceneName;
    [Header("Задержка перед загрузкой")]
    public float delay;

    public IEnumerator LoadScene(string name, float time)
    {
        yield return new WaitForSeconds(time);
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        UIManager.instance.SetCanvasRenderMode(UIManager.instance.instanceUi.gameObject, RenderMode.WorldSpace);
        UIManager.instance.PlayAnimation(UIManager.instance.loadingBGInstance.gameObject, "loading_on");
        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            UIManager.instance.SetLoadingProgress(progress);
            print("Scene loading...");
            yield return null;
        }
        UIManager.instance.SetCanvasRenderMode(UIManager.instance.instanceUi.gameObject, RenderMode.ScreenSpaceCamera);
    }
}
