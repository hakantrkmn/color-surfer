using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton


    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion


    public enum gameStates { start,game };

    public gameStates gameState;

    public GameObject groundObject;

    public List<GameObject> collectedCubes;

    public List<GameObject> collectedRedCubes;
    public List<GameObject> collectedBlueCubes;
    public List<GameObject> collectedPurpleCubes;
    public List<GameObject> collectedYellowCubes;
    public List<GameObject> collectedGreenCubes;

    public GameObject Player;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void CheckCubes()
    {
        //küpleri renklerine göre ayır
        foreach (var item in collectedCubes)
        {
            if (item.GetComponent<MeshRenderer>().material.name =="cubeRed (Instance)")
            {
                collectedRedCubes.Add(item);
            }
            else if (item.GetComponent<MeshRenderer>().material.name == "cubeBlue (Instance)")
            {
                collectedBlueCubes.Add(item);
            }
            else if (item.GetComponent<MeshRenderer>().material.name == "cubePurple (Instance)")
            {
                collectedPurpleCubes.Add(item);
            }
            else if (item.GetComponent<MeshRenderer>().material.name == "cubeYellow (Instance)")
            {
                collectedYellowCubes.Add(item);
            }
            else if (item.GetComponent<MeshRenderer>().material.name == "cubeGreen (Instance)")
            {
                collectedGreenCubes.Add(item);
            }
        }

        //koşul sağlanıyorsa hepsinden 1 tane sil ve küp listesinden çıkar
        bool haveAllColors = collectedBlueCubes.Count >= 1 && collectedGreenCubes.Count >= 1 && collectedPurpleCubes.Count >= 1 && collectedRedCubes.Count >= 1 && collectedYellowCubes.Count >= 1;
        if (haveAllColors)
        {
            collectedCubes.Remove(collectedBlueCubes[0]);
            Destroy(collectedBlueCubes[0]);

            collectedCubes.Remove(collectedGreenCubes[0]);
            Destroy(collectedGreenCubes[0]);
            
            collectedCubes.Remove(collectedPurpleCubes[0]);
            Destroy(collectedPurpleCubes[0]);
            
            collectedCubes.Remove(collectedRedCubes[0]);
            Destroy(collectedRedCubes[0]);
            
            collectedCubes.Remove(collectedYellowCubes[0]);
            Destroy(collectedYellowCubes[0]);
        }
        //listeleri temizle
        collectedRedCubes.Clear();
        collectedYellowCubes.Clear();
        collectedPurpleCubes.Clear();
        collectedBlueCubes.Clear();
        collectedGreenCubes.Clear();

        //ground objeyi yenile
        FindGroundObj();
       
    }

    public void FindGroundObj()
    {
        if (collectedCubes.Count>0)
        {
            if (collectedCubes[collectedCubes.Count - 1]!=null)
            {
                groundObject = collectedCubes[collectedCubes.Count - 1];
            }
            else
            {
                groundObject = collectedCubes[collectedCubes.Count - 2];
                collectedBlueCubes.Remove(collectedBlueCubes[collectedBlueCubes.Count - 1]);
            }
        }
        else
        {
            groundObject = Player;
        }
    }
}
