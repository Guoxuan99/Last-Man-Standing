using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PyramidSwitch : MonoBehaviour
{
    void Start()
    {
    }

    void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.CompareTag("agent") && m_State == false)
        if (other.gameObject.CompareTag("agent"))
        {
            Debug.Log("switch collide with agent");
        }
    }
}
