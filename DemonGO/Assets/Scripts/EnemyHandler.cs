using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    public GameObject body;
    public Rigidbody rb;
    public List<Vector2> symbol;
    public string name;
    public GameObject Miniature;
    public bool asBeenCaptured = false;
    public string description;

    private float randomSeconds;
    private float delay = 0;
    // Start is called before the first frame update
    void Start()
    {
        randomSeconds = Random.Range(1, 5);
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delay >= randomSeconds)
        {
            jump();
            delay = 0;
            randomSeconds = Random.Range(1, 5);
        }
        else
        {
            delay += Time.deltaTime;
        }
    }

    void jump()
    {
        rb.AddForce(Vector3.up * 200);
    }
}
