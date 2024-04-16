using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
    public void StopGameObject()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
