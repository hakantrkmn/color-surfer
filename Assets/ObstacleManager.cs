using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject engel;
    public List<GameObject> boxes;
    public List<GameObject> engelPattern;
    public GameObject engelParent;

    public int level;

    public int engelYAxis;
    public float engelZDistance;

    public static event Action<List<GameObject>> PatternUI;

    private int k = 1;

    private void Start()
    {
        levelEngelSettings();
        engelZDistance = 30;
        level = GameManager.Instance.level;
        PlayerController.PassedEngel += createEngel;
        createEngel();
    }
    private void OnDestroy()
    {
        PlayerController.PassedEngel -= createEngel;
    }

    private void levelEngelSettings()
    {
        if (level >= 0 && level < 10)
        {
            engelYAxis = 2;
        }
        else if (level >= 10 && level < 30)
        {
            engelYAxis = 3;
        }
        else if (level >= 30)
        {
            engelYAxis = 4;
        }
    }

    public void createEngel()
    {
        engelPattern.Clear();
        var rand = UnityEngine.Random.Range(0, 6);
        for (int i = 0; i < engelYAxis; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (j == rand)
                {
                    var random = UnityEngine.Random.Range(0, 5);
                    var box = Instantiate(boxes[random], new Vector3((j * 0.06f) + -0.1644f, (i * 0.043f) + 4.0815f, engelZDistance * k), Quaternion.identity, engelParent.transform);
                    engelPattern.Add(box);
                    GameManager.Instance.referansEngelCube = box.transform;
                }
                else
                {
                    Instantiate(engel, new Vector3((j * 0.06f) + -0.1644f, (i * 0.043f) + 4.0815f, engelZDistance * k), Quaternion.identity, engelParent.transform);
                }
            }
        }
        if (PatternUI != null)
        {
            PatternUI(engelPattern);
        }
        k++;
        GameManager.Instance.Player.GetComponent<PlayerController>().referansPoint += engelZDistance;
    }
}