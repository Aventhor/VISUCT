using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitSelector : Selector {
    public Image img;
    public Canvas canvas;
    public Exhibit[] exhs;

    private bool isEnabled;

    GvrPointerGraphicRaycaster raycast;

    public delegate void OnItemSelect();
    public OnItemSelect Callback;

    void Start()
    {
        raycast = canvas.GetComponent<GvrPointerGraphicRaycaster>();
        raycast.enabled = false;
        StartCoroutine(WaitTimeToCheck());
    }
    IEnumerator WaitTimeToCheck()
    {
        yield return new WaitForSeconds(0.5f);
        while (DataManager.instance == null)
        {
            Debug.Log("DataManager not initialized. Retrying check files...");
        }
        bool temp = DataManager.instance.CheckFiles();
        if (!temp)
            DataManager.instance.GenerateJsonFileWithExhibits(exhs);
    }
    public override void OnClickItem(int id)
    {
        LoadImage(id);
    }
    public void LoadImage(int id)
    {
        try
        {
            for (int num = 0; num < exhs.Length; num++)
            {
                if (exhs[num].id.Equals(id))
                {
                    ChangeEnabledStatus();
                    OpenImage(id, exhs[num].width, exhs[num].height);
                    if (!DataManager.save.exhibits[num].isExplored)
                    {
                        DataManager.save.exhibits[num].isExplored = true;
                        Callback();
                    }
                    break;
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log(String.Format(
                "Image with such id not found({0})", id));
        }
    }
    public void OpenImage(int id, int width, int height)
    {
        if (isEnabled)
        {
            raycast.enabled = true;
            UIManager.instance.SetCanvasRenderMode(canvas.gameObject, RenderMode.WorldSpace);
            img.sprite = Resources.Load<Sprite>("Images/" + id) as Sprite;
            img.rectTransform.sizeDelta = new Vector2(width, height);
            UIManager.instance.PlayAnimation(img.gameObject, "img_on");
            ChangeEnabledStatus();
        }
    }
    public void CloseImage()
    {
        StartCoroutine(WaitTime());
    }
    private IEnumerator WaitTime()
    {
        UIManager.instance.PlayAnimation(img.gameObject, "img_off");
        yield return new WaitForSeconds(0.5f);
        raycast.enabled = false;
        UIManager.instance.SetCanvasRenderMode(canvas.gameObject, RenderMode.ScreenSpaceCamera);
    }
    private void ChangeEnabledStatus()
    {
        isEnabled = !isEnabled;
    }
    public int GetCountExhibits()
    {
        return exhs.Length;
    }
    public void FindEqualsExhibits() {
        for(int i = 0; i < exhs.Length; i++) {
            for(int a = i + 1; a < exhs.Length; a++) {
                if(exhs[i].id.Equals(exhs[a].id)) {
                    print(exhs[a].id + "" + exhs[a]);
                }
            }
        }
    }
}
