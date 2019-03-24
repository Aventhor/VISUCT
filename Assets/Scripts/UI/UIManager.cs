using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null; // Singleton
    [Header("КружОчек")]
    public GameObject circle;
    [Header("Элементы уведомления")]
    public Image noticeBG;
    public Text noticeText;
    [Header("Прогресс бар загрузки")]
    public Image loadingBG;
    public Image loadingProgress;
    [Header("Прогресс бар задания")]
    public GameObject task;
    [Header("Холст")]
    public GameObject UICanvas;

    [HideInInspector] public GameObject instanceUi;
    [HideInInspector] public GameObject circleInstance;
    [HideInInspector] public GameObject noticeBGInstance;
    [HideInInspector] public GameObject noticeTextInstance;
    [HideInInspector] public GameObject loadingBGInstance;
    [HideInInspector] public GameObject loadingProgressInstance;
    [HideInInspector] public GameObject taskInstance;
    [HideInInspector] public GameObject taskTextInstance;
    [HideInInspector] public GameObject taskProgressInstance;
    [HideInInspector] public GameObject loadingProgressTextInstance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }
    public void InitializeUI()
    {
        if(instanceUi != null)
        {
            KillUI();
        }
        instanceUi = Instantiate(UICanvas, this.transform) as GameObject;
        instanceUi.GetComponent<Canvas>().worldCamera = Camera.main;

        circleInstance = Instantiate(circle, instanceUi.transform) as GameObject;

        noticeBGInstance = Instantiate(noticeBG.gameObject, instanceUi.transform) as GameObject;
        noticeTextInstance = Instantiate(noticeText.gameObject, noticeBGInstance.transform) as GameObject;

        loadingBGInstance = Instantiate(loadingBG.gameObject, instanceUi.transform) as GameObject;
        loadingProgressInstance = Instantiate(loadingProgress.gameObject, loadingBGInstance.transform) as GameObject;
        loadingProgressTextInstance = loadingBGInstance.transform.Find("Text").gameObject;

        taskInstance = Instantiate(task, instanceUi.transform) as GameObject;
        taskTextInstance = taskInstance.transform.Find("progress Text").gameObject;

        GameObject temp = taskInstance.transform.Find("circle").gameObject;
        taskProgressInstance = temp.transform.Find("progress Bar").gameObject;
    }
    public void KillUI()
    {
        Destroy(instanceUi);
    }
    public void SetNoticeText(string text)
    {
        noticeTextInstance.GetComponent<Text>().text = text.ToString();
    }
    public void SetLoadingProgress(float value)
    {
        loadingProgressInstance.GetComponent<Image>().fillAmount = value;
        loadingProgressTextInstance.GetComponent<Text>().text = String.Format("{0}%", (int)value * 100);
    }
    public void SetTaskText(string text)
    {
        taskTextInstance.GetComponent<Text>().text = text.ToString();
    }
    public void SetTaskProgress(int value, int count)
    {
        taskProgressInstance.GetComponent<Image>().fillAmount = ((float) value / count);
    }
    public void PlayAnimation(GameObject obj, string animName)
    {
        if (obj != null)
        {
            obj.GetComponent<Animation>().Play(animName);
        }
    }
    public void SetCanvasRenderMode(GameObject canvas, RenderMode state)
    {
        switch (state)
        {
            case RenderMode.ScreenSpaceCamera:
                canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                break;
            case RenderMode.WorldSpace:
                canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                break;
        }
    }
}