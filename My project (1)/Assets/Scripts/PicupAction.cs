using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PicupAction : MonoBehaviour
{

    [SerializeField] private int gold;
    [SerializeField] private int gems;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemsText;

    private void Start()
    {
        goldText.text = PlayerPrefs.GetInt("Gold").ToString();
        gemsText.text = PlayerPrefs.GetInt("Gem").ToString();
        gold = PlayerPrefs.GetInt("Gold");
        gems = PlayerPrefs.GetInt("Gem");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            gold++;
            PlayerPrefs.SetInt("Gold", gold);
            goldText.text = gold.ToString();
            Destroy(other.gameObject);         
        }
        else if(other.gameObject.tag == "Gem")
        {
            gems++;
            PlayerPrefs.SetInt("Gem", gems);
            gemsText.text = gems.ToString();
            Destroy(other.gameObject);
        }
    }
}
