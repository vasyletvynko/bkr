using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterOnMainScene : MonoBehaviour
{
    private GameObject[] characters;
    private int index;


    //public Vector3 posPlayer{get;set;}

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("SelectPlayer");

        characters = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
        }
            foreach (GameObject go in characters)
            go.SetActive(false);

        if (characters[index])
            characters[index].SetActive(true);
    }

    public void transformPlayerPosition()
    {

        Debug.Log(index);
        //characters[index].transform.position = vector3;

        //characters[index].transform.position = new Vector3(90,10,15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
