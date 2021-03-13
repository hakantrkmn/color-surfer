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
        transform.position = target.transform.position + offset;
    }
}
