using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelButton : MonoBehaviour
{
    public void clearPlayer()
    {
        PlayerPrefs.DeleteAll();
    }

    public void runLevel()
    {
        SceneManager.LoadScene(1);
    }
}
