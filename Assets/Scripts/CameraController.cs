using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float lerpSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState==GameManager.gameStates.start)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(13, 0, 0), lerpSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = target.transform.position + offset;
            transform.rotation = Quaternion.Euler(13, 0, 0);
        }
    }
}
