using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour {
    [Header("Текст уведомления")]
    public string text;
    [Header("Задержка перед выводом сообщения")]
    public float time;

    public IEnumerator EnableNotice(float time, string text)
    {
        yield return new WaitForSeconds(time);
        UIManager.instance.SetCanvasRenderMode(UIManager.instance.instanceUi, RenderMode.WorldSpace);
        UIManager.instance.PlayAnimation(UIManager.instance.noticeBGInstance.gameObject, "notice_on");
        UIManager.instance.SetNoticeText(text);
        yield return new WaitForSeconds(5f);
        UIManager.instance.PlayAnimation(UIManager.instance.noticeBGInstance.gameObject, "notice_off");
        yield return new WaitForSeconds(0.5f);
        UIManager.instance.SetCanvasRenderMode(UIManager.instance.instanceUi, RenderMode.ScreenSpaceCamera);
    }
}
