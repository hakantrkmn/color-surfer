using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region singleton


    private static LevelManager _instance;


    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("LevelManager");
                go.AddComponent<LevelManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public float zDistance;

    public GameObject cubesParent;
    public List<GameObject> Cubes;

    bool firstBoxes;
    public Vector3 createPos;
    void Start()
    {
        firstBoxes = true;
    }

    void Update()
    {
        generateLevel();
    }

    public void createBox()
    {
        //random bir küp seçip random bir x konumunda Instantiate ediyoruz
        var randomInt = UnityEngine.Random.Range(0, Cubes.Count);
        var randomIntX = UnityEngine.Random.Range(-0.15f, 0.15f);
        createPos = new Vector3(randomIntX, createPos.y, createPos.z);
        var cube = Instantiate(Cubes[randomInt], createPos,Quaternion.identity,cubesParent.transform);
        createPos += new Vector3(0, 0, zDistance);
    }

    public void generateLevel()
    {
        if (firstBoxes)
        {
            for (int i = 0; i < 20; i++)
            {
                var random = UnityEngine.Random.Range(0, Cubes.Count);
                var randomX = UnityEngine.Random.Range(-0.15f, 0.15f);
                createPos = new Vector3(randomX, createPos.y, createPos.z);
                var cube1 = Instantiate(Cubes[random], createPos, Quaternion.identity,cubesParent.transform);
                createPos += new Vector3(0, 0, zDistance);
            }
            firstBoxes = false;
        }
    }
    
}
