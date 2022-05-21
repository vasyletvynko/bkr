using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class followCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(character);
        transform.position = player.position + offset;
    }

}
