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
    }
    void Update()
    {
        if (GameManager.Instance.gameState==GameManager.gameStates.game)
        {
            gameObject.GetComponent<ParticleSystem>().Play();
        }
        transform.position = target.transform.position + offset;
        if (GameManager.Instance.gameState==GameManager.gameStates.end || GameManager.Instance.gameState == GameManager.gameStates.winEnd)
        {
            Destroy(gameObject);
        }
    }
}
