using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public Gaze gaze;
    public Selector selector;
    public int id;
    [HideInInspector] public bool isActive;

    public void OnPointerEnter()
    {
        selector.SendIndexItem(id);
        isActive = !isActive;
    }
    public void OnPointerExit()
    {
        isActive = !isActive;
        gaze.Resetinator();
    }
    public void OnClickButton()
    {
        selector.OnClickItem(id);
    }

}
