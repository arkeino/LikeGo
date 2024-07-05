using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> _monsters;
    private GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        if (monster != null)
        {
            Destroy(monster);
        }
        monster = Instantiate(_monsters[Configurator.monsterNB], transform.position, Quaternion.identity);
        monster.transform.parent = transform;
        monster.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
