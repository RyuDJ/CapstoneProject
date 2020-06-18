using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chain2 : MonoBehaviour
{
    public Image image, food;
    public Text have;
    public float speed;
    public Animator[] anim; /* 0은 뒷배경, 1은 이미지로고 2는 노이즈*/

    public static bool
        changeChain = false /*체인을 돌릴지의 여부*/,
        BeforeSelectOn = false /*선택하기 직전에 마우스가 올려져 있다면*/,
        ShowNoise = false /*노이즈를 보입니다*/;

    public void SelectButton()
    {
        if (Story.chapter == 1 && anim[2].GetCurrentAnimatorStateInfo(0).IsName("BtnB_ShowStop"))
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
        if (anim[2].GetCurrentAnimatorStateInfo(0).IsName("BtnB_Select"))
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

        have.text = "";

        for (int i = 0; i < Story.have.Length; i++)
        {
            if (Story.have[i])
            {
                switch (i)
                {
                    case 0:
                        have.text += (have.text.Length > 0 ? "\n" : "") + "칼";
                        break;
                    case 1:
                        have.text += (have.text.Length > 0 ? "\n" : "") + "방망이";
                        break;
                    case 2:
                        have.text += (have.text.Length > 0 ? "\n" : "") + "열쇠";
                        break;
                    default:
                        break;
                }
            }
        }

        food.fillAmount = Story.food / 100f;
    }
}
