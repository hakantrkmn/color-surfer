using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    private void Start()
    {
        target = GameManager.Instance.Player;
        UIManager.gameStarted += ParticleStart;
    }

    private void ParticleStart()
    {
            gameObject.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {

        transform.position = target.transform.position + offset;
        if (GameManager.Instance.gameState==GameManager.gameStates.end || GameManager.Instance.gameState == GameManager.gameStates.winEnd)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
        }
    }
}
