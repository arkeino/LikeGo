using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    public int width;
    public int height;
    // Start is called before the first frame update
    void Start()
    {
        height = Screen.height;
        width = Screen.width;
        transform.position = new Vector3(width / 2, height / 2, 0);
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
}
