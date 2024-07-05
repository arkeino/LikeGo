using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPanel : MonoBehaviour
{
    public void checkPan() {
        //enable the panel if not enabled else disable it
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
