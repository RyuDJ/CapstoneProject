using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal;
using System.Transactions;
using UnityEditor;

#region [스크립트 설명]

/*
 * 
 * 이 스크립트는 텍스트를 타자형식으로 적어나가는 것에 그치지 않고
 * 중간에 드러나는 수많은 자체제작 텍스트 커맨드를 해석하여
 * 독자적으로 스크립트를 형성해나가는 전략적 스크립트입니다.
 * 
 * 제작 : 2020년 04월 24일
 * 제작자 & 스크립트 검수자 : 유동재(서울과학기술대학교 컴퓨터공학과 14109352)
 * 
*/

#endregion

public class Texting : MonoBehaviour
{
    #region 변수 목록

    public static string nowtext = "";      // 현재 드러나야할 텍스트를 스크립트로부터 받습니다.
    public static bool textStart = false;   // 텍스트를 시작해야할 지 외부에서 결정받습니다.

    public TextMeshProUGUI ShowingText;                // 텍스트북에 등장할 텍스트입니다.

    string thistext = "";                   // 현재 드러나야할 텍스트를 다시 받습니다. 스크립트에서 일부 변경사항이 생길 수 있습니다.
    float watingTime = 0, time = 0;         // 문자 하나가 등장하고 다음 문자가 등장할 때까지의 시간(watingtime)을 정해두고 현재 시간(time)을 측정합니다.
    int nowtextnum = 0, phase = 0;          // 현재 드러나는 문자의 길이를 정하는 변수(nowtextnum)와 현재 단계(phase) 변수가 정해져있습니다.

    public Animator[] anim = new Animator[3];
    public Text[] possible = new Text[3];                  // 경우의 수에서 최대 3개까지의 경우가 가능합니다. 이들의 텍스트는 다음과 같습니다.
    public GameObject[] possible_box = new GameObject[3];
    public int[] Next = new int[3] { 0, 0, 0 };            // 다음 경우의 수로 이동하도록 한다.

    bool[] effect = new bool[]
    {
        /*글꼴을 진하게*/ false, /*글꼴을 기울이기*/ false, /*글꼴 색상 변경*/ false,
        /*중간에 멈추기*/ false,
        /*이미지 드러내기*/ false, /*선택지 드러내기*/ false
    };

    #region Answer 결과
    public void Answer1()
    {
        ShowingText.text = "";
        nowtextnum = 0;
        time = 0;
        phase = 0;

        Story.line = Next[0];
        anim[0].SetBool("Open", false);
    }

    public void Answer2()
    {
        ShowingText.text = "";
        nowtextnum = 0;
        time = 0;
        phase = 0;

        Story.line = Next[1];
        anim[0].SetBool("Open", false);
    }

    public void Answer3()
    {
        ShowingText.text = "";
        nowtextnum = 0;
        time = 0;
        phase = 0;

        Story.line = Next[2];
        anim[0].SetBool("Open", false);
    }
    #endregion

    #endregion

    #region Start()

    void Start()
    {
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        if (textStart)
        {
            if (phase == 0)
            {
                for(int i = 0; i < 3; i++)
                    possible_box[i].SetActive(false);

                thistext = Story.story[Story.chapter][Story.line];
                phase = 1;
            }

            else if (phase == 1)
            {
                if (!effect[3] && !ShowingText.text.Equals(thistext) && nowtextnum != thistext.Length)
                {
                    watingTime = 1*Time.deltaTime;
                    time += Time.deltaTime;
                    string BeforeTexting = "";

                    #region 글꼴 이펙트 시작 부분

                    //글꼴을 진하게 하고 싶었다면
                    if (thistext.Length - nowtextnum >= 3 && thistext.Substring(nowtextnum, 3).Equals("<b>"))
                    {
                        nowtextnum += 3;
                        effect[0] = true;
                    }

                    //글꼴을 기울이고 싶었다면
                    else if (thistext.Length - nowtextnum >= 3 && thistext.Substring(nowtextnum, 3).Equals("<i>"))
                    {
                        nowtextnum += 3;
                        effect[1] = true;
                    }

                    //색상과 관련된 문자
                    else if (thistext.Length - nowtextnum >= 8 && thistext.Substring(nowtextnum, 8).Equals("<color=#"))
                    {
                        nowtextnum += 17;
                        effect[2] = true;
                    }

                    //띄어쓰기가 등장했을 때
                    else if (thistext.Length - nowtextnum >= 2 && thistext.Substring(nowtextnum, 2).Equals("\n"))
                        nowtextnum += 2;

                    //대사와 관련된 문자가 등장했을 때 ("")
                    else if (thistext.Length - nowtextnum >= 2 && thistext.Substring(nowtextnum, 2).Equals("\""))
                        nowtextnum += 2;

                    //씬이 끝나기 전 다음 화면으로 옮겨진다.
                    else if (thistext.Length - nowtextnum >= 6 && thistext.Substring(nowtextnum, 6).Equals("<next>"))
                    {
                        thistext = thistext.Substring(0, nowtextnum) + thistext.Substring(nowtextnum + 6);
                        effect[3] = true;
                    }

                    //중간에 속도 변환하기
                    else if (thistext.Length - nowtextnum >= 7 && thistext.Substring(nowtextnum, 7).Equals("<speed="))
                    {
                        watingTime *= float.Parse(thistext.Substring(nowtextnum + 7, 4));
                        thistext = thistext.Substring(0, nowtextnum) + thistext.Substring(nowtextnum + 12);
                    }

                    //단순하게 다음으로 넘어가기
                    else if (thistext.Length - nowtextnum >= 4 && thistext.Substring(nowtextnum, 4).Equals("<go="))
                    {
                        Story.line = int.Parse(thistext.Substring(nowtextnum + 4, 3));
                        thistext = thistext.Substring(0, nowtextnum) + thistext.Substring(nowtextnum + 6);
                        phase = 2;
                    }

                    // 옵션 정해서 넘어가기
                    else if (thistext.Length - nowtextnum >= 7 && thistext.Substring(nowtextnum, 7).Equals("<option"))
                    {
                        int num = int.Parse(thistext.Substring(nowtextnum + 8, 1)); /*경우의 수*/
                        Debug.Log(num);
                        int Full_length = 0;

                        for (int i = 0; i < num; i++)
                        {
                            int length = int.Parse(thistext.Substring(nowtextnum + 10 + 11 * (num - 1) + 3, 3));

                            Full_length += length;

                            possible[i].text =
                                thistext.Substring(nowtextnum + 10 + 11 * num + Full_length - length + 2*(i+1), length);

                            possible_box[i].SetActive(true);

                            Next[i] = int.Parse(thistext.Substring(nowtextnum + 10 + 11 * i + 7, 3));
                            Debug.Log(thistext.Substring(nowtextnum + 10 + 11 * i + 7, 3));
                        }

                        anim[0].SetBool("Open", true);

                        phase = 3;
                    }

                    #endregion

                    BeforeTexting = thistext.Substring(0, nowtextnum);

                    #region 글꼴 이펙트 끝 부분

                    if (effect[0] && !thistext.Substring(nowtextnum, 4).Equals("</b>"))
                        BeforeTexting += "</b>";

                    else if (effect[0] && thistext.Substring(nowtextnum, 4).Equals("</b>"))
                    {
                        BeforeTexting += "</b>";
                        nowtextnum += 4;
                        effect[0] = false;
                    }

                    //글꼴을 기울이고 싶었다면
                    else if (effect[1] && !thistext.Substring(nowtextnum, 4).Equals("</i>"))
                        BeforeTexting += "</i>";

                    else if (effect[1] && thistext.Substring(nowtextnum, 4).Equals("</i>"))
                    {
                        BeforeTexting += "</i>";
                        nowtextnum += 4;
                        effect[1] = false;
                    }

                    //색상과 관련된 문자
                    else if (effect[2] && !thistext.Substring(nowtextnum, 8).Equals("</color>"))
                        BeforeTexting += "</color>";

                    else if (effect[2] && thistext.Substring(nowtextnum, 8).Equals("</color>"))
                    {
                        BeforeTexting += "</color>";
                        nowtextnum += 8;
                        effect[2] = false;
                    }

                    #endregion

                    ShowingText.text = BeforeTexting;

                    if (time > watingTime)
                    {
                        nowtextnum++;
                        time = 0;
                    }
                }

                else if(effect[3])
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        effect[3] = false;
                        thistext = thistext.Substring(nowtextnum);
                        nowtextnum = 0;
                    }
                }
            }

            else if (phase == 2)
            {
                if(Input.GetMouseButton(0))
                {
                    ShowingText.text = "";
                    nowtextnum = 0;
                    time = 0;
                    phase = 0;
                }
            }
        }
    }
}
