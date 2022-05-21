using UnityEngine;

public class levels : MonoBehaviour
{

    [SerializeField] private TextAsset levelJSON;

    [System.Serializable]
    public class Level
    {
        public string[] levelArray;
        public int levelID;
    }

    [System.Serializable]
    public class LevelList
    {
        public Level[] level;
    }

    public LevelList myLevelList = new LevelList();


    public levels(int lvl)
    {
        currentLevel = lvl;
    }

    public int currentLevel { get; set; }

    public void array()
    { 
        
        Debug.Log("test");
    }

    public int jsn(int i)
    {
        myLevelList = JsonUtility.FromJson<LevelList>(levelJSON.text);
        Debug.Log(myLevelList.level[0]);
        return i;
    }

    private void Start()
    {
        Debug.Log(currentLevel);
        jsn(0);
        //myLevelList = JsonUtility.FromJson<LevelList>(levelJSON.text);
        //jsn(myLevelList.level[currentLevel].levelID);
    }

}
