using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 설명

/*
 * 이 스크립트는 상태 게이지를 채우고 줄이는 인터페이스를 조작하는 스크립트입니다.
 * 
 * 체력 게이지 : 최대 >= 100 > 정상 >= 70 > 주의 >= 30 > 위험 > 0 = 사망
 * 자금 게이지 : 최대 >= 100 > 많음 >= 50 > 적음 > 0 = 빈털털이
 * 도덕 게이지 : 최대 >= 100 > 착함 >= 50 > 악함 > 0 = 살인마
 * 
 */

#endregion

public class Gauge : MonoBehaviour
{
    #region 세팅

    //스텟 이미지
    public Sprite[] status_image = new Sprite[17];
    public Image[] status = new Image[6];

    //스텟과 관련된 수치함수
    public static float[] gauge = new float[3] { 100f, 100f, 100f };

    bool male = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region 체력

        if (gauge[0] >= 100)
            gauge[0] = 100;

        else if (gauge[0] > 70)
            status[3].sprite = status_image[3];

        else if (gauge[0] > 30)
            status[3].sprite = status_image[2];

        else if (gauge[0] > 0)
            status[3].sprite = status_image[1];

        else
        {
            status[3].sprite = status_image[4];
            gauge[0] = 0;
        }

        #endregion

        #region 자금

        if (gauge[1] >= 100)
            gauge[1] = 100;

        else if (gauge[1] >= 50)
            status[4].sprite = status_image[7];

        else if (gauge[1] > 0)
            status[4].sprite = status_image[6];

        else
        {
            status[4].sprite = status_image[8];
            gauge[1] = 0;
        }

        #endregion

        #region 인간성

        status[2].sprite = (male ? status_image[9] : status_image[13]);

        if (gauge[2] >= 100)
            gauge[2] = 100;

        else if (gauge[2] >= 50)
            status[5].sprite = (male ? status_image[11] : status_image[15]);

        else if (gauge[2] > 0)
            status[5].sprite = (male ? status_image[10] : status_image[14]);

        else
        {
            status[5].sprite = (male ? status_image[12] : status_image[16]);
            gauge[2] = 0;
        }

        #endregion

        for (int i = 0; i < 3; i++)
        {
            gauge[i] -= 0.05f;
            status[i + 3].fillAmount = (gauge[i] > 0 ? gauge[i] / 100f : 100f);
        }
    }
}
