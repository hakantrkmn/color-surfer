using System;
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


    public enum gameStates { start,game,end,winEnd };
    public gameStates gameState;

    public int level;
    public float engelLimit = 0;
    
    
    public int point;

    public GameObject groundObject;
    public Transform referansEngelCube;
    public ParticleSystem cubeParticle;

    public GameObject cubeTakeText;

    public List<GameObject> collectedCubes;

    public List<GameObject> collectedRedCubes;
    public List<GameObject> collectedBlueCubes;
    public List<GameObject> collectedPurpleCubes;
    public List<GameObject> collectedYellowCubes;
    public List<GameObject> collectedGreenCubes;

    public GameObject Player;

    void Start()
    {
        levelEngelSettings();
        gameState = gameStates.start;
    }

    private void levelEngelSettings()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            level = 0;
            PlayerPrefs.SetInt("level", 0);
            LevelManager.Instance.zDistance = 0.6f;
        }
        if (level >= 0 && level<10)
        {
            engelLimit = 0;
            LevelManager.Instance.zDistance = 0.5f;
        }
        else if (level >= 10 && level <30)
        {
            engelLimit = 1;
            LevelManager.Instance.zDistance = 0.4f;
        }
        else if (level >= 30)
        {
            engelLimit = 2;
            LevelManager.Instance.zDistance = 0.3f;
        }
    }

    void Update()
    {
        destroyCubesAroundEngel();
    }

    private void destroyCubesAroundEngel()
    {
        if (referansEngelCube!=null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(referansEngelCube.transform.position, 2);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.tag == "box")
                {
                    if (hitCollider.transform.parent!=Player.transform)
                    {
                        Destroy(hitCollider.gameObject);

                    }
                }
            }
        }

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
            collectedCubes.Remove(collectedGreenCubes[0]);
            collectedCubes.Remove(collectedPurpleCubes[0]);
            collectedCubes.Remove(collectedRedCubes[0]);
            collectedCubes.Remove(collectedYellowCubes[0]);



            var thisParticle = Instantiate(cubeParticle, collectedYellowCubes[0].transform.position, Quaternion.identity);
            thisParticle.GetComponent<ParticleSystemRenderer>().material = collectedYellowCubes[0].GetComponent<MeshRenderer>().material;
            Destroy(collectedYellowCubes[0]);
            Destroy(thisParticle, 2);

            var thisParticle2 = Instantiate(cubeParticle, collectedBlueCubes[0].transform.position, Quaternion.identity);
            thisParticle2.GetComponent<ParticleSystemRenderer>().material = collectedYellowCubes[0].GetComponent<MeshRenderer>().material;
            Destroy(collectedBlueCubes[0]);
            Destroy(thisParticle2, 2);

            var thisParticle3 = Instantiate(cubeParticle, collectedGreenCubes[0].transform.position, Quaternion.identity);
            thisParticle3.GetComponent<ParticleSystemRenderer>().material = collectedYellowCubes[0].GetComponent<MeshRenderer>().material;
            Destroy(collectedGreenCubes[0]);
            Destroy(thisParticle3, 2);

            var thisParticle4 = Instantiate(cubeParticle, collectedPurpleCubes[0].transform.position, Quaternion.identity);
            thisParticle4.GetComponent<ParticleSystemRenderer>().material = collectedPurpleCubes[0].GetComponent<MeshRenderer>().material;
            Destroy(collectedPurpleCubes[0]);
            Destroy(thisParticle4, 2);

            var thisParticle5 = Instantiate(cubeParticle, collectedRedCubes[0].transform.position, Quaternion.identity);
            thisParticle5.GetComponent<ParticleSystemRenderer>().material = collectedRedCubes[0].GetComponent<MeshRenderer>().material;
            Destroy(collectedRedCubes[0]);
            Destroy(thisParticle5, 2);

            Camera.main.GetComponent<CameraController>().offset += new Vector3(0, 0, 5*0.05f);

            var text = Instantiate(cubeTakeText, Player.transform.position + new Vector3(0.05f, 0.05f, 0), Quaternion.identity);
            text.GetComponent<TextMesh>().text = "+1000";
            Destroy(text, 0.5f);

            point += 1000;
        }
        //listeleri temizle
        collectedRedCubes.Clear();
        collectedYellowCubes.Clear();
        collectedPurpleCubes.Clear();
        collectedBlueCubes.Clear();
        collectedGreenCubes.Clear();

        //ground objeyi yenile
        StartCoroutine(FindGroundObj());
       
    }


    //destroy metodu hemen çalışmadığı için sorun oluyordu.frame yi bekleyip çalıştırdığımızda düzeldi.
    public IEnumerator FindGroundObj()
    {
        yield return new WaitForEndOfFrame();
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
