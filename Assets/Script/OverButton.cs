using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 스크립트 설명 
 * 
 * 플레이어에게 주어진 버튼은 실제로 선택되는 버튼이 아닙니다.
 * 하지만 버튼처럼 Hightlight되는 모습을 보이기 위해서 위 스크립트를 통해 자체적으로 제작하기로 했습니다.
 * 
 */


public class OverButton : MonoBehaviour
{
    public Image btnImage;
    public Text text;
    public Animator anim;

    public void Over()
    {
        btnImage.color = new Color(127, 127, 0);
        text.color = new Color(0, 0, 255);
    }

    public void Select()
    {
        btnImage.color = new Color(255, 255, 0);
        text.color = new Color(0, 0, 255);
        text.text = "<b>" + text.text + "</b>";
    }

    public void Down()
    {
        btnImage.color = new Color(255, 255, 255);
        text.color = new Color(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Story"))
        {
            btnImage.color = new Color(255, 255, 255);
            text.color = new Color(0, 0, 0);
        }
    }
}
