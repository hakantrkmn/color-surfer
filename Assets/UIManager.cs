using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject UIBox;
    public GameObject panel;
    public Text point;
    public Text level;
    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    public static event Action gameStarted;

    bool endCheck = true;

    private void Awake()
    {
        ObstacleManager.PatternUI += ShowPattern;
        PlayerController.UpdatePoint += UpdatePoint;
        BoxController.UpdatePoint += UpdatePoint;
    }
    private void Start()
    {
        level.text = "Level "+PlayerPrefs.GetInt("level").ToString();
        Destroy(level, 2);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.gameState==GameManager.gameStates.start)
            {
                GameManager.Instance.gameState = GameManager.gameStates.game;
                gameStarted();
            }
        }

        if (GameManager.Instance.gameState==GameManager.gameStates.winEnd && endCheck)
        {
            winScreen.SetActive(true);
            if (GameManager.Instance.collectedCubes.Count!=0)
            {
                GameManager.Instance.point -= GameManager.Instance.collectedCubes.Count * 100;
                UpdatePoint(GameManager.Instance.point);
            }
            endCheck = false;
        }
        else if (GameManager.Instance.gameState == GameManager.gameStates.end && endCheck)
        {
            loseScreen.SetActive(true);
            if (GameManager.Instance.collectedCubes.Count != 0)
            {
                GameManager.Instance.point -= GameManager.Instance.collectedCubes.Count * 100;
                UpdatePoint(GameManager.Instance.point);
            }
            endCheck = false;
        }

        startScreenControl();
    }

    private void startScreenControl()
    {
        if (GameManager.Instance.gameState==GameManager.gameStates.game)
        {
            Destroy(startScreen);
        }
    }

    private void UpdatePoint(int obj)
    {
        point.text = obj.ToString();
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void ShowPattern(List<GameObject> obj)
    {
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
