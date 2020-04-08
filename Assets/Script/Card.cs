using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 설명

/*
 * 이 스크립트는 카드를 부르고 돌리는 등의 기능을 하는 애니메이션 전용 스크립트입니다.
 * 
 * 초반에 카드를 부른다 → 뒷면인 카드가 뒤집힌다 → 이후에 대사창이 뜬다.
 * 다음 카드를 부른다 → 다음 카드의 뒷면이 뒤집힌다 → 이후는 초반과 동일하다.
 * 해당 장소의 모든 카드가 끝마치면 카드는 현재 카드가 사라지고 다음 카드는 등장하지 않는다.
 * 
 */

#endregion

public class Card : MonoBehaviour
{
    #region 설정

    int i = 0;
    public GameObject obj;
    public Animator animate = new Animator();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (animate.GetCurrentAnimatorStateInfo(0).IsName("New State"))
                animate.SetInteger("Card", 0);

            else if (animate.GetCurrentAnimatorStateInfo(0).IsName("Card_Stop") && i < 5)
                animate.SetInteger("Card", 1);

            else if (animate.GetCurrentAnimatorStateInfo(0).IsName("Card_Stop") && i >= 5)
                animate.SetInteger("Card", 2);
        }

        if (animate.GetCurrentAnimatorStateInfo(0).IsName("Card_Next") || animate.GetCurrentAnimatorStateInfo(0).IsName("Card_Finish"))
            animate.SetInteger("Card", 0);

        else if (animate.GetCurrentAnimatorStateInfo(0).IsName("Card_FlipStop"))
            i++;
    }
}
