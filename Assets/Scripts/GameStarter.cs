using System;
using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public SceneLoader sl;
    public Notice notice;
    public string textNotice;

	void Start () {
        UIManager.instance.InitializeUI();
        StartCoroutine(notice.EnableNotice(2f, textNotice));
        StartCoroutine(sl.LoadScene("Main", 10f));
	}

}
