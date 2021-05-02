using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector3 mouseStartPosition;
    public float forwardSpeed;
    public float horizontalSpeed;

    public float referansPoint;


    public Animator animator;

    public static event Action PassedEngel;
    public static event Action<int> UpdatePoint;

    void Start()
    {
        referansPoint = GameManager.Instance.referansEngelCube.transform.position.z;
        animator = gameObject.GetComponentInChildren<Animator>();
        GameManager.Instance.groundObject = gameObject;
    }

    void Update()
    {

        if (GameManager.Instance.gameState == GameManager.gameStates.game)
        {
            
            move();
            if (GameManager.Instance.groundObject != gameObject)
            {
                animator.SetBool("ground", false);
            }
            else
            {
                animator.SetBool("ground", true);
            }
            checkPlayerPassedEngel();
        }
        else if(GameManager.Instance.gameState == GameManager.gameStates.winEnd)
        {
            animator.SetBool("dance", true);
        }
    }

    private void checkPlayerPassedEngel()
    {
        if (transform.position.z > referansPoint)
        {
            if (GameManager.Instance.engelLimit != 0)
            {
                    if (PassedEngel != null)
                    {
                        PassedEngel();
                    }
                    GameManager.Instance.engelLimit--;
            }
            else
            {
                if (PlayerPrefs.HasKey("level"))
                {
                    PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                    GameManager.Instance.gameState = GameManager.gameStates.winEnd;
                }
            }

        }
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
            var direction = (Input.mousePosition - mouseStartPosition)/ Screen.width ;
            //mousePositionun y değerini directiona eklemediğimiz için karaktere y ekseninde bir etki etmiyor
            direction = new Vector3((direction.x * horizontalSpeed) + transform.position.x, transform.position.y, transform.position.z);
            transform.position= direction ;
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
                GameManager.Instance.point += 100;
                UpdatePoint(GameManager.Instance.point);
            }
           
        }


    }


}
