using UnityEngine;
using UnityEngine.SceneManagement;

public class exitScene : MonoBehaviour
{
    public void exitSceneButton()
    {
        SceneManager.LoadScene(0);
    }

    public void levelUp()
    {
        int i = PlayerPrefs.GetInt("levelAdventure");
        PlayerPrefs.SetInt("levelAdventure", i+1);
    }

    public void getlevel()
    {
        Debug.Log(PlayerPrefs.GetInt("levelAdventure"));
    }
    private void Start()
    {
    }

    public void adventureLoad()
    {
        SceneManager.LoadScene(1);
    }
    public void shopScene()
    {
        SceneManager.LoadScene(2);
    }
    
}
