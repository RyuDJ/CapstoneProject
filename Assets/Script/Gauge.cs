using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 설명

/*
 * 이 스크립트는 상태 게이지를 채우고 줄이는 인터페이스를 조작하는 스크립트입니다.
 *
 */

#endregion
public class Gauge : MonoBehaviour
{
    #region 세팅

    //스텟 이미지
    public Sprite[] status_image = new Sprite[12];
    public Image[] status = new Image[6];

    //스텟과 관련된 수치함수
    public static float[] gauge = new float[3] { 0.01f, 0.01f, 0.01f };

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < gauge.Length; i++)
        {
            if (gauge[i] >= 100)
                gauge[i] = 100;

            else if (gauge[i] <= 0)
                status[i + 3].sprite = status_image[i * 4 + 3];

            else if (gauge[i] >= 50)
                status[i + 3].sprite = status_image[i * 4 + 1];

            else if(gauge[i] < 50)
                status[i + 3].sprite = status_image[i * 4 + 2];

            if (gauge[i] <= 0)
                status[i + 3].fillAmount = 1;

            else
                status[i + 3].fillAmount = gauge[i]/100f;
        }
    }
}
