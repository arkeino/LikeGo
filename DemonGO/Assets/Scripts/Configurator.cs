using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurator : MonoBehaviour
{
    public static int monsterNB = 1;

    public static void addMonster(int monsterNB)
    {
        ListMonsters.addMonster(monsterNB);
    }
}
