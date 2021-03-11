using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 mouseStartPosition;
    public float forwardSpeed;
    public float horizontalSpeed;

    void Start()
    {
        GameManager.Instance.groundObject = gameObject;
    }

    void Update()
    {
        move();
    }

    private void move()
    {
        transform.position += transform.forward * forwardSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.16f, 0.16f),transform.position.y,transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var direction = (Input.mousePosition - mouseStartPosition).normalized;
            direction = new Vector3((direction.x * horizontalSpeed) + transform.position.x, transform.position.y, transform.position.z);
            transform.position = direction ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="box")
        {
            if (GameManager.Instance.groundObject==gameObject)
            {
                LevelManager.Instance.createBox();
                transform.position += new Vector3(0, .045f, 0);
                collision.transform.position = transform.position - new Vector3(0, .025f, 0);
                collision.transform.parent = gameObject.transform;
                GameManager.Instance.groundObject = collision.gameObject;
                GameManager.Instance.collectedCubes.Add(collision.gameObject);
            }
           
        }


    }


}
