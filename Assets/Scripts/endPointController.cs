using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPointController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" || other.tag== "CollectedBox")
        {
            transform.parent.position += new Vector3(0, 0, 85);
        }

    }
}
