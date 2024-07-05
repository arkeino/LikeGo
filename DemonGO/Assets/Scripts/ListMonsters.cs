using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListMonsters : MonoBehaviour
{
    public static List<(int, int)> monsters = new List<(int, int)>();
    public List<GameObject> enemies;
    private List<GameObject> monsterList = new List<GameObject>();
    public GameObject buttonPrefab;
    public RectTransform content;
    private int buttonSize = 100;
    // Start is called before the first frame update
    void Start()
    {
        GenerateButtons(enemies.Count);
        adjustContent(enemies.Count);
    }

    // Update is called once per frame
    void Update()
    {
        int offset = 0;
        for (int i = 0; i < monsterList.Count; i++)
        {
            bool pan = monsterList[i].transform.GetChild(1).gameObject.activeSelf;
            //Debug.Log(pan);
            float PosY = -i * buttonSize - buttonSize / 2 - offset;
            float PosX = monsterList[i].GetComponent<RectTransform>().localPosition.x;
            monsterList[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX, PosY, 0f);
            offset += pan ? 500 : 0;
        }

        for (int i = 0; i < monsters.Count; i++)
        {
            //enemies[monsters[i].Item1].GetComponent<EnemyHandler>().asBeenCaptured = true;
            monsterList[monsters[i].Item1].transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemies[monsters[i].Item1].GetComponent<EnemyHandler>().description + "\nCaptured " + monsters[i].Item2 + " times";
        }
    }

    public static void addMonster(int monsterNB)
    {
        bool found = false;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].Item1 == monsterNB)
            {
                monsters[i] = (monsterNB, monsters[i].Item2 + 1);
                found = true;
                return;
            }
        }
        if (!found)
        {
            monsters.Add((monsterNB, 1));
        }
    }

    void GenerateButtons(int numberOfButtons)
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab) as GameObject;
            newButton.transform.SetParent(content, false);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = enemies[i].name;
            newButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 50;
            int monsterNB = i;
            //change size of button to match the size of the content
            float sizeX = transform.GetComponent<RectTransform>().rect.width;
            
            newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, buttonSize);

            
            float PosX = 0;
            float PosY = -i * buttonSize - buttonSize / 2;
            newButton.GetComponent<RectTransform>().localPosition = new Vector3(PosX, PosY, 0f);
            newButton.transform.GetChild(1).GetComponent<RectTransform>().localPosition = new Vector3(PosX, buttonSize - 500 + buttonSize, 0f);
            newButton.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, 500);
            GameObject child = newButton.transform.GetChild(1).gameObject;
            GameObject mini = Instantiate(enemies[i].GetComponent<EnemyHandler>().Miniature, newButton.transform.GetChild(1));
            //pos in the left up angle of the child 1
            float sizeX_C = newButton.transform.GetChild(1).GetComponent<RectTransform>().rect.width;
            float sizeY_C = newButton.transform.GetChild(1).GetComponent<RectTransform>().rect.height;
            child.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX_C / 2, sizeX_C / 2);
            child.transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(-sizeX / 2 + buttonSize * 1.25f , 10, 0);
            child.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX_C, sizeY_C);
            child.transform.GetChild(1).GetComponent<RectTransform>().localPosition = new Vector3(sizeX_C / 8, 0, 0);
            string description = enemies[i].GetComponent<EnemyHandler>().description;
            string asBeenCaptured = enemies[i].GetComponent<EnemyHandler>().asBeenCaptured ? "Captured" : "Not Captured";
            child.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = description + "\n" + asBeenCaptured;
            child.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 100;
            child.transform.GetChild(1).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX_C * 2, sizeY_C * 2);
    
            mini.transform.localPosition = new Vector3(-sizeX / 2 + buttonSize * 1.25f , -sizeY_C/16, 0);
            mini.transform.localScale = new Vector3(sizeX_C / 10, sizeX_C / 10, sizeX_C / 10);
            mini.transform.localRotation = Quaternion.Euler(0, 180, 0);
            
            monsterList.Add(newButton);
        }
    }

    void adjustContent(int monsterNB)
    {
        float sizeY = monsterNB * buttonSize;
        content.sizeDelta = new Vector2(content.sizeDelta.x, sizeY);
    }
}
