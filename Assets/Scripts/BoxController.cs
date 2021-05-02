using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxController : MonoBehaviour
{
    GameObject parent;
    public GameObject player;
    public ParticleSystem particle;
    public static event Action<int> UpdatePoint;

    public GameObject cubeTakeText;

    void Start()
    {
        player = GameManager.Instance.Player;
    }

    void Update()
    {
        checkPlayer();
    }

    private void checkPlayer()
    {
        if (player.transform.position.z > transform.position.z + 0.5f)
        {
            LevelManager.Instance.createBox();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="box")
        {
            if (GameManager.Instance.groundObject == gameObject)
            {
                parent = transform.parent.gameObject;

                LevelManager.Instance.createBox();
                parent.transform.position += new Vector3(0, .1f, 0);
                collision.transform.position = transform.position - new Vector3(0, .1f, 0);
                collision.transform.parent = parent.transform;
                var text = Instantiate(cubeTakeText, gameObject.transform.parent.position + new Vector3(0.05f,0.05f,0), Quaternion.identity);
                Destroy(text, 0.5f);
                gameObject.tag = "CollectedBox";
                GameManager.Instance.collectedCubes.Add(collision.gameObject);
                GameManager.Instance.CheckCubes();
                GameManager.Instance.point += 100;
                UpdatePoint(GameManager.Instance.point);
                Camera.main.GetComponent<CameraController>().offset += new Vector3(0, 0, -0.05f);

            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "engelCube")
        {
            if (gameObject.GetComponent<MeshRenderer>().material.name== other.gameObject.GetComponent<MeshRenderer>().material.name)
            {
                Crash(gameObject, other.gameObject);
            }
            else
            {
                GameManager.Instance.gameState = GameManager.gameStates.end;
                //SceneManager.LoadScene(0);

            }
        }
    }

    public void Crash(GameObject thisGO, GameObject CollisionGO)
    {
        var thisParticle = Instantiate(particle, thisGO.transform.position, Quaternion.identity);
        thisParticle.GetComponent<ParticleSystemRenderer>().material = thisGO.GetComponent<MeshRenderer>().material;
        Destroy(thisParticle, 2);

        var collisonParticle = Instantiate(particle, CollisionGO.gameObject.transform.position, Quaternion.identity);
        collisonParticle.GetComponent<ParticleSystemRenderer>().material = CollisionGO.GetComponent<MeshRenderer>().material;
        Destroy(CollisionGO.gameObject);
        Destroy(collisonParticle, 2);

        GameManager.Instance.collectedCubes.Remove(gameObject);
        FindGroundObj();

        //GameManager.Instance.gameState = GameManager.gameStates.end;
        Destroy(gameObject);
    }

    public void FindGroundObj()
    {
        if (GameManager.Instance.collectedCubes.Count > 0)
        {
            if (GameManager.Instance.collectedCubes[GameManager.Instance.collectedCubes.Count - 1] != null)
            {
                GameManager.Instance.groundObject = GameManager.Instance.collectedCubes[GameManager.Instance.collectedCubes.Count - 1];
            }
            else
            {
                GameManager.Instance.groundObject = GameManager.Instance.collectedCubes[GameManager.Instance.collectedCubes.Count - 2];
                GameManager.Instance.collectedBlueCubes.Remove(GameManager.Instance.collectedBlueCubes[GameManager.Instance.collectedBlueCubes.Count - 1]);
            }
        }
        else
        {
            GameManager.Instance.groundObject = GameManager.Instance.Player;
        }
    }
}
