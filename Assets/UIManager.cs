using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject UIBox;
    public GameObject panel;
    public Text point;
    public Text level;

    private void Awake()
    {
        ObstacleManager.PatternUI += ShowPattern;
        PlayerController.UpdatePoint += UpdatePoint;
        BoxController.UpdatePoint += UpdatePoint;
    }
    private void Start()
    {
        level.text = "Level "+PlayerPrefs.GetInt("level").ToString();
    }

    private void UpdatePoint(int obj)
    {
        point.text = obj.ToString();
    }

    private void ShowPattern(List<GameObject> obj)
    {
        //bakılacak !!

        if (panel.transform.childCount!=0)
        {
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                var go = panel.transform.GetChild(i).gameObject;
                Destroy(go);
            } 
        }
        foreach (var item in obj)
        {
            var box = Instantiate(UIBox,panel.transform.position,Quaternion.identity,panel.transform);
            Color temp = item.GetComponent<Renderer>().material.color;
            temp.a = 0.7f;
            box.GetComponent<Image>().color = temp;
        }

    }
    private void OnDestroy()
    {
        ObstacleManager.PatternUI -= ShowPattern;
        PlayerController.UpdatePoint -= UpdatePoint;
        BoxController.UpdatePoint -= UpdatePoint;
    }

}
