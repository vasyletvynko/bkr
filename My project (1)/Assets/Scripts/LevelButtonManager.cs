using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject levelHolder;
    [SerializeField] private GameObject levelButton;
    [SerializeField] private GameObject thisCanvas;
    [SerializeField] private int numberOfLevels = 50;

    private Rect panelDimensions;
    private Rect iconDimensions;

    private int amountPerPage;
    private int currentLevelCount;
    private int numberButtons = 1;
    private int currentLevel;

    void Start()
    {
        if(PlayerPrefs.GetInt("levelAdventure") == 0)
        {
            currentLevel = 1;
        }
        else currentLevel = PlayerPrefs.GetInt("levelAdventure");

        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelButton.GetComponent<RectTransform>().rect;

        int maxInARow = 5;
        int maxInACol = 2;

        amountPerPage = maxInARow * maxInACol;

        int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);

        LoadPanels(totalPages);
    }

    void LoadPanels(int numberOfPanels)
    {
        GameObject panelClone = Instantiate(levelHolder) as GameObject;

        PanelMover mover = levelHolder.AddComponent<PanelMover>();

        mover.totalPages = numberOfPanels;

        for(int i = 1; i<=numberOfPanels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;

            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Panel " + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);

            SetUpGrid(panel);

            int numberOfIcons = i == numberOfPanels ? numberOfLevels - currentLevelCount : amountPerPage;
            LoadIcons(numberOfIcons, panel);            
        }
        Destroy(panelClone);
    }

    void LoadIcons(int numberOfIcons, GameObject parentObject)
    {
        //Debug.Log(numberButtons);
        for (int i = numberButtons; i < numberButtons + numberOfIcons; i++)
        {
            currentLevelCount++;

            GameObject icon = Instantiate(levelButton) as GameObject;

            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level" + i;
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());

            if (i>currentLevel)
            {
                icon.GetComponent<Button>().enabled = false;
            }
            else icon.GetComponent<Button>().enabled = true;

        }
        numberButtons += numberOfIcons;
    }

    void SetUpGrid(GameObject panel)
    {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.spacing = new Vector2(150, 150);
        grid.startAxis = GridLayoutGroup.Axis.Vertical;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 2;
        grid.childAlignment = TextAnchor.MiddleCenter;
    }

    private void printNameButtons(int i) {
        Debug.Log(i);
    }
}
