using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameManager.Instance.Player;
    }
    private void Update()
    {
        CheckEndPoint();
    }

    private void CheckEndPoint()
    {
        if (player.transform.position.z>transform.position.z)
        {
            transform.parent.position += new Vector3(0, 0, 85);
        }
    }

}
