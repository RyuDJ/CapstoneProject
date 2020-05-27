using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region [스크립트 설명]

/*
 * 
 * 이 스크립트는 프롤로그에 대한 애니메이션을 전반적으로 운영하고
 * 대사를 작성하여 게임진행에 원활하도록 만드는 전체 스크립트입니다.
 * 
 * 제작 : 2020년 04월 24일
 * 제작자 & 스크립트 검수자 : 유동재(서울과학기술대학교 컴퓨터공학과 14109352)
 * 대본 제작자 & 오류발견자 : 김송현(서울과학기술대학교 컴퓨터공학과 19110112)
 * 
*/

#endregion

public class Prologue : MonoBehaviour
{
    #region 변수 목록

    // Animation과 관련된 변수들
    public Animator animator;   // Prologue Aniamtion System이라는 게임 오브젝트에게 부여한 Animator를 관리할 변수.
    int phase = 0;              // Animation의 현 Phase 변수를 저장할 스크립트 변수. 간편화를 위해서 추가해두었다.
    

    #endregion

    #region Start()

    void Start()
    {
    }

    #endregion

    #region Update()

    void Update()
    {
        // 애니메이터는 phase가 변경될 때마다 자동으로 이와 같이 변경된다.
        animator.SetInteger("Phase", phase);

        // 만약에 phase가 0이라면, 시작 바로 초기이므로 1로 변경한다.
        if (phase == 0)
            phase = 1;

        // 만약에 phase가 1이라면, 주의사항과 시간대가 등장하고 바로 #2로 전환된다.
        else if (phase == 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("1#"))
            phase = 2;

        else if (phase == 2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("1#"))
        {
            Texting.nowtext = Story.story[Story.chapter][Story.line];
            Texting.textStart = true;
        }
    }

    #endregion
}