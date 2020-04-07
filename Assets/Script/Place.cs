using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place : MonoBehaviour
{
    #region 설정

    int i = 0;
    public GameObject obj;
    public Text[] text = new Text[2];
    public Animator animate = new Animator();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (animate.GetCurrentAnimatorStateInfo(0).IsName("New State") || animate.GetCurrentAnimatorStateInfo(0).IsName("PlaceName_Stop")))
        { animate.SetBool("TurnOn", true); i++; }

        if (animate.GetCurrentAnimatorStateInfo(0).IsName("PlaceName_First") || animate.GetCurrentAnimatorStateInfo(0).IsName("PlaceName_Next"))
        {
            animate.SetBool("TurnOn", false);
            text[1].text = "현재 i = " + i;

            if (obj.transform.position.x < -200)
                text[0].text = "현재 i = " + i;
        }
    }
}
