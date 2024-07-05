using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject player;
    public int area;
    public int enemyCount;
    private List<GameObject> enemyList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            GameObject temp = Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(Random.Range(-area - 5, area + 5), 0, Random.Range(-area - 5, area + 5)), Quaternion.identity);
            temp.transform.localScale *= 10;
            enemyList.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count < enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject temp = Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(Random.Range(-area, area), 0, Random.Range(-area, area)), Quaternion.identity);
                enemyList.Add(temp);
            }
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchPos = Input.GetTouch(0).position;
                Vector3 worldPos = ScreenPosToWorldPos(touchPos);
                foreach (GameObject enemy in enemyList)
                {
                    if (Vector3.Distance(enemy.transform.position, worldPos) < 10)
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if (enemy.name == enemies[i].name + "(Clone)")
                            {
                                Configurator.monsterNB = i;
                                SceneManager.LoadScene("SamCapture");
                                break;
                            }
                        }
                    }
                }
            }
        }
    }


    Vector3 ScreenPosToWorldPos(Vector3 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
