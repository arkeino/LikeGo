using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class CheckConn : MonoBehaviour
{
    int nb_points = 0;
    private IEnumerator Start() {
        while (Input.location.status != LocationServiceStatus.Running) {
            Debug.Log(Input.location.status);
            transform.GetComponent<TextMeshProUGUI>().text = "Connection" + new string('.', nb_points);
            nb_points = (nb_points + 1) % 4;
            yield return new WaitForSeconds(1);
        }
        transform.GetComponent<TextMeshProUGUI>().text = "Connected!";
        SceneManager.LoadScene("SamMap");
        yield return null;
    }
}
