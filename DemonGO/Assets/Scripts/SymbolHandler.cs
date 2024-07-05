using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SymbolHandler : MonoBehaviour
{
    private Vector2 ScreenSize;
    public List<Vector2> symbol;
    public GameObject EnemyHandler;
    public GameObject Enemy;
    private List<Image> symbolImage = new List<Image>();
    public Sprite circle;
    public float timing = 0.5f;
    private float delta = 0;
    private int index = 0;
    private List<bool> isTouched = new List<bool>();
    private bool isCorrect = false;
    private int indexTouched = 0;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        ScreenSize = new Vector2(Screen.width, Screen.height);
        Enemy = EnemyHandler.transform.GetChild(0).gameObject;
        text.text = Enemy.GetComponent<EnemyHandler>().name;
        symbol = Enemy.GetComponent<EnemyHandler>().symbol;
        for(int i = 0; i < symbol.Count; i++)
        {
            GameObject temp = new GameObject();
            temp.transform.SetParent(transform);
            Image tempImage = temp.AddComponent<Image>();
            tempImage.sprite = circle;
            tempImage.rectTransform.sizeDelta = new Vector2(70, 70);
            tempImage.rectTransform.anchoredPosition = new Vector2(ScreenSize.x * symbol[i].x, ScreenSize.y * symbol[i].y);
            symbolImage.Add(tempImage);
            temp.SetActive(false);
        }
        for (int i = 0; i < symbol.Count; i++)
        {
            isTouched.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if(delta >= timing)
        {
            delta = 0;
            if(index < symbol.Count)
            {
                symbolImage[index].gameObject.SetActive(true);
                index++;
            }
            else
            {
                for(int i = 0; i < symbolImage.Count; i++)
                {
                    symbolImage[i].gameObject.SetActive(false);
                }
                index = 0;
            }
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if(Vector2.Distance(touch.position, symbolImage[indexTouched].rectTransform.position) < 70)
                {
                    //Debug.Log("Correct");
                    isTouched[indexTouched] = true;
                    indexTouched++;
                    if (indexTouched >= symbol.Count)
                    {
                        indexTouched = 0;
                    }
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                for (int i = 0; i < isTouched.Count; i++)
                {
                    if (isTouched[i] != true)
                    {
                        //Debug.Log("Wrong");
                        for (int j = 0; j < isTouched.Count; j++)
                        {
                            isTouched[j] = false;
                        }
                        isCorrect = false;
                        indexTouched = 0;
                        break;
                    } else
                    {
                        isCorrect = true;
                    }
                }
                if (isCorrect)
                {
                    Debug.Log("Complete Schema");
                    for (int j = 0; j < isTouched.Count; j++)
                    {
                        isTouched[j] = false;
                    }
                    indexTouched = 0;
                    Configurator.addMonster(Configurator.monsterNB);
                    SceneManager.LoadScene("SamMap");
                }
            }
        }
    }
}
