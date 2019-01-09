using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {
    [Header("Текст задания")]
    public string taskText;
    [Header("Текст при завершении задания")]
    public string completeText;
    [Header("Время отображения прогресса в сек.")]
    public float time;

    public ExhibitSelector el;

    int count;
    int temp;

    void Start()
    {
        count = el.GetCountExhibits();
        el.Callback += CheckExploredItems;
        DataManager.instance.LoadData();
    }
    public void CheckExploredItems()
    {
        temp = 0;
        for (int i = 0; i < el.exhs.Length; i++)
        {
            if(DataManager.save.exhibits[i].isExplored)
                temp++;
        }
        SetTaskProgress();
        SaveAndLoadProgress();
        if (temp.Equals(count))
            CompleteTask();
        StartCoroutine(ShowDelay());
    }
    public void SetTaskProgress()
    {
        string status = String.Format("{0} {1}/{2}", taskText, temp, count);
        UIManager.instance.SetTaskText(status);

        UIManager.instance.SetTaskProgress(temp, count);
    }
    public void CompleteTask()
    {
        string status = completeText;
        UIManager.instance.SetTaskText(status);

        UIManager.instance.SetTaskProgress(temp, count);
    }
    public void SaveAndLoadProgress()
    {
        DataManager.instance.SaveData();
        DataManager.instance.LoadData();
    }
    public IEnumerator ShowDelay()
    {
        UIManager.instance.SetCanvasRenderMode(UIManager.instance.instanceUi.gameObject, RenderMode.WorldSpace);
        UIManager.instance.PlayAnimation(UIManager.instance.taskInstance, "achive_on");
        yield return new WaitForSeconds(time);
        UIManager.instance.PlayAnimation(UIManager.instance.taskInstance, "achive_off");
        yield return new WaitForSeconds(1.2f);
        UIManager.instance.SetCanvasRenderMode(UIManager.instance.instanceUi.gameObject, RenderMode.ScreenSpaceCamera);
    }
}
