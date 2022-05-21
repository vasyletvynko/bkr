using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPickUp : MonoBehaviour
{
    [SerializeField] private int gem;
    public Text gemText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gem++;
            PlayerPrefs.SetInt("Gem", gem++);
            Destroy(other.gameObject);
            gemText.text = PlayerPrefs.GetInt("Gem").ToString();
        }

    }
}
