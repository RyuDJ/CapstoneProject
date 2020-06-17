using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chain1 : MonoBehaviour
{
    public Image image, health, mental;
    public Text healthtext, mentaltext;
    public float speed;
    public Animator[] anim; /* 0은 뒷배경, 1은 이미지로고 2는 노이즈*/

    public static bool
        changeChain = false /*체인을 돌릴지의 여부*/,
        BeforeSelectOn = false /*선택하기 직전에 마우스가 올려져 있다면*/,
        ShowNoise = false /*노이즈를 보입니다*/;

    public void SelectButton()
    {
        if (Story.chapter == 1)
        {
            anim[0].SetBool("Select", true);
            anim[2].SetBool("Select", true);
            speed = 1600;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (anim[2].GetCurrentAnimatorStateInfo(0).IsName("BtnA_Select"))
        {
            speed = 40;
            anim[2].SetBool("Select", false);
            changeChain = false;
        }

        else if (anim[2].GetCurrentAnimatorStateInfo(0).IsName("SelectStop"))
        {
            anim[0].SetBool("Select", false);
            changeChain = true;
        }

        if (changeChain && !FullGame.Setting)
            image.transform.Rotate(Vector3.forward, speed * Time.deltaTime);

        else
            image.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (BeforeSelectOn)
            anim[0].SetBool("Show", true);

        else
            anim[0].SetBool("Show", false);

        if (ShowNoise)
            anim[1].SetBool("Show", true);

        else
            anim[1].SetBool("Show", false);

        healthtext.text = (Story.health == 0 ? "정상" : (Story.health == 1 ? "물림" : (Story.health == 2 ? "머리 부상" : (Story.health == 3 ? "상체 부상" : (Story.health == 4 ? "다리 부상" : "확인 안 됨"))))
               + (Story.hungry > 30 ? ", 배고픔" : ""));
        mentaltext.text = (Story.mental == 0 ? "정상" : (Story.mental == 1 ? "우울함" : (Story.mental == 2 ? "정신분열" : (Story.mental == 3 ? "패닉" : "확인 안 됨"))));
        health.fillAmount = Story.life/100f;
        mental.fillAmount = (100-Story.psycho)/100f;
    }
}
