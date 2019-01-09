using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    [Header("Задержка")]
    [SerializeField] private float delay;

    private float myTime = 0f;
    private Transform radialProgress;

    [HideInInspector] public GameObject player;

    public Selector[] selectors;

    private bool isSelected;
    private string tempType;
    private int tempID;

    void Start()
    {
        if (player == null)
        {
            StartCoroutine(FindCamera(0.5f));
        }
    }
    public void Update()
    {
        if (isSelected)
        {
            myTime += Time.deltaTime;
            radialProgress.GetComponent<Image>().fillAmount = myTime / delay;
            if (myTime >= delay)
            {
                ClickBtn(tempType, tempID);
                Resetinator();
            }
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public void Resetinator()
    {
        myTime = 0f;
        radialProgress.GetComponent<Image>().fillAmount = myTime;
        ChangeSelectedStatus();
    }
    public void AcceptVariables(string type, int id)
    {
        tempType = type;
        tempID = id;
        print(tempType + tempID);
        ChangeSelectedStatus();
    }
    public void ClickBtn(string type, int id)
    {
        for(int i = 0; i < selectors.Length; i++)
        {
            if (selectors[i].type.Equals(type))
            {
                selectors[i].OnClickItem(id);
                break;
            }
        }
        ChangeSelectedStatus();
    }
    private void ChangeSelectedStatus()
    {
        isSelected = !isSelected;
    }
    private IEnumerator FindCamera(float time)
    {
        yield return new WaitForSeconds(time);
        while (player == null)
        {
            Debug.Log("Camera not initialized. Retrying...");
            player = GameObject.Find("Camera Container");
            radialProgress = UIManager.instance.circleInstance.transform;
        }
    }
}


