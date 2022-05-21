using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    int level;
    
    public void LoadLevelButton()
    {
        string currentText = levelButton.GetComponentInChildren<TextMeshProUGUI>().text;
        level = Int32.Parse(currentText);
        PlayerPrefs.SetInt("currentLevel", level);
        SceneManager.LoadScene(3);
    }

}
