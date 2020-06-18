using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chain3 : MonoBehaviour
{
    public Image image;
    public float speed;
    public Animator[] anim; /* 0은 뒷배경, 1은 이미지로고 2는 노이즈*/

    public static bool
        changeChain = false /*체인을 돌릴지의 여부*/,
        BeforeSelectOn = false /*선택하기 직전에 마우스가 올려져 있다면*/,
        ShowNoise = false /*노이즈를 보입니다*/,
        changeSpeed = false;

    float time = 0;

    public void SelectButton()
    {
        if(Story.chapter == 1)
        {
            anim[0].SetBool("Select", true);
            changeSpeed = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSpeed && anim[0].GetBool("Select"))
        {
            if (time < 10 * Time.deltaTime)
            {
                time += Time.deltaTime;
                image.transform.Rotate(Vector3.forward, 1600 * Time.deltaTime);
            }

            else
            {
                time = 0;
                changeSpeed = false;
                anim[0].SetBool("Select", false);
                FullGame.Setting = true;
            }
        }

        else if (changeSpeed && !FullGame.Setting)
        {
            if (time < 10 * Time.deltaTime)
            {
                time += Time.deltaTime;
                image.transform.Rotate(Vector3.forward, 1600 * Time.deltaTime);
            }

            else
            {
                time = 0;
                changeSpeed = false;
            }
        }

        else
        {
            if (changeChain && !FullGame.Setting)
                image.transform.Rotate(Vector3.forward, speed * Time.deltaTime);

            else
                image.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (BeforeSelectOn)
            anim[0].SetBool("Show", true);

        else
            anim[0].SetBool("Show", false);

        if (ShowNoise)
            anim[1].SetBool("Show", true);

        else
            anim[1].SetBool("Show", false);
    }
}
