using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TODO: оепепнахрх бяе
public class BuyAndSelectCharact : MonoBehaviour
{
    private int index;

    private int characterDefPos = 0;
    private const int characBetwenChar = 15;
    private const float camPosX = 10.5f;

    private int Gold;
    private int Gem;

    protected CharacterInfo characterInfo;

    private GameObject[] characters;

    [SerializeField] private Text goldText;
    [SerializeField] private Text gemText;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Image GoldImage;
    [SerializeField] private Image GemImage;


    void Start()
    {
        Gold = PlayerPrefs.GetInt("Gold");
        Gem = PlayerPrefs.GetInt("Gem");
        Scene scene = SceneManager.GetActiveScene();

        index = PlayerPrefs.GetInt("SelectPlayer");

        characters = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;

            var component = characters[i].GetComponent<CharacterInfo>();
            
            if (component.CharacterCost == 0)
            {
                component.isUnlocked = true;
            }

            else
            {
                component.isUnlocked = PlayerPrefs.GetInt(component.CharacterName_EN, 0) == 0 ? false : true;
            }

            characters[i].transform.position = new Vector3(characterDefPos, 0, 0);
            characterDefPos += characBetwenChar;
        }
        
        if (scene.name != "mainScene") {
        characterFilterActive();
        }
        else Camera.main.transform.position = new Vector3(characters[index].transform.position.x + camPosX, 20, -39);

        buySelsectButtonsUI();

        textMoney();
    }

    private void characterFilterActive()
    {
        foreach (GameObject go in characters)
            go.SetActive(false);

        if (characters[index])
            characters[index].SetActive(true);

    }

    public void SelectLeftButton()
    {
        index--;
        if (index < 0)
        {
            index = characters.Length - 1;
        }

        Camera.main.transform.position = new Vector3(characters[index].transform.position.x + camPosX, 20, -39);
    }

    public void SelectRightButton()
    {
        index++;
        if (index == characters.Length)
        {
            index = 0;
        }
        Camera.main.transform.position = new Vector3(characters[index].transform.position.x + camPosX, 20, -39);
    }

    public void selectPlayer()
    {
        PlayerPrefs.SetInt("SelectPlayer", index);
    }

    private void buySelsectButtonsUI()
    {
        var component = characters[index].GetComponent<CharacterInfo>();

        if (component.isUnlocked == false)
        {
            if (component.isPrime == true)
            {
                GemImage.enabled = true;
                GoldImage.enabled = false;
            }
            else
            {
                GemImage.enabled = false;
                GoldImage.enabled = true;
            }

            buyButton.SetActive(true);
            selectButton.SetActive(false);
            buyButton.GetComponentInChildren<Text>().text = $"{component.CharacterCost}";
        }
        else
        {
            buyButton.SetActive(false);

            if (PlayerPrefs.GetInt("SelectPlayer") == index)
            {
                selectButton.SetActive(false);
            }
            else
            {
                selectButton.SetActive(true); ;
            }
        }

        textMoney();
    }

    public void buyCharacter()
    {
        var component = characters[index].GetComponent<CharacterInfo>();

        
        if (component.isPrime == true)
        {
            if (PlayerPrefs.GetInt("Gem") >= component.CharacterCost) {
                PlayerPrefs.SetInt(component.CharacterName_EN, 1);
                PlayerPrefs.SetInt("SelectPlayer", index);
                Gem = Gem - component.CharacterCost;
                PlayerPrefs.SetInt("Gem", Gem);

                component.isUnlocked = true;
            }
            else component.isUnlocked = false;
        }
        else
        {
            if(Gold >= component.CharacterCost)
            {
                PlayerPrefs.SetInt(component.CharacterName_EN, 1);
                PlayerPrefs.SetInt("SelectPlayer", index);
                Gold = Gold - component.CharacterCost;
                PlayerPrefs.SetInt("Gold", Gold);
                
                component.isUnlocked = true;
            }
            else component.isUnlocked = false;
        }
    }

    private void textMoney()
    {
        goldText.text = Gold.ToString();
        gemText.text = Gem.ToString();
    }

    private void Update()
    {
        buySelsectButtonsUI();
    }
}