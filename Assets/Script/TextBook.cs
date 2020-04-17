using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBook : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image, AnswerImage;
    public Animator[] anim;
    public ScrollRect sr;

    int i = 0;

    #region 이미지 보이기

    bool showImage = false, showAnswer = false, showText = false;

    void ShowImage()
    {
        anim[0].SetBool("Card", showImage);
        anim[1].SetBool("Card", showImage);

        if (showImage)
        {
            image.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);

            if (anim[0].GetCurrentAnimatorStateInfo(0).IsName("cardStay"))
                showText = true;
        }

        else
        {
            image.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            showText = false;
        }
    }

    void ShowAnswer()
    {
        if (showAnswer)
            AnswerImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);

        else
            AnswerImage.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float d = 0;
        string t = "이것은 텍스트로, 앞으로 계속해서 타자형식으로 계속해서 등장하게 될 것입니다. 정말 놀랍죠? 저도 정말 놀랍다고 생각되는데요. 이거 테스트용으로 쓰는 거니까 그냥 아무말로 쓴다고 하면" +
            "이거 구현하는데 오늘 하루종일 걸린 것 같은 기분이 들어요. 분명 아까까지는 3시였는데 지금 8시인 걸 보면 제 시간감각 정말 망했네요. 지금 뭐 듣고 있냐면요 원어스의 쉽게 쓰여진 노래를 듣고 있는데요.." +
            "이거 정말 대박입니다. 노래 처음에 들었을 때는 무난해서 '음 그저 그렇네'라고 생각했는데 아니 이럴수가 머리에서 떠나지를 않아요. 날 내버려두란 말이야. 내 뇌에서 나가 이것들아! 라고 외쳤는데 안 통해요 정말 1도 안 통합니다. " +
            "그리고 이건 비밀은 아닌데요, 여환웅이가 그렇게 이쁘지 않을 수가 없어요. 내내 무대에서 걔만 쳐다봤네요. 미친 거 아냐. 별명이 요정이라고 하더라고요. 정말 요정이네요. 우리 마음 속에 사람 한 명 요정으로 받아들이잖아요. 남자는 아니라고요? 조용히 하세요." +
            "전 환웅이로 정했습니다. 개인취향 받아들이시죠. 이거 아무말 재밌네요. 지금 딱 보는데 원래 칸을 넘어갔잖아요. 정말 해피하네요.";

        text.text = "<color=#FFFFFFFF>" + t.Substring(0, i) + "</color><color=#00000000>" + t.Substring(i) + "</color>";

        showAnswer = true; showImage = true;

        if (showText)
        {
            if (!text.text.Equals("<color=#FFFFFFFF>" + t + "</color><color=#00000000></color>"))
            {
                i++;

                while (d < 500f)
                    d += Time.deltaTime / 60f;

                Debug.Log(text.maxVisibleLines);
            }

            else if(Input.GetMouseButtonDown(0))
                anim[2].SetBool("Answer", AnswerImage);
        }

        ShowImage();
        ShowAnswer();
        
    }
}
