using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chain3 : MonoBehaviour
{
    public Image image;
    public float speed;
    public static bool changeSpeed = false;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSpeed)
        {
            if (time < 200*Time.deltaTime)
            {
                time += Time.deltaTime;
                transform.Rotate(Vector3.forward, 800 * Time.deltaTime);
            }

            else
            {
                time = 0;
                changeSpeed = false;
            }
        }

        else
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
