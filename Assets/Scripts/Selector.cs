using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    public Gaze gaze;
    public string type;

    public void SendIndexItem(int id)
    {
        gaze.AcceptVariables(type, id);
    }
    public virtual void OnClickItem(int id) { }
}
