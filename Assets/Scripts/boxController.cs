using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxController : MonoBehaviour
{
    GameObject parent;
    public GameObject player;
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
                parent.transform.position += new Vector3(0, .045f, 0);
                collision.transform.position = transform.position - new Vector3(0, .03f, 0);
                collision.transform.parent = parent.transform;
                gameObject.tag = "CollectedBox";
                GameManager.Instance.collectedCubes.Add(collision.gameObject);
                GameManager.Instance.CheckCubes();

            }

        }
    }
}
